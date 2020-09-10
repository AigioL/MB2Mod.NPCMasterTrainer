using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

#pragma warning disable IDE0060 // 删除未使用的参数

namespace MB2Mod.NPCMasterTrainer
{
    public static partial class ConsoleCommand
    {
        #region npc

        /// <summary>
        /// npc.reset_perks_check_smith <see langword="true"/> 重置npc 技能点时，将检测铁匠系提升属性点或专精点的技能点，并退还相应的属性点或专精点
        /// <para><see cref="Utils.reset_perks_check_smith"/> 默认值：<see langword="false"/></para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("reset_perks_check_smith", "npc")]
        public static string SetResetPerksCheckSmith(List<string> args)
        {
            Utils.reset_perks_check_smith = string.Equals(args.FirstOrDefault(), bool.TrueString, StringComparison.OrdinalIgnoreCase);
            return Utils.Done;
        }

        /// <summary>
        /// npc.reset_perks [name] 重置npc 技能点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("reset_perks", "npc")]
        public static string ResetHeroPerks(List<string> args)
        {
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, Utils.ResetPerks);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.reset_focus [name] 重置npc 专精点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("reset_focus", "npc")]
        public static string ResetHeroFocus(List<string> args)
        {
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, Utils.ResetFocus);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.reset_attrs [name] 重置npc 属性点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("reset_attrs", "npc")]
        public static string ResetHeroAttrs(List<string> args)
        {
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, Utils.ResetAttrs);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.reset [name] 重置npc 技能点/专精点/属性点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("reset", "npc")]
        public static string ResetHero(List<string> args)
        {
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, Utils.Reset);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.check_legendary_smith [name] 检查是否有传奇铁匠技能点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("check_legendary_smith", "npc")]
        public static string CheckLegendarySmith(List<string> args)
        {
            static bool LegendarySmithExist(Hero hero) => DefaultPerks.Crafting.LegendarySmith.Exist(hero);
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultBoolean(heroes, LegendarySmithExist,
                Resources.LegendarySmith, Resources.TrueString_Exist, Resources.FalseString_Exist);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.add_perk_legendary_smith [name] 点上传奇铁匠技能点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("add_perk_legendary_smith", "npc")]
        public static string AddPerkLegendarySmith(List<string> args)
        {
            static bool LegendarySmithAdd(Hero hero) => DefaultPerks.Crafting.LegendarySmith.Add(hero);
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultBoolean(heroes, LegendarySmithAdd,
                Resources.LegendarySmith, Resources.TrueString_Exist, Resources.FalseString_Exist);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.refresh_last_seen_location 刷新最后一次见到的位置
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("refresh_last_seen_location", "npc")]
        public static string RefreshHeroLastSeenLocation(List<string> args)
        {
            var heroes = Hero.FindAll(x => (x.IsNoble || x.IsWanderer) && x.IsAlive);
            return Utils.HandleResultVoid(heroes, Action, displayMessage: false);
            static void Action(Hero hero) => hero.SetHeroLastSeenLocation(true);
        }

        /// <summary>
        /// npc.random_body [name] 给指定角色重新随机生成一个新的捏脸数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("random_body", "npc")]
        public static string RandomBody(List<string> args)
        {
            static StaticBodyProperties? GetRandomBodyProperties(Hero hero)
            {
                if (hero == default) return default;
                var template = hero.Template;
                if (template == default) return default;
                var newCharacter = hero.CharacterObject;
                if (newCharacter == default) return default;
                // TaleWorlds.CampaignSystem.HeroCreator.CreateNewHero
                var staticBodyProperties = BodyProperties.GetRandomBodyProperties(
                    template.IsFemale,
                    template.GetBodyPropertiesMin(false),
                    template.GetBodyPropertiesMax(),
                    0,
                    MBRandom.RandomInt(),
                    newCharacter.HairTags,
                    newCharacter.BeardTags,
                    newCharacter.TattooTags).StaticProperties;
                return staticBodyProperties;
            }
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, hero =>
            {
                var body = GetRandomBodyProperties(hero);
                if (body.HasValue)
                {
                    hero.SetStaticBodyProperties(body.Value);
                }
                else
                {
                    Utils.DisplayMessage($"RandomBody Fail, Hero: {hero?.Name}");
                }
            });
            return Utils.HandleSearchHeroes(args, HandleHeroes, inMyTroops: false);
        }

        /// <summary>
        /// npc.change_body [name] 更改指定角色的捏脸数据，需将捏脸数据复制到剪贴板后执行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("change_body", "npc")]
        public static string ChangeBody(List<string> args)
        {
            var text = Utils.Clipboard.GetTextOrEmpty();
            if (string.IsNullOrWhiteSpace(text)) return Utils.NotFound;
            bool parseResult;
            BodyProperties bodyProperties;
            try
            {
                parseResult = BodyProperties.FromString(text, out bodyProperties);
            }
            catch
            {
                bodyProperties = default;
                parseResult = false;
            }
            if (!parseResult) return "BodyProperties Incorrect Format";
            string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, hero => hero.SetStaticBodyProperties(bodyProperties.StaticProperties));
            return Utils.HandleSearchHeroes(args, HandleHeroes, inMyTroops: false);
        }

        /// <summary>
        /// npc.fill_up [name] | [num?] 加满npc的所有
        /// <para>example: </para>
        /// <para>npc.fill_up all</para>
        /// <para>npc.fill_up all | 999</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("fill_up", "npc")]
        public static string FillUpHero(List<string> args)
        {
            if (!Utils.CheatMode) return "Cheat mode disabled!";
            var args2 = args.SplitArgments();
            if (args2.Any())
            {
                var maxSkillValue = int.TryParse(args2.LastOrDefault().LastOrDefault(), out var number) ? number : (int?)null;
                string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, h => Utils.FillUp(h, maxSkillValue));
                return Utils.HandleSearchHeroes(args2.First(), HandleHeroes);
            }
            return Utils.NotFound;
        }

        /// <summary>
        /// npc.clone [name] | [count?] 克隆npc数量(不能克隆自己，因为克隆出来的玩家不会被AI控制)
        /// <para>已知问题：</para>
        /// <para>使用克隆出多个后，不可指派总督，创建新部队，否则会崩溃，需将count设为1恢复单个角色后再进行这些操作。</para>
        /// <para>克隆数量太多，疑似超过部队最大数量，会在战场上崩溃，或坐镇卡死。</para>
        /// <para>example: </para>
        /// <para>npc.clone noble</para>
        /// <para>npc.clone wanderer | 10</para>
        /// <para>npc.clone all_not_me | 15</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("clone", "npc")]
        public static string CloneHero(List<string> args)
        {
            try
            {
                var troopRoster = Campaign.Current?.MainParty.MemberRoster;
                if (troopRoster != default)
                {
                    if (troopRoster.TotalWoundedHeroes > 0) return "Fail, You can't have wounded heroes in your troops.";
                    var args2 = args.SplitArgments();
                    if (args2.Any())
                    {
                        var count = ushort.TryParse(args2.LastOrDefault().LastOrDefault(), out var number) ? number : 1;
                        if (count <= 0) count = 1;
                        string HandleHeroes(IEnumerable<Hero> heroes)
                        {
                            foreach (var hero in heroes)
                            {
                                int index = -2, countChange = index;
                                TroopRosterElement item = default;
                                bool isNegativeCountChange = default;
                                try
                                {
                                    index = troopRoster.FindIndexOfTroop(hero.CharacterObject);
                                    if (index >= 0 && index < troopRoster.Count)
                                    {
                                        item = troopRoster.GetElementCopyAtIndex(index);
                                        countChange = count - item.Number;
                                        //var hasWounded = item.WoundedNumber > 0;
                                        isNegativeCountChange = countChange < 0;
                                        if (isNegativeCountChange) countChange -= 1;
                                        /*, woundedCountChange: hasWounded ? -item.WoundedNumber : 0*/
                                        troopRoster.AddToCountsAtIndex(index, countChange, removeDepleted: false);
                                        // ---- isNegativeCountChange Fix ----
                                        // if (this.OwnerParty != null & isHero)
                                        // else if (countChange < 0)
                                        // if (!this.IsPrisonRoster)
                                        // this.OwnerParty.OnHeroRemoved(character.HeroObject);
                                        // -- in AddToCountsAtIndex(int index, int countChange, int woundedCountChange = 0, int xpChange = 0, bool removeDepleted = true) --
                                        if (isNegativeCountChange) troopRoster.AddToCountsAtIndex(index, 1, removeDepleted: false);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Utils.DisplayMessage(e);
                                }
                                Utils.DisplayMessage($"NPC Clone: {hero?.Name}({(isNegativeCountChange ? countChange + 1 : countChange)})");
                            }
                            return Utils.Done;
                        }
                        return Utils.HandleSearchHeroes(args2.First(), HandleHeroes, excludeMe: true);
                    }
                }
                return Utils.NotFound;
            }
            catch (Exception e)
            {
                Utils.DisplayMessage(e);
                return Utils.Catch;
            }
        }

        /// <summary>
        /// npc.remove_attrs [name] | [attrType] | [value] 移除玩家部队中角色的属性并返还到可用点数
        /// <para>attrType: Vigor(1), Control(2), Endurance(3), Cunning(4), Social(5), Intelligence(6)</para>
        /// <para>example: </para>
        /// <para>npc.remove_attrs me | 1 | 1</para>
        /// <para>npc.remove_attrs me | Vigor | 1</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("remove_attrs", "npc")]
        public static string RemoveHeroAttrs(List<string> args)
        {
            try
            {
                var args2 = args.SplitArgments();
                if (args2.Count == 3)
                {
                    var attrTypeInputText = args2[1].LastOrDefault();
                    var attrType = (CharacterAttributesEnum)(ushort.TryParse(attrTypeInputText, out var number) ?
                        number - 1 :
                        (Enum.TryParse<CharacterAttributesEnum>(attrTypeInputText, true, out var @enum) ?
                        (int)@enum : -1));
                    var value = ushort.TryParse(args2[2].LastOrDefault(), out var number2) ? number2 : 0;
                    if (value <= 0) return "value must be greater than zero.";
                    if (value > HeroDeveloper.MaxAttribute) return $"value must be less than or equal to {HeroDeveloper.MaxAttribute}";
                    if (attrType >= CharacterAttributesEnum.First && attrType < CharacterAttributesEnum.End)
                    {
                        string HandleHeroes(IEnumerable<Hero> heroes)
                        {
                            foreach (var hero in heroes)
                            {
                                var value2 = value;
                                var currentAttrValue = hero.GetAttributeValue(attrType);
                                if (currentAttrValue >= 1) // 属性必须保留至少一点
                                {
                                    if (value2 >= currentAttrValue) value2 = currentAttrValue - 1;
                                    var tempValue = currentAttrValue - value2;
                                    hero.SetAttributeValue(attrType, tempValue);
                                    if (hero.GetAttributeValue(attrType) == tempValue)
                                    {
                                        hero.HeroDeveloper.UnspentAttributePoints += value2;
                                    }
                                    Utils.DisplayMessage(
                                        $"{hero.Name?.ToString()} " +
                                        $"{attrType.ToString2()} {currentAttrValue}=>{tempValue}");
                                }
                            }
                            return Utils.Done;
                        }
                        return Utils.HandleSearchHeroes(args2.First(), HandleHeroes);
                    }
                    else
                    {
                        return $"attrType must be greater than or equal to {(int)CharacterAttributesEnum.First} and less than {(int)CharacterAttributesEnum.End}";
                    }
                }
                return Utils.NotFound;
            }
            catch (Exception e)
            {
                Utils.DisplayMessage(e);
                return Utils.Catch;
            }
        }

        /// <summary>
        /// npc.remove_focus [name] | [row] | [column] | [value] 移除玩家部队中角色的专精并返还到可用点数
        /// <para>row: 1~6</para>
        /// <para>column: 1~3</para>
        /// <para>example: </para>
        /// <para>npc.remove_focus me | 2 | 1 | 1</para>
        /// <para>npc.remove_focus me | control | 1 | 1</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("remove_focus", "npc")]
        public static string RemoveHeroFocus(List<string> args)
        {
            try
            {
                var args2 = args.SplitArgments();
                if (args2.Count == 4)
                {
                    var rowInputText = args2[1].LastOrDefault();
                    var row = ushort.TryParse(args2[1].LastOrDefault(), out var row_num) ?
                        row_num : (Enum.TryParse<CharacterAttributesEnum>(rowInputText, true, out var @enum) ?
                        (int)@enum : -1);
                    if (!(row >= 1 && row <= 6)) return $"row(1~6) must be greater than or equal to 1 and less than or equal to 6";
                    var column = ushort.TryParse(args2[2].LastOrDefault(), out var column_num) ? column_num : -1;
                    if (!(column >= 1 && column <= 3)) return $"column(1~3) must be greater than or equal to 1 and less than or equal to 3";
                    var skill = Utils.GetSkillObject(row, column);
                    var value = ushort.TryParse(args2[3].LastOrDefault(), out var value_num) ? value_num : 0;
                    if (value <= 0) return "value must be greater than zero.";
                    if (value > HeroDeveloper.MaxFocusPerSkill) return $"value must be less than or equal to {HeroDeveloper.MaxFocusPerSkill}";
                    if (skill != default)
                    {
                        string HandleHeroes(IEnumerable<Hero> heroes)
                        {
                            foreach (var hero in heroes)
                            {
                                var currentValue = hero.HeroDeveloper.GetFocus(skill);
                                var value2 = value;
                                if (value2 > currentValue) value2 = currentValue;
#if DEBUG
                                Console.WriteLine(
                                    $"skill: {skill.Name?.ToString()}, " +
                                    $"currentValue: {currentValue}, " +
                                    $"value2: {value2}, " +
                                    $"value: {value}");
#endif
                                if (value2 > 0) // 专精可以全部返还
                                {
                                    var newValue = currentValue - value2;
#if DEBUG
                                    Console.WriteLine($"newValue: {newValue}");
#endif
                                    hero.HeroDeveloper.AddFocus(skill, -value2, false);
                                    if (hero.HeroDeveloper.GetFocus(skill) == newValue)
                                    {
                                        hero.HeroDeveloper.UnspentFocusPoints += value2;
                                    }
                                    Utils.DisplayMessage(
                                        $"{hero.Name?.ToString()} " +
                                        $"{skill.Name?.ToString()} {currentValue}=>{newValue}");
                                }
                            }
                            return Utils.Done;
                        }
                        return Utils.HandleSearchHeroes(args2.First(), HandleHeroes);
                    }
                }
                return Utils.NotFound;
            }
            catch (Exception e)
            {
                Utils.DisplayMessage(e);
                return Utils.Catch;
            }
        }

        /// <summary>
        /// npc.remove_focus_by_entire_line [name] | [row] | [value] 移除玩家部队中角色的某一整行的专精并返还到可用点数
        /// <para>row: 1~6</para>
        /// <para>example: </para>
        /// <para>npc.remove_focus_by_entire_line me | 2 | 1</para>
        /// <para>npc.remove_focus_by_entire_line me | control | 1</para>
        /// <para>npc.remove_focus_by_entire_line me | 1 | 5</para>
        /// <para>npc.remove_focus_by_entire_line all | 1 | 5</para>
        /// <para>npc.remove_focus_by_entire_line all | 2 | 5</para>
        /// <para>npc.remove_focus_by_entire_line all | 3 | 5</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("remove_focus_by_entire_line", "npc")]
        public static string RemoveHeroFocusByEntireLine(List<string> args)
        {
            try
            {
                var args2 = args.SplitArgments();
                if (args2.Count == 3)
                {
                    var rowInputText = args2[1].LastOrDefault();
                    var row = ushort.TryParse(args2[1].LastOrDefault(), out var row_num) ?
                        row_num : (Enum.TryParse<CharacterAttributesEnum>(rowInputText, true, out var @enum) ?
                        (int)@enum : -1);
                    if (!(row >= 1 && row <= 6)) return $"row(1~6) must be greater than or equal to 1 and less than or equal to 6";
                    var value = ushort.TryParse(args2[2].LastOrDefault(), out var value_num) ? value_num : 0;
                    if (value <= 0) return "value must be greater than zero.";
                    if (value > HeroDeveloper.MaxFocusPerSkill) return $"value must be less than or equal to {HeroDeveloper.MaxFocusPerSkill}";
                    var skills = new int[3].Select((x, i) => Utils.GetSkillObject(row, i + 1));
                    return Utils.HandleSearchHeroes(args2.First(), HandleHeroes);
                    string HandleHeroes(IEnumerable<Hero> heroes)
                    {
                        foreach (var hero in heroes)
                        {
                            foreach (var skill in skills)
                            {
                                if (skill != default)
                                {
                                    var currentValue = hero.HeroDeveloper.GetFocus(skill);
                                    var value2 = value;
                                    if (value2 > currentValue) value2 = currentValue;
#if DEBUG
                                    Console.WriteLine(
                                        $"skill: {skill.Name?.ToString()}, " +
                                        $"currentValue: {currentValue}, " +
                                        $"value2: {value2}, " +
                                        $"value: {value}");
#endif
                                    if (value2 > 0) // 专精可以全部返还
                                    {
                                        var newValue = currentValue - value2;
#if DEBUG
                                        Console.WriteLine($"newValue: {newValue}");
#endif
                                        hero.HeroDeveloper.AddFocus(skill, -value2, false);
                                        if (hero.HeroDeveloper.GetFocus(skill) == newValue)
                                        {
                                            hero.HeroDeveloper.UnspentFocusPoints += value2;
                                        }
                                        Utils.DisplayMessage(
                                            $"{hero.Name?.ToString()} " +
                                            $"{skill.Name?.ToString()} {currentValue}=>{newValue}");
                                    }
                                }
                            }
                        }
                        return Utils.Done;
                    }
                }
                return Utils.NotFound;
            }
            catch (Exception e)
            {
                Utils.DisplayMessage(e);
                return Utils.Catch;
            }
        }

        #endregion

        #region export csv

        [CommandLineFunctionality.CommandLineArgumentFunction("query_path", "export_csv")]
        public static string ExportQueryPath(List<string> args)
        {
            var path = Utils.ExportDirectory;
            Utils.DisplayMessage(path);
            return path;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("open_dir", "export_csv")]
        public static string ExportOpenDir(List<string> args)
        {
            if (Utils.IsWindows)
            {
                var path = Utils.ExportDirectory;
                Process.Start("explorer.exe", path);
            }
            return Utils.Done;
        }

        /// <summary>
        /// 导出流浪者数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("wanderers", "export_csv")]
        public static string ExportWanderers(List<string> args)
        {
            var result = Utils.Export(Utils.Wanderers, Resources.Wanderer);
            return result.HasValue ? (result.Value ? Utils.Done : Utils.NotFound) : Utils.Catch;
        }

        /// <summary>
        /// 导出贵族数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("nobles", "export_csv")]
        public static string ExportNobles(List<string> args)
        {
            var result = Utils.Export(Utils.Nobles, Resources.Noble);
            return result.HasValue ? (result.Value ? Utils.Done : Utils.NotFound) : Utils.Catch;
        }

        /// <summary>
        /// 导出所有角色数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("all_hero", "export_csv")]
        public static string ExportAllHero(List<string> args)
        {
            var result = Utils.Export(Hero.All, Resources.All);
            return result.HasValue ? (result.Value ? Utils.Done : Utils.NotFound) : Utils.Catch;
        }

        /// <summary>
        /// 导出城镇数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("all_towns", "export_csv")]
        public static string ExportAllTowns(List<string> args)
        {
            var result = Utils.Export(Town.AllTowns, Resources.All);
            return result.HasValue ? (result.Value ? Utils.Done : Utils.NotFound) : Utils.Catch;
        }

        /// <summary>
        /// 输出繁荣度最高的几个城市
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("towns_name_prosperity_desc", "print")]
        public static string PrintTownsNameByProsperityOrderByDesc(List<string> args)
        {
            ushort count = 8;
            if (args != null && args.Any() && ushort.TryParse(args.FirstOrDefault(), out var number) && number > 0) count = number;
            var towns = Town.AllTowns.Where(x => x != null).OrderByDescending(x => x.Prosperity).Take(count);
            var any_towns = false;
            foreach (var item in towns)
            {
                if (!any_towns)
                {
                    Utils.DisplayMessage($"---- {Resources.Prosperity} ----");
                }
                Utils.DisplayMessage($"{item.Name} {item.Prosperity}");
                any_towns = true;
            }
            if (any_towns)
            {
                if (Utils.Config.Instance.HasWin32Console()) Console.WriteLine();
                return Utils.Done;
            }
            return Utils.NotFound;
        }

        #endregion

        #region rename

        /// <summary>
        /// rename.children [num] 玩家的第 num 个孩子重命名(num从1开始)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("children", "rename")]
        public static string ReNameChildren(List<string> args)
        {
            if (Campaign.Current == null) return Utils.CampaignIsNull;
            var me = Hero.MainHero;
            if (me == default || me.Children == default || !me.Children.Any() ||
                !int.TryParse(args?.FirstOrDefault(), out var indexStartForOne) || indexStartForOne > me.Children.Count || indexStartForOne <= 0) return Utils.NotFound;
            var child = me.Children[indexStartForOne - 1];
            var newName = Utils.Clipboard.GetTextOrEmpty();
            child.ReName(newName);
            return Utils.Done;
        }

        #endregion

        #region pregnancy

        /// <summary>
        /// 输出当前妊娠配置参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("pregnancy_model", "print")]
        public static string PrintPregnancyModel(List<string> args)
        {
            return Utils.NPCMT_PregnancyModel.Print() ? Utils.Done : Utils.NotFound;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("npcmt_model", "print")]
        public static string PrintModel(List<string> args)
        {
            return (Utils.NPCMT_PregnancyModel.Print(true) &&
            Utils.NPCMT_ClanTierModel.Print() &&
            Utils.NPCMT_WorkshopModel.Print()) ? Utils.Done : Utils.NotFound;
        }

        /// <summary>
        /// npc.check_is_fertile 检查角色是否可生育
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("check_is_fertile", "npc")]
        public static string CheckIsFertile(List<string> args)
        {
            static bool IsFertile(Hero hero) => hero.IsFertile;
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultBoolean(heroes, IsFertile,
                null, Resources.IsFertile, Resources.FalseString_IsFertile);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.set_is_fertile_true 设置角色可生育
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("set_is_fertile_true", "npc")]
        public static string SetIsFertileTrue(List<string> args)
        {
            static bool SetIsFertileTrue(Hero hero) => hero.IsFertile = true;
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultBoolean(heroes, SetIsFertileTrue,
                null, Resources.IsFertile, Resources.FalseString_IsFertile);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        /// <summary>
        /// npc.set_is_fertile_false 设置角色不可生育
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("set_is_fertile_false", "npc")]
        public static string SetIsFertileFalse(List<string> args)
        {
            static bool SetIsFertileFalse(Hero hero) => hero.IsFertile = false;
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultBoolean(heroes, SetIsFertileFalse,
                null, Resources.IsFertile, Resources.FalseString_IsFertile);
            return Utils.HandleSearchHeroes(args, HandleHeroes);
        }

        #endregion

        #region npc_control

        static string CheckControlHero(Func<string> @delegate)
        {
            if (Mission.Current.IsBattle())
            {
                return @delegate();
            }
            else
            {
                Utils.DisplayMessage(Resources.NPCControlCanOnlyBeUsedOnTheBattlefield, Colors.Yellow);
                return Utils.FailSeeMessage;
            }
        }

        /// <summary>
        /// npc_control.name [name] 在战场上控制指定npc
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("name", "npc_control")]
        public static string ControlHeroByName(List<string> args)
        {
            return CheckControlHero(() =>
            {
                var heroes = Utils.SearchHeroes(args, Utils.NpcType.Noble | Utils.NpcType.Wanderer | Utils.NpcType.Player);
                var agent = Utils.BattlefieldControl.GetAgentV2(heroes.Select(x => x.CharacterObject));
                var result = Utils.BattlefieldControl.ControlV2(agent);
                return result ? Utils.Done : Utils.NotFound;
            });
        }

        /// <summary>
        /// npc_control.index [index] 在战场上控制指定npc
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("index", "npc_control")]
        public static string ControlHeroByIndex(List<string> args)
        {
            return CheckControlHero(() =>
            {
                var index = args.Select(x => int.TryParse(x, out var number) ? number : -1).FirstOrDefault(x => x != -1);
                if (index >= 0)
                {
                    var heroes = Utils.GetNpcs(Utils.NpcType.Noble | Utils.NpcType.Wanderer | Utils.NpcType.Player);
                    if (index < heroes.Length)
                    {
                        var agent = Utils.BattlefieldControl.GetAgentV2(new[] { heroes[index].CharacterObject });
                        var result = Utils.BattlefieldControl.ControlV2(agent);
                        return result ? Utils.Done : Utils.NotFound;
                    }
                }
                return Utils.NotFound;
            });
        }

        internal static string ControlHeroNext(Utils.NpcType npcType)
        {
            return CheckControlHero(() =>
            {
                var main = Agent.Main;
                if (main != default)
                {
                    var heroes = Utils.GetNpcs(npcType).Select(x => x.CharacterObject);
                    var agents = Utils.BattlefieldControl.GetAgentsV2(heroes)?.ToArray();
                    if (agents != null)
                    {
                        var mainIndex = Array.IndexOf(agents, main);
                        int index;
                        if (mainIndex < 0) index = 0;
                        else
                        {
                            var mainIndexAdd1 = mainIndex + 1;
                            if (mainIndexAdd1 < agents.Length) index = mainIndexAdd1;
                            else index = 0;
                        }
                        var result = Utils.BattlefieldControl.ControlV2(agents[index]);
                        Utils.DisplayMessage($"Current Index: {index}");
                        return result ? Utils.Done : Utils.NotFound;
                    }
                }
                return Utils.NotFound;
            });
        }

        /// <summary>
        /// npc_control.next 在战场上控制下一个npc
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("next", "npc_control")]
        public static string ControlHeroNext(List<string> args)
        {
            return ControlHeroNext(Utils.NpcType.Noble | Utils.NpcType.Wanderer | Utils.NpcType.Player);
        }

        /// <summary>
        /// npc_control.next_noble 在战场上控制下一个npc(noble)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("next_noble", "npc_control")]
        public static string ControlHeroNextNoble(List<string> args)
        {
            return ControlHeroNext(Utils.NpcType.Noble);
        }

        /// <summary>
        /// npc_control.next_wanderer 在战场上控制下一个npc(wanderer)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [CommandLineFunctionality.CommandLineArgumentFunction("next_wanderer", "npc_control")]
        public static string ControlHeroNextWanderer(List<string> args)
        {
            return ControlHeroNext(Utils.NpcType.Wanderer);
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("npcs_index", "print")]
        public static string PrintHeroIndex(List<string> args)
        {
            var heroes = Utils.GetNpcs(Utils.NpcType.Noble | Utils.NpcType.Wanderer | Utils.NpcType.Player).Select(x => x.CharacterObject)?.ToArray();
            if (heroes != default)
            {
                for (int i = 0; i < heroes.Length; i++)
                {
                    Utils.DisplayMessage($"index: {i}, name: {heroes[i].Name?.ToString()}");
                }
                return Utils.Done;
            }
            return Utils.NotFound;
        }

        #endregion

        #region npc_set_battle_commander

        public static string BattleCommander { get; private set; }

        public static void InitBattleCommander()
        {
            BattleCommander = null;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("set_battle_commander_name", "npc")]
        public static string SetBattleCommanderByName(List<string> args)
        {
            var heroes = Utils.SearchHeroes(args, Utils.NpcType.Noble | Utils.NpcType.Wanderer | Utils.NpcType.Player);
            var hero = heroes?.FirstOrDefault();
            if (hero != null)
            {
                BattleCommander = hero.StringId;
#if DEBUG
                Console.WriteLine($"SetBattleCommanderByName commander:{hero?.Name.ToString()}, BattleCommander: {BattleCommander}");
#endif
                return Utils.Done;
            }
            return Utils.NotFound;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("set_battle_commander_index", "npc")]
        public static string SetBattleCommanderByIndex(List<string> args)
        {
            var index = args.Select(x => int.TryParse(x, out var number) ? number : -1).FirstOrDefault(x => x != -1);
            if (index >= 0)
            {
                var heroes = Utils.GetNpcs(Utils.NpcType.Noble | Utils.NpcType.Wanderer | Utils.NpcType.Player);
                if (index < heroes.Length)
                {
                    var hero = heroes[index];
                    BattleCommander = hero.StringId;
#if DEBUG
                    Console.WriteLine($"SetBattleCommanderByIndex commander:{hero?.Name.ToString()}, BattleCommander: {BattleCommander}");
#endif
                    return Utils.Done;
                }
            }
            return Utils.NotFound;
        }

        #endregion

        [CommandLineFunctionality.CommandLineArgumentFunction("kill_player", "campaign")]
        public static string KillPlayer(List<string> strings)
        {
            if (Campaign.Current == null) return "Campaign was not started.";
            var player = Hero.MainHero;
            if (player == null) return "Hero is not found.";
            if (!player.IsAlive) return "Hero is already dead.";
            KillCharacterAction.ApplyByMurder(player, null, true);
            return "Hero is killed.";
        }

        //[CommandLineFunctionality.CommandLineArgumentFunction("close", "menu")]
        //public static string MenuClose(List<string> strings)
        //{
        //    Campaign.Current?.GameMenuManager?.ExitToLast();
        //    return "Ok";
        //}
    }
}