using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

#pragma warning disable IDE0060 // 删除未使用的参数

namespace MB2Mod.NPCMasterTrainer.Launcher
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var gamePath = Utils.GetGamePath();
            if (Directory.Exists(gamePath))
            {
                await Utils.ExitGameAsync();
                var currentPath = Utils.CurrentPath;
                var projPath = Utils.GetProjectPath(currentPath);
                var isOK = Utils.Deploy(currentPath, projPath, gamePath, !Utils.IsDevelopment);
                if (isOK) Console.WriteLine("Done");
            }
            else
            {
                Console.WriteLine("Fail GamePath Not Found.");
            }
            if (!Debugger.IsAttached) Console.ReadLine();
        }

        //private static void Main(string[] args)
        //{
        //    LocalizationMigrate.Localization();

        //    //var currentPath = Utils.CurrentPath;
        //    //var projPath = Utils.GetProjectPath(currentPath);
        //    //NPCMasterTrainer.Utils.Localization.ReadStringXml(Path.Combine(projPath, Utils.Languages_DIR, "strings-zh-Hans.xml"));

        //    //LocalizationMigrate.Search();
        //}
    }
}