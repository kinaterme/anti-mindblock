using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using antiMindblock.Views;
using Avalonia.Interactivity;

namespace antiMindblock.LinuxOS
{
    public class linuxGetAndEditSkinFolder
    {
        private MainWindow _mainWindow;
        public linuxGetAndEditSkinFolder(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
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
        public string ExtractExecutablePath(string cmdLine)
        {
            string[] parts = cmdLine.Split(' ');
            string executableFullPath = parts[4];
            string unixStylePath = ConvertWinePathToUnix(executableFullPath);
            return Path.GetDirectoryName(unixStylePath);
        }
        public string ConvertWinePathToUnix(string windowsPath)
        {
            if (windowsPath.StartsWith("Z:\\"))
            {
                string unixPath = windowsPath.Replace("Z:\\", "/").Replace('\\', '/');
                return unixPath;
            }
            return windowsPath;
        }
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

        linuxOpenAndEditSkin linuxOpenAndEditSkin = new linuxOpenAndEditSkin();
        public void GetAndEditSkinFolder()
        {
            try
            {
                Console.WriteLine("Button clicked - Starting process check");

                string targetExecutable = "osu!/osu!.exe";

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

                                    string[] configLines = File.ReadAllLines(configFile);
                                    _mainWindow.skinName = GetSkinNameFromConfig(configLines);
                                    Console.WriteLine($"Skin Name: {_mainWindow.skinName}");

                                    if (!string.IsNullOrEmpty(_mainWindow.skinName))
                                    {
                                        string skinsDirectory = Path.Combine(executablePath, "Skins");

                                        if (Directory.Exists(skinsDirectory))
                                        {
                                            string targetSkinFolder = Path.Combine(skinsDirectory, _mainWindow.skinName);

                                            if (Directory.Exists(targetSkinFolder))
                                            {
                                                Console.WriteLine($"Skin folder found: {targetSkinFolder}");

                                                linuxOpenAndEditSkin.OpenAndEditSkin(targetSkinFolder);
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
    }
}