using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

using antiMindblock.Views;

namespace antiMindblock.LinuxOS
{
    public class linuxSkinFetcher
    {
        private linuxGetAndEditSkinFolder _linuxGetAndEditSkinFolder;
        private MainWindow _mainWindow;
        public linuxSkinFetcher(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _linuxGetAndEditSkinFolder = new linuxGetAndEditSkinFolder(mainWindow); 
        }

        //linuxGetAndEditSkinFolder linuxGetAndEditSkinFolder = new linuxGetAndEditSkinFolder();
        public void SkinFetcher()
        {
            try
            {
                Console.WriteLine("Button clicked - Starting process check");

                string targetExecutable = "osu!/osu!.exe";

                // Get all running processes
                Process[] allProcesses = Process.GetProcesses();
                Console.WriteLine($"Found {allProcesses.Length} processes");

                foreach (Process proc in allProcesses)
                {
                    try
                    {
                        string cmdLine = _linuxGetAndEditSkinFolder.GetProcessCommandLine(proc);

                        // DEBUG OF PROCESSES: Console.WriteLine($"Process: {proc.ProcessName}, CmdLine: {cmdLine}");

                        if (!string.IsNullOrEmpty(cmdLine) && cmdLine.ToLower().Contains(targetExecutable.ToLower()))
                        {
                            Console.WriteLine($"Found osu!.exe running under Wine with Process ID: {proc.Id}");

                            // Extract the directory where osu!.exe is located
                            string executablePath = _linuxGetAndEditSkinFolder.ExtractExecutablePath(cmdLine);
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
                                    _mainWindow.skinName = _linuxGetAndEditSkinFolder.GetSkinNameFromConfig(configLines);
                                    Console.WriteLine(_mainWindow.skinName);
                                    Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                                    {
                                        _mainWindow.AutoDetectSkinLabel.Text = $"Skin detected: {_mainWindow.skinName}";
                                    });
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
    }
}