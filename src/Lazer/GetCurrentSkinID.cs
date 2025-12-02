using System;
using System.IO;

namespace antiMindblock
{
    public partial class Lazer
    {
        public static Guid GetCurrentSkinID()
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
    }
}