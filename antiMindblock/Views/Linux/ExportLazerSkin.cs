using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace antiMindblock.LinuxOS
{
    public class linuxExportLazerSkin
    {
        public void ExportLazerSkin()
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string lazerDefaultExportDirectory = Path.Combine(homeDirectory, ".local/share/osu/exports");
            string lazerExportDirectoryCleanup = Path.Combine(homeDirectory, ".local/share/osu/backupmindblock");
            string quotedExportDirectoryCleanup = $"\"{lazerExportDirectoryCleanup}\"";
            string everyFileInDirectory = Path.Combine(lazerExportDirectoryCleanup, "*");
            string everyReplayInDefaultDirectory = Path.Combine(lazerDefaultExportDirectory, "*.osr");
            string everyFileInDefaultDirectory = Path.Combine(lazerDefaultExportDirectory, "*");
            if (Directory.Exists($"{lazerExportDirectoryCleanup}"))
            {
                Console.WriteLine("Export directory exists.");
            }
            else 
            {
                linuxRunShellCommand.RunShellCommand($"mkdir {quotedExportDirectoryCleanup}");
            }
            linuxRunShellCommand.RunShellCommand($"mv {everyReplayInDefaultDirectory} {lazerExportDirectoryCleanup}");
            linuxRunShellCommand.RunShellCommand($"cd {lazerDefaultExportDirectory} && rm -rf {everyFileInDefaultDirectory}");
            Thread.Sleep(500);
            linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
            Task.Delay(2000).GetAwaiter().GetResult();
            File.Delete("/tmp/screenshot.png");

            linuxRunShellCommand.RunShellCommand("xdotool keydown ctrl key 32 keyup ctrl");

            Task.Delay(1500).GetAwaiter().GetResult();

            linuxRunShellCommand.RunShellCommand("xdotool type 'export skin'");
            Thread.Sleep(2000);

            var buttonLocation = linuxFindImageOnScreen.FindImageOnScreen("/tmp/squashfs-root/button.png", 0.50);
            if (buttonLocation != null)
            {
                linuxRunShellCommand.RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
                Thread.Sleep(500);
                linuxDeleteLazerSkin.DeleteLazerSkin();
            }
            else
            {
                Console.WriteLine("Button not found!");
            }

        linuxRunShellCommand.RunShellCommand("xdotool key Escape");
        linuxRunShellCommand.RunShellCommand("xdotool key Escape");
        }
    }
}