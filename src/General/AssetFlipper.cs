using System;
using System.IO;
using SkiaSharp;

namespace antiMindblock
{
    public class AssetFlipper
    {
        public static void Flip(string path)
        {
            using var bitmap = SKBitmap.Decode(path);

            using var rotated = new SKBitmap(bitmap.Width, bitmap.Height);

            using (var canvas = new SKCanvas(rotated))
            {
                canvas.Translate(bitmap.Width / 2f, bitmap.Height / 2f);
                canvas.RotateDegrees(180);
                
                canvas.Translate(-bitmap.Width / 2f, -bitmap.Height / 2f);
                canvas.DrawBitmap(bitmap, 0, 0);
            }

            using var output = new FileStream(path, FileMode.Create, FileAccess.Write);
            rotated.Encode(output, SKEncodedImageFormat.Png, 100);
        }
    }
}
