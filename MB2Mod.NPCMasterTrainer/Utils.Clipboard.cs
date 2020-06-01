using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        /// <summary>
        /// https://github.com/CopyText/TextCopy/blob/4.2.0/src/TextCopy/WindowsClipboard.cs
        /// </summary>
        public static class Clipboard
        {
            public static string GetTextOrEmpty()
            {
                if (IsWindows)
                {
                    return GetText() ?? string.Empty;
                }
                return string.Empty;
            }

            static string GetText()
            {
                if (!IsClipboardFormatAvailable(cfUnicodeText))
                {
                    return null;
                }
                TryOpenClipboard();

                return InnerGet();
            }

            static string InnerGet()
            {
                IntPtr handle = default;

                IntPtr pointer = default;
                try
                {
                    handle = GetClipboardData(cfUnicodeText);
                    if (handle == default)
                    {
                        return null;
                    }

                    pointer = GlobalLock(handle);
                    if (pointer == default)
                    {
                        return null;
                    }

                    var size = GlobalSize(handle);
                    var buff = new byte[size];

                    Marshal.Copy(pointer, buff, 0, size);

                    return Encoding.Unicode.GetString(buff).TrimEnd('\0');
                }
                finally
                {
                    if (pointer != default)
                    {
                        GlobalUnlock(handle);
                    }

                    CloseClipboard();
                }
            }

            static void TryOpenClipboard()
            {
                var num = 10;
                while (true)
                {
                    if (OpenClipboard(default))
                    {
                        break;
                    }

                    if (--num == 0)
                    {
                        ThrowWin32();
                    }

                    Thread.Sleep(100);
                }
            }

            const uint cfUnicodeText = 13;

            static void ThrowWin32()
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool IsClipboardFormatAvailable(uint format);

            [DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr GetClipboardData(uint uFormat);

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern IntPtr GlobalLock(IntPtr hMem);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool OpenClipboard(IntPtr hWndNewOwner);

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern int GlobalSize(IntPtr hMem);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool GlobalUnlock(IntPtr hMem);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool CloseClipboard();
        }
    }
}
