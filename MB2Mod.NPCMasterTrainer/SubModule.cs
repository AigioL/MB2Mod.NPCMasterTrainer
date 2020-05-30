using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using MB2Mod.NPCMasterTrainer.Properties;

namespace MB2Mod.NPCMasterTrainer
{
    public class SubModule : MBSubModuleBase
    {
        bool isLoaded;
        bool keyPressedDC;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            if (Utils.Config.Instance.HasWin32Console())
            {
                Utils.Win32Console.Show(Utils.AssemblyTitle + " Development Console");
                //Console.WriteLine($"Language: {Resources.Language}" + Environment.NewLine);
                Utils.CurrentAppDomain.Print();
                //Utils.Localization.Print(CultureInfo.CurrentUICulture, nameof(CultureInfo.CurrentUICulture));
                Console.WriteLine();
            }
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            Utils.Config.Instance.HandleItemObjects();
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
            Utils.Localization.SetLanguageByGame();
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

        public SubModule() : base()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
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
