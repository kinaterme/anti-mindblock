using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace antiMindblock;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Settings.Initialize();
    }

    public void BTN_GetCurrentLazerSkinID(object sender, RoutedEventArgs args)
    {
        Guid currentSkinID = Lazer.GetCurrentSkinID();
        Console.WriteLine(currentSkinID);
    }

    public void BTN_GetLazerPath(object sender, RoutedEventArgs args)
    {
        string lazerPath = Lazer.GetLazerPath();
        Console.WriteLine(lazerPath);
    }
    
    public void BTN_GetLazerSkins(object sender, RoutedEventArgs args)
    {
        List<(string SkinName, Guid SkinID, string Filename, string? Hash)> skins = Lazer.GetLazerSkins();
        foreach (var skin in skins)
            Console.WriteLine($"{skin.SkinName} ({skin.SkinID}) / {skin.Filename} / {skin.Hash}");
    }

    public void BTN_GetCurrentLazerSkinContents(object sender, RoutedEventArgs args)
    {
        List<(string Filename, string? Hash)> currentSkin = Lazer.GetCurrentSkinFiles();
        
        foreach (var file in currentSkin)
            Console.WriteLine($"{file.Filename} / {file.Hash}");
    }

    public void BTN_FlipAsset(object sender, RoutedEventArgs args)
    {
        if (System.IO.File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "image.png")))
            AssetFlipper.Flip(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "image.png"));
        if (System.IO.File.Exists("image.png"))
            AssetFlipper.Flip("image.png");
    }

    public void BTN_FlipScreen(object sender, RoutedEventArgs args)
    {
        Screen.Flip(true);
    }

    public void BTN_RevertScreen(object sender, RoutedEventArgs args)
    {
        Screen.Flip(false);
    }

    public void BTN_PrintCurrentLazerSkinFilePaths(object sender, RoutedEventArgs args)
    {
        string[] paths = Lazer.GetCurrentSkinFilePaths();
        foreach (string path in paths)
            Console.WriteLine(path);
    }

    public void BTN_FlipCurrentLazerSkin(object sender, RoutedEventArgs args)
    {
        Lazer.FlipCurrentSkin();
    }

    public void BTN_LazerFlipEverything(object sender, RoutedEventArgs args)
    {
        Screen.Flip(true);
        Lazer.FlipCurrentSkin();
        Input.FlipTablet();
        Lazer.ReloadSkin();
        //Screen.FocusOsuWindow();
    }

    public void BTN_LazerRevertEverything(object sender, RoutedEventArgs args)
    {
        Screen.Flip(false);
        Lazer.FlipCurrentSkin();
        Input.FlipTablet();
        Lazer.ReloadSkin();
        //Screen.FocusOsuWindow();
    }

    public void BTN_Test(object sender, RoutedEventArgs args)
    {
        //Screen.FocusOsuWindow();
        //Input.FlipTablet();
        Lazer.ReloadSkin();
        Console.WriteLine($"Lazer path is {Lazer.GetLazerPath()}");
        Console.WriteLine($"Lazer path is {Settings.OsuLazerPath} (settings)");
    }
}