using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TaleWorlds.Core;
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

            public const string Turkish = "tr";

            public const string German = "de";

            public const string Polish = "pl";

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

            internal static readonly IReadOnlyDictionary<string, string> mapping_language_id__culture_name = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "English", English },
                { "简体中文", SimplifiedChinese },
                { "繁體中文", TraditionalChinese },
                { "Türkçe", Turkish },
                { "German", German },
                { "Polskie", Polish },
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

            #region Languages\String-*.xml

            private const string Languages_DIR = "Languages";

            internal static IReadOnlyDictionary<string, string> ReadStringXml(string filePath, string fileName = null)
            {
                if (File.Exists(filePath))
                {
                    try
                    {
                        var dict = new Dictionary<string, string>();
                        var doc = new XmlDocument();
                        using var reader = XmlReader.Create(filePath, new XmlReaderSettings
                        {
                            IgnoreComments = true
                        });
                        doc.Load(reader);
                        var root = doc.SelectSingleNode("strings");
                        if (root != null)
                        {
                            var items = root.SelectNodes("string");
                            foreach (XmlNode item in items)
                            {
                                if (item == null) continue;
                                var name = item.Attributes["name"]?.Value;
                                if (!string.IsNullOrWhiteSpace(name))
                                {
                                    if (dict.ContainsKey(name))
                                    {
                                        dict[name] = item.InnerText;
                                    }
                                    else
                                    {
                                        dict.Add(name, item.InnerText);
                                    }
                                }
                            }
                        }
                        return dict;
                    }
                    catch (Exception e)
                    {
                        DisplayMessage(e);
                        DisplayMessage($"Load {(fileName ?? Path.GetFileName(filePath))} Fail.", Colors.OrangeRed);
                    }
                }
                return default;
            }

            static Lazy<IReadOnlyDictionary<string, string>> GetStringDictByLanguage(string lang)
                => new Lazy<IReadOnlyDictionary<string, string>>(() =>
                {
                    var fileName = $"strings-{lang}.xml";
                    var filePath = Path.Combine(CurrentModDirectory, Languages_DIR, fileName);
                    return ReadStringXml(filePath, fileName);
                });

            static readonly IReadOnlyDictionary<string, Lazy<IReadOnlyDictionary<string, string>>> pairs = mapping_language_id__culture_name.Values.ToDictionary(k => k, v => GetStringDictByLanguage(v));

            static string GetStringByXmlFiles(string name, string lang, bool useEnglish = false)
            {
                if (pairs.ContainsKey(lang))
                {
                    var d = pairs[lang]?.Value;
                    if (d?.ContainsKey(name) ?? false)
                    {
                        return d[name];
                    }
                }
                else if (!useEnglish && lang != English)
                {
                    GetStringByXmlFiles(name, English, true);
                }
                return null;
            }

            public static string GetStringByXmlFiles(string name)
                => GetStringByXmlFiles(name, Resources.Language);

            #endregion
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

        public static string ToString2(this CharacterAttributesEnum attrType) => attrType switch
        {
            CharacterAttributesEnum.Vigor => Resources.Vigor,
            CharacterAttributesEnum.Control => Resources.Control,
            CharacterAttributesEnum.Endurance => Resources.Endurance,
            CharacterAttributesEnum.Cunning => Resources.Cunning,
            CharacterAttributesEnum.Social => Resources.Social,
            CharacterAttributesEnum.Intelligence => Resources.Intelligence,
            _ => attrType.ToString(),
        };
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