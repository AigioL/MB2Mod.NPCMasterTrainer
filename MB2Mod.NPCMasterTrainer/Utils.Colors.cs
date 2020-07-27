using System;
using TaleWorlds.Library;
using SDColor = System.Drawing.Color;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class Colors
        {
            private static readonly Lazy<Color> lazy_OrangeRed = new Lazy<Color>(() => SDColor.OrangeRed.GetColor());

            public static Color OrangeRed => lazy_OrangeRed.Value;

            private static readonly Lazy<Color> lazy_BlueViolet = new Lazy<Color>(() => SDColor.BlueViolet.GetColor());

            public static Color BlueViolet => lazy_BlueViolet.Value;
        }

        public static Color GetColor(this SDColor color) => new Color(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
    }
}