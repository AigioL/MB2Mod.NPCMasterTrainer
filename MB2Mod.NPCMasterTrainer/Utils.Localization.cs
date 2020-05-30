using System;
using System.Collections.Generic;
using System.Globalization;
using TaleWorlds.Localization;
using MB2Mod.NPCMasterTrainer.Properties;
using System.Reflection;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class Localization
        {
            const string _currentLanguageId = "_currentLanguageId";

            public const string English = "en";

            public const string SimplifiedChinese = "zh-Hans";

            public const string TraditionalChinese = "zh-Hant";

            public static void SetLanguageByGame()
            {
                try
                {
                    var currentLanguageId = GetCurrentLanguageIdByMBTextManager();
                    if (currentLanguageId != null && mapping_language_id__culture_name.TryGetValue(currentLanguageId, out var value))
                    {
                        Resources.Language = value;
                    }
                }
                catch
                {
                }
            }

            public static string GetCurrentLanguageIdByMBTextManager()
            {
                var typeMBTextManager = typeof(MBTextManager);
                return typeMBTextManager.GetField(_currentLanguageId, BindingFlags.NonPublic | BindingFlags.Static).GetValue(null)?.ToString();
            }

            public static void Print(CultureInfo cultureInfo, string tag)
            {
                if (cultureInfo != null)
                {
                    Console.WriteLine($"{tag}: {cultureInfo.Name}");
                    Console.WriteLine($"    EnglishName: {cultureInfo.EnglishName}");
                    Console.WriteLine($"    NativeName: {cultureInfo.NativeName}");
                    Console.WriteLine($"    DisplayName: {cultureInfo.DisplayName}");
                    Console.WriteLine($"    IetfLanguageTag: {cultureInfo.IetfLanguageTag}");
                    Console.WriteLine($"    TwoLetterISOLanguageName: {cultureInfo.TwoLetterISOLanguageName}");
                    Console.WriteLine($"    ThreeLetterISOLanguageName: {cultureInfo.ThreeLetterISOLanguageName}");
                    Console.WriteLine($"    ThreeLetterWindowsLanguageName: {cultureInfo.ThreeLetterWindowsLanguageName}");
                    Console.WriteLine($"    ToString: {cultureInfo}");
                }
            }

            static readonly IReadOnlyDictionary<string, string> mapping_language_id__culture_name = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "English", English },
                { "简体中文", SimplifiedChinese },
                { "繁体中文", TraditionalChinese },
                //{ "Türkçe", "" },
                //{ "German", "" },
            };

            public static void SetLanguage(CultureInfo culture)
            {
                if (culture.Match(SimplifiedChinese)) Resources.Language = SimplifiedChinese;
                else if (culture.Match(TraditionalChinese)) Resources.Language = TraditionalChinese;
            }
        }

        static bool Match(this CultureInfo culture, string name)
        {
            int i = 0;
            do
            {
                if (i > 10) break;
                if (culture.Name == name) return true;
                culture = culture.Parent;
                i++;
            } while (culture != null && culture != CultureInfo.InvariantCulture);
            return false;
        }
    }
}
