using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;

// 功能：我的配偶不会死于分娩

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void ApplyInLaborDest(Hero lostMother, bool showNotification = true)
        {
            var mySpouse = MainHero?.Spouse;
            if (mySpouse != null && lostMother == mySpouse)
            {
                if (Config.Instance.EnableDevConsole())
                {
                    Console.WriteLine("my spouse say: not today");
                }
                return;
            }
            ApplyInLaborSource(lostMother, showNotification);
        }

#pragma warning disable IDE0060 // 删除未使用的参数
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void ApplyInLaborSource(Hero lostMother, bool showNotification = true)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
        }

        public static void InitModifyApplyInLabor()
        {
            if (Config.Instance.MySpouseCanDiedInLabor) return;
            var destType = typeof(Utils);
            var sourceType = typeof(KillCharacterAction);
            var source = destType.GetMethod(nameof(ApplyInLaborSource),
                BindingFlags.NonPublic | BindingFlags.Static, null,
                new[] { typeof(Hero), typeof(bool) }, null);
            var destination = sourceType.GetMethod(nameof(KillCharacterAction.ApplyInLabor),
                BindingFlags.Public | BindingFlags.Static, null,
                new[] { typeof(Hero), typeof(bool) }, null);
            Hook.ReplaceMethod(source, destination);
            source = destination;
            destination = destType.GetMethod(nameof(ApplyInLaborDest),
                BindingFlags.NonPublic | BindingFlags.Static, null,
                new[] { typeof(Hero), typeof(bool) }, null);
            Hook.ReplaceMethod(source, destination);
            if (Config.Instance.HasWin32Console())
            {
                Console.WriteLine("InitModifyApplyInLabor Success.");
            }
        }
    }
}