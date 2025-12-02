using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Controls;

namespace antiMindblock
{
    public partial class Lazer
    {
        public static void ReloadSkin()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string lazerExecutablePath = "";

                foreach (var process in Process.GetProcesses())
                {
                    string cmdLine = System.IO.File.ReadAllText($"/proc/{process.Id}/cmdline").Replace('\0', ' ').Trim();

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

                        // lazer doesn't start

                        //ProcessStartInfo lazerStartInfo = new ProcessStartInfo()
                        //{
                        //    FileName = lazerExecutablePath,
                        //    UseShellExecute = true
                        //};
                        //Process.Start(lazerStartInfo);
                    }
                }
            }
        }
    }
}