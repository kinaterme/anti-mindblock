using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SkiaSharp;

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

    // Experimental button for flipping currently selected skin's elements
    public void TestClickHandler(object sender, RoutedEventArgs args)
    {
        try
        {
            Console.WriteLine("Button clicked - Starting process check");

            // Target executable name (case-insensitive check)
            string targetExecutable = "osu!.exe";

            // Get all running processes
            Process[] allProcesses = Process.GetProcesses();
            Console.WriteLine($"Found {allProcesses.Length} processes");

            foreach (Process proc in allProcesses)
            {
                try
                {
                    // Call method to get the process command line
                    string cmdLine = GetProcessCommandLine(proc);

                    // DEBUG OF PROCESSES: Console.WriteLine($"Process: {proc.ProcessName}, CmdLine: {cmdLine}");

                    if (!string.IsNullOrEmpty(cmdLine) && cmdLine.ToLower().Contains(targetExecutable.ToLower()))
                    {
                        Console.WriteLine($"Found osu!.exe running under Wine with Process ID: {proc.Id}");

                        // Extract the directory where osu!.exe is located
                        string executablePath = ExtractExecutablePath(cmdLine);
                        Console.WriteLine($"Executable Path: {executablePath}");

                        if (Directory.Exists(executablePath))
                        {
                            // Search for the osu! cfg file in the executable's directory
                            string configFilePattern = "osu!.*.cfg";
                            string[] configFiles = Directory.GetFiles(executablePath, configFilePattern);

                            if (configFiles.Length > 0)
                            {
                                string configFile = configFiles.FirstOrDefault();
                                Console.WriteLine($"Config File Found: {configFile}");

                                // Read the content of the config file
                                string[] configLines = File.ReadAllLines(configFile);
                                string skinName = GetSkinNameFromConfig(configLines);
                                Console.WriteLine($"Skin Name: {skinName}");

                                if (!string.IsNullOrEmpty(skinName))
                                {
                                    // Navigate to the Skins directory in the osu! executable directory
                                    string skinsDirectory = Path.Combine(executablePath, "Skins");

                                    if (Directory.Exists(skinsDirectory))
                                    {
                                        string targetSkinFolder = Path.Combine(skinsDirectory, skinName);

                                        if (Directory.Exists(targetSkinFolder))
                                        {
                                            Console.WriteLine($"Skin folder found: {targetSkinFolder}");

                                            OpenFolder(targetSkinFolder);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Skin folder not found: {targetSkinFolder}");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Skins directory does not exist.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No Skin setting found in the config file.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No config file found with pattern osu!.*.cfg");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Executable directory does not exist");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not access process: {proc.ProcessName}, {ex.Message}");
                }
            }

            Console.WriteLine("Process check completed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in TestClickHandler: {ex.Message}");
        }
    }

    // Method to get the command line of a process
    public string GetProcessCommandLine(Process process)
    {
        try
        {
            string procFilePath = $"/proc/{process.Id}/cmdline";
            return File.ReadAllText(procFilePath).Replace('\0', ' ');
        }
        catch
        {
            return null;
        }
    }

    // Method to extract the directory from the command line of osu!.exe
    public string ExtractExecutablePath(string cmdLine)
    {
        string[] parts = cmdLine.Split(' ');
        string executableFullPath = parts[0];
        string unixStylePath = ConvertWinePathToUnix(executableFullPath);
        return Path.GetDirectoryName(unixStylePath);
    }

    // Method to convert Wine paths to Unix paths
    public string ConvertWinePathToUnix(string windowsPath)
    {
        if (windowsPath.StartsWith("Z:\\"))
        {
            string unixPath = windowsPath.Replace("Z:\\", "/").Replace('\\', '/');
            return unixPath;
        }
        return windowsPath;
    }

    // Method to find the Skin line in the config file and extract the skin name
    public string GetSkinNameFromConfig(string[] configLines)
    {
        foreach (string line in configLines)
        {
            if (line.StartsWith("Skin = "))
            {
                return line.Substring(7).Trim();
            }
        }
        return null;
    }

    public void OpenFolder(string folderPath)
    {
        string quotedPath = $"\"{folderPath}\"";
        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-2.png");

            using (var input = File.OpenRead(imagePath))
            using (var originalBitmap = SKBitmap.Decode(input))
            {
                // Create a new bitmap with the same dimensions
                var rotatedBitmap = new SKBitmap(originalBitmap.Width, originalBitmap.Height);

                // Rotation
                using (var canvas = new SKCanvas(rotatedBitmap))
                {
                    canvas.Clear(SKColors.Transparent);
                    canvas.Translate(originalBitmap.Width / 2, originalBitmap.Height / 2);
                    canvas.RotateDegrees(180);
                    canvas.Translate(-originalBitmap.Width / 2, -originalBitmap.Height / 2);
                    canvas.DrawBitmap(originalBitmap, SKRect.Create(originalBitmap.Width, originalBitmap.Height));
                }

                // Save the rotated image
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-2-rotated.png");
                using (var output = File.OpenWrite(rotatedImagePath))
                {
                    rotatedBitmap.Encode(output, SKEncodedImageFormat.Png, 100);
                }

                Console.WriteLine($"Rotated image saved to: {rotatedImagePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error rotating image: {ex.Message}");
        }
    }
}
