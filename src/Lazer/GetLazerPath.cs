using System;
using System.IO;
using System.Runtime.InteropServices;

namespace antiMindblock
{
    public partial class Lazer
    {
        public static string GetLazerPath()
        {
            string path = GetDefaultLazerPath();

            if (Settings.OsuLazerPath != path)
                return Settings.OsuLazerPath;
            else
                return path;
        }

        public static string GetDefaultLazerPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/osu");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "osu");
            else
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library/Application Support/osu");
        }
    }
}