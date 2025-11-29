using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Realms;

namespace antiMindblock
{
    public class Skin : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }

        public string Name { get; set; }
        public IList<RealmNamedFileUsage> Files { get; }
    }

    public class RealmNamedFileUsage : RealmObject
    {
        public File? File { get; set; }
        public string? Filename { get; set; }
    }

    public class File : RealmObject
    {
        [PrimaryKey]
        public string? Hash { get; set; }
    }

    public class Lazer
    {
        public static string GetLazerPath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".local/share/osu");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "osu");
            else
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Library/Application Support/osu");
        }

        public static List<(string SkinName, Guid SkinID, string Filename, string? Hash)> GetLazerSkins()
        {
            string lazerPath = GetLazerPath();
            string dbPath = Path.Combine(lazerPath, "client.realm");
            var config = new RealmConfiguration(dbPath)
            {
                IsReadOnly = true,
                SchemaVersion = 51,
                Schema = new[]
                {
                    typeof(Skin),
                    typeof(File),
                    typeof(RealmNamedFileUsage)
                }
            };
            var realm = Realm.GetInstance(config);

            var skinFiles = new List<(string SkinName, Guid SkinID, string Filename, string? Hash)>();
            foreach (var skin in realm.All<Skin>())
            {
                foreach (var usage in skin.Files)
                {
                    if (usage.Filename.Contains("default-") || usage.Filename.Contains("hit0") || usage.Filename.Contains("hit50") || usage.Filename.Contains("hit100") || usage.Filename.Contains("hit300") || usage.Filename.Contains("hitcircle.png"))
                        skinFiles.Add((skin.Name, skin.ID, usage.Filename, usage.File?.Hash));
                }
            }

            return skinFiles;
        }

        public static Guid GetCurrentSkin()
        {
            string lazerPath = GetLazerPath();
            using (StreamReader sr = new StreamReader(Path.Combine(lazerPath, "game.ini")))
            {
                sr.ReadLine();
                string skinID = sr.ReadLine();
                skinID = skinID.Split(" = ")[1];
                return Guid.Parse(skinID);
            };
        }

       public static string ConstructFilePath(string hash)
        {
            string constructedPath = Path.Combine(GetLazerPath(), "files", $"{hash[0]}", hash.Substring(0, 2), hash);
            return constructedPath;
        }

        public static List<(string Filename, string? Hash)> GetCurrentSkinContents()
        {
            Guid currentSkinID = GetCurrentSkin();
            List<(string SkinName, Guid SkinID, string Filename, string? Hash)> skins = GetLazerSkins();
            List<(string Filename, string? Hash)> currentSkin = new List<(string Filename, string? Hash)>();

            foreach (var skin in skins)
            {
                if (skin.SkinID == currentSkinID)
                    currentSkin.Add((skin.Filename, skin.Hash));
            }

            return currentSkin;
        }

        public static string[] GetCurrentSkinFilePaths()
        {
            string[] paths = GetCurrentSkinContents()
                .Select(x => ConstructFilePath(x.Hash ?? string.Empty))
                .ToArray();

            return paths;
        }

        public static void FlipCurrentSkin()
        {
            string[] skinFilePaths = GetCurrentSkinFilePaths();

            foreach (string path in skinFilePaths)
            {
                AssetFlipper.Flip(path);
            }
        }
    }
}
