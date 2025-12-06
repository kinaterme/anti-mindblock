using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace antiMindblock
{
    public partial class Input
    {
        public static void FlipTablet()
        {            
            string path;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "OpenTabletDriver", "settings.json");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "OpenTabletDriver", "settings.json");    
            else
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