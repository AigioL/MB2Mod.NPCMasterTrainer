using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using TaleWorlds.MountAndBlade;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class Localization
        {
            //const string _currentLanguageId = "_currentLanguageId";

            public const string English = "en";

            public const string SimplifiedChinese = "zh-Hans";

            public const string TraditionalChinese = "zh-Hant";

            //static readonly Lazy<FieldInfo> lazy_field_currentLanguageId = new Lazy<FieldInfo>(() =>
            //{
            //    var typeMBTextManager = typeof(MBTextManager);
            //    return typeMBTextManager.GetField(_currentLanguageId, BindingFlags.NonPublic | BindingFlags.Static);
            //});

            //public static string GetCurrentLanguageIdByMBTextManager() => lazy_field_currentLanguageId.Value.GetValue(null)?.ToString();

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

            private static readonly IReadOnlyDictionary<string, string> mapping_language_id__culture_name = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "English", English },
                { "简体中文", SimplifiedChinese },
                { "繁体中文", TraditionalChinese },
                //{ "Türkçe", "" },
                //{ "German", "" },
            };

            private static string Language;

            public static string GetLanguage()
            {
                if (!string.IsNullOrWhiteSpace(Language)) return Language;
                //var currentLanguageId = GetCurrentLanguageIdByMBTextManager();
                var currentLanguageId = BannerlordConfig.Language;
                if (currentLanguageId != null && mapping_language_id__culture_name.TryGetValue(currentLanguageId, out var value)) return value;
                return English;
            }

            public static void SetLanguage(string value)
            {
                if (value != Language)
                {
                    Language = value;
                }
            }

            public static void SetLanguage(CultureInfo value)
            {
                if (value.Match(SimplifiedChinese)) Resources.Language = SimplifiedChinese;
                else if (value.Match(TraditionalChinese)) Resources.Language = TraditionalChinese;
            }
        }

        private static bool Match(this CultureInfo culture, string name)
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

namespace MB2Mod.NPCMasterTrainer.Properties
{
    partial class Resources
    {
        internal static string Language
        {
            get => Utils.Localization.GetLanguage();
            set => Utils.Localization.SetLanguage(value);
        }
    }
}