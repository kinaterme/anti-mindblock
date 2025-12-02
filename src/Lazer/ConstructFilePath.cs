using System.IO;

namespace antiMindblock
{
    public partial class Lazer
    {
        public static string ConstructFilePath(string hash)
        {
            string constructedPath = Path.Combine(GetLazerPath(), "files", $"{hash[0]}", hash.Substring(0, 2), hash);
            return constructedPath;
        }
    }
}