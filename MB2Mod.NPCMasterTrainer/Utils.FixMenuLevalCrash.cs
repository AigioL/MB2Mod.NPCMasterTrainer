//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using TaleWorlds.CampaignSystem;
//using TaleWorlds.CampaignSystem.Overlay;
//using TaleWorlds.CampaignSystem.ViewModelCollection;
//using TaleWorlds.MountAndBlade.ViewModelCollection.GameMenu;
//using SettlementMenuOverlayVM = TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.SettlementMenuOverlayVM;

//namespace MB2Mod.NPCMasterTrainer
//{
//    partial class Utils
//    {
//        partial class Config
//        {
//            public bool FixMenuLevalCrash { get; set; }
//        }

//        public class FixMenuLevalCrash
//        {
//            private FixMenuLevalCrash()
//            {
//                throw new NotSupportedException();
//            }

//#pragma warning disable IDE0060 // 删除未使用的参数
//            [MethodImpl(MethodImplOptions.NoInlining)]
//            private void InitPartyListDest()
//            {
//                try
//                {
//                    var @this = (SettlementMenuOverlayVM)(object)this;
//                    var isNullPartyList = @this.PartyList == null;
//                    if (Config.Instance.HasWin32Console())
//                    {
//                        Console.WriteLine($"FixMenuLevalCrash, isNullPartyList: {isNullPartyList}");
//                    }
//                    //MobileParty.MainParty.LastVisitedSettlement
//                    //var _type = GetType(@this);
//                    //if (_type == GameOverlays.MenuOverlayType.SettlementWithBoth || _type == GameOverlays.MenuOverlayType.SettlementWithParties)
//                    //{
//                    //    var list = (from p in (MobileParty.MainParty.CurrentSettlement ?? MobileParty.MainParty.LastVisitedSettlement).Parties
//                    //                where WillBeListed(@this, p)
//                    //                select p).ToList();
//                    //    if (list.Count == @this.PartyList.Count && !list.Any((MobileParty p) => !@this.PartyList.Any((GameMenuPartyItemVM p2) => p2.Party.MobileParty == p)))
//                    //    {
//                    //        return;
//                    //    }
//                    //    @this.PartyList.Clear();
//                    //    list.Sort(CampaignUIHelper.MobilePartyPrecedenceComparerInstance);
//                    //    foreach (var mobileParty in list)
//                    //    {
//                    //        var itemVM = new GameMenuPartyItemVM(
//                    //            ExecuteOnSetAsActiveContextMenuItem(@this), mobileParty.Party, false);
//                    //        @this.PartyList.Add(itemVM);
//                    //    }
//                    //}
//                    //else
//                    //{
//                    //    @this.PartyList.Clear();
//                    //}
//                    InitPartyListSource();
//                }
//                catch (Exception e)
//                {
//                    if (Config.Instance.HasWin32Console())
//                    {
//                        Console.WriteLine($"FixMenuLevalCrash Exception: {Environment.NewLine}{e}");
//                    }
//                    //throw e;
//                }
//            }

//            public static GameOverlays.MenuOverlayType GetType(SettlementMenuOverlayVM @this)
//            {
//                return (GameOverlays.MenuOverlayType)typeof(SettlementMenuOverlayVM).GetField("_type", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(@this);
//            }

//            public static bool WillBeListed(SettlementMenuOverlayVM @this, MobileParty mobileParty)
//            {
//                return (bool)typeof(SettlementMenuOverlayVM).GetMethod("WillBeListed", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(@this, new[] { mobileParty });
//            }

//            public static Action<GameMenuPartyItemVM> ExecuteOnSetAsActiveContextMenuItem(SettlementMenuOverlayVM @this)
//            {
//                return troop => typeof(GameMenuOverlay).GetMethod("ExecuteOnSetAsActiveContextMenuItem", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(@this, new[] { troop });
//            }

//            [MethodImpl(MethodImplOptions.NoInlining)]
//            private void InitPartyListSource()
//            {
//            }
//#pragma warning restore IDE0060 // 删除未使用的参数

//            public static void Fix()
//            {
//                if (!Config.Instance.FixMenuLevalCrash) return;
//                try
//                {
//                    var sourceType = typeof(SettlementMenuOverlayVM);
//                    var destination = sourceType.GetMethod("InitPartyList", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
//                    if (destination == null)
//                    {
//                        if (Config.Instance.HasWin32Console())
//                        {
//                            Console.WriteLine($"FixMenuLevalCrash Fail.");
//                        }
//                        return;
//                    }
//                    var destType = typeof(FixMenuLevalCrash);
//                    var source = destType.GetMethod(nameof(InitPartyListSource), BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
//                    Hook.ReplaceMethod(source, destination);
//                    source = destination;
//                    destination = destType.GetMethod(nameof(InitPartyListDest), BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
//                    Hook.ReplaceMethod(source, destination);
//                    if (Config.Instance.HasWin32Console())
//                    {
//                        Console.WriteLine("FixMenuLevalCrash Success.");
//                    }
//                }
//                catch (Exception e)
//                {
//                    DisplayMessage(e);
//                }
//            }
//        }
//    }
//}