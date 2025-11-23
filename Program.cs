using System;
using System.IO;
using Realms;

namespace OsuSkinReader
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

    class Program
    {
        static void Main(string[] args)
        {
            string dbPath = $"/home/{Environment.UserName}/.local/share/osu/client.realm";
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

            Console.WriteLine("Skins in the database:");
            foreach (var skin in realm.All<Skin>())
            {
                Console.WriteLine($"Skin: {skin.Name} ({skin.ID})");

                foreach (var usage in skin.Files)
                {
                    Console.WriteLine($" File: {usage.Filename} => Hash: {usage.File?.Hash}");
                }
            }
        }
    }
}

