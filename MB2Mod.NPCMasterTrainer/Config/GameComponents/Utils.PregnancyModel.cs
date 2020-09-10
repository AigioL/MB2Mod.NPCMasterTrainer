using Helpers;
using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        partial class Config
        {
            /// <summary>
            /// 开启妊娠配置，默认值<see langword="false"/>(关)
            /// </summary>
            public bool EnablePregnancyModel { get; set; }

            ///// <summary>
            ///// [妊娠配置]在游戏创建成功后初始化所有的角色能够生育的占比，当前版本(1.4.0.230377)默认值为 0.95
            ///// </summary>
            //public float? CharacterFertilityProbability { get; set; }

            /// <summary>
            /// [妊娠配置]妊娠期(天数)，当前版本(1.4.0.230377)默认值为 36
            /// </summary>
            public float? PregnancyDurationInDays { get; set; }

            /// <summary>
            /// [妊娠配置]产妇分娩死亡率，当前版本(1.4.0.230377)默认值为 0.015
            /// </summary>
            public float? MaternalMortalityProbabilityInLabor { get; set; }

            /// <summary>
            /// [妊娠配置]死胎概率，当前版本(1.4.0.230377)默认值为 0.01
            /// </summary>
            public float? StillbirthProbability { get; set; }

            /// <summary>
            /// [妊娠配置]生育女性后代率，当前版本(1.4.0.230377)默认值为 0.51
            /// </summary>
            public float? DeliveringFemaleOffspringProbability { get; set; }

            /// <summary>
            /// [妊娠配置]生双胞胎的概率，当前版本(1.4.0.230377)默认值为 0.03
            /// </summary>
            public float? DeliveringTwinsProbability { get; set; }

            /// <summary>
            /// [妊娠配置]最大孕龄，当前版本(1.4.0.230377)默认值为 45
            /// </summary>
            public ushort? MaxPregnancyAge { get; set; }

            /// <summary>
            /// [妊娠配置]我或我的配偶的最大孕龄，当前版本(1.4.0.230377)默认值为 45
            /// </summary>
            public ushort? MaxPregnancyAgeForMeOrMySpouse { get; set; }

            /// <summary>
            /// [妊娠配置]增加每日怀孕几率倍数，默认值为 1
            /// </summary>
            public ulong AddDailyChanceOfPregnancyForHeroMultiple { get; set; } = 1;

            /// <summary>
            /// [妊娠配置]增加我或我的配偶每日怀孕几率倍数，默认值为 1
            /// </summary>
            public ulong AddDailyChanceOfPregnancyForMeOrMySpouseMultiple { get; set; } = 1;
        }

        public sealed class NPCMT_PregnancyModel : DefaultPregnancyModel
        {
            private readonly Config config;

            private NPCMT_PregnancyModel(Config config) => this.config = config;

            //public override float CharacterFertilityProbability
            //    => GetPercentage(config?.CharacterFertilityProbability) ?? base.CharacterFertilityProbability;

            public override float PregnancyDurationInDays
                => GetDays(config?.PregnancyDurationInDays) ?? base.PregnancyDurationInDays;

            public override float MaternalMortalityProbabilityInLabor
                => GetPercentage(config?.MaternalMortalityProbabilityInLabor) ?? base.MaternalMortalityProbabilityInLabor;

            public override float StillbirthProbability
                => GetPercentage(config?.StillbirthProbability) ?? base.StillbirthProbability;

            public override float DeliveringFemaleOffspringProbability
                => GetPercentage(config?.DeliveringFemaleOffspringProbability) ?? base.DeliveringFemaleOffspringProbability;

            public override float DeliveringTwinsProbability
                => GetPercentage(config?.DeliveringTwinsProbability) ?? base.DeliveringTwinsProbability;

            private static readonly Lazy<PregnancyModel> lazy_instance = new Lazy<PregnancyModel>(() => new NPCMT_PregnancyModel(Config.Instance));

            public static PregnancyModel Instance => lazy_instance.Value;

            private const int MinPregnancyAge = 18;
            private const int MaxPregnancyAge = 45;

            private static readonly Lazy<FieldInfo> lazy_field_MaxPregnancyAge = new Lazy<FieldInfo>(() =>
               {
                   var field = typeof(DefaultPregnancyModel).GetField(nameof(MaxPregnancyAge), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                   return field.FieldType == typeof(int) && field.IsLiteral ? field : null;
               });

            private int GetMaxPregnancyAge()
            {
                if ((config?.MaxPregnancyAge).HasValue) return config.MaxPregnancyAge.Value;
                try
                {
                    return Convert.ToInt32(lazy_field_MaxPregnancyAge.Value.GetValue(null));
                }
                catch
                {
                    return MaxPregnancyAge;
                }
            }

            // PregnancyCampaignBehavior.DailyTickHero(Hero hero) Age<=18 直接 return
            // 不进行 RefreshSpouseVisit -> GetDailyChanceOfPregnancyForHero 调用

            private bool IsHeroAgeSuitableForPregnancy(Hero hero, int maxPregnancyAge) => hero.Age >= MinPregnancyAge && hero.Age <= maxPregnancyAge;

            private bool IsMeOrMySpouse(Hero hero)
            {
                var main = Hero.MainHero;
                return hero == main || hero == main.Spouse;
            }

            public override float GetDailyChanceOfPregnancyForHero(Hero hero)
            {
                float result = default;
                var isMeOrMySpouse = IsMeOrMySpouse(hero);
                var maxPregnancyAge = isMeOrMySpouse ? config.MaxPregnancyAgeForMeOrMySpouse ?? GetMaxPregnancyAge() : GetMaxPregnancyAge();
                if (hero.Spouse != null && hero.IsFertile && IsHeroAgeSuitableForPregnancy(hero, maxPregnancyAge))
                {
                    var bonuses = new ExplainedNumber(1f);
                    PerkHelper.AddPerkBonusForCharacter(DefaultPerks.Medicine.PerfectHealth, hero.Clan.Leader.CharacterObject, true, ref bonuses);
                    result = (float)((6.5 - (hero.Age - MinPregnancyAge) * 0.230000004172325) * 0.0199999995529652) * bonuses.ResultNumber;
                    if (hero.Children.Count == 0) result *= 3f;
                    else if (hero.Children.Count == 1) result *= 2f;
                    if (isMeOrMySpouse && config.AddDailyChanceOfPregnancyForMeOrMySpouseMultiple != 1)
                    {
                        result *= config.AddDailyChanceOfPregnancyForMeOrMySpouseMultiple;
                    }
                    else if (config.AddDailyChanceOfPregnancyForHeroMultiple != 1)
                    {
                        result *= config.AddDailyChanceOfPregnancyForHeroMultiple;
                    }
                    if (config.HasWin32Console())
                    {
                        var hero_name = hero?.Name?.ToString();
                        var hero_culture = hero?.Culture?.Name?.ToString();
                        var spouce_name = hero?.Spouse?.Name?.ToString();
                        var spouce_culture = hero?.Spouse?.Culture?.Name?.ToString();
                        var one_culture = hero_culture == spouce_culture;
                        Console.WriteLine(
                            $"GetDailyChanceOfPregnancyForHero: {result}, isMeOrMySpouse: {isMeOrMySpouse}, " +
                            $"{hero_name}({hero_culture}) & " +
                            $"{spouce_name}{(!one_culture ? $"({spouce_culture})" : null)}"
                            );
                    }
                }
                return result;
            }

            public static bool Print(bool isV2 = false)
            {
                var model = Campaign.Current?.Models?.PregnancyModel;
                if (model != default)
                {
                    var typeName = model.GetType().FullName;
                    typeName = isV2 ? typeName : typeName.Replace("NPCMT_", "NPCMT");
                    DisplayMessage($"{(isV2 ? "PregnancyModel" : Resources.PregnancyModel)}: {typeName}");
                    if (!isV2)
                    {
                        //DisplayMessage($"CharacterFertilityProbability: {model.CharacterFertilityProbability}");
                        DisplayMessage($"PregnancyDurationInDays: {model.PregnancyDurationInDays}");
                        DisplayMessage($"MaternalMortalityProbabilityInLabor: {model.MaternalMortalityProbabilityInLabor}");
                        DisplayMessage($"StillbirthProbability: {model.StillbirthProbability}");
                        DisplayMessage($"DeliveringFemaleOffspringProbability: {model.DeliveringFemaleOffspringProbability}");
                        DisplayMessage($"DeliveringTwinsProbability: {model.DeliveringTwinsProbability}");
                        if (model is NPCMT_PregnancyModel model_npcmt)
                        {
                            DisplayMessage($"MaxPregnancyAge: {model_npcmt.GetMaxPregnancyAge()}");
                            DisplayMessage($"MaxPregnancyAgeForMeOrMySpouse: {model_npcmt.config.MaxPregnancyAgeForMeOrMySpouse ?? model_npcmt.GetMaxPregnancyAge()}");
                            DisplayMessage($"AddDailyChanceOfPregnancyForHeroMultiple: {model_npcmt.config.AddDailyChanceOfPregnancyForHeroMultiple}");
                            DisplayMessage($"AddDailyChanceOfPregnancyForMeOrMySpouseMultiple: {model_npcmt.config.AddDailyChanceOfPregnancyForMeOrMySpouseMultiple}");
                        }
                    }
                    return true;
                }
                return false;
            }

            public static PregnancyModel Init(Config config)
            {
                if (config.EnablePregnancyModel)
                    return Instance;
                return default;
            }
        }

        private static float? GetSingle(float? f, float max, float min = 0f)
        {
            if (f.HasValue)
            {
                if (f.Value < min) f = min;
                if (f.Value > max) f = max;
            }
            return f;
        }

        private static float? GetPercentage(float? f) => GetSingle(f, 1f);

        private static float? GetDays(float? f) => GetSingle(f, 36500f);
    }
}

// Campaign.Current.Models.PregnancyModel
//int num1 = (double)MBRandom.RandomFloat <= (double)pregnancyModel.DeliveringTwinsProbability ? 1 : 0; 生双胞胎的概率 0.03f;
//int num2 = num1 != 0 ? 2 : 1;
// if ((double) MBRandom.RandomFloat > (double) pregnancyModel.StillbirthProbability) 死胎概率 0.01f;

// TaleWorlds.CampaignSystem.SandBox.GameComponents.DefaultPregnancyModel : PregnancyModel
// GetDailyChanceOfPregnancyForHero 获取怀孕概率
// Hero.IsFertile 是可生育的
// CharacterFertilityProbability 0.95f 表明了当前是所有角色 有5%是绝育的
// 在 TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.PregnancyCampaignBehavior.DetermineCharacterFertilities
// MaternalMortalityProbabilityInLabor 产妇分娩死亡率 0.015f
// DeliveringFemaleOffspringProbability 生育女性后代率 0.51f
// DeliveringTwinsProbability 生双胞胎的概率 0.03f
// 在 18 ~ 45 岁才有能力生育

// 在 PregnancyCampaignBehavior.CheckOffspringsToDeliver 中 如果玩家角色不为母亲，则会计算 产妇分娩死亡率