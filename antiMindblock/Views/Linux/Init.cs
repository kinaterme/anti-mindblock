using System;
using System.Diagnostics;
using System.IO;

using antiMindblock.Views;

namespace antiMindblock.LinuxOS
{
    public class linuxInit 
    {
        private MainWindow _mainWindow;

        public linuxInit(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        public void Init()
        {
            // if running from source detected:
            string binaryPath = Path.GetDirectoryName(Environment.ProcessPath);
            string appimageOrSourceCheck = Path.Combine(binaryPath, "button.png");
            string copyImagesIfSource = Path.Combine(binaryPath, "*.png");
            string quotedcopyImagesIfSource = $"\"{copyImagesIfSource}\"";
            string quotedappimageOrSourceCheck = $"\"{appimageOrSourceCheck}\"";
            if (binaryPath == null)
            {
                Console.WriteLine("Failed to determine the AppImage directory.");
                return;
            }

            string binaryFileName = Path.GetFileName(Environment.ProcessPath);
            if (string.IsNullOrEmpty(binaryFileName))
            {
                Console.WriteLine("Failed to determine the AppImage filename.");
                return;
            }

            string fullbinaryPath = Path.Combine(binaryPath, binaryFileName);

            if (File.Exists(Path.Combine(binaryPath, "button.png"))) 
            {
                Directory.Delete("/tmp/squashfs-root/", true);
                Directory.CreateDirectory("/tmp/squashfs-root/");
                linuxRunShellCommand.RunShellCommand($"cp -r {quotedcopyImagesIfSource} '/tmp/squashfs-root/'");
            }

            // If appimage gets detected:
            string appImageOriginalPath = Environment.GetEnvironmentVariable("APPIMAGE");
            if (string.IsNullOrEmpty(appImageOriginalPath))
            {
                Console.WriteLine("Failed to determine the original AppImage path. \n Running from source. \n If you're running the AppImage build, \n $APPIMAGE might not be set!");
                return;
            }

            Console.WriteLine($"AppImage Path: {appImageOriginalPath}");

            string appImageDirectory = Path.GetDirectoryName(appImageOriginalPath);
            string appImageFileName = Path.GetFileName(appImageOriginalPath);

            if (appImageDirectory == null || appImageFileName == null)
            {
                Console.WriteLine("Failed to parse the AppImage path.");
                return;
            }

            Process extractProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = $"-c \"./{appImageFileName} --appimage-extract\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = appImageDirectory
                }
            };

            try
            {
                if (!File.Exists("/tmp/squashfs-root/button.png"))
                {
                    extractProcess.Start();
                    string output = extractProcess.StandardOutput.ReadToEnd();
                    string error = extractProcess.StandardError.ReadToEnd();
                    extractProcess.WaitForExit();

                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine("Extraction Error:");
                        Console.WriteLine(error);
                    }

                    string extractedFolder = Path.Combine(appImageDirectory, "squashfs-root");
                    string tmpFolder = Path.Combine("/tmp", "squashfs-root");

                    if (Directory.Exists(extractedFolder))
                    {
                        if (Directory.Exists(tmpFolder))
                        {
                            Directory.Delete(tmpFolder, true);
                        }

                        linuxRunShellCommand.RunShellCommand($"cp -r {extractedFolder} {tmpFolder}");
                        Console.WriteLine($"Moved extracted folder to {tmpFolder}");
                        Directory.Delete(Path.Combine(appImageDirectory, "squashfs-root"), true);
                    }
                    else
                    {
                        Console.WriteLine("The extracted folder 'squashfs-root' was not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

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
                    _mainWindow.Opened += async (sender, args) => await _mainWindow.ShowMessageBoxAsync("xdotool");
                }
                else if (!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine("xdotool check: Installed");
                }
                else
                {
                    _mainWindow.Opened += async (sender, args) => await _mainWindow.ShowMessageBoxAsync("xdotool");
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
                    _mainWindow.Opened += async (sender, args) => await _mainWindow.ShowMessageBoxAsync("scrot");
                }
                else if (!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine("scrot check: Installed");
                }
                else
                {
                    _mainWindow.Opened += async (sender, args) => await _mainWindow.ShowMessageBoxAsync("scrot");
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
                    _mainWindow.Opened += async (sender, args) => await _mainWindow.ShowMessageBoxAsync("wmctrl");
                }
                else if (!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine("wmctrl check: Installed");
                }
                else
                {
                    _mainWindow.Opened += async (sender, args) => await _mainWindow.ShowMessageBoxAsync("wmctrl");
                }
            }
        }
    }
}
