using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        partial class Config
        {
            /// <summary>
            /// 开启家族等级配置，默认值<see langword="false"/>(关)
            /// </summary>
            public bool EnableClanTierModel { get; set; }

            /// <summary>
            /// [家族等级配置]玩家所能拥有的同伴(流浪者)数量
            /// </summary>
            public int? CompanionLimit { get; set; }

            /// <summary>
            /// [家族等级配置]解锁玩家最高家族等级所能拥有的同伴(流浪者)数量
            /// </summary>
            public bool UnlockMaxTierCompanionLimit { get; set; } = true;
        }

        public sealed class NPCMT_ClanTierModel : DefaultClanTierModel
        {
            private readonly Config config;

            private NPCMT_ClanTierModel(Config config) => this.config = config;

            public override int GetCompanionLimitForTier(int clanTier)
            {
                if (config.CompanionLimit.HasValue && config.CompanionLimit.Value > 3)
                {
                    return config.CompanionLimit.Value;
                }
                if (config.UnlockMaxTierCompanionLimit)
                {
                    return base.GetCompanionLimitForTier(MaxClanTier);
                }
                return base.GetCompanionLimitForTier(clanTier);
            }

            private static readonly Lazy<ClanTierModel> lazy_instance = new Lazy<ClanTierModel>(() => new NPCMT_ClanTierModel(Config.Instance));

            public static ClanTierModel Instance => lazy_instance.Value;

            public static ClanTierModel Init(Config config)
            {
                if (config.EnableClanTierModel)
                    return Instance;
                return default;
            }

            public static bool Print()
            {
                var model = Campaign.Current?.Models?.ClanTierModel;
                if (model != default)
                {
                    var typeName = model.GetType().FullName;
                    DisplayMessage($"ClanTierModel: {typeName}");
                    return true;
                }
                return false;
            }
        }
    }
}