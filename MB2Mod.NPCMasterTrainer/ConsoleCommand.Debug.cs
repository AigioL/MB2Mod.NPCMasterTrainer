#if DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

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
            // print.test 哈劳斯国王
            var arg = args?.FirstOrDefault() ?? string.Empty;
            Utils.DisplayMessage($"inputStr: {arg}");
            Utils.DisplayMessage($"inputStr(GB2312_UTF8): {GB2312_UTF8(arg)}");
            static string GB2312_UTF8(string str)
            {
                // utf8bytes -> gb2312string 导致不可逆数据丢失 反过来转也没法还原
                return Encoding.UTF8.GetString(Encoding.GetEncoding(936).GetBytes(str));
            }
            return arg;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("clipboard", "print")]
        public static string PrintClipboard(List<string> args)
        {
            var str = Utils.Clipboard.GetTextOrEmpty();
            Utils.DisplayMessage($"inputStr: {str}");
            return str;
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

//| <span>rename.me</span> | 玩家重命名 |

///// <summary>
///// rename.me 玩家重命名
///// </summary>
///// <param name="args"></param>
///// <returns></returns>
//[CommandLineFunctionality.CommandLineArgumentFunction("me", "rename")]
//public static string ReNameMe(List<string> args)
//{
//      no work
//    if (Campaign.Current == null) return Utils.CampaignIsNull;
//    var me = Hero.MainHero;
//    if (me == default) return Utils.NotFound;
//    var newName = Utils.Clipboard.GetTextOrEmpty();
//    me.ReName(newName);
//    // TaleWorlds.CampaignSystem.ConversationManager.SetupGameStringsForConversation()
//    Helpers.StringHelpers.SetCharacterProperties("PLAYER", me.CharacterObject, null, null, false);
//    return Utils.Done;
//}

// TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors.DefaultNotificationsCampaignBehavior
// OnGivenBirth(Hero mother, List<Hero> aliveOffsprings, int stillbornCount)