#if DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;

#pragma warning disable IDE0060 // 删除未使用的参数

namespace MB2Mod.NPCMasterTrainer
{
    partial class ConsoleCommand
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("exception", "print")]
        public static string PrintException(List<string> args)
        {
            try
            {
                throw new NullReferenceException(nameof(args));
            }
            catch (Exception e)
            {
                Utils.DisplayMessage(e.ToString(), Utils.Colors.BlueViolet);
                Utils.DisplayMessage(e);
            }
            return Utils.Done;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("test", "print")]
        public static string PrintTest(List<string> args)
        {
            var arg = args?.FirstOrDefault() ?? string.Empty;
            Utils.DisplayMessage(arg);
            return arg;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("clan_companions", "print")]
        public static string PrintClanCompanions(List<string> args)
        {
            Utils.Print(Hero.MainHero.Clan.Companions, "clan_companions");
            return Utils.Done;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("clan_heroes", "print")]
        public static string PrintClanHeroes(List<string> args)
        {
            Utils.Print(Hero.MainHero.Clan.Heroes, "clan_heroes");
            return Utils.Done;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("children", "print")]
        public static string PrintChildren(List<string> args)
        {
            Utils.Print(Hero.MainHero.Children, "children");
            return Utils.Done;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("wanderers", "print")]
        public static string PrintWanderers(List<string> args)
        {
            Utils.Print(Utils.Wanderers, "wanderers");
            return Utils.Done;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("all_towns", "print")]
        public static string PrintAllTowns(List<string> args)
        {
            Utils.Print(Town.AllTowns, "all_towns");
            return Utils.Done;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("all_items", "print")]
        public static string PrintAllItems(List<string> args)
        {
            Utils.Print(ItemObject.All, "all_items");
            return Utils.Done;
        }

        //[CommandLineFunctionality.CommandLineArgumentFunction("test_dynamic", "print")]
        //public static string TestDynamic(List<string> args)
        //{
        //    // crashes !!!
        //    dynamic a = new { };
        //    a.test = "123";
        //    return a.test;
        //}
    }
}
#endif