using System.Diagnostics;

namespace antiMindblock.LinuxOS
{
    public class linuxRunShellCommand
    {
        public static void RunShellCommand(string command)
        {
            var processInfo = new ProcessStartInfo("bash", $"-c \"{command}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = Process.Start(processInfo);
            process.WaitForExit();
        }
    }
}