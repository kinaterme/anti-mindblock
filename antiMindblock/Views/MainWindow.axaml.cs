using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SkiaSharp;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Drawing.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace antiMindblock.Views;

public partial class MainWindow : Avalonia.Controls.Window
{

    public async Task ShowMessageBoxAsync(string program)
    {
        var box = MessageBoxManager
                    .GetMessageBoxStandard("Caution", $"{program} is not installed, this program will not work unless you install it.",
                        ButtonEnum.Ok);

                var result = await box.ShowAsync();
    }

    private string skinName;
    public MainWindow()
    {
        InitializeComponent();

        var xdotoolcheck = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = "-c \"xdotool\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var scrotcheck = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = "-c \"scrot --help\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var wmctrlcheck = new ProcessStartInfo
        {
            FileName = "bash",
            Arguments = "-c \"wmctrl -m\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = new Process { StartInfo = xdotoolcheck })
        {
            process.Start();

            // Read the output and error streams
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            // Check if there is an error output
            if (!string.IsNullOrWhiteSpace(error) && error.Contains("bash: xdotool:"))
            {
                this.Opened += async (sender, args) => await ShowMessageBoxAsync("xdotool");
            }
            else if (!string.IsNullOrWhiteSpace(output))
            {
                Console.WriteLine("xdotool check: Installed");
            }
            else
            {
                this.Opened += async (sender, args) => await ShowMessageBoxAsync("xdotool");
            }
        }

        using (var process = new Process { StartInfo = scrotcheck })
        {
            process.Start();

            // Read the output and error streams
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            // Check if there is an error output
            if (!string.IsNullOrWhiteSpace(error) && error.Contains("bash: scrot:"))
            {
                this.Opened += async (sender, args) => await ShowMessageBoxAsync("scrot");
            }
            else if (!string.IsNullOrWhiteSpace(output))
            {
                Console.WriteLine("scrot check: Installed");
            }
            else
            {
                this.Opened += async (sender, args) => await ShowMessageBoxAsync("scrot");
            }
        }

        using (var process = new Process { StartInfo = wmctrlcheck })
        {
            process.Start();

            // Read the output and error streams
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            // Check if there is an error output
            if (!string.IsNullOrWhiteSpace(error) && error.Contains("bash: wmctrl:"))
            {
                this.Opened += async (sender, args) => await ShowMessageBoxAsync("wmctrl");
            }
            else if (!string.IsNullOrWhiteSpace(output))
            {
                Console.WriteLine("wmctrl check: Installed");
            }
            else
            {
                this.Opened += async (sender, args) => await ShowMessageBoxAsync("wmctrl");
            }
        }


    }

    public void Flipping_Click(object sender, RoutedEventArgs args)
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


        else
        {
            Console.WriteLine("Unknown operating system");
        }
    }

    public void Unflipping_Click(object sender, RoutedEventArgs args)
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
    public void GetAndEditSkinFolder(object sender, RoutedEventArgs args)
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

                                            OpenAndEditSkin(targetSkinFolder);
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
            Console.WriteLine($"Error in GetAndEditSkinFolder: {ex.Message}");
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

    public void SkinFetcher()
    {
        try
        {
            Console.WriteLine("Button clicked - Starting process check");

            string targetExecutable = "osu!.exe";

            // Get all running processes
            Process[] allProcesses = Process.GetProcesses();
            Console.WriteLine($"Found {allProcesses.Length} processes");

            foreach (Process proc in allProcesses)
            {
                try
                {
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
                                Console.WriteLine(skinName);
                                AutoDetectSkinLabel.Text = $"Skin detected: {skinName}";
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public void OpenAndEditSkin(string folderPath)
    {
        string quotedPath = $"\"{folderPath}\"";
        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-1.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-1.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-1@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-1@2x.png");
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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-2.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-2@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-2@2x.png");
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


        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-3.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-3.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-3@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-3@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-4.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-4.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-4@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-4@2x.png");
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


        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-5.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-5.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-5@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-5@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-6.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-6.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-6@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-6@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-7.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-7.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-7@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-7@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-8.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-8.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-8@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-8@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-9.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-9.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-9@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-9@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-0.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-0.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-0@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-0@2x.png");
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

    public void DoAll_Click(object sender, RoutedEventArgs args)
    {
        FlipTabletArea(sender, args, 180.0);
        Flipping_Click(sender, args);
        GetAndEditSkinFolder(sender, args);
        FocusAndRefresh(sender, args);
    }
    public void UndoAll_Click(object sender, RoutedEventArgs args)
    {
        FlipTabletArea(sender, args, 0.0);
        Unflipping_Click(sender, args);
        GetAndEditSkinFolder(sender, args);
        FocusAndRefresh(sender, args);
    }

    public void FocusAndRefresh(object sender, RoutedEventArgs args)
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
        Thread.Sleep(700);
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

    public void FlipTabletArea(object sender, RoutedEventArgs args, double newRotationValue)
    {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(homeDirectory, ".config/OpenTabletDriver/settings.json");

        try
        {
            string jsonString = File.ReadAllText(filePath);

            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                JsonObject root = JsonSerializer.Deserialize<JsonObject>(jsonString);

                var profiles = root["Profiles"] as JsonArray;

                if (profiles != null && profiles.Count > 0)
                {
                    var firstProfile = profiles[0] as JsonObject;
                    if (firstProfile != null)
                    {
                        var absoluteModeSettings = firstProfile["AbsoluteModeSettings"] as JsonObject;
                        var tabletSettings = absoluteModeSettings?["Tablet"] as JsonObject;

                        if (tabletSettings != null)
                        {
                            tabletSettings["Rotation"] = JsonValue.Create(newRotationValue);

                            string updatedJsonString = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });

                            File.WriteAllText(filePath, updatedJsonString);

                            Console.WriteLine("Rotation value updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Tablet settings not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Profile not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Profiles array is empty or not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        string command = "systemctl --user restart opentabletdriver";

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
    }

    public void FlipTabletAreaStandalone(object sender, RoutedEventArgs args)
    {
        FlipTabletArea(sender, args, 180.0);
    }
    public void UnflipTabletAreaStandalone(object sender, RoutedEventArgs args)
    {
        FlipTabletArea(sender, args, 0.0);
    }

    private string selectedFolderPath;

    private async void FolderPickerButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog
        {
            Title = "Select a folder"
        };

        string result = await dialog.ShowAsync(this);

        if (!string.IsNullOrEmpty(result))
        {
            selectedFolderPath = result;

            FolderPathTextBlock.Text = $"Selected Folder: {selectedFolderPath}";
            FolderPathTextBlockMisc.Text = $"Selected Folder: {selectedFolderPath}";
        }
        else
        {
            FolderPathTextBlock.Text = "No skin selected.";
            FolderPathTextBlockMisc.Text = "No skin selected.";
        }
    }

    public void FlipSkinManually(object sender, RoutedEventArgs args)
    {
        string quotedPath = $"\"{selectedFolderPath}\"";
        try
        {
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-1.png");

            using (var input = File.OpenRead(imagePath))
            using (var originalBitmap = SKBitmap.Decode(input))
            {
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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-1.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-1@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-1@2x.png");
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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-2.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-2@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-2@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-3.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-3.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-3@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-3@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-4.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-4.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-4@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-4@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-5.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-5.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-5@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-5@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-6.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-6.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-6@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-6@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-7.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-7.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-7@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-7@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-8.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-8.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-8@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-8@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-9.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-9.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-9@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-9@2x.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-0.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-0.png");
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

        try
        {
            // Create the full path to the image
            string imagePath = Path.Combine(quotedPath.Trim('"'), "default-0@2x.png");

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
                string rotatedImagePath = Path.Combine(quotedPath.Trim('"'), "default-0@2x.png");
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

    public void AutoDetectSkinInfo(object sender, RoutedEventArgs args)
    {
        SkinFetcher();
        Console.WriteLine(skinName);
    }

    public void DoAllManual_Click(object sender, RoutedEventArgs args)
    {
        FlipTabletArea(sender, args, 180.0);
        Flipping_Click(sender, args);
        FlipSkinManually(sender, args);
        FocusAndRefresh(sender, args);
    }
    public void UndoAllManual_Click(object sender, RoutedEventArgs args)
    {
        FlipTabletArea(sender, args, 0.0);
        Unflipping_Click(sender, args);
        FlipSkinManually(sender, args);
        FocusAndRefresh(sender, args);
    }
    public void ExportLazerSkin(object sender, RoutedEventArgs args)
    {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string lazerExportDirectoryCleanup = Path.Combine(homeDirectory, ".local/share/osu/exports");
        string quotedExportDirectoryCleanup = $"\"{lazerExportDirectoryCleanup}\"";
        string everyFileInDirectory = Path.Combine(lazerExportDirectoryCleanup, "*");
        RunShellCommand($"cd {quotedExportDirectoryCleanup} && rm -rf {everyFileInDirectory}");
        Thread.Sleep(500);
        RunShellCommand("wmctrl -R 'osu!'");
        Task.Delay(2000).GetAwaiter().GetResult();
        File.Delete("/tmp/screenshot.png");

        RunShellCommand("xdotool keydown ctrl key 32 keyup ctrl");

        Task.Delay(1500).GetAwaiter().GetResult();

        RunShellCommand("xdotool type 'export skin'");
        Thread.Sleep(2000);

        var buttonLocation = FindImageOnScreen("button.png", 0.50);
        if (buttonLocation != null)
        {
            RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
            Thread.Sleep(500);
            DeleteLazerSkin(sender, args);
        }
        else
        {
            Console.WriteLine("Button not found!");
        }
    }

    public void DeleteLazerSkin(object sender, RoutedEventArgs args)
    {
        RunShellCommand("wmctrl -R 'osu!'");
        Task.Delay(2000).GetAwaiter().GetResult();
        File.Delete("/tmp/screenshot.png");

        RunShellCommand("xdotool keydown ctrl key 38 keyup ctrl");

        Task.Delay(1500).GetAwaiter().GetResult();

        RunShellCommand("xdotool type 'delete sel skin'");
        Thread.Sleep(2000);

        var buttonLocation = FindImageOnScreen("buttondelete.png", 0.50);
        if (buttonLocation != null)
        {
            RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
        }
        else
        {
            Console.WriteLine("Button not found!");
        }

        File.Delete("/tmp/screenshot.png");
        Thread.Sleep(1000);

        var confirmationButtonLocation = FindImageOnScreen("confirm.png", 0.50);
        if (confirmationButtonLocation != null)
        {
            RunShellCommand($"xdotool mousemove {confirmationButtonLocation.Value.X} {confirmationButtonLocation.Value.Y} mousedown 1");
            Thread.Sleep(3000);
            RunShellCommand($"xdotool mouseup 1");
        }
        else
        {
            Console.WriteLine("Button not found!");
        }
    }

    static System.Drawing.Point? FindImageOnScreen(string imagePath, double confidence)
    {
        RunShellCommand("scrot /tmp/screenshot.png");

        using var screenshot = new Mat("/tmp/screenshot.png", ImreadModes.Grayscale);
        using var template = new Mat(imagePath, ImreadModes.Grayscale);

        using var result = new Mat();
        Cv2.MatchTemplate(screenshot, template, result, TemplateMatchModes.CCoeffNormed);

        Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

        if (maxVal >= confidence)
        {
            return new System.Drawing.Point(maxLoc.X + template.Width / 2, maxLoc.Y + template.Height / 2);
        }

        return null;
    }

    static void RunShellCommand(string command)
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

    public void EditLazerSkin(object sender, RoutedEventArgs args)
    {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string osuLazerExportDirectory = Path.Combine(homeDirectory, ".local/share/osu/exports");
        string osuLazerExportedSkinPath = Path.Combine(osuLazerExportDirectory, "*.osk");
        string quotedOsuLazerExportedSkinPath = $"\"{osuLazerExportedSkinPath}\"";
        string osuLazerExtractedSkinFolder = Path.Combine(osuLazerExportDirectory, "rotated");
        string quotedOsuLazerExtractedSkinFolder = $"\"{osuLazerExtractedSkinFolder}\"";
        string unzipCommand = $"unzip -o {quotedOsuLazerExportedSkinPath} -d {quotedOsuLazerExtractedSkinFolder}";
        string zipCommand = $"cd {quotedOsuLazerExtractedSkinFolder} && zip -r ../rotated.osk *";
        Console.WriteLine(quotedOsuLazerExportedSkinPath);

        if (Directory.Exists(osuLazerExtractedSkinFolder))
        {
            RunShellCommand("wmctrl -R 'osu!'");
            Task.Delay(2000).GetAwaiter().GetResult();
            File.Delete("/tmp/screenshot.png");

            RunShellCommand("xdotool keydown ctrl key 32 keyup ctrl");

            Task.Delay(1500).GetAwaiter().GetResult();

            RunShellCommand("xdotool type 'delete sel skin'");
            Thread.Sleep(2000);

            var buttonLocation = FindImageOnScreen("buttondelete.png", 0.50);
            if (buttonLocation != null)
            {
                RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
            }
            else
            {
                Console.WriteLine("Button not found!");
            }

            File.Delete("/tmp/screenshot.png");
            Thread.Sleep(1000);

            var confirmationButtonLocation = FindImageOnScreen("confirm.png", 0.50);
            if (confirmationButtonLocation != null)
            {
                RunShellCommand($"xdotool mousemove {confirmationButtonLocation.Value.X} {confirmationButtonLocation.Value.Y} mousedown 1");
                Thread.Sleep(2500);
                RunShellCommand($"xdotool mouseup 1");
            }
            else
            {
                Console.WriteLine("Button not found!");
            }
        }
        else
        {
            Console.WriteLine("Already exported");
        }

        if (Directory.Exists(osuLazerExportDirectory))
        {            
            var unzipProcessInfo = new ProcessStartInfo("bash", $"-c \"{unzipCommand}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var unzipProcess = Process.Start(unzipProcessInfo);
            unzipProcess.Start();
            unzipProcess.WaitForExit();

            Thread.Sleep(1100);

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0@2x.png");
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

            Thread.Sleep(1100);

            var zipProcessInfo = new ProcessStartInfo("bash", $"-c \"{zipCommand}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var zipProcess = Process.Start(zipProcessInfo);
            zipProcess.Start();
            zipProcess.WaitForExit();

            Thread.Sleep(100);

            string keyword = "osu";
            string excludeKeyword = "appimagelauncher";
            string parameter = Path.Combine(osuLazerExportDirectory, "rotated.osk");

            var appImagePath = FindRunningAppImage(keyword, excludeKeyword);

            if (appImagePath != null)
            {
                RunAppImage(appImagePath, parameter);
                Thread.Sleep(500);
                RunShellCommand("wmctrl -R 'osu!'");
            }
            else
            {
                Console.WriteLine("No matching AppImage found.");
            }
            

            static string FindRunningAppImage(string keyword, string excludeKeyword)
            {
                var processes = Process.GetProcesses();

                foreach (var process in processes)
                {
                    try
                    {
                        var processName = process.MainModule.FileName;
                        if (processName.Contains(keyword, StringComparison.OrdinalIgnoreCase) &&
                            !processName.Contains(excludeKeyword, StringComparison.OrdinalIgnoreCase))
                        {
                            return processName;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error accessing process: {ex.Message}");
                    }
                }

                return null;
            }

            static void RunAppImage(string appImagePath, string parameter)
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = appImagePath,
                    Arguments = parameter,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processStartInfo))
                {
                    process.WaitForExit();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    Console.WriteLine($"Output: {output}");
                    Console.WriteLine($"Error: {error}");
                }
            }

            Thread.Sleep(1500);

            File.Delete("/tmp/screenshot.png");
            var buttonLocation = FindImageOnScreen("buttonimported.png", 0.50);
            if (buttonLocation != null)
            {
                RunShellCommand("wmctrl -R 'osu!'");
                Thread.Sleep(100);
                RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
            }
            else
            {
                Console.WriteLine("Button not found!");
            }
        }
    }

    public void UndoEditsLazerSkin(object sender, RoutedEventArgs args)
    {
        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string osuLazerExportDirectory = Path.Combine(homeDirectory, ".local/share/osu/exports");
        string osuLazerExportedSkinPath = Path.Combine(osuLazerExportDirectory, "*.osk");
        string quotedOsuLazerExportedSkinPath = $"\"{osuLazerExportedSkinPath}\"";
        string osuLazerExtractedSkinFolder = Path.Combine(osuLazerExportDirectory, "rotated");
        string quotedOsuLazerExtractedSkinFolder = $"\"{osuLazerExtractedSkinFolder}\"";
        string unzipCommand = $"unzip -o {quotedOsuLazerExportedSkinPath} -d {quotedOsuLazerExtractedSkinFolder}";
        string zipCommand = $"cd {quotedOsuLazerExtractedSkinFolder} && zip -r ../rotated.osk *";
        Console.WriteLine(quotedOsuLazerExportedSkinPath);

        RunShellCommand("wmctrl -R 'osu!'");
        Task.Delay(2000).GetAwaiter().GetResult();
        File.Delete("/tmp/screenshot.png");

        RunShellCommand("xdotool keydown ctrl key 32 keyup ctrl");

        Task.Delay(1500).GetAwaiter().GetResult();

        RunShellCommand("xdotool type 'delete sel skin'");
        Thread.Sleep(2000);

        var buttonLocation = FindImageOnScreen("buttondelete.png", 0.50);
        if (buttonLocation != null)
        {
            RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
        }
        else
        {
            Console.WriteLine("Button not found!");
        }

        File.Delete("/tmp/screenshot.png");
        Thread.Sleep(1000);

        var confirmationButtonLocation = FindImageOnScreen("confirm.png", 0.50);
        if (confirmationButtonLocation != null)
        {
            RunShellCommand($"xdotool mousemove {confirmationButtonLocation.Value.X} {confirmationButtonLocation.Value.Y} mousedown 1");
            Thread.Sleep(3000);
            RunShellCommand($"xdotool mouseup 1");
        }
        else
        {
            Console.WriteLine("Button not found!");
        }

        if (Directory.Exists(osuLazerExtractedSkinFolder))
        {            
            Thread.Sleep(1100);

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9@2x.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0.png");
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

            try
            {
                // Create the full path to the image
                string imagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0@2x.png");

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
                    string rotatedImagePath = Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0@2x.png");
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

            Thread.Sleep(1100);

            var zipProcessInfo = new ProcessStartInfo("bash", $"-c \"{zipCommand}\"")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var zipProcess = Process.Start(zipProcessInfo);
            zipProcess.Start();
            zipProcess.WaitForExit();

            Thread.Sleep(100);

            string keyword = "osu";
            string excludeKeyword = "appimagelauncher";
            string parameter = Path.Combine(osuLazerExportDirectory, "rotated.osk");

            var appImagePath = FindRunningAppImage(keyword, excludeKeyword);

            if (appImagePath != null)
            {
                RunAppImage(appImagePath, parameter);
                
            }
            else
            {
                Console.WriteLine("No matching AppImage found.");
            }
            

            static string FindRunningAppImage(string keyword, string excludeKeyword)
            {
                var processes = Process.GetProcesses();

                foreach (var process in processes)
                {
                    try
                    {
                        var processName = process.MainModule.FileName;
                        if (processName.Contains(keyword, StringComparison.OrdinalIgnoreCase) &&
                            !processName.Contains(excludeKeyword, StringComparison.OrdinalIgnoreCase))
                        {
                            return processName;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error accessing process: {ex.Message}");
                    }
                }

                return null;
            }

            static void RunAppImage(string appImagePath, string parameter)
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = appImagePath,
                    Arguments = parameter,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processStartInfo))
                {
                    process.WaitForExit();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    Console.WriteLine($"Output: {output}");
                    Console.WriteLine($"Error: {error}");
                }
            }

            Thread.Sleep(1000);

            File.Delete("/tmp/screenshot.png");
            var importButtonLocation = FindImageOnScreen("buttonimported.png", 0.50);
            if (importButtonLocation != null)
            {
                RunShellCommand("wmctrl -R 'osu!'");
                Thread.Sleep(100);
                RunShellCommand($"xdotool mousemove {importButtonLocation.Value.X} {importButtonLocation.Value.Y} click 1");
            }
            else
            {
                Console.WriteLine("Button not found!");
            }
        
        }
    }

    public void ExportLazer_Click(object sender, RoutedEventArgs args)
    {
        ExportLazerSkin(sender, args);
    }

    public void EditLazer_Click(object sender, RoutedEventArgs args)
    {
        EditLazerSkin(sender, args);
        FlipTabletArea(sender, args, 180.0);
        Flipping_Click(sender, args);
    }

    public void UndoLazer_Click(object sender, RoutedEventArgs args)
    {
        FlipTabletArea(sender, args, 0.0);
        UndoEditsLazerSkin(sender, args);
        Unflipping_Click(sender, args);
    }
}

