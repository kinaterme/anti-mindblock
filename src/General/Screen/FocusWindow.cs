using System.Runtime.InteropServices;
using System;

namespace antiMindblock
{
    public partial class Screen
    {   
        public static void FocusOsuWindow()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SetWindowsFocus(GetHWND());
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                switch (Environment.GetEnvironmentVariable("XDG_SESSION_TYPE"))
                {
                    case "x11":
                        Console.WriteLine("Focusing osu! window (x11)");
                        break;
                    case "wayland":
                        Console.WriteLine("Focusing osu! window (wayland)");
                        break;
                }
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        static IntPtr GetHWND()
        {
            IntPtr hwnd = FindWindow(null, "osu!");
            return hwnd;   
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        static void SetWindowsFocus(IntPtr hwnd)
        {
            SetForegroundWindow(hwnd);
        }
    }
}