using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace antiMindblock
{
    public partial class Input
    {
        public static void FlipTablet()
        {            
            string path = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OpenTabletDriver", "settings.json");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "OpenTabletDriver", "settings.json");    
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library", "Application Support", "OpenTabletDriver", "settings.json");

            EditOTDJson(path);
            ReloadOTD();
        }

        static void EditOTDJson(string path)
        {
            var json = JsonNode.Parse(System.IO.File.ReadAllText(path));

            var tablet = json?["Profiles"]?[0]?["AbsoluteModeSettings"]?["Tablet"];

            if (tablet != null)
            {
                double rotation = tablet["Rotation"]?.GetValue<double>() ?? 0.0;
                switch (rotation)
                {
                    case 0.0:
                        tablet["Rotation"] = 180.0;
                        break;
                    case 180.0:
                        tablet["Rotation"] = 0.0;
                        break;
                }

                System.IO.File.WriteAllText(path, json?.ToJsonString(new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
            }
        }

        static void ReloadOTD()
        {   
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                List<string> otdExecutables = new List<string>();
                foreach (var process in Process.GetProcesses())
                {
                    if (process.ProcessName.ToLower().Contains("opentabletdriver"))
                    {
                        otdExecutables.Add(process.MainModule?.FileName ?? "");
                        process.Kill();
                    }   
                }
                foreach (var executable in otdExecutables)
                {
                    if (executable.ToLower().Contains("daemon"))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo()
                        {
                            FileName = executable,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        Process.Start(startInfo);
                    }
                }
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "systemctl",
                    Arguments = "--user restart opentabletdriver",
                    UseShellExecute = true
                };
                Process.Start(startInfo);                
            }
        }
    }
}