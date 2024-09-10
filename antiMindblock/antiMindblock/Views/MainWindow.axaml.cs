using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace antiMindblock.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void FlippingClickHandler(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Running on Windows");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Console.WriteLine("Running on macOS");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
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

                        Console.WriteLine("outputs:\n" + result);

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
        else
        {
            Console.WriteLine("Unknown operating system");
        }
    }

    public void UnFlippingClickHandler(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("Running on Windows");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Console.WriteLine("Running on macOS");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
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

                        Console.WriteLine("outputs:\n" + result);

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
                        string unflipCommand = $"xrandr --output {outputName} --rotate normal";
                        Process process = new Process();

                        process.StartInfo.FileName = "/bin/bash";
                        process.StartInfo.Arguments = $"-c \"{unflipCommand}\"";
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