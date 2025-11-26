using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
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
                        switch (Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP"))
                        {
                            case "KDE":
                                if (inverted)
                                    rotation = "inverted";
                                else
                                    rotation = "normal";
                                FlipKDEMonitor(rotation);
                                break;
                            default:
                                if (inverted)
                                    rotation = "180";
                                else
                                    rotation = "normal";
                                FlipWaylandMonitor(rotation);
                                break;
                        }
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
        
        static void FlipKDEMonitor(string rotation)
        {
            Console.WriteLine($"Running KDE Wayland");

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "kscreen-doctor",
                Arguments = "-o",
                RedirectStandardOutput = true
            };

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader sr = process.StandardOutput)
                {
                    string output = sr.ReadToEnd();
                    string[] lines = output.Split('\n');

                    //Console.WriteLine(output);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("Output:") && lines[i + 3].Contains("priority 1"))
                        {
                            string outputID = lines[i].Split(' ')[2];
                            //Console.WriteLine(outputID);

                            ProcessStartInfo flipStartInfo = new ProcessStartInfo()
                            {
                                FileName = "kscreen-doctor",
                                Arguments = $"kscreen-doctor output.{outputID}.rotation.{rotation}",
                                UseShellExecute = true
                            };

                            Process.Start(flipStartInfo);
                            break;
                        }
                    }
                }
            }
        }

        static void FlipWaylandMonitor(string rotation)
        {
            Console.WriteLine("running wayland default");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "wlr-randr",
                RedirectStandardOutput = true
            };

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader sr = process.StandardOutput)
                {
                    string output = sr.ReadToEnd();
                    string[] lines = output.Split('\n');
                    List<string> outputIDs = new List<string>();
                    //Console.WriteLine(output);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("Make:"))
                        {
                            string outputID = lines[i - 1].Split(' ')[0];
                            outputIDs.Add(outputID);
                        }
                    }
                    foreach (var id in outputIDs)
                    {
                        //Console.WriteLine(id);
                        ProcessStartInfo flipStartInfo = new ProcessStartInfo()
                        {
                            FileName = "wlr-randr",
                            Arguments = $"--output {id} --transform {rotation}",
                            UseShellExecute = true
                        };
                        
                        // This needs delay because wlr-randr sometimes fails without it.
                        Process.Start(flipStartInfo);
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
