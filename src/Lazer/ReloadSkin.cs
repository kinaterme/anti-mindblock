using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace antiMindblock
{
    public partial class Lazer
    {
        public static void ReloadSkin()
        {
            switch (Settings.OsuLazerReloadMode)
            {
                case "Restart":
                    ReloadSkinSafely();
                    Console.WriteLine("Reloading lazer skin (restart game)");
                    break;
                case "RestartDesktop":
                    ReloadSkinUsingDesktopFile();
                    Console.WriteLine("Reloading lazer skin (restart game .desktop)");
                    break;
                case "ImageRecognition":
                    ReloadSkinImageRecognition();
                    Console.WriteLine("Reloading lazer skin (image recognition)");
                    break;
            }
        }

        static void ReloadSkinSafely()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string lazerExecutablePath = "";
                foreach (var process in Process.GetProcesses())
                {
                    if (process.ProcessName.Contains("osu"))
                    {
                        lazerExecutablePath = process.MainModule?.FileName ?? "";
                        Console.WriteLine($"Killing {process.ProcessName}");
                        process.Kill();
                        break;
                    }
                }

                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                  FileName = lazerExecutablePath,
                  UseShellExecute = false,
                };
                Console.WriteLine($"Launching {lazerExecutablePath}");
                Process.Start(startInfo);

            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string lazerExecutablePath = "";

                foreach (var process in Process.GetProcesses())
                {
                    string cmdLine = "";

                    try
                    {
                        cmdLine = System.IO.File.ReadAllText($"/proc/{process.Id}/cmdline").Replace('\0', ' ').Trim();    
                    }
                    catch
                    {
                        continue;
                    }
                    

                    if (cmdLine.ToLower().Contains("appimagelauncher"))
                        continue;

                    if (cmdLine.ToLower().Contains("osu"))
                    {
                        if (cmdLine.ToLower().EndsWith(".appimage"))
                        {
                            Console.WriteLine(cmdLine);
                            lazerExecutablePath = cmdLine;
                            Process.Start("killall", "osu!");
                        }

                        ProcessStartInfo lazerStartInfo = new ProcessStartInfo()
                        {
                            FileName = "bash",
                            Arguments = $"-c {lazerExecutablePath}",
                            RedirectStandardError = true,
                            UseShellExecute = false
                        };
                        Process.Start(lazerStartInfo);
                    }
                }
            }
        }

        static void ReloadSkinUsingDesktopFile()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                
            }
            else
                Console.WriteLine("not running linux");
        }

        static void ReloadSkinImageRecognition()
        {
            
        }
    }
}