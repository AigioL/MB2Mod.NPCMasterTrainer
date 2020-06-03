using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Core;
using MB2Mod.NPCMasterTrainer.Properties;

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
        /// npc.reset_perks 重置npc 技能点
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
        /// npc.reset_focus 重置npc 专精点
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
        /// npc.reset_attrs 重置npc 属性点
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
        /// npc.reset 重置npc 技能点/专精点/属性点
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
        /// npc.check_legendary_smith 检查是否有传奇铁匠技能点
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
        /// npc.add_perk_legendary_smith 点上传奇铁匠技能点
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
            static ValueTuple<DynamicBodyProperties, StaticBodyProperties>? GetRandomBodyProperties(Hero hero)
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
                var dynamicBodyProperties = new DynamicBodyProperties(hero.Age, 0.0f, 0.0f);
                return (dynamicBodyProperties, staticBodyProperties);
            }
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, hero =>
            {
                var body = GetRandomBodyProperties(hero);
                if (body.HasValue)
                {
                    hero.SetBodyProperties(body.Value.Item1, body.Value.Item2);
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
            string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultVoid(heroes, hero => hero.SetBodyProperties(bodyProperties));
            return Utils.HandleSearchHeroes(args, HandleHeroes, inMyTroops: false);
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
            return Utils.NPCMTPregnancyModel.Print() ? Utils.Done : Utils.NotFound;
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
    }
}