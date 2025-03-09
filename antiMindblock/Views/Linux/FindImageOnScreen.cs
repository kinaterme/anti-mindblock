using SkiaSharp;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace antiMindblock.LinuxOS
{
    public class linuxFindImageOnScreen
    {
        public static System.Drawing.Point? FindImageOnScreen(string imagePath, double confidence)
        {
            linuxRunShellCommand.RunShellCommand("scrot /tmp/screenshot.png");

            using var screenshot = new Mat("/tmp/screenshot.png", ImreadModes.Grayscale);
            using var template = new Mat(imagePath, ImreadModes.Grayscale);

            using var result = new Mat();
            Cv2.MatchTemplate(screenshot, template, result, TemplateMatchModes.CCoeffNormed);

            Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

            if (maxVal >= confidence)
            {
                return new System.Drawing.Point(maxLoc.X + template.Width / 2, maxLoc.Y + template.Height / 2);
            }

            return null;
        }
    }
}