using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace antiMindblock.LinuxOS
{
    public class linuxEditLazerSkin
    {
        linuxAssetFlipper linuxAssetFlipper = new linuxAssetFlipper();
        public void EditLazerSkin()
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
                linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
                Task.Delay(2000).GetAwaiter().GetResult();
                File.Delete("/tmp/screenshot.png");

                linuxRunShellCommand.RunShellCommand("xdotool keydown ctrl key 32 keyup ctrl");

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
                    Thread.Sleep(2500);
                    linuxRunShellCommand.RunShellCommand($"xdotool mouseup 1");
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

                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursor.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursortrail.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursortail.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit0.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit0@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50-0.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50-0@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100-0.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100-0@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k-0.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k-0@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircle.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircle@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircleoverlay.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircleoverlay@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "reversearrow.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "reversearrow@2x.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "sliderfollowcircle.png"));
                linuxAssetFlipper.AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "sliderfollowcircle@2x.png"));

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
                    linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
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
                var buttonLocation = linuxFindImageOnScreen.FindImageOnScreen("/tmp/squashfs-root/buttonimported.png", 0.50);
                if (buttonLocation != null)
                {
                    linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
                    Thread.Sleep(100);
                    linuxRunShellCommand.RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
                }
                else
                {
                    Console.WriteLine("Button not found!");
                }
            }
        }
    }
}