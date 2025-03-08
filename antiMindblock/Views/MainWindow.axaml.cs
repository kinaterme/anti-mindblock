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
using System.Linq.Expressions;
using Avalonia.Controls.Documents;
using System.Transactions;
using antiMindblock.LinuxOS;

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

    public string skinName;
    public MainWindow()
    {
        InitializeComponent();
        
        Width = 550;
        Height = 300;

        linuxInit linuxinit = new linuxInit(this);
        linuxinit.Init();
    }
    
    public void Flipping_Click(object sender, RoutedEventArgs args)
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxFlipping.Flipping();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void Unflipping_Click(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxUnflipping.Unflipping();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    
    public void GetAndEditSkinFolder(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxGetAndEditSkinFolder linuxGetAndEditSkinFolder = new linuxGetAndEditSkinFolder(this);
            linuxGetAndEditSkinFolder.GetAndEditSkinFolder();    
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void DoAll_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        FlipTabletArea(sender, args, 180.0);
        Flipping_Click(sender, args);
        GetAndEditSkinFolder(sender, args);
        FocusAndRefresh(sender, args);
    }
    public void UndoAll_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        FlipTabletArea(sender, args, 0.0);
        Unflipping_Click(sender, args);
        GetAndEditSkinFolder(sender, args);
        FocusAndRefresh(sender, args);
    }

    public void FocusAndRefresh(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxFocusAndRefresh.FocusAndRefresh();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void FlipTabletArea(object sender, RoutedEventArgs args, double rotationValue)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxFlipTabletArea.FlipTabletArea(rotationValue);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public string selectedFolderPath;

    
    private async void FolderPickerButton_Click(object sender, RoutedEventArgs e)
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxFolderPickerButton linuxFolderPickerButton = new linuxFolderPickerButton(this);
            linuxFolderPickerButton.FolderPickerButton();
        }
    }
    
    public void FlipSkinManually(object sender, RoutedEventArgs args)
    {
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxFlipSkinManually linuxFlipSkinManually = new linuxFlipSkinManually(this);
            linuxFlipSkinManually.FlipSkinManually();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void AutoDetectSkinInfo(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxAutoDetectSkinInfo linuxAutoDetectSkinInfo = new linuxAutoDetectSkinInfo(this);
            linuxAutoDetectSkinInfo.AutoDetectSkinInfo();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){

        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){

        }
        
    }

    public void DoAllManual_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        FlipTabletArea(sender, args, 180.0);
        Flipping_Click(sender, args);
        FlipSkinManually(sender, args);
        FocusAndRefresh(sender, args);
    }
    public void UndoAllManual_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        FlipTabletArea(sender, args, 0.0);
        Unflipping_Click(sender, args);
        FlipSkinManually(sender, args);
        FocusAndRefresh(sender, args);
    }

    // continue linux support here vvv

    public void ExportLazerSkin(object sender, RoutedEventArgs args)
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

        var buttonLocation = FindImageOnScreen("/tmp/squashfs-root/button.png", 0.50);
        if (buttonLocation != null)
        {
            linuxRunShellCommand.RunShellCommand($"xdotool mousemove {buttonLocation.Value.X} {buttonLocation.Value.Y} click 1");
            Thread.Sleep(500);
            DeleteLazerSkin(sender, args);
        }
        else
        {
            Console.WriteLine("Button not found!");
        }

    linuxRunShellCommand.RunShellCommand("xdotool key Escape");
    linuxRunShellCommand.RunShellCommand("xdotool key Escape");
    }

    public void DeleteLazerSkin(object sender, RoutedEventArgs args)
    {
        linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
        Task.Delay(2000).GetAwaiter().GetResult();
        File.Delete("/tmp/screenshot.png");

        linuxRunShellCommand.RunShellCommand("xdotool keydown ctrl key 38 keyup ctrl");

        Task.Delay(1500).GetAwaiter().GetResult();

        linuxRunShellCommand.RunShellCommand("xdotool type 'delete skin'");
        linuxRunShellCommand.RunShellCommand("xdotool mousemove 1 1");
        Thread.Sleep(2000);

        var buttonLocation = FindImageOnScreen("/tmp/squashfs-root/buttondelete.png", 0.50);
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

        var confirmationButtonLocation = FindImageOnScreen("/tmp/squashfs-root/confirm.png", 0.50);
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

    static System.Drawing.Point? FindImageOnScreen(string imagePath, double confidence)
    {
        linuxRunShellCommand.RunShellCommand("scrot /tmp/screenshot.png");

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

    static void RunShellCommand.RunShellCommand(string command)
    {
        var processInfo = new ProcessStartInfo("bash", $"-c \"{command}\"")
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        var process = Process.Start(processInfo);
        process.WaitForExit();
    
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
            linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
            Task.Delay(2000).GetAwaiter().GetResult();
            File.Delete("/tmp/screenshot.png");

            linuxRunShellCommand.RunShellCommand("xdotool keydown ctrl key 32 keyup ctrl");

            Task.Delay(1500).GetAwaiter().GetResult();

            linuxRunShellCommand.RunShellCommand("xdotool type 'delete skin'");
            linuxRunShellCommand.RunShellCommand("xdotool mousemove 1 1");
            Thread.Sleep(2000);

            var buttonLocation = FindImageOnScreen("/tmp/squashfs-root/buttondelete.png", 0.50);
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

            var confirmationButtonLocation = FindImageOnScreen("/tmp/squashfs-root/confirm.png", 0.50);
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

            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursor.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursortrail.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursortail.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircle.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircle@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircleoverlay.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircleoverlay@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "reversearrow.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "reversearrow@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "sliderfollowcircle.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "sliderfollowcircle@2x.png"));

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
            var buttonLocation = FindImageOnScreen("/tmp/squashfs-root/buttonimported.png", 0.50);
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

        linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
        Task.Delay(2000).GetAwaiter().GetResult();
        File.Delete("/tmp/screenshot.png");

        linuxRunShellCommand.RunShellCommand("xdotool keydown ctrl key 32 keyup ctrl");

        Task.Delay(1500).GetAwaiter().GetResult();

        linuxRunShellCommand.RunShellCommand("xdotool type 'delete skin'");
        Thread.Sleep(2000);

        var buttonLocation = FindImageOnScreen("/tmp/squashfs-root/buttondelete.png", 0.50);
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

        var confirmationButtonLocation = FindImageOnScreen("/tmp/squashfs-root/confirm.png", 0.50);
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

        if (Directory.Exists(osuLazerExtractedSkinFolder))
        {            
            Thread.Sleep(1100);

            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-1@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-2@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-3@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-4@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-5@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-6@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-7@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-8@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-9@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "default-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursor.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursortrail.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "cursortail.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit50-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k-0.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hit100k-0@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircle.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircle@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircleoverlay.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "hitcircleoverlay@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "reversearrow.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "reversearrow@2x.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "sliderfollowcircle.png"));
            AssetFlipper(Path.Combine(quotedOsuLazerExtractedSkinFolder.Trim('"'), "sliderfollowcircle@2x.png"));

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
            var importButtonLocation = FindImageOnScreen("/tmp/squashfs-root/buttonimported.png", 0.50);
            if (importButtonLocation != null)
            {
                linuxRunShellCommand.RunShellCommand("wmctrl -R 'osu!'");
                Thread.Sleep(100);
                linuxRunShellCommand.RunShellCommand($"xdotool mousemove {importButtonLocation.Value.X} {importButtonLocation.Value.Y} click 1");
            }
            else
            {
                Console.WriteLine("Button not found!");
            }
        }
        linuxRunShellCommand.RunShellCommand("xdotool key Escape");
        linuxRunShellCommand.RunShellCommand("xdotool key Escape");
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