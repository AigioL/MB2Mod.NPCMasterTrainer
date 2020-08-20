using System.Collections.Generic;
using TaleWorlds.CampaignSystem;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static object Print(this Town town)
        {
            if (town == null) return null;
            var jsonObj = new
            {
                Id = town.Id.ToString(),
                town.StringId,
                Name = town.TryGetValue(x => x.Name?.ToString()),
                ToString = town.ToString(),
                //town.IsOwnerUnassigned,
                Owner = town.TryGetValue(t => t.Owner?.Name.ToString()),
                Buildings = town.Buildings.TryGetValue(x => new
                {
                    Name = x.Name?.ToString(),
                    x.CurrentLevel,
                }),
                town.Loyalty, // 忠诚
                town.Security, // 安全
                town.Construction,
                Governor = town.TryGetValue(t => t.Governor?.Name.ToString()),  // 统治者
                Culture = town.TryGetValue(t => t.Culture?.Name.ToString()),
                Workshops = town.Workshops.TryGetValue(x => new
                {
                    Name = x.TryGetValue(y => y.Name?.ToString()),
                    x.Level,
                    Owner = x.TryGetValue(y => y.Owner?.Name.ToString()),
                    WorkshopType = x.TryGetValue(y => new
                    {
                        Id = y.WorkshopType?.StringId,
                        Name = y.WorkshopType?.Name.ToString(),
                    }),
                    x.Tag,
                    x.IsRunning,
                    x.Capital,
                }),
                //MercenaryData = town.MercenaryData.TryGetValue(x => new
                //{
                //    TroopType = town.MercenaryData.TryGetValue(y => y.TroopType?.Name.ToString()),
                //    town.MercenaryData.Number,
                //}), // 城镇雇佣兵数据
                WallLevel = town.TryGetValue(x => x.GetWallLevel()), // 城墙等级,
                town.Prosperity, // 繁荣度
                ProsperityLevel = town.TryGetValue(x => x.GetProsperityLevel()),
                town.FoodStocks, // 食物储备
                town.Militia, // 民兵
                town.Gold,
            };
            return jsonObj;
        }

        public static void Print(this IEnumerable<Town> towns, string tag) => towns.Print(tag, "Towns", Print);
    }
}