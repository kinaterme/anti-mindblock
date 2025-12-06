using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

namespace antiMindblock;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Settings.Initialize();
        LoadSettings();
    }

    private void LoadSettings()
    {
        TB_LazerPath.Text = Lazer.GetLazerPath();
        switch (Settings.OsuLazerReloadMode)
        {
            case "Restart":
                CB_LazerReloadMode.SelectedIndex = 0;
                break;
            case "RestartDesktop":
                CB_LazerReloadMode.SelectedIndex = 1;
                break;
            case "ImageRecognition":
                CB_LazerReloadMode.SelectedIndex = 2;
                break; 
        }
        TB_LazerDesktopPath.Text = Settings.OsuLazerDesktopFilePath;
    }

    public void BTN_ApplySettings(object sender, RoutedEventArgs args)
    {
        switch (CB_LazerReloadMode.SelectedIndex)
        {
            case 0:
                Settings.OsuLazerReloadMode = "Restart";
                break;
            case 1:
                Settings.OsuLazerReloadMode = "RestartDesktop";
                break;
            case 2:
                Settings.OsuLazerReloadMode = "ImageRecognition";
                break;
        }
        Settings.WriteSettingsFile();
    }

    public void BTN_RestoreDefaultSettings(object sender, RoutedEventArgs args)
    {
        Settings.RestoreDefaultSettings();
        LoadSettings();
    }

    public async void BTN_PickLazerPath(object sender, RoutedEventArgs args)
    {
        string lazerPath = await OpenFolder();
        Settings.OsuLazerPath = lazerPath;
        TB_LazerPath.Text = lazerPath;
    }

    public async void BTN_PickLazerDesktopPath(object sender, RoutedEventArgs args)
    {
        string lazerDesktopPath = await OpenFolder();
        Settings.OsuLazerDesktopFilePath = lazerDesktopPath;
        TB_LazerDesktopPath.Text = lazerDesktopPath;
    }
    
    private async Task<string> OpenFolder()
    {
        var folders = await this.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = false
        });

        if (folders.Count > 0)
        {
            return folders[0].Path.LocalPath;
        }
        return "";
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

    public void BTN_FlipScreen(object sender, RoutedEventArgs args)
    {
        Screen.Flip(true);
    }

    public void BTN_RevertScreen(object sender, RoutedEventArgs args)
    {
        Screen.Flip(false);
    }

    // This also reverts the tablet area back to normal if it's already flipped.
    public void BTN_FlipTablet(object sender, RoutedEventArgs args)
    {
        Input.FlipTablet();
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

    public void BTN_ReloadLazerSkin(object sender, RoutedEventArgs args)
    {
        Lazer.ReloadSkin();
    }
    public void BTN_Test(object sender, RoutedEventArgs args)
    {
        //Screen.FocusOsuWindow();
        //Input.FlipTablet();
        //Lazer.ReloadSkin();
        //Console.WriteLine($"Lazer path is {Lazer.GetLazerPath()}");
        //Console.WriteLine($"Lazer path is {Settings.OsuLazerPath} (settings)");
    }
}