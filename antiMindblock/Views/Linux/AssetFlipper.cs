using System;
using System.IO;
using SkiaSharp;

namespace antiMindblock.LinuxOS
{
    public class linuxAssetFlipper
    {
        public void AssetFlipper(string imagePathAr) 
        {
            try
            {
                // Create the full path to the image
                string imagePath = imagePathAr;

                using (var input = File.OpenRead(imagePath))
                using (var originalBitmap = SKBitmap.Decode(input))
                {
                    // Create a new bitmap with the same dimensions
                    var rotatedBitmap = new SKBitmap(originalBitmap.Width, originalBitmap.Height);

                    // Rotation
                    using (var canvas = new SKCanvas(rotatedBitmap))
                    {
                        canvas.Clear(SKColors.Transparent);
                        canvas.Translate(originalBitmap.Width / 2, originalBitmap.Height / 2);
                        canvas.RotateDegrees(180);
                        canvas.Translate(-originalBitmap.Width / 2, -originalBitmap.Height / 2);
                        canvas.DrawBitmap(originalBitmap, SKRect.Create(originalBitmap.Width, originalBitmap.Height));
                    }

                    // Save the rotated image
                    string rotatedImagePath = imagePathAr;
                    using (var output = File.OpenWrite(rotatedImagePath))
                    {
                        rotatedBitmap.Encode(output, SKEncodedImageFormat.Png, 100);
                    }

                    Console.WriteLine($"Rotated image saved to: {rotatedImagePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error rotating image: {ex.Message}");
            }
        }
    }
}