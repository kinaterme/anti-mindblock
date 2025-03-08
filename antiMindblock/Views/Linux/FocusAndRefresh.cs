using System.Diagnostics;
using System.Threading;

namespace antiMindblock.LinuxOS
{
    public class linuxFocusAndRefresh
    {
        public static void FocusAndRefresh()
        {
            string command = "wmctrl -R 'osu!'";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(startInfo);
            Thread.Sleep(1050);
            string refreshSkin = "xdotool key ctrl+alt+shift+s";

            ProcessStartInfo refreshS = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{refreshSkin}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(refreshS);
        }
    }
}