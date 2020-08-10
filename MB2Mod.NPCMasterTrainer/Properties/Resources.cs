using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace MB2Mod.NPCMasterTrainer.Properties
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

        static string GetResString(Func<string> planB, [CallerMemberName] string name = null)
        {
            var value = Utils.Localization.GetStringByXmlFiles(name); // planA
            if (string.IsNullOrWhiteSpace(value))
            {
                value = planB?.Invoke(); // planB
            }
            return value;
        }

        static string GetResString(string id, Func<string> planB, [CallerMemberName] string name = null)
            => GetResString(new[] { id }, planB, name);

        internal static string GetStringByTextObject(string id)
        {
            try
            {
                var value = LocalizedTextManager.GetTranslatedText(BannerlordConfig.Language, id);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }
            catch (Exception e)
            {
                Utils.DisplayMessage(e);
            }
            return null;
        }

        static string GetResString(string[] ids, Func<string> planB, [CallerMemberName] string name = null)
        {
            foreach (var id in ids)
            {
                var value = GetStringByTextObject(id);
                if (value != null)
                {
                    return value;
                }
            }
            return GetResString(planB, name);
        }

        internal static string LoadedDeveloperConsoleInfoMessage => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "已加载 DeveloperConsole。按 CTRL 和 ~ 启用。",
            Utils.Localization.TraditionalChinese => "已加載 DeveloperConsole。按 CTRL 和 ~ 啟用。",
            _ => "Loaded DeveloperConsole. Press CTRL and ~ to enable it.",
        });

        internal static string UnspentStatFocus => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "可用点数：{0} 属性点 | {1} 专精点",
            Utils.Localization.TraditionalChinese => "可用點數：{0} 屬性點 | {1} 專精點",
            _ => "Unspent: {0} stat | {1} focus",
        });

        internal static string Name => GetResString("PDdh1sBj", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "名称",
            Utils.Localization.TraditionalChinese => "名稱",
            _ => "Name",
        });

        internal static string Gender => GetResString("fGFMqlGz", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "性别";
                default:
                    return "Gender";
            }
        });

        internal static string Female => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "女";
                default:
                    return "Female";
            }
        });

        internal static string Male => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "男";
                default:
                    return "Male";
            }
        });

        internal static string Culture => GetResString("PUjDWe5j", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "文化";
                default:
                    return "Culture";
            }
        });

        internal static string Level => GetResString(new[] { "OKUTPdaa", "DzviXC3W" }, () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "等级",
            Utils.Localization.TraditionalChinese => "等級",
            _ => "Level",
        });

        internal static string Generosity => GetResString("IuWu5Bu7", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "胸怀",
            Utils.Localization.TraditionalChinese => "胸懷",
            _ => "Generosity",
        });

        internal static string Honor => GetResString("0oGz5rVx", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "荣誉",
            Utils.Localization.TraditionalChinese => "榮譽",
            _ => "Honor",
        });

        internal static string Valor => GetResString("toQLHG6x", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "胆气",
            Utils.Localization.TraditionalChinese => "膽氣",
            _ => "Valor",
        });

        internal static string Mercy => GetResString("2I2uKJlw", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "善恶";
                default:
                    return "Mercy";
            }
        });

        internal static string Calculating => GetResString(() => "Calculating");

        internal static string OneHanded => GetResString("PiHpR4QL", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "单手",
            Utils.Localization.TraditionalChinese => "單手",
            _ => "One Handed",
        });

        internal static string TwoHanded => GetResString("t78atYqH", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "双手",
            Utils.Localization.TraditionalChinese => "雙手",
            _ => "Two Handed",
        });

        internal static string Polearm => GetResString("haax8kMa", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "长杆",
            Utils.Localization.TraditionalChinese => "長杆",
            _ => "Polearm",
        });

        internal static string Bow => GetResString("5rj7xQE4", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "弓";
                default:
                    return "Bow";
            }
        });

        internal static string Crossbow => GetResString("TTWL7RLe", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "弩";

                default:
                    return "Crossbow";
            }
        });

        internal static string Throwing => GetResString("2wclahIJ", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "投掷",
            Utils.Localization.TraditionalChinese => "投擲",
            _ => "Throwing",
        });

        internal static string Riding => GetResString("p9i3zRm9", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "骑术",
            Utils.Localization.TraditionalChinese => "騎術",
            _ => "Riding",
        });

        internal static string Athletics => GetResString("skZS2UlW", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "跑动",
            Utils.Localization.TraditionalChinese => "體能",
            _ => "Athletics",
        });

        internal static string Smithing => GetResString(new[] { "smithingverb", "smithingskill" }, () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "锻造",
            Utils.Localization.TraditionalChinese => "鍛造",
            _ => "Smithing",
        });

        internal static string Scouting => GetResString("LJ6Krlbr", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "侦察",
            Utils.Localization.TraditionalChinese => "偵察",
            _ => "Scouting",
        });

        internal static string Tactics => GetResString("m8o51fc7", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "战术",
            Utils.Localization.TraditionalChinese => "戰術",
            _ => "Tactics",
        });

        internal static string Roguery => GetResString("V0ZMJ0PX", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "流氓习气",
            Utils.Localization.TraditionalChinese => "流氓習氣",
            _ => "Roguery",
        });

        internal static string Charm => GetResString("EGeY1gfs", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "魅力";

                default:
                    return "Charm";
            }
        });

        internal static string Leadership => GetResString("HsLfmEmb", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "统御",
            Utils.Localization.TraditionalChinese => "統禦",
            _ => "Leadership",
        });

        internal static string Trade => GetResString("GmcgoiGy", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "交易";

                default:
                    return "Trade";
            }
        });

        internal static string Steward => GetResString("Ymw2PKBo", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "管理";

                default:
                    return "Steward";
            }
        });

        internal static string Medicine => GetResString("JKH59XNp", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "医术",
            Utils.Localization.TraditionalChinese => "醫術",
            _ => "Medicine",
        });

        internal static string Engineering => GetResString("PCZ23wvW", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "工程";

                default:
                    return "Engineering";
            }
        });

        [Obsolete("use Profession.", true)]
        internal static string Occupation => Profession;

        internal static string Profession => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "职业",
            Utils.Localization.TraditionalChinese => "職業",
            _ => "Profession",
        });

        internal static string Age => GetResString("jaaQijQs", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "年龄",
            Utils.Localization.TraditionalChinese => "年齡",
            _ => "Age",
        });

        internal static string Vigor => GetResString("YWkdD7Ki", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "活力";

                default:
                    return "Vigor";
            }
        });

        internal static string Control => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "控制";

                default:
                    return "Control";
            }
        });

        internal static string Endurance => GetResString("kvOavzcs", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "耐力";

                default:
                    return "Endurance";
            }
        });

        internal static string Cunning => GetResString("JZM1mQvb", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "狡诈",
            Utils.Localization.TraditionalChinese => "狡詐",
            _ => "Cunning",
        });

        internal static string Social => GetResString(new[] { "socialskill", "socialtab" }, () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "社交";

                default:
                    return "Social";
            }
        });

        internal static string Intelligence => GetResString("sOrJoxiC", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "智力";

                default:
                    return "Intelligence";
            }
        });

        internal static string Focus => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "专精点",
            Utils.Localization.TraditionalChinese => "專精點",
            _ => "Focus",
        });

        internal static string UnspentAttributePoints => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "可用属性点",
            Utils.Localization.TraditionalChinese => "可用屬性點",
            _ => "Unspent Attribute Points",
        });

        internal static string UnspentFocusPoints => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "可用专精点",
            Utils.Localization.TraditionalChinese => "可用專精點",
            _ => "Unspent Focus Points",
        });

        internal static string Noble => GetResString("vmMqs3Ck", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "贵族",
            Utils.Localization.TraditionalChinese => "貴族",
            _ => "Noble",
        });

        internal static string Wanderer => GetResString("FLa5OuyK", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "流浪者";

                default:
                    return "Wanderer";
            }
        });

        internal static string Artisan => GetResString("qfzkMuLj", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "工匠";

                default:
                    return "Artisan";
            }
        });

        internal static string IsChild => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "是孩子";

                default:
                    return "Is Child";
            }
        });

        internal static string IsAlive => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "还活着",
            Utils.Localization.TraditionalChinese => "還活著",
            _ => "Is Alive",
        });

        internal static string FirstName => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "名字";

                default:
                    return "First Name";
            }
        });

        internal static string HeroesData => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "角色数据",
            Utils.Localization.TraditionalChinese => "角色數據",
            _ => "HeroesData",
        });

        internal static string All => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "所有";

                default:
                    return "All";
            }
        });

        internal static string TownsData => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "城镇数据",
            Utils.Localization.TraditionalChinese => "城鎮數據",
            _ => "TownsData",
        });

        internal static string Prosperity => GetResString("IagYTD5O", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "繁荣度",
            Utils.Localization.TraditionalChinese => "繁榮度",
            _ => "Prosperity",
        });

        internal static string Militia => GetResString("gsVtO9A7", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "民兵";

                default:
                    return "Militia";
            }
        });

        internal static string Loyalty => GetResString("YO0x7ZAo", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "忠诚度",
            Utils.Localization.TraditionalChinese => "忠誠度",
            _ => "Loyalty",
        });

        internal static string FoodStocks => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "食物";

                default:
                    return "FoodStocks";
            }
        });

        internal static string Security => GetResString("MqCH7R4A", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "治安度";

                default:
                    return "Security";
            }
        });

        internal static string Gold => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "金币",
            Utils.Localization.TraditionalChinese => "金幣",
            _ => "Gold",
        });

        internal static string Governor => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "所有者";

                default:
                    return "Governor";
            }
        });

        internal static string WallLevel => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "城墙等级",
            Utils.Localization.TraditionalChinese => "城牆等級",
            _ => "WallLevel",
        });

        internal static string Workshops => GetResString("NbgeKwVr", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "工坊";

                default:
                    return "Workshops";
            }
        });

        internal static string LegendarySmith => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "传奇铁匠",
            Utils.Localization.TraditionalChinese => "傳奇鐵匠",
            _ => "LegendarySmith",
        });

        internal static string Horse => GetResString("LwfILaRH", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "马",
            Utils.Localization.TraditionalChinese => "馬",
            _ => "Horse",
        });

        internal static string OneHandedWeapon => GetResString("iFg5N7r3", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "单手武器",
            Utils.Localization.TraditionalChinese => "單手武器",
            _ => "One Handed Weapon",
        });

        internal static string TwoHandedWeapon => GetResString("baHmYsx3", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "双手武器",
            Utils.Localization.TraditionalChinese => "雙手武器",
            _ => "Two Handed Weapon",
        });

        internal static string Arrows => GetResString("gFtu0j4T", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "箭";

                default:
                    return "Arrows";
            }
        });

        internal static string Bolts => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "弩箭";

                default:
                    return "Bolts";
            }
        });

        internal static string Shield => GetResString("Jd0Kq9lD", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "盾牌";

                default:
                    return "Shield";
            }
        });

        internal static string Thrown => GetResString("MW8rlyfL", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "投掷",
            Utils.Localization.TraditionalChinese => "投擲",
            _ => "Thrown",
        });

        internal static string Goods => GetResString("I2YhWaIt", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "货物",
            Utils.Localization.TraditionalChinese => "貨物",
            _ => "Goods",
        });

        internal static string HeadArmor => GetResString("O3dhjtOS", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "头盔",
            Utils.Localization.TraditionalChinese => "頭盔",
            _ => "Head Armor",
        });

        internal static string LegArmor => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "胫甲",
            Utils.Localization.TraditionalChinese => "脛甲",
            _ => "Leg Armor",
        });

        internal static string HandArmor => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "臂甲";

                default:
                    return "Hand Armor";
            }
        });

        internal static string Animal => GetResString("7a3aryUB", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "动物",
            Utils.Localization.TraditionalChinese => "動物",
            _ => "Animal",
        });

        internal static string Book => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "书",
            Utils.Localization.TraditionalChinese => "書",
            _ => "Book",
        });

        internal static string ChestArmor => GetResString("oiSW6MyB", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "胸甲";

                default:
                    return "Chest Armor";
            }
        });

        internal static string Cape => GetResString("k8QpbFnj", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "斗篷";

                default:
                    return "Cape";
            }
        });

        internal static string HorseHarness => GetResString("b5t34yLX", () => Language switch
        {
            Utils.Localization.SimplifiedChinese => "马具",
            Utils.Localization.TraditionalChinese => "馬具",
            _ => "Horse Harness",
        });

        internal static string ClearItemDifficulty => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "清空物品的熟练度要求",
            Utils.Localization.TraditionalChinese => "清空物品的熟練度要求",
            _ => "Clear All Item Difficulty",
        });

        internal static string UnlockLongBowForUseOnHorseBack => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "解锁长弓在马背上使用",
            Utils.Localization.TraditionalChinese => "解鎖長弓在馬背上使用",
            _ => "Unlock LongBow For Use On Horse Back",
        });

        internal static string UnlockItemCivilian => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "解锁平民装扮",
            Utils.Localization.TraditionalChinese => "解鎖平民裝扮",
            _ => "Unlock Item Civilian",
        });

        internal static string Marriageable => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "可结婚",
            Utils.Localization.TraditionalChinese => "可結婚",
            _ => "Marriageable",
        });

        internal static string AddAmmo => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "添加弹药量",
            Utils.Localization.TraditionalChinese => "添加彈藥量",
            _ => "AddAmmo",
        });

        internal static string LastKnownLocation => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "最后已知位置",
            Utils.Localization.TraditionalChinese => "最後已知位置",
            _ => "Last Known Location",
        });

        internal static string Renamed => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "已更名为",
            Utils.Localization.TraditionalChinese => "已更名為",
            _ => "Renamed",
        });

        internal static string PregnancyModel => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "妊娠配置",
            Utils.Localization.TraditionalChinese => "妊娠配寘",
            _ => "Pregnancy Model",
        });

        internal static string IsFertile => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "可生育";

                default:
                    return "Fertile";
            }
        });

        internal static string Done => GetResString("WiNRdfsm", () =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "完成";

                default:
                    return "Done";
            }
        });

        internal static string Exception => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "异常";

                default:
                    return "Exception";
            }
        });

        internal static string TrueString_Exist => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "存在 {0}";

                default:
                    return "Exist {0}";
            }
        });

        internal static string FalseString_Exist => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "不存在 {0}";

                default:
                    return "No Exist {0}";
            }
        });

        internal static string FalseString_IsFertile => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "不可生育";

                default:
                    return "Barrenness";
            }
        });

        internal static string YouAreNowControlling_ => GetResString(() =>
        {
            switch (Language)
            {
                case Utils.Localization.SimplifiedChinese:
                case Utils.Localization.TraditionalChinese:
                    return "正在控制: {0}";

                default:
                    return "You are now controlling: {0}";
            }
        });

        internal static string NPCControlCanOnlyBeUsedOnTheBattlefield => GetResString(() => Language switch
        {
            Utils.Localization.SimplifiedChinese => "npc_control 只能在战场上使用",
            Utils.Localization.TraditionalChinese => "npc_control 只能在戰場上使用",
            _ => "npc_control can only be used on the battlefield",
        });

        //internal static string Template => GetResString(null);
    }
}

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static string Format(string s, params object[] args)
        {
            try
            {
                return string.Format(s, args);
            }
            catch
            {
                return s + " " + string.Join(", ", args);
            }
        }
    }
}