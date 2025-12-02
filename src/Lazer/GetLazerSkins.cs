using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public partial class Lazer
    {
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
                    var filename = usage.Filename.ToLowerInvariant();

                    if (!filename.EndsWith(".png"))
                        continue;

                    string[] patterns = {
                        "default-",
                        "hit0",
                        "hit50",
                        "hit100",
                        "hit300",
                        "hitcircle"
                    };

                    if (patterns.Any(p => filename.Contains(p)))
                        skinFiles.Add((skin.Name, skin.ID, usage.Filename, usage.File?.Hash));
                }
            }

            return skinFiles
                .DistinctBy(f => f.Hash) // get rid of duplicates 
                .ToList();
        }
    }
}