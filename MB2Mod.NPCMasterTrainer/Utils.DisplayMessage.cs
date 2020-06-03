﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static void DisplayMessage(string information, Color? color = null)
        {
            var infoMessage = color.HasValue ? new InformationMessage(information, color.Value) : new InformationMessage(information);
            InformationManager.DisplayMessage(infoMessage);
            if (Utils.Config.Instance.HasWin32Console())
            {
                Console.WriteLine(information);
                // not impl win api SetConsoleMode set custom colors
            }
        }

        public static void DisplayMessage(Exception e) => DisplayMessage(e.ToString(), Colors.OrangeRed);

        public static void DisplayMessage(IEnumerable<string> strings, Color? color = null, string separator = " ")
        {
            var information = string.Join(separator, strings);
            DisplayMessage(information, color);
        }
    }
}