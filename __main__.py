from tkinter import *
from tkinter import ttk, messagebox, Listbox
import Linux
import Universal
import os
import platform

platform = platform.system()

root = Tk()
#frame = ttk.Frame(root, padding=10)
#frame.grid()

style = ttk.Style(root)
root.tk.call("source", "forest-dark.tcl")
style.theme_use("forest-dark")

tabControl = ttk.Notebook(root)
tab_automatic = ttk.Frame(tabControl)
tab_manual = ttk.Frame(tabControl)
tab_misc = ttk.Frame(tabControl)
tab_lazer = ttk.Frame(tabControl)
tabControl.add(tab_automatic, text="Automatic")
tabControl.add(tab_manual, text="Manual")
tabControl.add(tab_misc, text="Misc")
tabControl.add(tab_lazer, text="Lazer")
tabControl.pack(expand=1, fill="both")

def linux_fetch_current_skin():
    skin_folder = Linux.fetch_current_skin()
    skin_name = skin_folder.split("/")[-1]
    messagebox.showinfo("Skin detected", f"{skin_name}")

if platform == "Linux":
    ttk.Button(tab_automatic, text="Fetch current skin", command=lambda: linux_fetch_current_skin()).grid(column=0, row=0, padx=50, pady=50)
    ttk.Button(tab_automatic, text="Flip everything", command=lambda: Linux.flip_everything()).grid(column=0, row=1)
    ttk.Button(tab_automatic, text="Revert everything", command=lambda: Linux.revert_everything()).grid(column=0, row=2, pady=10)
    ttk.Button(tab_misc, text="Flip screen", command=lambda: Linux.flip_screen("inverted")).grid(column=0, row=0, padx=5)
    ttk.Button(tab_misc, text="Unflip screen", command=lambda: Linux.flip_screen("normal")).grid(column=0, row=1, pady=10, padx=5)
    ttk.Button(tab_misc, text="Fetch current skin", command=lambda: linux_fetch_current_skin()).grid(column=1, row=0, padx=5)
    ttk.Button(tab_misc, text="Focus osu! window", command=lambda: Linux.focus_on_stable()).grid(column=1, row=1, pady=10, padx=5)
    ttk.Button(tab_misc, text="Flip tablet", command=lambda: Linux.flip_tablet(180.0)).grid(column=2, row=0, padx=5)
    ttk.Button(tab_misc, text="Revert tablet", command=lambda: Linux.flip_tablet(0.0)).grid(column=2, row=1, pady=10, padx=5)
    ttk.Button(tab_lazer, text="Export Lazer skin", command=lambda: Linux.export_lazer_skin()).grid(column=0, row=0, padx=50, pady=50)
    ttk.Button(tab_lazer, text="Flip Lazer skin", command=lambda: Linux.flip_lazer_skin()).grid(column=0, row=1)
    ttk.Button(tab_lazer, text="Revert Lazer skin", command=lambda: Linux.revert_lazer_skin()).grid(column=0, row=2, pady=10) 

ttk.Button(tab_misc, text="Flip current skin", command=lambda: Universal.flip_skin("stable")).grid(column=4, row=0)
skins_list = Listbox(tab_manual, width=60, height=10)
skins_list.pack(side="left", fill="both", expand=True, padx=25, pady=25)

selected_skin_label = ttk.Label(tab_manual, text="No skin is selected!")
selected_skin_label.pack(pady=5)

def select_skin():
    global detected_skin_path, last_method_used
    selected_skin = skins_list.get(skins_list.curselection())
    detected_skin_path = os.path.join(Linux.get_root_stable_directory(), "Skins", selected_skin)
    selected_skin_label.config(text=f"{selected_skin} skin is selected!")
    print(f"Selected skin path: {detected_skin_path}")
    #last_method_used = 'manual'

def update_skins_list():
    skins_path = f"{Linux.get_root_stable_directory()}/Skins"
    if os.path.exists(skins_path):
        skins = [name for name in os.listdir(skins_path) if os.path.isdir(os.path.join(skins_path, name))]
        skins.sort() 
        skins_list.delete(0, "end")
        for skin in skins:
            skins_list.insert("end", skin)
    else:
        print(f"Skins directory not found: {skins_path}")

select_skin_button = ttk.Button(tab_manual, text="Select", command=select_skin, style="Accent.TButton")
select_skin_button.pack(pady=5)
update_skins_list()

ttk.Button(tab_manual, text="Refresh", command=lambda: update_skins_list()).pack()
ttk.Button(tab_manual, text="Flip everything", command=lambda: Linux.flip_everything_manually()).pack()
ttk.Button(tab_manual, text="Revert everything", command=lambda: Linux.revert_everything_manually()).pack()


root.mainloop()