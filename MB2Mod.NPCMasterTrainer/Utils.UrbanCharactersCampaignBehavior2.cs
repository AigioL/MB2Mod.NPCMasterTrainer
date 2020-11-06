using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        partial class Config
        {
            /// <summary>
            /// 仅创建女性或男性流浪者
            /// </summary>
            public bool? OnlyCreateFemaleOrMaleWanderer { get; set; }

            /// <summary>
            /// 创建流浪者时排除的文化，默认排除帝国
            /// </summary>
            public string[] CreateWandererExcludeCultures { get; set; } = new[] { "empire" };

            ///// <summary>
            ///// 创建流浪者时的年龄
            ///// </summary>
            //public byte? CreateWandererSetAge { get; set; } = 19;
        }

        public static string[] GetCultures()
        {
            return CharacterObject.Templates?.Where(x => x.Occupation == Occupation.Wanderer)
                .Select(x => x.Culture.StringId.ToLower()).Distinct().ToArray();
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("cultures", "print")]
#pragma warning disable IDE0060 // 删除未使用的参数
        public static string PrintCultures(List<string> args)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            var cultures = GetCultures();
            if (cultures != null)
            {
                return string.Join(", ", cultures);
            }
            return "null";
        }

        public static bool Contains(this CultureObject culture, IEnumerable<string> cultures)
        {
            return cultures.Contains(culture.StringId, StringComparer.OrdinalIgnoreCase);
        }

        public sealed class UrbanCharactersCampaignBehavior2
        {
            [Obsolete("", true)]
            private UrbanCharactersCampaignBehavior2() { }

            static readonly Lazy<FieldInfo> _companionTemplates = new Lazy<FieldInfo>(() =>
            {
                return typeof(UrbanCharactersCampaignBehavior).GetField("_companionTemplates", BindingFlags.NonPublic | BindingFlags.Instance);
            });

            static void SetCompanionTemplates(object behavior, List<CharacterObject> characters) => _companionTemplates.Value.SetValue(behavior, characters);

            [MethodImpl(MethodImplOptions.NoInlining)]
            void CreateCompanionDest(CharacterObject companionTemplate)
            {
                if (companionTemplate == null) return;
                if (companionTemplate.Occupation == Occupation.Wanderer)
                {
                    var config = Config.Instance;
                    //if (config.CreateWandererSetAge.HasValue && companionTemplate.IsHero)
                    //{
                    //    companionTemplate.HeroObject.BirthDay = CampaignTime.Now - CampaignTime.Years(config.CreateWandererSetAge.Value);
                    //}
                    var hasFilterGender = config.OnlyCreateFemaleOrMaleWanderer.HasValue;
                    var hasFilterCulture = config.CreateWandererExcludeCultures != null && config.CreateWandererExcludeCultures.Any();
                    var filterGenderFail = hasFilterGender && companionTemplate.IsFemale != config.OnlyCreateFemaleOrMaleWanderer.Value;
                    var filterCultureFail = hasFilterCulture && companionTemplate.Culture.Contains(config.CreateWandererExcludeCultures);
                    if (filterGenderFail || filterCultureFail)
                    {
                        var query = from x in CharacterObject.Templates
                                    where x.Occupation == Occupation.Wanderer
                                    let filterGender = !hasFilterGender || x.IsFemale == config.OnlyCreateFemaleOrMaleWanderer.Value
                                    let filterCulture = !hasFilterCulture || !x.Culture.Contains(config.CreateWandererExcludeCultures)
                                    where filterGender && filterCulture
                                    select x;
                        var companionTemplates = new List<CharacterObject>(query);
                        SetCompanionTemplates(this, companionTemplates);
                        companionTemplate = companionTemplates.GetRandomElement();
                    }
                }
                //#if DEBUG
                //                if (Config.Instance.HasWin32Console())
                //                {
                //                    Console.WriteLine($"CreateCompanionDest: {companionTemplate.Name}, " +
                //                        $"Culture: {companionTemplate.Culture?.Name}");
                //                }
                //#endif
                CreateCompanionSource(companionTemplate);
            }

#pragma warning disable IDE0060 // 删除未使用的参数
            [MethodImpl(MethodImplOptions.NoInlining)]
            void CreateCompanionSource(CharacterObject companionTemplate)
            {
            }
#pragma warning restore IDE0060 // 删除未使用的参数

            static bool Enable
            {
                get
                {
                    var config = Config.Instance;
                    return /*config.CreateWandererSetAge.HasValue ||*/
                        config.OnlyCreateFemaleOrMaleWanderer.HasValue ||
                        (config.CreateWandererExcludeCultures != null &&
                        config.CreateWandererExcludeCultures.Any());
                }
            }

            public static void InitOnlyCreateFemaleOrMaleWanderer()
            {
                if (!Enable) return;
                var behaviorDestType = typeof(UrbanCharactersCampaignBehavior2);
                var behaviorSourceType = typeof(UrbanCharactersCampaignBehavior);
                var source = behaviorDestType.GetMethod(nameof(CreateCompanionSource), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(CharacterObject) }, null);
                var destination = behaviorSourceType.GetMethod("CreateCompanion", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(CharacterObject) }, null);
                Hook.ReplaceMethod(source, destination);
                source = destination;
                destination = behaviorDestType.GetMethod(nameof(CreateCompanionDest), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(CharacterObject) }, null);
                Hook.ReplaceMethod(source, destination);
                if (Config.Instance.HasWin32Console())
                {
                    Console.WriteLine("InitOnlyCreateFemaleOrMaleWanderer Success.");
                }
            }
        }
    }
}