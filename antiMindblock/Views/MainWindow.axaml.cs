using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SkiaSharp;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing;
using System.Drawing.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Linq.Expressions;
using Avalonia.Controls.Documents;
using System.Transactions;
using antiMindblock.LinuxOS;

namespace antiMindblock.Views;

public partial class MainWindow : Avalonia.Controls.Window
{

    public async Task ShowMessageBoxAsync(string program)
    {
        var box = MessageBoxManager
                    .GetMessageBoxStandard("Caution", $"{program} is not installed, this program will not work unless you install it.",
                        ButtonEnum.Ok);

                var result = await box.ShowAsync();
    }

    public string skinName;
    public MainWindow()
    {
        InitializeComponent();
        
        Width = 550;
        Height = 300;

        linuxInit linuxinit = new linuxInit(this);
        linuxinit.Init();
    }
    
    public void Flipping_Click(object sender, RoutedEventArgs args)
    {
        if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxFlipping.Flipping();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void Unflipping_Click(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxUnflipping.Unflipping();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    
    public void GetAndEditSkinFolder(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxGetAndEditSkinFolder linuxGetAndEditSkinFolder = new linuxGetAndEditSkinFolder(this);
            linuxGetAndEditSkinFolder.GetAndEditSkinFolder();    
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void DoAll_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        FlipTabletArea(sender, args);
        Flipping_Click(sender, args);
        GetAndEditSkinFolder(sender, args);
        FocusAndRefresh(sender, args);
    }
    public void UndoAll_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        UnflipTabletArea(sender, args);
        Unflipping_Click(sender, args);
        GetAndEditSkinFolder(sender, args);
        FocusAndRefresh(sender, args);
    }

    public void FocusAndRefresh(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxFocusAndRefresh.FocusAndRefresh();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    linuxFlipTabletArea linuxFlipTabletArea = new linuxFlipTabletArea();
    public void FlipTabletArea(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxFlipTabletArea.FlipTabletArea(180.0);
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void UnflipTabletArea(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxFlipTabletArea.FlipTabletArea(0.0);
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return; 
    }

    public string selectedFolderPath;

    
    private async void FolderPickerButton_Click(object sender, RoutedEventArgs e)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxFolderPickerButton linuxFolderPickerButton = new linuxFolderPickerButton(this);
            linuxFolderPickerButton.FolderPickerButton();
        }
    }
    
    public void FlipSkinManually(object sender, RoutedEventArgs args)
    {
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxFlipSkinManually linuxFlipSkinManually = new linuxFlipSkinManually(this);
            linuxFlipSkinManually.FlipSkinManually();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }

    public void AutoDetectSkinInfo(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            linuxAutoDetectSkinInfo linuxAutoDetectSkinInfo = new linuxAutoDetectSkinInfo(this);
            linuxAutoDetectSkinInfo.AutoDetectSkinInfo();
        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){

        }
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){

        }
        
    }

    public void DoAllManual_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        FlipTabletArea(sender, args);
        Flipping_Click(sender, args);
        FlipSkinManually(sender, args);
        FocusAndRefresh(sender, args);
    }
    public void UndoAllManual_Click(object sender, RoutedEventArgs args)
    {
        // Cross-platform solution is already provided in each method,
        // don't add it here.
        UnflipTabletArea(sender, args);
        Unflipping_Click(sender, args);
        FlipSkinManually(sender, args);
        FocusAndRefresh(sender, args);
    }

    public void DeleteLazerSkin()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxDeleteLazerSkin.DeleteLazerSkin();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }
    
    linuxEditLazerSkin linuxEditLazerSkin = new linuxEditLazerSkin();
    public void EditLazerSkin()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxEditLazerSkin.EditLazerSkin();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }
    
    linuxUndoEditsLazerSkin linuxUndoEditsLazerSkin = new linuxUndoEditsLazerSkin();
    public void UndoEditsLazerSkin()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxUndoEditsLazerSkin.UndoEditsLazerSkin();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }
    
    linuxExportLazerSkin linuxExportLazerSkin = new linuxExportLazerSkin();
    public void ExportLazer_Click(object sender, RoutedEventArgs args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            linuxExportLazerSkin.ExportLazerSkin();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;
    }
    public void EditLazer_Click(object sender, RoutedEventArgs args)
    {
       EditLazerSkin();
       FlipTabletArea(sender, args);
       Flipping_Click(sender, args);
    }
    public void UndoLazer_Click(object sender, RoutedEventArgs args)
    {
        UnflipTabletArea(sender, args);
        UndoEditsLazerSkin();
        Unflipping_Click(sender, args);
    }
}