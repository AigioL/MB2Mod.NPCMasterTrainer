using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using MB2Mod.NPCMasterTrainer.Properties;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public sealed class BuildingExportData
        {
            public string Name { get; set; }

            public int CurrentLevel { get; set; }
        }

        public sealed class TownExportData : ExportData<TownExportData>
        {
            public static TownExportData Convert(Town town)
            {
                if (town == null) return null;
                var data = new TownExportData
                {
                    Id = town.Id.ToString(),
                    StringId = town.StringId,
                    NameId = town.Name?.GetID(),
                    Name = town.TryGetValue(x => x.Name?.ToString())?.ToString(),
                    Prosperity = town.Prosperity,
                    Militia = town.Militia,
                    Loyalty = town.Loyalty,
                    FoodStocks = town.FoodStocks,
                    Security = town.Security,
                    Gold = town.Gold,
                    Culture = town.TryGetValue(x => x.Culture?.Name.ToString())?.ToString(),
                    Governor = town.TryGetValue(x => x.Governor?.Name.ToString())?.ToString(),
                    WallLevel = town.GetWallLevel(),
                };
                if (town.Workshops != null)
                {
                    var workshops = town.Workshops.Where(x => x.WorkshopType?.Name != null && x.WorkshopType.StringId != "artisans")
                        .Select(x => x.WorkshopType.Name.ToString()).ToArray();
                    if (workshops.Any())
                    {
                        data.Workshops = string.Join(", ", workshops);
                    }
                }
                if (town.Buildings != null && town.Buildings.Any())
                {
                    data.Buildings = town.Buildings.Where(x => x?.Name != null)
                        .Select(x => new BuildingExportData { Name = x.Name.ToString(), CurrentLevel = x.CurrentLevel }).ToArray();
                }
                return data;
            }

            public static string[] GetDynamicHeaders(IEnumerable<TownExportData> datas)
                => datas.Where(x => x.Buildings != null).SelectMany(x => x.Buildings).Where(x => x != null).Select(x => x.Name).Distinct().ToArray();

            public static string GetDynamicValue(string header, TownExportData data)
            {
                if (data.Buildings != default)
                {
                    var building = data.Buildings.FirstOrDefault(x => x.Name == header);
                    if (building != default)
                    {
                        return building.CurrentLevel.ToString();
                    }
                }
                return null;
            }

            #region 名称,繁荣度,民兵,忠诚度,食物,治安度,金币,文化,所有者,城墙等级,工坊

            [Int32(0)]
            public string Id { get; set; }

            [Int32(1)]
            public string StringId { get; set; }

            [Int32(2)]
            public string NameId { get; set; }

            [Int32(3)]
            public string Name { get; set; }

            [Int32(4)]
            public float Prosperity { get; set; }

            [Int32(5)]
            public float Militia { get; set; }

            [Int32(6)]
            public float Loyalty { get; set; }

            [Int32(7)]
            public float FoodStocks { get; set; }

            [Int32(8)]
            public float Security { get; set; }

            [Int32(9)]
            public int Gold { get; set; }

            [Int32(10)]
            public string Culture { get; set; }

            [Int32(11)]
            public string Governor { get; set; }

            [Int32(12)]
            public int WallLevel { get; set; }

            [Int32(13)]
            public string Workshops { get; set; }

            #endregion

            /// <summary>
            /// 建筑
            /// </summary>
            public BuildingExportData[] Buildings { get; set; }
        }

        public static bool? Export(IEnumerable<Town> towns, string mark)
        {
            var fileNamePrefix = $"{Resources.TownsData}({mark})";
            return Export(towns, TownExportData.Convert, TownExportData.TableHeader, fileNamePrefix,
                TownExportData.GetDynamicHeaders, TownExportData.GetDynamicValue);
        }
    }
}
