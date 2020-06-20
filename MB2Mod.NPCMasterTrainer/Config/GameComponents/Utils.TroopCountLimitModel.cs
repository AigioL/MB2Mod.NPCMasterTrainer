using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        partial class Config
        {
            /// <summary>
            /// 开启藏身处人数配置，默认值<see langword="false"/>(关)
            /// </summary>
            public bool EnableTroopCountLimitModel { get; set; }

            /// <summary>
            /// [藏身处人数配置]藏身处人数最大限制
            /// </summary>
            public int? HideoutBattlePlayerMaxTroopCount { get; set; }

            /// <summary>
            /// [藏身处人数配置]使用玩家部队中所有NPC总数作为最大限制
            /// </summary>
            public bool UseAllNPCHideoutBattlePlayerMaxTroop { get; set; } = true;
        }

        public sealed class NPCMT_TroopCountLimitModel : DefaultTroopCountLimitModel
        {
            readonly Config config;

            NPCMT_TroopCountLimitModel(Config config) => this.config = config;

            public override int GetHideoutBattlePlayerMaxTroopCount()
            {
                if (config.HideoutBattlePlayerMaxTroopCount.HasValue && config.HideoutBattlePlayerMaxTroopCount.Value > 0)
                {
                    return config.HideoutBattlePlayerMaxTroopCount.Value;
                }
                if (config.UseAllNPCHideoutBattlePlayerMaxTroop)
                {
                    var clanTier = ClanTierModel;
                    if (clanTier != default)
                    {
                        var player = Hero.MainHero;
                        if (player != default)
                        {
                            var count = GetNotMeNpcs(player.Clan?.Heroes, player, isNoble: true, isWanderer: true, inMyTroops: true).Count();
                            if (count > 0)
                            {
                                return count;
                            }
                        }
                        return clanTier.GetCompanionLimitForTier(clanTier.MaxClanTier) + 2;
                    }
                }
                return base.GetHideoutBattlePlayerMaxTroopCount();
            }

            static readonly Lazy<TroopCountLimitModel> lazy_instance = new Lazy<TroopCountLimitModel>(() => new NPCMT_TroopCountLimitModel(Config.Instance));

            public static TroopCountLimitModel Instance => lazy_instance.Value;

            public static TroopCountLimitModel Init(Config config)
            {
                if (config.EnableTroopCountLimitModel)
                    return Instance;
                return default;
            }
        }
    }
}
