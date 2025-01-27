# Anti Mindblock
A tool made for avoiding mindblock by flipping your screen and skin automatically. <br>Made for Linux, Windows and _macOS (eventually)_.
**osu!lazer support included!**

### NOTE: No support for Windows and macOS yet (use [Shikkesora's tool](https://github.com/ShikkesoraSIM/anti-mindblock))

This is still in development, osu!stable works perfectly fine when using the [Winello](https://github.com/NelloKudo/osu-winello) script and osu!lazer works if you wait patiently.

![image](https://github.com/user-attachments/assets/bfb64c52-b073-4f0f-bc67-2ca6ff5c8637)

* [How to build](https://github.com/kinaterme/osuToolbox?tab=readme-ov-file#how-to-build)
* [MissAnalyzer on macOS](https://github.com/kinaterme/osuToolbox?tab=readme-ov-file#how-to-use-missanalyzer-on-macos)

## How to build
### Windows:<br/>

1. Install .NET 8 https://dotnet.microsoft.com/en-us/download/dotnet/8.0
  
2. Build the toolbox
```
git clone https://github.com/kinaterme/anti-mindblock
cd anti-mindblock/antiMindblock.Desktop
dotnet build
dotnet publish -r win-x64 -c Release
cd bin/Release/net8.0/win-x64/publish
explorer .
```
3. Run antiMindblock.Desktop.exe

### macOS (Apple Silicon):<br/>

1. Install .NET 8 https://dotnet.microsoft.com/en-us/download/dotnet/8.0

2. Build the toolbox and run it
```
git clone https://github.com/kinaterme/anti-mindblock
cd anti-mindblock/antiMindblock.Desktop
dotnet build
dotnet publish -r osx-arm64 -c Release
cd bin/Release/net8.0/osx-arm64/publish
./antiMindblock.Desktop
```

### macOS (Intel):<br/>

1. Install .NET 8 https://dotnet.microsoft.com/en-us/download/dotnet/8.0

2. Build the toolbox and run it
```
git clone https://github.com/kinaterme/anti-mindblock
cd anti-mindblock/antiMindblock.Desktop
dotnet build
dotnet publish -r osx-x64 -c Release
cd bin/Release/net8.0/osx-x64/publish
./antiMindblock.Desktop
```
### Linux:<br/>

1. Install .NET 8 https://dotnet.microsoft.com/en-us/download/dotnet/8.0 (or use a package manager)

2. Build the toolbox and run it
```
git clone https://github.com/kinaterme/anti-mindblock
cd anti-mindblock/antiMindblock.Desktop
dotnet build
dotnet publish -r linux-x64 -c Release
cd bin/Release/net8.0/linux-x64/publish
./antiMindblock.Desktop
```
