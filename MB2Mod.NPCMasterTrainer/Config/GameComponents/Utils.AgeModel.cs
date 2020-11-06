using System;
using System.Diagnostics;
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
            /// 开启年龄配置，默认值<see langword="false"/>(关)
            /// </summary>
            public bool EnableAgeModel { get; set; }
#if DEBUG
            = true;
#endif

            /// <summary>
            /// 变成婴儿年龄
            /// </summary>
            public int? BecomeInfantAge { get; set; }

            /// <summary>
            /// 变成儿童年龄
            /// </summary>
            public int? BecomeChildAge { get; set; }

            /// <summary>
            /// 变成青年年龄
            /// </summary>
            public int? BecomeTeenagerAge { get; set; }
#if DEBUG
            = 9;
#endif

            /// <summary>
            /// 成年年龄
            /// </summary>
            public int? HeroComesOfAge { get; set; }
#if DEBUG
            = 11;
#endif

            /// <summary>
            /// 老年年龄
            /// </summary>
            public int? BecomeOldAge { get; set; }

            /// <summary>
            /// 最大寿命年龄
            /// </summary>
            public int? MaxAge { get; set; }
        }

        public static void AddAgeModel(this IGameStarter gameStarter)
        {
            var config = Config.Instance;
            if (config.EnableAgeModel)
            {
                gameStarter.AddModel(NPCMT_AgeModel.Instance);
            }
        }

        public sealed class NPCMT_AgeModel : DefaultAgeModel
        {
            readonly Config config;

            NPCMT_AgeModel(Config config) => this.config = config;

            static readonly Lazy<AgeModel> lazy_instance =
                new Lazy<AgeModel>(() => new NPCMT_AgeModel(Config.Instance));

            public static AgeModel Instance => lazy_instance.Value;

            public override int BecomeInfantAge => config.BecomeInfantAge ?? base.BecomeInfantAge;

            public override int BecomeChildAge => config.BecomeChildAge ?? base.BecomeChildAge;

            public override int BecomeTeenagerAge => config.BecomeTeenagerAge ?? base.BecomeTeenagerAge;

            public override int HeroComesOfAge => config.HeroComesOfAge ?? base.HeroComesOfAge;

            public override int BecomeOldAge => config.BecomeOldAge ?? base.BecomeOldAge;

            public override int MaxAge => config.MaxAge ?? base.MaxAge;
        }
    }
}