using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MB2Mod.NPCMasterTrainer
{
    public class SubModule : MBSubModuleBase
    {
        private bool isLoaded;
        private bool keyPressedDC;

        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            base.OnNewGameCreated(game, initializerObject);
            OnNewGameCreated2(game, initializerObject);
            Utils.Config.Instance.HandleItemObjects();
        }

        [Conditional("DEBUG")]
        void OnNewGameCreated2(Game game, object initializerObject)
        {
            Console.WriteLine($"OnNewGameCreated2({game?.ToString()},{initializerObject?.ToString()})");
            Console.WriteLine($"mainHero: {Utils.MainHero?.Name?.ToString()}");
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            try
            {
                ConsoleCommand.InitBattleCommander();
                OnGameInitializationFinished2(game);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Conditional("DEBUG")]
        void OnGameInitializationFinished2(Game game)
        {
            Console.WriteLine($"OnGameInitializationFinished({game?.ToString()})");
            Console.WriteLine($"mainHero: {Utils.MainHero?.Name?.ToString()}");
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);
            try
            {
                OnGameStart2(game, gameStarter);
                gameStarter.AddModel(Utils.NPCMT_ClanTierModel.Init);
                gameStarter.AddModel(Utils.NPCMT_PregnancyModel.Init);
                //gameStarter.AddModel(Utils.NPCMT_TroopCountLimitModel.Init);
                gameStarter.AddModel(Utils.NPCMT_WorkshopModel.Init);
                gameStarter.AddAgeModel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Conditional("DEBUG")]
        void OnGameStart2(Game game, IGameStarter gameStarter)
        {
            Console.WriteLine($"OnGameStart HasCraftedItemObject: {Utils.HasCraftedItemObject()}");
        }

        public override void OnGameLoaded(Game game, object initializerObject)
        {
            base.OnGameLoaded(game, initializerObject);
            try
            {
                OnGameLoaded2(game, initializerObject);
                OnGameLoadedAfter();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        bool GameIsRunning;

        async void OnGameLoadedAfter()
        {
            GameIsRunning = true;
            await Task.Delay(5000);
            if (GameIsRunning && Game.Current != null && Game.Current.CurrentState == Game.State.Running)
            {
                Utils.Config.Instance.HandleItemObjects();
            }
        }

        [Conditional("DEBUG")]
        void OnGameLoaded2(Game game, object initializerObject)
        {
            Console.WriteLine($"OnGameLoaded HasCraftedItemObject: {Utils.HasCraftedItemObject()}");
            Console.WriteLine($"initializerObject: {initializerObject?.ToString()}");
            Console.WriteLine($"mainHero: {Utils.MainHero?.Name?.ToString()}");
            //Utils.PrintCraftedWeapons();
        }

        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);
            try
            {
                GameIsRunning = false;
                OnGameEnd2(game);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Conditional("DEBUG")]
        void OnGameEnd2(Game game)
        {
            Console.WriteLine($"OnGameEnd({game?.ToString()})");
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                if (Utils.Config.Instance.HasWin32Console())
                {
                    Utils.Win32Console.Show(Utils.AssemblyTitle + " Development Console");
                    Console.WriteLine($"GameVersion: {Utils.GameVersion}");
                    Console.WriteLine($"DefaultEncodingName: {Encoding.Default.EncodingName}");
                    Console.WriteLine($"CurrentPlatform: {ApplicationPlatform.CurrentPlatform}");
                    Console.WriteLine($"CurrentRuntimeLibrary: {ApplicationPlatform.CurrentRuntimeLibrary}");
                    Console.WriteLine();
                    //Utils.CurrentAppDomain.Print();
                    Utils.Config.PrintConfigInstanceLog();
                    Console.WriteLine();
                }
                if (Utils.Config.Instance.FixGetClipboardText)
                {
                    ScriptingInterfaceOfIInput.FixGetClipboardText();
                }
                Utils.UrbanCharactersCampaignBehavior2.InitOnlyCreateFemaleOrMaleWanderer();
                Utils.InitModifyApplyInLabor();
                //Utils.FixMenuLevalCrash.Fix();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
            try
            {
                if (Utils.Config.Instance.HasWin32Console())
                {
                    Utils.Win32Console.Hide();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            try
            {
                if (isLoaded) return;
                //if (Utils.Localization.UseGameLanguage)
                //{
                //Console.WriteLine($"Game Language: {Resources.Language}" + Environment.NewLine);
                //}
                if (Utils.Config.Instance.EnableDevConsole())
                {
                    Utils.DisplayMessage(Resources.LoadedDeveloperConsoleInfoMessage, Color.FromUint(4282569842U));
                }
                isLoaded = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
            try
            {
                if (Utils.Config.Instance.EnableDevConsole())
                {
                    if (!keyPressedDC && Input.DebugInput.IsControlDown() && Input.DebugInput.IsKeyDown(InputKey.Tilde))
                    {
                        Utils.DeveloperConsole.toggle_imgui_console_visibility(new UIntPtr(1u));
                        keyPressedDC = true;
                    }
                    else if (Input.DebugInput.IsKeyReleased(InputKey.Tilde))
                    {
                        keyPressedDC = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public override void OnCampaignStart(Game game, object starterObject)
        {
            base.OnCampaignStart(game, starterObject);
            try
            {
                OnCampaignStart2(game, starterObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [Conditional("DEBUG")]
        void OnCampaignStart2(Game game, object starterObject)
        {
            try
            {
                Console.WriteLine($"OnCampaignStart({game}, {starterObject})");
                //Console.WriteLine($"mainHero: {Hero.MainHero?.Name?.ToString()}");
                //System.NullReferenceException: Object reference not set to an instance of an object.
                //    at TaleWorlds.CampaignSystem.Hero.get_MainHero()
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);
            try
            {
#if DEBUG
                //if (Utils.Config.Instance.EnableDevConsole())
                //{
                //    static void OnResetMission()
                //    {
                //        Console.WriteLine("OnResetMission");
                //    }
                //    static void OnEndMission()
                //    {
                //        Console.WriteLine("OnEndMission");
                //    }
                //    var listener = new Utils.BattlefieldControl.MissionListener(mission)
                //    {
                //        OnResetMission = OnResetMission,
                //        OnEndMission1 = OnEndMission,
                //    };
                //    mission.AddListener(listener);
                //    mission.AddMissionBehaviour(listener);
                //    Console.WriteLine($"OnMissionBehaviourInitialize({mission})");
                //    Console.WriteLine(
                //        $"Mode: {mission.Mode}, " +
                //        $"CurrentState:{mission.CurrentState}, " +
                //        $"CombatType: {mission.CombatType}, " +
                //        $"TimeSpeed: {mission.TimeSpeed}, " +
                //        $"IsFieldBattle: {mission.IsFieldBattle}, " +
                //        $"ForceTickOccasionally: {mission.ForceTickOccasionally}, " +
                //        $"IsTeleportingAgents: {mission.IsTeleportingAgents}, " +
                //        $"SceneLevels: {mission.SceneLevels}, " +
                //        $"SceneName: {mission.SceneName}, " +
                //        $"Time: {mission.Time}, " +
                //        $"MissionTeamAIType: {mission.MissionTeamAIType}, " +
                //        $"Agents.Count: {mission.Agents.Count}, " +
                //        $"AllAgents.Count: {mission.AllAgents.Count}, " +
                //        $"RetreatSide:{mission.RetreatSide}");
                //    foreach (var item in mission.Agents)
                //    {
                //        Console.WriteLine(
                //            $"Name:{item.Name}, " +
                //            $"Index: {item.Index}, " +
                //            $"State:{item.State}, " +
                //            $"IsHuman:{item.IsHuman}, " +
                //            $"IsHero:{item.IsHero}, " +
                //            $"IsMainAgent:{item.IsMainAgent}, " +
                //            $"IsAIControlled:{item.IsAIControlled}, " +
                //            $"IsPlayerControlled:{item.IsPlayerControlled}, " +
                //            $"IsMine:{item.IsMine}, " +
                //            $"Health:{item.Health}, " +
                //            $"HealthLimit:{item.HealthLimit}");
                //    }
                //    //if (mission.MissionBehaviours != null)
                //    //{
                //    //    foreach (var item in mission.MissionBehaviours)
                //    //    {
                //    //        Console.WriteLine(
                //    //            $"Name:{item}, " +
                //    //            $"BehaviourType: {item.BehaviourType}");
                //    //    }
                //    //}
                //    //if (mission.MissionLogics != null)
                //    //{
                //    //    foreach (var item in mission.MissionLogics)
                //    //    {
                //    //        Console.WriteLine(
                //    //            $"Name:{item}, " +
                //    //            $"BehaviourType: {item.BehaviourType}");
                //    //        if (item is BattleObserverMissionLogic battleObserverMissionLogic)
                //    //        {
                //    //            Console.WriteLine(
                //    //                $"BattleObserver:{battleObserverMissionLogic.BattleObserver}");
                //    //        }
                //    //    }
                //    //}
                //}
#endif
                mission.AddSetBattlefieldCommander();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public override void OnMultiplayerGameStart(Game game, object starterObject)
        {
            throw new NotSupportedException("Currently mod does not support multiplayer game.");
        }

        public SubModule() : base()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject is Exception exception ? exception : null;
            var msg = "CurrentDomain_UnhandledException" + Environment.NewLine +
                "IsTerminating: " + e.IsTerminating
                + (ex != null ? Environment.NewLine + ex.ToString() : null);
            try
            {
                File.WriteAllText(
                    Path.Combine(Utils.CurrentModDirectory,
                    $"{DateTime.Now:yyyy-MM-dd HH.mm.ss.fffffff}.error.log"), msg);
            }
            catch
            {

            }
            try
            {
                if (Utils.Config.Instance.EnableDevConsole())
                {
                    Console.WriteLine(msg);
                }
            }
            catch
            {

            }
            try
            {
                Utils.DisplayMessage(msg, Utils.Colors.OrangeRed);
            }
            catch
            {

            }
        }
    }
}