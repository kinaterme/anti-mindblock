using System;
using System.IO;
using System.Runtime.InteropServices;
using Realms;

namespace antiMindblock
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. stable");
            Console.WriteLine("2. lazer");
            Console.Write("Choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("not implemented yet.");
                    break;
                case 2:
                    string lazerPath = Lazer.GetLazerPath();
                    Guid currentSkinID = Lazer.GetCurrentSkin();

                    List<(string SkinName, Guid SkinID, string Filename, string? Hash)> skins = Lazer.GetLazerSkins();
                    List<(string Filename, string? Hash)> currentSkin = new List<(string Filename, string? Hash)>();

                    foreach (var skin in skins)
                    {
                        if (skin.SkinID == currentSkinID)
                            currentSkin.Add((skin.Filename, skin.Hash));
                    }
                    foreach (var file in currentSkin)
                        Console.WriteLine($"{file.Filename} | {file.Hash}");

                    Console.WriteLine("1. 0deg");
                    Console.WriteLine("2. 180deg");
                    Console.Write("Choice: ");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            // revert everything
                            break;
                        case 2:
                            // flip everything
                            break;
                    }


                    break;
            }


            //AssetFlipper.Flip("/home/user/Downloads/image.png");
        }
    }
}
