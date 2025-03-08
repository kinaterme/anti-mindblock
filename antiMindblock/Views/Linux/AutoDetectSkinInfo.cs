using System;
using antiMindblock.Views;

namespace antiMindblock.LinuxOS
{
    public class linuxAutoDetectSkinInfo
    {
        private MainWindow _mainWindow;
        private linuxSkinFetcher _linuxSkinFetcher;
        public linuxAutoDetectSkinInfo(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _linuxSkinFetcher = new linuxSkinFetcher(mainWindow);
        }
        public void AutoDetectSkinInfo()
        {
            _linuxSkinFetcher.SkinFetcher();
            Console.WriteLine(_mainWindow.skinName);
        }
    }
}