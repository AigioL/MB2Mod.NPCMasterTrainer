using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using MB2Mod.NPCMasterTrainer.Properties;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        partial class ExportData
        {

            public static readonly Type typeSkillsValueExportData = typeof(SkillsValueExportData);

            public static readonly Type typeDefaultSkills = typeof(DefaultSkills);

            public static readonly IDictionary<string, string> mapping_skill_name = new Dictionary<string, string>
            {
                { "Smithing", "Crafting" }
            };
        }

        public sealed class SkillsValueExportData
        {
            public int Focus { get; set; }

            public int Value { get; set; }

            public override string ToString() => Value.ToString();
        }

        public sealed class HeroExportData : ExportData<HeroExportData>
        {
            public static HeroExportData Convert(Hero hero)
            {
                if (hero == null) return null;
                var traits = hero.GetHeroTraits();
                var data = new HeroExportData
                {
                    Id = hero.Id.ToString(),
                    StringId = hero.StringId,
                    NameId = hero.Name?.GetID(),
                    Name = hero.Name?.ToString(),
                    Gender = hero.GetGender(),
                    Culture = hero.Culture?.ToString(),
                    Age = hero.Age,
                    Level = hero.Level,
                    Profession = hero.GetProfession(),
                    Generosity = traits?.Generosity ?? default,
                    Honor = traits?.Honor ?? default,
                    Valor = traits?.Valor ?? default,
                    Mercy = traits?.Mercy ?? default,
                    Calculating = traits?.Calculating ?? default,
                    UnspentAttributePoints = hero.HeroDeveloper.UnspentAttributePoints,
                    UnspentFocusPoints = hero.HeroDeveloper.UnspentFocusPoints,
                    IsChild = hero.IsChild,
                    IsAlive = hero.IsAlive,
                    FirstName = hero.FirstName?.ToString(),
                    BodyProperties = hero.BodyProperties.ToString(),
                    Marriageable = Campaign.Current?.Models.MarriageModel.IsCoupleSuitableForMarriage(Hero.MainHero, hero) ?? false,
                    IsFertile = hero.IsFemale,
                    LastKnownLocation = hero.GetHeroLastKnownLocation(),
                };
                for (var i = CharacterAttributesEnum.First; i < CharacterAttributesEnum.End; i++)
                {
                    var attrName = i.ToString();
                    var property = lazy_properties.Value.FirstOrDefault(x => string.Equals(attrName, x.Name));
                    if (property == default) continue;
                    property.SetValue(data, hero.GetAttributeValue(i));
                }
                foreach (var item in lazy_properties.Value.Where(x => x.PropertyType == typeSkillsValueExportData))
                {
                    var name = mapping_skill_name.TryGetValue(item.Name, out var new_name) ? new_name : item.Name;
                    var property = typeDefaultSkills.GetProperty(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);
                    if (property == default) continue;
                    var skillObj = property.GetValue(null);
                    if (skillObj != null && skillObj is SkillObject skill)
                    {
                        var skillValue = new SkillsValueExportData
                        {
                            Focus = hero.HeroDeveloper.GetFocus(skill),
                            Value = hero.GetSkillValue(skill),
                        };
                        item.SetValue(data, skillValue);
                    }
                };
                return data;
            }

            public static new string TableHeader
            {
                get
                {
                    var strings = TableHeaders;
                    var properties = lazy_properties.Value;
                    strings = strings.Concat(properties.Where(x => x.PropertyType == typeSkillsValueExportData)
                        .Select(x => $"{Resources.GetString(x.Name)}({Resources.Focus})"));
                    return Join(strings);
                }
            }

            public override string ToRowString()
            {
                var strings = Values;
                var properties = lazy_properties.Value;
                strings = strings.Concat(properties.Where(x => x.PropertyType == typeSkillsValueExportData).Select(x =>
                {
                    var value = x.GetValue(this);
                    if (value != null && value is SkillsValueExportData skillsValue)
                    {
                        return skillsValue.Focus.ToString();
                    }
                    return string.Empty;
                }));
                return Join(strings);
            }

            #region 名称,性别,文化,年龄,等级,职业 6

            [Int32(0)]
            public string Id { get; set; }

            [Int32(1)]
            public string StringId { get; set; }

            [Int32(2)]
            public string NameId { get; set; }

            [Int32(3)]
            public string Name { get; set; }

            [Int32(4)]
            public string Gender { get; set; }

            [Int32(5)]
            public string Culture { get; set; }

            [Int32(6)]
            public float Age { get; set; }

            [Int32(7)]
            public int Level { get; set; }

            [Int32(8)]
            public string Profession { get; set; }

            const int _1 = 9;

            #endregion

            #region 胸怀,荣誉,胆气,善恶 5

            [Int32(_1)]
            public int Generosity { get; set; }

            [Int32(_1 + 1)]
            public int Honor { get; set; }

            [Int32(_1 + 2)]
            public int Valor { get; set; }

            [Int32(_1 + 3)]
            public int Mercy { get; set; }

            [Int32(_1 + 4)]
            public int Calculating { get; set; }

            const int _2 = _1 + 5;

            #endregion

            #region 单手,双手,长杆 3

            [Int32(_2)]
            public SkillsValueExportData OneHanded { get; set; }

            [Int32(_2 + 1)]
            public SkillsValueExportData TwoHanded { get; set; }

            [Int32(_2 + 2)]
            public SkillsValueExportData Polearm { get; set; }

            const int _3 = _2 + 3;

            #endregion

            #region 弓,弩,投掷 3

            [Int32(_3)]
            public SkillsValueExportData Bow { get; set; }

            [Int32(_3 + 1)]
            public SkillsValueExportData Crossbow { get; set; }

            [Int32(_3 + 2)]
            public SkillsValueExportData Throwing { get; set; }

            const int _4 = _3 + 3;

            #endregion

            #region 骑术,跑动,锻造 3

            [Int32(_4)]
            public SkillsValueExportData Riding { get; set; }

            [Int32(_4 + 1)]
            public SkillsValueExportData Athletics { get; set; }

            [Int32(_4 + 2)]
            public SkillsValueExportData Smithing { get; set; }

            const int _5 = _4 + 3;

            #endregion

            #region 侦察,战术,流氓习气 3

            [Int32(_5)]
            public SkillsValueExportData Scouting { get; set; }

            [Int32(_5 + 1)]
            public SkillsValueExportData Tactics { get; set; }

            [Int32(_5 + 2)]
            public SkillsValueExportData Roguery { get; set; }

            const int _6 = _5 + 3;

            #endregion

            #region 魅力,统御,交易 3

            [Int32(_6)]
            public SkillsValueExportData Charm { get; set; }

            [Int32(_6 + 1)]
            public SkillsValueExportData Leadership { get; set; }

            [Int32(_6 + 2)]
            public SkillsValueExportData Trade { get; set; }

            const int _7 = _6 + 3;

            #endregion

            #region 管理,医术,工程 3

            [Int32(_7)]
            public SkillsValueExportData Steward { get; set; }

            [Int32(_7 + 1)]
            public SkillsValueExportData Medicine { get; set; }

            [Int32(_7 + 2)]
            public SkillsValueExportData Engineering { get; set; }

            const int _8 = _7 + 3;

            #endregion

            #region 活力,控制,耐力,狡诈,社交,智力 6

            [Int32(_8)]
            public int Vigor { get; set; }

            [Int32(_8 + 1)]
            public int Control { get; set; }

            [Int32(_8 + 2)]
            public int Endurance { get; set; }

            [Int32(_8 + 3)]
            public int Cunning { get; set; }

            [Int32(_8 + 4)]
            public int Social { get; set; }

            [Int32(_8 + 5)]
            public int Intelligence { get; set; }

            const int _9 = _8 + 6;

            #endregion

            #region Unspent 2

            [Int32(_9)]
            public int UnspentAttributePoints { get; set; }

            [Int32(_9 + 1)]
            public int UnspentFocusPoints { get; set; }

            const int _10 = _9 + 2;

            #endregion

            #region Other

            [Int32(_10)]
            public bool IsChild { get; set; }

            [Int32(_10 + 1)]
            public bool IsAlive { get; set; }

            [Int32(_10 + 2)]
            public string FirstName { get; set; }

            [Int32(_10 + 3)]
            public bool Marriageable { get; set; }

            [Int32(_10 + 4)]
            public bool IsFertile { get; set; }

            [Int32(_10 + 5)]
            public string BodyProperties { get; set; }

            [Int32(_10 + 6)]
            public string LastKnownLocation { get; set; }

            #endregion
        }

        public static bool? Export(IEnumerable<Hero> heroes, string mark)
        {
            var fileNamePrefix = $"{Resources.HeroesData}({mark})";
            return Export(heroes, HeroExportData.Convert, HeroExportData.TableHeader, fileNamePrefix);
        }
    }
}
