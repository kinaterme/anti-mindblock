# Anti Mindblock tool

I remade Shikkesora's osu! Anti Mindblock tool for Linux.

## UPDATE
I will be rewriting this tool in C# for Windows, Mac and Linux with support for both osu!stable and osu!lazer

## Known issues
- All monitors flip on multi-monitor setups
- Bad UI

You might need to download dependencies in order for this program to work (search how to download them for your distro):
- tkinter
- pynput
- xorg-xrandr

Installation:
- Download the .zip file from Releases
- Unzip the archive
- Run Anti_Mindblock/main.dist/main.bin

Implementation:
- Create a new .desktop file
  
  ![image](https://github.com/kinaterme/anti-mindblock/assets/61877280/421b45ce-a7d2-4906-847a-4cacbf360a2e)
- Open it in your text editor of choice, paste this text and edit the path in "Exec" to match yours:
  ```
  [Desktop Entry]
  Type=Application
  Name=Anti Mindblock
  Comment=Tool for removing mindblock in osu!
  Exec=/home/jakub/Documents/github/anti-mindblock/main.dist/main.bin
  Categories=Games;
  ```
- After saving, make sure to make the file executable

  ![image](https://github.com/kinaterme/anti-mindblock/assets/61877280/389fac36-2327-4bf8-b65d-35d68c971a34)
- Open a terminal in the same location
- Move the .desktop file to /usr/share/applications/
  ```
  sudo mv AntiMindblock.desktop /usr/share/applications/
  ```
- Anti Mindblock should now be available as an entry

  ![image](https://github.com/kinaterme/anti-mindblock/assets/61877280/8e9e838c-d54d-424e-840d-63f55ba91224)


To select your skin, select your skin folder.
Example: /home/USER/.local/share/osu-wine/osu!/Skins/RafisHDDT

![image](https://github.com/kinaterme/anti-mindblock/assets/61877280/a98783d9-00ac-491f-9438-8141d0d9feff)

