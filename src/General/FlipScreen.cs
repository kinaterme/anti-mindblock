using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia.Controls.Shapes;

namespace antiMindblock
{
    public class Screen
    {
        public static void Flip(bool inverted)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                switch (Environment.GetEnvironmentVariable("XDG_SESSION_TYPE"))
                {
                    case "x11":
                        string rotation;

                        if (inverted)
                            rotation = "inverted";
                        else
                            rotation = "normal";

                        FlipXrandrMonitor(rotation);
                        break;
                    case "wayland":
                        break;
                }
            }
        }

        static void FlipXrandrMonitor(string rotation)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "xrandr",
                RedirectStandardOutput = true,                    
            };

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader sr = process.StandardOutput)
                {
                    string output = sr.ReadToEnd();
                    string[] lines = output.Split('\n');

                    foreach (string line in lines)
                    {
                        if (line.Contains("connected primary") && !line.Contains("disconnected"))
                        {
                            string outputID = line.Split(' ')[0];

                            ProcessStartInfo flipStartInfo = new ProcessStartInfo()
                            {
                                FileName = "xrandr",
                                Arguments = $"--output {outputID} --rotate {rotation}",
                                UseShellExecute = true
                            };
                            Process.Start(flipStartInfo);
                        }
                    }
                }
            }
        }
    }
}