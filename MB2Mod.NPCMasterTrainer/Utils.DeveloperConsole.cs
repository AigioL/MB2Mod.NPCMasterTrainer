using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // 命名样式

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class DeveloperConsole
        {
            [DllImport("Rgl.dll", EntryPoint = "?toggle_imgui_console_visibility@rglCommand_line_manager@@QEAAXXZ", CallingConvention = CallingConvention.Cdecl)]
            public static extern void toggle_imgui_console_visibility(UIntPtr x);

            static readonly Lazy<bool> lazy_Exists = new Lazy<bool>(() => CurrentAppDomain.Exists("DeveloperConsole"));

            public static bool Exists => lazy_Exists.Value;
        }

        partial class Config
        {
            public bool EnableDevConsole()
            {
                return EnableDeveloperConsole && !DeveloperConsole.Exists;
            }
        }
    }
}
