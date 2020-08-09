using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Text;
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

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            Utils.Config.Instance.HandleItemObjects();
            ConsoleCommand.InitBattleCommander();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);
            gameStarter.AddModel(Utils.NPCMT_ClanTierModel.Init);
            gameStarter.AddModel(Utils.NPCMT_PregnancyModel.Init);
            //gameStarter.AddModel(Utils.NPCMT_TroopCountLimitModel.Init);
            gameStarter.AddModel(Utils.NPCMT_WorkshopModel.Init);
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            if (Utils.Config.Instance.HasWin32Console())
            {
                Utils.Win32Console.Show(Utils.AssemblyTitle + " Development Console");
                Console.WriteLine($"GameVersion: {Utils.GameVersion}");
                Console.WriteLine($"DefaultEncodingName: {Encoding.Default.EncodingName}");
                Console.WriteLine($"CurrentPlatform: {ApplicationPlatform.CurrentPlatform}");
                Console.WriteLine($"CurrentRuntimeLibrary: {ApplicationPlatform.CurrentRuntimeLibrary}");
                Console.WriteLine();
                Utils.CurrentAppDomain.Print();
                Utils.Config.PrintConfigInstanceLog();
                Console.WriteLine();
            }
            if (Utils.Config.Instance.FixGetClipboardText)
            {
                ScriptingInterfaceOfIInput.FixGetClipboardText();
            }
            Utils.UrbanCharactersCampaignBehavior2.InitOnlyCreateFemaleOrMaleWanderer();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
            if (Utils.Config.Instance.HasWin32Console())
            {
                Utils.Win32Console.Hide();
            }
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
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

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
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

        public override void OnCampaignStart(Game game, object starterObject)
        {
            base.OnCampaignStart(game, starterObject);
            if (Utils.Config.Instance.EnableDevConsole())
            {
                Console.WriteLine($"OnCampaignStart({game}, {starterObject})");
            }
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);
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
            Utils.DisplayMessage(
                "CurrentDomain_UnhandledException" + Environment.NewLine +
                "IsTerminating: " + e.IsTerminating
                + (ex != null ? Environment.NewLine + ex.ToString() : null),
                Utils.Colors.OrangeRed);
        }
    }
}