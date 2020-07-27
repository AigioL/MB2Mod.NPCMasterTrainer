using System;
using System.Runtime.InteropServices;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class Win32Console
        {
            [DllImport("kernel32.dll")]
            private static extern bool AllocConsole();

            [DllImport("kernel32.dll")]
            private static extern bool FreeConsole();

            [DllImport("kernel32.dll")]
            private static extern IntPtr GetConsoleWindow();

            private static bool HasConsole => GetConsoleWindow() != IntPtr.Zero;

            public static void Show(string title = null)
            {
                if (IsWindows)
                {
                    if (!HasConsole)
                    {
                        AllocConsole();
                    }
                    if (title != null)
                    {
                        Console.Title = title;
                    }
                }
            }

            public static void Hide()
            {
                if (IsWindows)
                {
                    if (HasConsole)
                    {
                        FreeConsole();
                    }
                }
            }
        }

        partial class Config
        {
            public bool HasWin32Console()
            {
                return IsDevelopment || ShowWin32Console;
            }
        }
    }
}