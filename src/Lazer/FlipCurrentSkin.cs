namespace antiMindblock
{
    public partial class Lazer
    {
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
