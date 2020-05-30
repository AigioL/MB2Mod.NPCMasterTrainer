using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
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
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultBoolean(heroes, LegendarySmithExist, Resources.LegendarySmith);
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
            static string HandleHeroes(IEnumerable<Hero> heroes) => Utils.HandleResultBoolean(heroes, LegendarySmithAdd, Resources.LegendarySmith);
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
    }
}
