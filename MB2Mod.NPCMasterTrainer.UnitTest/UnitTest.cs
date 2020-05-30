using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using TaleWorlds.Core;

namespace MB2Mod.NPCMasterTrainer.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        public UnitTest()
        {
            Utils.Localization.SetLanguage(CultureInfo.CurrentUICulture);
        }

        [TestMethod]
        public void NameIndexAnalysis()
        {
            var testName = "Adam";
            var testIndex = 2;
            var testString = testName + Utils.NameIndexAnalysisSeparator + testIndex;
            (var name, var index) = Utils.NameIndexAnalysis(testString);
            Console.WriteLine($"str: {testString} name: {name} index: {index}");
            Assert.IsTrue(name == testName && index == testIndex - 1);
        }

        [TestMethod]
        public void Localization()
        {
            var currentLanguageId = Utils.Localization.GetCurrentLanguageIdByMBTextManager();
            Console.WriteLine(currentLanguageId);
            //var lang = Utils.Localization.GetLanguageByWin32Registry();
            //Console.WriteLine(lang);
            //var lang = Utils.Localization.GetUserDefaultUILanguage();
            //var culture = new CultureInfo(lang);
            //Utils.Localization.Print(culture, "Culture");
            //Assert.IsTrue(Utils.Localization.GetCultureByMBTextManager(out var culture, out var _));
            //Resources.Culture = culture;
            //Utils.Localization.Print(culture, "Culture");
            //Utils.Localization.Print(culture?.Parent, "Culture.Parent");
            //Console.WriteLine(Resources.LoadedDeveloperConsoleInfoMessage);
            //if (Resources.Culture != CultureInfo.CurrentUICulture)
            //{
            //    Resources.Culture = CultureInfo.CurrentUICulture;
            //    Utils.Localization.Print(Resources.Culture, "CurrentUICulture");
            //    Utils.Localization.Print(Resources.Culture?.Parent, "CurrentUICulture.Parent");
            //    Console.WriteLine(Resources.LoadedDeveloperConsoleInfoMessage);
            //}
        }

        [TestMethod]
        public void CurrentAppDomain()
        {
            Utils.CurrentAppDomain.Print();
        }

        [TestMethod]
        public void Export()
        {
            var header = Utils.HeroExportData.TableHeader;
            Console.WriteLine(header);
            var json = new Utils.HeroExportData().ToJsonString();
            Console.WriteLine(json);
        }

        [TestMethod]
        public void EnumFlags()
        {
            var test = ItemObject.ItemUsageSetFlags.RequiresNoMount | ItemObject.ItemUsageSetFlags.RequiresNoShield;
            Assert.IsTrue(test.HasFlag(ItemObject.ItemUsageSetFlags.RequiresNoMount));
        }

        [TestMethod]
        public void SetDifficulty()
        {
            var item = new ItemObject();
            item.SetDifficulty(sbyte.MaxValue);
            Assert.IsTrue(item.Difficulty == sbyte.MaxValue);
        }
    }
}
