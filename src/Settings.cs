using System;
using System.IO;
using System.Runtime.InteropServices;

namespace antiMindblock
{
    public class Settings
    {
        private static readonly string Folder = GetSettingsFolder();
        private static readonly string FilePath = Path.Combine(Folder, "settings.ini");
        
        public static string OsuLazerPath = "";
        public static string OsuLazerReloadMode = "";
        public static string OsuLazerDesktopFilePath = "";
        public static void Initialize()
        {
            EnsureSettingsFile();
            
            OsuLazerPath = GetSettingValue("OsuLazerPath");
            OsuLazerReloadMode = GetSettingValue("OsuLazerReloadMode"); // mode used for reloading the in-game skin - Restart, RestartDesktop, ImageRecognition
            OsuLazerDesktopFilePath = GetSettingValue("OsuLazerDesktopFilePath"); // path to osu!(lazer) .desktop file
        }



        static string GetSettingsFolder()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AntiMindblock");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "AntiMindblock");

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library", "Application Support", "AntiMindblock");
        }

        static void EnsureSettingsFile()
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);

            if (!System.IO.File.Exists(FilePath))
            {
                using (System.IO.File.Create(FilePath)) { }
                WriteDefaultSettings();
            }
        }

        private static void WriteDefaultSettings()
        {
            OsuLazerPath = Lazer.GetDefaultLazerPath();
            OsuLazerReloadMode = "Restart";
            OsuLazerDesktopFilePath = "";
            WriteSettingsFile();
        }

        public static void RestoreDefaultSettings()
        {
            OsuLazerPath = Lazer.GetDefaultLazerPath();
            OsuLazerReloadMode = "Restart";
            OsuLazerDesktopFilePath = "";
        }

        public static void WriteSettingsFile()
        {
            using (var sw = new StreamWriter(FilePath))
            {
                sw.WriteLine("[Stable]");
                sw.WriteLine("[Lazer]");
                sw.WriteLine($"OsuLazerPath={OsuLazerPath}");
                sw.WriteLine($"OsuLazerReloadMode={OsuLazerReloadMode}");
                sw.WriteLine($"OsuLazerDesktopFilePath={OsuLazerDesktopFilePath}");
                sw.Close();
            }
        }

        static string GetSettingValue(string settingName)
        {
            foreach (string line in System.IO.File.ReadAllLines(FilePath))
            {
                if (line.StartsWith(settingName))
                    return line.Split("=")[1];
            }

            return "";
        }
    }
 
}
