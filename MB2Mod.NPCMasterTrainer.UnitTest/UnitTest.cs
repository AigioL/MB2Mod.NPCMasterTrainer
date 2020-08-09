using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;

namespace MB2Mod.NPCMasterTrainer.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        public UnitTest()
        {
            Utils.Localization.SetLanguage(CultureInfo.CurrentUICulture);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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
            var currentLanguageId = Utils.Localization.GetLanguage();
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
            var header = Utils.ExportData.Join(new StringBuilder(), Utils.HeroExportData.TableHeaders).ToString();
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

        [TestMethod]
        public void Lazy()
        {
            var typeUtils = typeof(Utils);
            var dels = new[] { typeUtils.Namespace + ".", ", " + typeUtils.Assembly };
            TestLazyFields(typeUtils);
            dynamic FormatDynamic(dynamic d)
            {
                if (d is string str)
                {
                    return FormatString(str);
                }
                return d;
            }
            string FormatString(string str)
            {
                foreach (var item in dels)
                {
                    str = str.Replace(item, string.Empty);
                }
                return str;
            }
            void TestLazyFields(Type type)
            {
                if (type.IsEnum || (type.IsGenericType && !type.IsConstructedGenericType)) return;
                if (type.BaseType != null && type.BaseType != typeof(object)) TestLazyFields(type.BaseType);
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField);
                fields = (from f in fields where f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(Lazy<>) select f).ToArray();
                foreach (var item in fields)
                {
                    dynamic value;
                    bool @catch = default;
                    try
                    {
                        value = item.GetValue(null);
                        value = value.Value;
                        if (value is IReadOnlyCollection<object> collection)
                        {
                            value = $"{value} count: {collection.Count}";
                        }
                    }
                    catch (Exception e)
                    {
                        value = e;
                        @catch = true;
                    }
                    Console.WriteLine($"{FormatString(type.FullName)}.{item.Name}: {FormatDynamic(value)}");
                    if (@catch) Assert.Fail();
                }
                var nestedTypes = type.GetNestedTypes();
                if (nestedTypes != null) Array.ForEach(nestedTypes, TestLazyFields);
            }
        }

        //[TestMethod]
        //public void Encoding936()
        //{
        //    var gb2312 = Encoding.GetEncoding(936);
        //    var source_str = "哈劳斯国王";
        //    var source_bytes = Encoding.UTF8.GetBytes(source_str);
        //    var str = gb2312.GetString(source_bytes); // utf8bytes -> gb2312string 导致不可逆数据丢失 反过来转也没法还原
        //    Console.WriteLine($"gb2312: {str}");
        //    var bytes = gb2312.GetBytes(str);
        //    Console.WriteLine($"UTF8 :{Encoding.UTF8.GetString(bytes)}");
        //}

        [TestMethod]
        public void OnlyCreateFemaleOrMaleWanderer()
        {
            var _companionTemplates = typeof(UrbanCharactersCampaignBehavior).GetField("_companionTemplates", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(_companionTemplates);
            Assert.IsTrue(_companionTemplates.FieldType == typeof(List<CharacterObject>));
            var mCreateCompanion = typeof(UrbanCharactersCampaignBehavior).GetMethod("CreateCompanion", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(CharacterObject) }, null);
            Assert.IsNotNull(mCreateCompanion);
            var parm = mCreateCompanion.GetParameters();
            Assert.IsNotNull(parm);
            Assert.IsTrue(parm.Length == 1);
            Assert.IsTrue(parm.Single().ParameterType == typeof(CharacterObject));
            Assert.IsTrue(mCreateCompanion.ReturnType == typeof(void));
        }
    }
}