using System;
using System.Collections.Generic;

namespace antiMindblock
{
    public partial class Lazer
    {
        public static List<(string Filename, string? Hash)> GetCurrentSkinFiles()
        {
            Guid currentSkinID = GetCurrentSkinID();
            List<(string SkinName, Guid SkinID, string Filename, string? Hash)> skins = GetLazerSkins();
            List<(string Filename, string? Hash)> currentSkin = new List<(string Filename, string? Hash)>();

            foreach (var skin in skins)
            {
                if (skin.SkinID == currentSkinID)
                    currentSkin.Add((skin.Filename, skin.Hash));
            }

            return currentSkin;
        }
    }
}