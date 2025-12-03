using System;
using System.IO;
using System.Runtime.InteropServices;

namespace antiMindblock
{
    public class Settings
    {
        private static readonly string Folder = GetSettingsFolder();
        private static readonly string FilePath = Path.Combine(Folder, "settings.ini");

        public static void Initialize()
        {
            EnsureSettingsFile();
        }

        public static string OsuLazerPath => GetSettingValue("OsuLazerPath");
        public static string OsuLazerReloadMode => GetSettingValue("OsuLazerReloadMode"); // mode used for reloading the in-game skin - Restart, RestartDesktop, ImageRecognition
        public static string OsuLazerDesktopFilePath => GetSettingValue("OsuLazerDesktopFilePath"); // path to osu!(lazer) .desktop file

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

        static void WriteDefaultSettings()
        {
            using (var sw = new StreamWriter(FilePath))
            {
                sw.WriteLine("[Stable]");
                sw.WriteLine("[Lazer]");
                sw.WriteLine($"OsuLazerPath={Lazer.GetDefaultLazerPath()}");
                sw.WriteLine($"OsuLazerReloadMode=Restart"); // Restart, RestartDesktop, ImageRecognition
                sw.WriteLine($"OsuLazerDesktopFilePath=");
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
