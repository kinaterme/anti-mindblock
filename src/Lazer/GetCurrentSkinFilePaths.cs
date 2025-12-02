using System.Linq;

namespace antiMindblock
{
    public partial class Lazer
    {
        public static string[] GetCurrentSkinFilePaths()
        {
            string[] paths = GetCurrentSkinFiles()
                .Select(x => ConstructFilePath(x.Hash ?? string.Empty))
                .ToArray();

            return paths;
        }
    }
}