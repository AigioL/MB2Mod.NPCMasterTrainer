using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        partial class Config
        {
            /// <summary>
            /// 开启工坊配置，默认值<see langword="false"/>(关)
            /// </summary>
            public bool EnableWorkshopModel { get; set; }

            /// <summary>
            /// [工坊配置]玩家可拥有的工坊最大数量
            /// </summary>
            public int? MaxWorkshopCountForPlayer { get; set; }

            /// <summary>
            /// [工坊配置]解除玩家可拥有的工坊最大数量限制
            /// </summary>
            public bool RemoveMaxWorkshopCountLimit { get; set; }

            /// <summary>
            /// [工坊配置]解锁玩家最高家族等级所能拥有的工坊数量
            /// </summary>
            public bool UnlockMaxTierWorkshopCount { get; set; } = true;
        }

        public sealed class NPCMT_WorkshopModel : DefaultWorkshopModel
        {
            readonly Config config;

            NPCMT_WorkshopModel(Config config) => this.config = config;

            public override int GetMaxWorkshopCountForPlayer()
            {
                if (config.MaxWorkshopCountForPlayer.HasValue && config.MaxWorkshopCountForPlayer.Value > 0)
                {
                    return config.MaxWorkshopCountForPlayer.Value;
                }
                if (config.RemoveMaxWorkshopCountLimit)
                {
                    var allTowns = Town.AllTowns;
                    if (allTowns != default)
                    {
                        return allTowns.Count;
                    }
                    return byte.MaxValue;
                }
                if (config.UnlockMaxTierWorkshopCount)
                {
                    var clanTier = ClanTierModel;
                    if (clanTier != default)
                    {
                        var playerClanTier = Clan.PlayerClan?.Tier ?? 0;
                        return clanTier.MaxClanTier - playerClanTier + base.GetMaxWorkshopCountForPlayer();
                    }
                }
                return base.GetMaxWorkshopCountForPlayer();
            }

            static readonly Lazy<WorkshopModel> lazy_instance = new Lazy<WorkshopModel>(() => new NPCMT_WorkshopModel(Config.Instance));

            public static WorkshopModel Instance => lazy_instance.Value;

            public static WorkshopModel Init(Config config)
            {
                if (config.EnableWorkshopModel)
                    return Instance;
                return default;
            }
        }
    }
}
