using System.Diagnostics;
using System.IO;

namespace antiMindblock.LinuxOS
{
    public class linuxFlipping
    {
        public static void Flipping()
        {
            static void GatherOutputs()
            {
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "xrandr",
                    Arguments = "",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();

                        //Console.WriteLine("outputs:\n" + result);

                        ParseXrandrOutput(result);
                    }
                }
            }

            GatherOutputs();

            static void ParseXrandrOutput(string xrandrOutput)
            {
                string[] lines = xrandrOutput.Split('\n');

                foreach (string line in lines)
                {
                    if (line.Contains("connected primary") && !line.Contains("disconnected"))
                    {
                        string outputName = line.Split(' ')[0];
                        string flipCommand = $"xrandr --output {outputName} --rotate inverted";
                        Process process = new Process();

                        process.StartInfo.FileName = "/bin/bash";
                        process.StartInfo.Arguments = $"-c \"{flipCommand}\"";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.CreateNoWindow = true;

                        process.Start();
                        process.WaitForExit();
                    }
                }
            }
        }
    }
}