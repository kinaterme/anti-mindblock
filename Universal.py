from PIL import Image
import subprocess
import os
import Linux
import __main__
import platform

skin_folder = None
platform = platform.system()

def flip_skin(choice, mode):
    if choice == "stable":
        if platform == "Linux":
            if mode == "automatic":
                skin_folder = Linux.fetch_current_skin()
            if mode == "manual":
                skin_folder = __main__.detected_skin_path
    if choice == "lazer":
        if platform == "Linux":
            skin_folder = os.path.join(Linux.get_lazer_exports_folder("exports"), "rotated")
    # hitcircle rotation
    hitcircle = Image.open(skin_folder + "/hitcircle.png")
    rotated_hitcircle = hitcircle.rotate(180)
    rotated_hitcircle.save(skin_folder + "/hitcircle.png")
    # number0 rotation
    number0 = Image.open(skin_folder + "/default-0.png")
    rotated_number0 = number0.rotate(180)
    rotated_number0.save(skin_folder + "/default-0.png")
    # number1 rotation
    number1 = Image.open(skin_folder + "/default-1.png")
    rotated_number1 = number1.rotate(180)
    rotated_number1.save(skin_folder + "/default-1.png")
    # number2 rotation
    number2 = Image.open(skin_folder + "/default-2.png")
    rotated_number2 = number2.rotate(180)
    rotated_number2.save(skin_folder + "/default-2.png")
    # number3 rotation
    number3 = Image.open(skin_folder + "/default-3.png")
    rotated_number3 = number3.rotate(180)
    rotated_number3.save(skin_folder + "/default-3.png")
    # number4 rotation
    number4 = Image.open(skin_folder + "/default-4.png")
    rotated_number4 = number4.rotate(180)
    rotated_number4.save(skin_folder + "/default-4.png")
    # number5 rotation
    number5 = Image.open(skin_folder + "/default-5.png")
    rotated_number5 = number5.rotate(180)
    rotated_number5.save(skin_folder + "/default-5.png")
    # number6 rotation
    number6 = Image.open(skin_folder + "/default-6.png")
    rotated_number6 = number6.rotate(180)
    rotated_number6.save(skin_folder + "/default-6.png")
    # number7 rotation
    number7 = Image.open(skin_folder + "/default-7.png")
    rotated_number7 = number7.rotate(180)
    rotated_number7.save(skin_folder + "/default-7.png")
    # number8 rotation
    number8 = Image.open(skin_folder + "/default-8.png")
    rotated_number8 = number8.rotate(180)
    rotated_number8.save(skin_folder + "/default-8.png")
    # number9 rotation
    number9 = Image.open(skin_folder + "/default-9.png")
    rotated_number9 = number9.rotate(180)
    rotated_number9.save(skin_folder + "/default-9.png")
    # hitcircleoverlay rotation
    hitcircleoverlay = Image.open(skin_folder + "/hitcircleoverlay.png")
    rotated_hitcircleoverlay = hitcircleoverlay.rotate(180)
    rotated_hitcircleoverlay.save(skin_folder + "/hitcircleoverlay.png")