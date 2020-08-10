using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MB2Mod.NPCMasterTrainer
{
    public static class LocalizationMigrate
    {
        internal static partial class Resources
        {
            private static readonly Lazy<PropertyInfo[]> lazy_properties = new Lazy<PropertyInfo[]>(() =>
            {
                var properties = typeof(Resources).GetProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetProperty);
                var query = from p in properties where p.PropertyType == typeof(string) select p;
                return query.ToArray();
            });

            internal static PropertyInfo[] Properties => lazy_properties.Value;

            public static string GetString(string name)
            {
                var property = lazy_properties.Value.FirstOrDefault(x => string.Equals(name, x.Name));
                if (property != null) return property.GetValue(null)?.ToString();
                return name;
            }

            internal static string LoadedDeveloperConsoleInfoMessage => Language switch
            {
                Utils.Localization.SimplifiedChinese => "已加载 DeveloperConsole。按 CTRL 和 ~ 启用。",
                Utils.Localization.TraditionalChinese => "已加載 DeveloperConsole。按 CTRL 和 ~ 啟用。",
                _ => "Loaded DeveloperConsole. Press CTRL and ~ to enable it.",
            };

            internal static string UnspentStatFocus => Language switch
            {
                Utils.Localization.SimplifiedChinese => "可用点数：{0} 属性点 | {1} 专精点",
                Utils.Localization.TraditionalChinese => "可用點數：{0} 屬性點 | {1} 專精點",
                _ => "Unspent: {0} stat | {1} focus",
            };

            internal static string Name
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "名称";

                        case Utils.Localization.TraditionalChinese:
                            return "名稱";

                        default:
                            return "Name";
                    }
                }
            }

            internal static string Gender
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "性别";

                        default:
                            return "Gender";
                    }
                }
            }

            internal static string Female
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "女";

                        default:
                            return "Female";
                    }
                }
            }

            internal static string Male
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "男";

                        default:
                            return "Male";
                    }
                }
            }

            internal static string Culture
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "文化";

                        default:
                            return "Culture";
                    }
                }
            }

            internal static string Level
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "等级";

                        case Utils.Localization.TraditionalChinese:
                            return "等級";

                        default:
                            return "Level";
                    }
                }
            }

            internal static string Generosity
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "胸怀";

                        case Utils.Localization.TraditionalChinese:
                            return "胸懷";

                        default:
                            return "Generosity";
                    }
                }
            }

            internal static string Honor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "荣誉";

                        case Utils.Localization.TraditionalChinese:
                            return "榮譽";

                        default:
                            return "Honor";
                    }
                }
            }

            internal static string Valor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "胆气";

                        case Utils.Localization.TraditionalChinese:
                            return "膽氣";

                        default:
                            return "Valor";
                    }
                }
            }

            internal static string Mercy
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "善恶";

                        default:
                            return "Mercy";
                    }
                }
            }

            internal static string Calculating
            {
                get
                {
                    //switch (Language)
                    //{
                    //    case Utils.Localization.SimplifiedChinese:
                    //    case Utils.Localization.TraditionalChinese:
                    //    default:
                    return "Calculating";
                    //}
                }
            }

            internal static string OneHanded
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "单手";

                        case Utils.Localization.TraditionalChinese:
                            return "單手";

                        default:
                            return "One Handed";
                    }
                }
            }

            internal static string TwoHanded
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "双手";

                        case Utils.Localization.TraditionalChinese:
                            return "雙手";

                        default:
                            return "Two Handed";
                    }
                }
            }

            internal static string Polearm
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "长杆";

                        case Utils.Localization.TraditionalChinese:
                            return "長杆";

                        default:
                            return "Polearm";
                    }
                }
            }

            internal static string Bow
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "弓";

                        default:
                            return "Bow";
                    }
                }
            }

            internal static string Crossbow
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "弩";

                        default:
                            return "Crossbow";
                    }
                }
            }

            internal static string Throwing
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "投掷";

                        case Utils.Localization.TraditionalChinese:
                            return "投擲";

                        default:
                            return "Throwing";
                    }
                }
            }

            internal static string Riding
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "骑术";

                        case Utils.Localization.TraditionalChinese:
                            return "騎術";

                        default:
                            return "Riding";
                    }
                }
            }

            internal static string Athletics
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "跑动";

                        case Utils.Localization.TraditionalChinese:
                            return "體能";

                        default:
                            return "Athletics";
                    }
                }
            }

            internal static string Smithing
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "锻造";

                        case Utils.Localization.TraditionalChinese:
                            return "鍛造";

                        default:
                            return "Smithing";
                    }
                }
            }

            internal static string Scouting
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "侦察";

                        case Utils.Localization.TraditionalChinese:
                            return "偵察";

                        default:
                            return "Scouting";
                    }
                }
            }

            internal static string Tactics
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "战术";

                        case Utils.Localization.TraditionalChinese:
                            return "戰術";

                        default:
                            return "Tactics";
                    }
                }
            }

            internal static string Roguery
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "流氓习气";

                        case Utils.Localization.TraditionalChinese:
                            return "流氓習氣";

                        default:
                            return "Roguery";
                    }
                }
            }

            internal static string Charm
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "魅力";

                        default:
                            return "Charm";
                    }
                }
            }

            internal static string Leadership
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "统御";

                        case Utils.Localization.TraditionalChinese:
                            return "統禦";

                        default:
                            return "Leadership";
                    }
                }
            }

            internal static string Trade
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "交易";

                        default:
                            return "Trade";
                    }
                }
            }

            internal static string Steward
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "管理";

                        default:
                            return "Steward";
                    }
                }
            }

            internal static string Medicine
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "医术";

                        case Utils.Localization.TraditionalChinese:
                            return "醫術";

                        default:
                            return "Medicine";
                    }
                }
            }

            internal static string Engineering
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "工程";

                        default:
                            return "Engineering";
                    }
                }
            }

            [Obsolete("", true)]
            internal static string Occupation => Profession;

            internal static string Profession
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "职业";

                        case Utils.Localization.TraditionalChinese:
                            return "職業";

                        default:
                            return "Profession";
                    }
                }
            }

            internal static string Age
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "年龄";

                        case Utils.Localization.TraditionalChinese:
                            return "年齡";

                        default:
                            return "Age";
                    }
                }
            }

            internal static string Vigor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "活力";

                        default:
                            return "Vigor";
                    }
                }
            }

            internal static string Control
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "控制";

                        default:
                            return "Control";
                    }
                }
            }

            internal static string Endurance
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "耐力";

                        default:
                            return "Endurance";
                    }
                }
            }

            internal static string Cunning
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "狡诈";

                        case Utils.Localization.TraditionalChinese:
                            return "狡詐";

                        default:
                            return "Cunning";
                    }
                }
            }

            internal static string Social
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "社交";

                        default:
                            return "Social";
                    }
                }
            }

            internal static string Intelligence
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "智力";

                        default:
                            return "Intelligence";
                    }
                }
            }

            internal static string Focus
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "专精点";

                        case Utils.Localization.TraditionalChinese:
                            return "專精點";

                        default:
                            return "Focus";
                    }
                }
            }

            internal static string UnspentAttributePoints
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "可用属性点";

                        case Utils.Localization.TraditionalChinese:
                            return "可用屬性點";

                        default:
                            return "Unspent Attribute Points";
                    }
                }
            }

            internal static string UnspentFocusPoints
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "可用专精点";

                        case Utils.Localization.TraditionalChinese:
                            return "可用專精點";

                        default:
                            return "Unspent Focus Points";
                    }
                }
            }

            internal static string Noble
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "贵族";

                        case Utils.Localization.TraditionalChinese:
                            return "貴族";

                        default:
                            return "Noble";
                    }
                }
            }

            internal static string Wanderer
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "流浪者";

                        default:
                            return "Wanderer";
                    }
                }
            }

            internal static string Artisan
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "工匠";

                        default:
                            return "Artisan";
                    }
                }
            }

            internal static string IsChild
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "是孩子";

                        default:
                            return "Is Child";
                    }
                }
            }

            internal static string IsAlive
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "还活着";

                        case Utils.Localization.TraditionalChinese:
                            return "還活著";

                        default:
                            return "Is Alive";
                    }
                }
            }

            internal static string FirstName
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "姓氏";

                        default:
                            return "First Name";
                    }
                }
            }

            internal static string HeroesData
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "角色数据";

                        case Utils.Localization.TraditionalChinese:
                            return "角色數據";

                        default:
                            return "HeroesData";
                    }
                }
            }

            internal static string All
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "所有";

                        default:
                            return "All";
                    }
                }
            }

            internal static string TownsData
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "城镇数据";

                        case Utils.Localization.TraditionalChinese:
                            return "城鎮數據";

                        default:
                            return "TownsData";
                    }
                }
            }

            internal static string Prosperity
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "繁荣度";

                        case Utils.Localization.TraditionalChinese:
                            return "繁榮度";

                        default:
                            return "Prosperity";
                    }
                }
            }

            internal static string Militia
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "民兵";

                        default:
                            return "Militia";
                    }
                }
            }

            internal static string Loyalty
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "忠诚度";

                        case Utils.Localization.TraditionalChinese:
                            return "忠誠度";

                        default:
                            return "Loyalty";
                    }
                }
            }

            internal static string FoodStocks
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "食物";

                        default:
                            return "FoodStocks";
                    }
                }
            }

            internal static string Security
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "治安度";

                        default:
                            return "Security";
                    }
                }
            }

            internal static string Gold
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "金币";

                        case Utils.Localization.TraditionalChinese:
                            return "金幣";

                        default:
                            return "Gold";
                    }
                }
            }

            internal static string Governor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "所有者";

                        default:
                            return "Governor";
                    }
                }
            }

            internal static string WallLevel
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "城墙等级";

                        case Utils.Localization.TraditionalChinese:
                            return "城牆等級";

                        default:
                            return "WallLevel";
                    }
                }
            }

            internal static string Workshops
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "工坊";

                        default:
                            return "Workshops";
                    }
                }
            }

            internal static string LegendarySmith
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "传奇铁匠";

                        case Utils.Localization.TraditionalChinese:
                            return "傳奇鐵匠";

                        default:
                            return "LegendarySmith";
                    }
                }
            }

            internal static string Horse
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "马";

                        case Utils.Localization.TraditionalChinese:
                            return "馬";

                        default:
                            return "Horse";
                    }
                }
            }

            internal static string OneHandedWeapon
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "单手武器";

                        case Utils.Localization.TraditionalChinese:
                            return "單手武器";

                        default:
                            return "One Handed Weapon";
                    }
                }
            }

            internal static string TwoHandedWeapon
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "双手武器";

                        case Utils.Localization.TraditionalChinese:
                            return "雙手武器";

                        default:
                            return "Two Handed Weapon";
                    }
                }
            }

            internal static string Arrows
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "箭";

                        default:
                            return "Arrows";
                    }
                }
            }

            internal static string Bolts
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "弩箭";

                        default:
                            return "Bolts";
                    }
                }
            }

            internal static string Shield
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "盾牌";

                        default:
                            return "Shield";
                    }
                }
            }

            internal static string Thrown
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "投掷";

                        case Utils.Localization.TraditionalChinese:
                            return "投擲";

                        default:
                            return "Thrown";
                    }
                }
            }

            internal static string Goods
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "货物";

                        case Utils.Localization.TraditionalChinese:
                            return "貨物";

                        default:
                            return "Goods";
                    }
                }
            }

            internal static string HeadArmor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "头盔";

                        case Utils.Localization.TraditionalChinese:
                            return "頭盔";

                        default:
                            return "Head Armor";
                    }
                }
            }

            internal static string LegArmor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "胫甲";

                        case Utils.Localization.TraditionalChinese:
                            return "脛甲";

                        default:
                            return "Leg Armor";
                    }
                }
            }

            internal static string HandArmor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "臂甲";

                        default:
                            return "Hand Armor";
                    }
                }
            }

            internal static string Animal
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "动物";

                        case Utils.Localization.TraditionalChinese:
                            return "動物";

                        default:
                            return "Animal";
                    }
                }
            }

            internal static string Book
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "书";

                        case Utils.Localization.TraditionalChinese:
                            return "書";

                        default:
                            return "Book";
                    }
                }
            }

            internal static string ChestArmor
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "胸甲";

                        default:
                            return "Chest Armor";
                    }
                }
            }

            internal static string Cape
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "斗篷";

                        default:
                            return "Cape";
                    }
                }
            }

            internal static string HorseHarness
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "马具";

                        case Utils.Localization.TraditionalChinese:
                            return "馬具";

                        default:
                            return "Horse Harness";
                    }
                }
            }

            internal static string ClearItemDifficulty
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "清空物品的熟练度要求";

                        case Utils.Localization.TraditionalChinese:
                            return "清空物品的熟練度要求";

                        default:
                            return "Clear All Item Difficulty";
                    }
                }
            }

            internal static string UnlockLongBowForUseOnHorseBack
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "解锁长弓在马背上使用";

                        case Utils.Localization.TraditionalChinese:
                            return "解鎖長弓在馬背上使用";

                        default:
                            return "Unlock LongBow For Use On Horse Back";
                    }
                }
            }

            internal static string UnlockItemCivilian
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "解锁平民装扮";

                        case Utils.Localization.TraditionalChinese:
                            return "解鎖平民裝扮";

                        default:
                            return "Unlock Item Civilian";
                    }
                }
            }

            internal static string Marriageable
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "可结婚";

                        case Utils.Localization.TraditionalChinese:
                            return "可結婚";

                        default:
                            return "Marriageable";
                    }
                }
            }

            internal static string AddAmmo
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "添加弹药量";

                        case Utils.Localization.TraditionalChinese:
                            return "添加彈藥量";

                        default:
                            return "AddAmmo";
                    }
                }
            }

            internal static string LastKnownLocation
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "最后已知位置";

                        case Utils.Localization.TraditionalChinese:
                            return "最後已知位置";

                        default:
                            return "Last Known Location";
                    }
                }
            }

            internal static string Renamed
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "已更名为";

                        case Utils.Localization.TraditionalChinese:
                            return "已更名為";

                        default:
                            return "Renamed";
                    }
                }
            }

            internal static string PregnancyModel
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "妊娠配置";

                        case Utils.Localization.TraditionalChinese:
                            return "妊娠配寘";

                        default:
                            return "Pregnancy Model";
                    }
                }
            }

            internal static string IsFertile
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "可生育";

                        default:
                            return "Fertile";
                    }
                }
            }

            internal static string Done
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "完成";

                        default:
                            return "Done";
                    }
                }
            }

            internal static string Exception
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "异常";

                        default:
                            return "Exception";
                    }
                }
            }

            internal static string TrueString_Exist
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "存在 {0}";

                        default:
                            return "Exist {0}";
                    }
                }
            }

            internal static string FalseString_Exist
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "不存在 {0}";

                        default:
                            return "No Exist {0}";
                    }
                }
            }

            internal static string FalseString_IsFertile
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "不可生育";

                        default:
                            return "Barrenness";
                    }
                }
            }

            internal static string YouAreNowControlling_
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                        case Utils.Localization.TraditionalChinese:
                            return "正在控制: {0}";
                        default:
                            return "You are now controlling: {0}";
                    }
                }
            }

            internal static string NPCControlCanOnlyBeUsedOnTheBattlefield
            {
                get
                {
                    switch (Language)
                    {
                        case Utils.Localization.SimplifiedChinese:
                            return "npc_control 只能在战场上使用";
                        case Utils.Localization.TraditionalChinese:
                            return "npc_control 只能在戰場上使用";
                        default:
                            return "npc_control can only be used on the battlefield";
                    }
                }
            }
        }

        partial class Resources
        {
            internal static string Language
            {
                get => Utils.Localization.GetLanguage();
                set => Utils.Localization.SetLanguage(value);
            }
        }

        public static void Localization()
        {
            var itemsExcluded = Search();
            var languages = Utils.Localization.mapping_language_id__culture_name.Values;
            var dirPath = Launcher.Utils.CurrentPath;
            dirPath = Launcher.Utils.GetProjectPath(dirPath);
            dirPath = Path.Combine(dirPath, "Languages");
            var items = Resources.Properties;
            foreach (var language in languages)
            {
                Resources.Language = language;
                var stringBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("<strings>");
                foreach (var item in items)
                {
                    Resources.Language = Utils.Localization.English;
                    var enValue = item.GetValue(null);
                    Resources.Language = language;
                    if (itemsExcluded.Contains(enValue)) continue;
                    if (item.Name == "Language") continue;
                    var itemStr = $" <string name=\"{item.Name}\">{item.GetValue(null)}</string>";
                    stringBuilder.AppendLine(itemStr);
                }
                stringBuilder.AppendLine("</strings>");
                var fileContent = stringBuilder.ToString();
                var fileName = $"strings-{language}.xml";
                var filePath = Path.Combine(dirPath, fileName);
                if (File.Exists(filePath))
                {
                    if (File.ReadAllText(filePath) != fileContent)
                    {
                        File.Delete(filePath);
                    }
                    else
                    {
                        continue;
                    }
                }
                File.WriteAllText(filePath, fileContent, new UTF8Encoding(true));
            }
        }

        static readonly string[] Modules = new[] { "CustomBattle", "Native", "SandBox", "SandBoxCore", "StoryMode" };

        public static string[] Search()
        {
            var gamePath = Launcher.Utils.GetGamePath();

            var xmlDict = new Dictionary<string, Dictionary<string, string[]>>
            {
                { Utils.Localization.English, new Dictionary<string, string[]>() },
                { Utils.Localization.SimplifiedChinese, new Dictionary<string, string[]>() }
            };
            void Append(string lang, string xmlDirPath)
            {
                if (Directory.Exists(xmlDirPath))
                {
                    var files = Directory.GetFiles(xmlDirPath, "*.xml");
                    foreach (var file in files)
                    {
                        var str = File.ReadAllText(file).Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        xmlDict[lang].Add(file, str);
                    }
                }
            }
            foreach (var item in Modules)
            {
                var xmlDirPath_en = Path.Combine(gamePath, "Modules", item, "ModuleData", "Languages");
                var xmlDirPath_chs = Path.Combine(xmlDirPath_en, "CNs");
                Append(Utils.Localization.English, xmlDirPath_en);
                Append(Utils.Localization.SimplifiedChinese, xmlDirPath_chs);
            }
            var dict = Resources.Properties.ToDictionary(k => k.Name, v =>
            {
                Resources.Language = Utils.Localization.English;
                var en = v.GetValue(null)?.ToString();
                Resources.Language = Utils.Localization.SimplifiedChinese;
                var chs = v.GetValue(null)?.ToString();
                return (en, chs);
            });
            var list_en = new List<(string id, string value, string filePath, int line)>();
            var list_chs = new List<(string id, string value, string filePath, int line)>();
            foreach (var kv in dict)
            {
                foreach (var xmlFileContent in xmlDict[Utils.Localization.English])
                {
                    for (int i = 0; i < xmlFileContent.Value.Length; i++)
                    {
                        var xmlFileContentLine = xmlFileContent.Value[i];
                        const string index_id_l_mark = "id=\"";
                        var index_id_l = xmlFileContentLine.IndexOf(index_id_l_mark);
                        if (index_id_l < 0) continue;
                        var temp = xmlFileContentLine.Substring(index_id_l + index_id_l_mark.Length);
                        var index_id_r = temp.IndexOf("\"");
                        if (index_id_r < 0) continue;
                        var id = temp.Substring(0, index_id_r);
                        if (xmlFileContentLine.IndexOf($"text=\"{kv.Value.en}\"") >= 0)
                        {
                            list_en.Add((id, kv.Value.en, xmlFileContent.Key, i + 1));
                        }
                    }
                }
                foreach (var xmlFileContent in xmlDict[Utils.Localization.SimplifiedChinese])
                {
                    for (int i = 0; i < xmlFileContent.Value.Length; i++)
                    {
                        var xmlFileContentLine = xmlFileContent.Value[i];
                        const string index_id_l_mark = "id=\"";
                        var index_id_l = xmlFileContentLine.IndexOf(index_id_l_mark);
                        if (index_id_l < 0) continue;
                        var temp = xmlFileContentLine.Substring(index_id_l + index_id_l_mark.Length);
                        var index_id_r = temp.IndexOf("\"");
                        if (index_id_r < 0) continue;
                        var id = temp.Substring(0, index_id_r);
                        if (xmlFileContentLine.IndexOf($"text=\"{kv.Value.chs}\"") >= 0)
                        {
                            list_chs.Add((id, kv.Value.chs, xmlFileContent.Key, i + 1));
                        }
                    }
                }
            }
            var ids = list_en.Select(x => x.id).Intersect(list_chs.Select(x => x.id)).ToArray();
            list_en = list_en.Where(x => ids.Contains(x.id)).ToList();
            list_chs = list_chs.Where(x => ids.Contains(x.id)).ToList();
            static void Export(string fileName, List<(string id, string value, string filePath, int line)> data)
            {
                var stringBuilder = new StringBuilder("id,value,filePath,line");
                stringBuilder.AppendLine();
                foreach (var (id, value, filePath, line) in data)
                {
                    stringBuilder.AppendLine($"{id},{value},{filePath},{line}");
                }
                var result_search_path = Path.Combine(Launcher.Utils.CurrentPath, fileName);
                if (File.Exists(result_search_path)) File.Delete(result_search_path);
                File.WriteAllText(result_search_path, stringBuilder.ToString(), new UTF8Encoding(true));
            }
            Export("result_search_en.csv", list_en);
            Export("result_search_chs.csv", list_chs);
            return list_en.Select(x => x.value).Distinct().ToArray();
        }
    }
}