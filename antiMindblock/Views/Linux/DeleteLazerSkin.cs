using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace antiMindblock.LinuxOS
{
    public class linuxDeleteLazerSkin
    {
        public static void DeleteLazerSkin()
        {
            linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
            Task.Delay(2000).GetAwaiter().GetResult();
            File.Delete("/tmp/screenshot.png");

            linuxRunShellCommand.RunShellCommand("xdotool keydown ctrl key 38 keyup ctrl");

            Task.Delay(1500).GetAwaiter().GetResult();

            linuxRunShellCommand.RunShellCommand("xdotool type 'delete skin'");
            linuxRunShellCommand.RunShellCommand("xdotool mousemove 1 1");
            Thread.Sleep(2000);

            var buttonLocation = linuxFindImageOnScreen.FindImageOnScreen("/tmp/squashfs-root/buttondelete.png", 0.50);
            if (buttonLocation != null)
            {
                linuxRunShellCommand.RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
            }
            else
            {
                Console.WriteLine("Button not found!");
            }

            File.Delete("/tmp/screenshot.png");
            Thread.Sleep(1000);

            var confirmationButtonLocation = linuxFindImageOnScreen.FindImageOnScreen("/tmp/squashfs-root/confirm.png", 0.50);
            if (confirmationButtonLocation != null)
            {
                linuxRunShellCommand.RunShellCommand($"xdotool mousemove {confirmationButtonLocation.Value.X} {confirmationButtonLocation.Value.Y} mousedown 1");
                Thread.Sleep(3000);
                linuxRunShellCommand.RunShellCommand($"xdotool mouseup 1");
            }
            else
            {
                Console.WriteLine("Button not found!");
            }
        }
    }
}