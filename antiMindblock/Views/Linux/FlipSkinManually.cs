using System.IO;
using antiMindblock.Views;

namespace antiMindblock.LinuxOS
{
    public class linuxFlipSkinManually
    {
        private MainWindow _mainWindow;
        public linuxFlipSkinManually(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        linuxAssetFlipper linuxAssetFlipper = new linuxAssetFlipper();
        
        public void FlipSkinManually()
        {
            string quotedPath = $"\"{_mainWindow.selectedFolderPath}\"";
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-1.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-1@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-2.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-2@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-3.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-3@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-4.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-4@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-5.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-5@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-6.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-6@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-7.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-7@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-8.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-8@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-9.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-9@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-0.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "default-0@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "cursor.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "cursortrail.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "cursortail.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit0.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit0@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit50.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit50@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit50-0.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit50-0@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100-0.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100-0@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100k.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100k@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100k-0.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hit100k-0@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hitcircle.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hitcircle@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hitcircleoverlay.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "hitcircleoverlay@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "reversearrow.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "reversearrow@2x.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "sliderfollowcircle.png"));
            linuxAssetFlipper.AssetFlipper(Path.Combine(quotedPath.Trim('"'), "sliderfollowcircle@2x.png"));
        }
    }
}