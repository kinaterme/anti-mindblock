import subprocess
import os
import pyautogui
import time
import Universal
import cv2
import json

def parse_x_display():
    try:
        output = subprocess.check_output(['xrandr']).decode('utf-8')
        
        lines = output.split('\n')
        
        connected_monitors = []
        connected_primary_monitors = []
        for line in lines:
            if ' connected primary' in line:
                monitor_name = line.split()[0]
                connected_primary_monitors.append(monitor_name)
            elif ' connected' in line:
                monitor_name = line.split()[0]
                connected_monitors.append(monitor_name)
        
        if connected_primary_monitors:
            return connected_primary_monitors
        else:
            return connected_monitors
    except subprocess.CalledProcessError:
        print("Error: Unable to run xrandr.")
        return None

def flip_tablet(rotation):
    otd_config_location = f"/home/{os.getlogin()}/.config/OpenTabletDriver/settings.json"
    with open(otd_config_location, "r") as file:
        config = json.load(file)

    config["Profiles"][0]["AbsoluteModeSettings"]["Tablet"]["Rotation"] = rotation
    with open(otd_config_location, "w") as file:
        json.dump(config, file, indent=2)
        time.sleep(0.3)
        os.system("systemctl restart --user opentabletdriver.service")

def flip_screen(mode):
    monitors = parse_x_display()
    if os.getenv("XDG_SESSION_TYPE") == "x11":
        print(os.getenv("XDG_SESSION_TYPE"))
        for monitor in monitors:
            os.system(f"xrandr --output {monitor} --rotate {mode}") 
    elif os.getenv("XDG_SESSION_TYPE") == "wayland":
        print(os.getenv("XDG_SESSION_TYPE")) 

# stable
def fetch_current_skin():
    username = os.getlogin()
    command = f"find / -name osu!.{username}.cfg"
    cfg_path = subprocess.run(command, shell=True, text=True, stdout=subprocess.PIPE, stderr=subprocess.DEVNULL).stdout
    cfg_path = cfg_path.split('\n')[0]
    
    skin_name = None
    with open(cfg_path, "r") as file:
        lines = file.read().split('\n')
        for line in lines:
            if line.startswith("Skin"):
                skin_name = line.split(" = ")[1]
                break

    if skin_name:
        print(skin_name)
    else:
        print("Skin name not found.")
    osu_folder = os.path.dirname(cfg_path)
    skin_folder = os.path.join(osu_folder, "Skins", skin_name)
    print(skin_folder)
    return skin_folder

def get_root_stable_directory():
    username = os.getlogin()
    command = f"find / -name osu!.{username}.cfg"
    cfg_path = subprocess.run(command, shell=True, text=True, stdout=subprocess.PIPE, stderr=subprocess.DEVNULL).stdout
    cfg_path = cfg_path.split('\n')[0]
    osu_path = os.path.dirname(cfg_path)
    return osu_path

def focus_on_stable():
    os.system("xdotool search --name osu! windowactivate")
    time.sleep(1.5)
    pyautogui.keyDown("ctrl")
    pyautogui.keyDown("alt")
    pyautogui.keyDown("shift")
    pyautogui.keyDown("s")
    time.sleep(0.2)
    pyautogui.keyUp("ctrl")
    pyautogui.keyUp("alt")
    pyautogui.keyUp("shift")
    pyautogui.keyUp("s")

def flip_everything():
    flip_screen("inverted")
    Universal.flip_skin("stable", "automatic")
    flip_tablet(180.0)
    focus_on_stable()

def flip_everything_manually():
    flip_screen("inverted")
    Universal.flip_skin("stable", "manual")
    flip_tablet(180.0)
    focus_on_stable()

def revert_everything():
    flip_screen("normal")
    Universal.flip_skin("stable", "automatic")
    flip_tablet(0.0)
    focus_on_stable()

def revert_everything_manually():
    flip_screen("normal")
    Universal.flip_skin("stable", "manual")
    flip_tablet(0.0)
    focus_on_stable()

# lazer
def get_lazer_exports_folder(choice):
    command = "find / -name client.realm"
    lazer_path = subprocess.run(command, shell=True, text=True, stdout=subprocess.PIPE, stderr=subprocess.DEVNULL).stdout
    lazer_path = lazer_path.split('\n')[0]
    lazer_path = os.path.dirname(lazer_path)
    backups_path = os.path.join(lazer_path, "backups")
    exports_path = os.path.join(lazer_path, "exports")
    if choice == "exports":
        return exports_path
    elif choice == "lazer":
        return lazer_path
    elif choice == "backups":
        return backups_path

def get_lazer_appimage_path():
    command = "ps aux | grep osu"
    lazer_path = subprocess.run(command, shell=True, text=True, stdout=subprocess.PIPE, stderr=subprocess.DEVNULL).stdout
    lines = lazer_path.split("\n")
    for line in lines:
        if '.AppImage' in line:
            lazer_path = line
    lazer_path = lazer_path.split()[-1]
    return lazer_path

def export_lazer_skin():
    lazer_path = get_lazer_exports_folder("lazer")
    exports_path = get_lazer_exports_folder("exports")
    backups_path = get_lazer_exports_folder("backups")
    if not os.path.exists(backups_path):
        os.makedirs(backups_path)
    os.system(f"mv {exports_path}/*.osr {backups_path}/")
    os.system(f"rm -rf {exports_path}/*")
    os.system(f"mkdir {exports_path}/rotated")
    os.system("xdotool search --name osu! windowactivate")
    time.sleep(0.2)
    pyautogui.moveTo(10, 10, 0.1)
    pyautogui.keyDown("ctrl")
    pyautogui.keyDown("o")
    time.sleep(0.2)
    pyautogui.keyUp("ctrl")
    pyautogui.keyUp("o")
    time.sleep(0.35)
    pyautogui.write("export skin")
    time.sleep(0.35)
    x, y = pyautogui.locateCenterOnScreen("button.png", confidence=0.7)
    pyautogui.moveTo(x, y, 0.1)
    pyautogui.click()
    time.sleep(4.5)
    os.system(f"unzip -o {exports_path}/*.osk -d {exports_path}/rotated/")
    pyautogui.press("esc")
    pyautogui.press("esc")

def flip_lazer_skin():
    exports_path = get_lazer_exports_folder("exports")
    Universal.flip_skin("lazer", "automatic")
    os.system(f"zip -r {exports_path}/rotated.osk {exports_path}/rotated/*")
    os.system("xdotool search --name osu! windowactivate")
    time.sleep(1.5)
    pyautogui.keyDown("ctrl")
    pyautogui.keyDown("o")
    time.sleep(0.15)
    pyautogui.keyUp("ctrl")
    pyautogui.keyUp("o")
    time.sleep(0.15)
    pyautogui.keyDown("ctrl")
    pyautogui.keyDown("a")
    time.sleep(0.15)
    pyautogui.keyUp("ctrl")
    pyautogui.keyUp("a")
    pyautogui.write("delete skin")
    x, y = pyautogui.locateCenterOnScreen("buttondelete.png", confidence=0.7)
    time.sleep(0.15)
    pyautogui.moveTo(x, y, 0.1)
    pyautogui.click()
    time.sleep(0.35)
    x, y = pyautogui.locateCenterOnScreen("confirm.png", confidence=0.7)
    pyautogui.moveTo(x, y, 0.1)
    time.sleep(0.15)
    pyautogui.mouseDown()
    time.sleep(2.5)
    pyautogui.mouseUp()

    flip_tablet(180.0)

    appimage_path = get_lazer_appimage_path()
    print(appimage_path)
    command = f"{appimage_path} {exports_path}/rotated.osk"
    print(command)
    time.sleep(0.15)
    subprocess.run(command, shell=True)
    os.system("xdotool search --name osu! windowactivate")
    time.sleep(1)
    x, y = pyautogui.locateCenterOnScreen("buttonimported.png", confidence=0.7)
    time.sleep(0.15)
    pyautogui.moveTo(x, y, 0.1)
    pyautogui.click()
    pyautogui.press("esc")
    pyautogui.press("esc")

    flip_screen("inverted")

def revert_lazer_skin():
    exports_path = get_lazer_exports_folder("exports")
    Universal.flip_skin("lazer", "automatic")
    os.system(f"zip -r {exports_path}/rotated.osk {exports_path}/rotated/*")
    os.system("xdotool search --name osu! windowactivate")
    time.sleep(1.5)
    pyautogui.keyDown("ctrl")
    pyautogui.keyDown("o")
    time.sleep(0.15)
    pyautogui.keyUp("ctrl")
    pyautogui.keyUp("o")
    time.sleep(0.15)
    pyautogui.keyDown("ctrl")
    pyautogui.keyDown("a")
    time.sleep(0.15)
    pyautogui.keyUp("ctrl")
    pyautogui.keyUp("a")
    pyautogui.write("delete skin")
    x, y = pyautogui.locateCenterOnScreen("buttondelete.png", confidence=0.7)
    time.sleep(0.15)
    pyautogui.moveTo(x, y, 0.1)
    pyautogui.click()
    time.sleep(0.35)
    x, y = pyautogui.locateCenterOnScreen("confirm.png", confidence=0.7)
    pyautogui.moveTo(x, y, 0.1)
    time.sleep(0.15)
    pyautogui.mouseDown()
    time.sleep(2.5)
    pyautogui.mouseUp()

    flip_tablet(0.0)

    appimage_path = get_lazer_appimage_path()
    print(appimage_path)
    command = f"{appimage_path} {exports_path}/rotated.osk"
    print(command)
    time.sleep(0.15)
    subprocess.run(command, shell=True)
    os.system("xdotool search --name osu! windowactivate")
    time.sleep(1)
    x, y = pyautogui.locateCenterOnScreen("buttonimported.png", confidence=0.7)
    time.sleep(0.15)
    pyautogui.moveTo(x, y, 0.1)
    pyautogui.click()
    pyautogui.press("esc")
    pyautogui.press("esc")

    flip_screen("normal")