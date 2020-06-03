using System;
using System.Linq;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using System.Runtime.InteropServices;
using TaleWorlds.ObjectSystem;
using TaleWorlds.Core;
using colors = TaleWorlds.Library.Colors;

namespace MB2Mod.NPCMasterTrainer
{
    internal static partial class Utils
    {
        public static bool IsWindows { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.OSVersion.Platform == PlatformID.Win32NT;

        public const string CampaignIsNull = "Campaign IsNull";

        public const string InvalidArguments = "Invalid Arguments";

        public const string Done = "Done";

        public const string NotFound = "Not Found";

        public const string Catch = "Catch";

        public static bool IsDefault<T>(T t) => EqualityComparer<T>.Default.Equals(t, default);

        public static void AddRange<T>(this HashSet<T> ts, IEnumerable<T> collection, bool checkDefNotAdd = false)
        {
            if (ts != default && collection != default)
            {
                foreach (var item in collection)
                {
                    if (checkDefNotAdd && IsDefault(item)) continue;
                    ts.Add(item);
                }
            }
        }

        #region Handle

        static string ToString<T>(T obj) where T : MBObjectBase
        {
            if (obj is Hero hero) return hero.Name?.ToString() ?? hero.ToString();
            if (obj is Town town) return town.Name?.ToString() ?? town.ToString();
            if (obj is ItemObject item) return item.Name?.ToString() ?? item.ToString();
            return obj.ToString();
        }

        public static string HandleResultVoid<T>(IEnumerable<T> items, Action<T> action, bool displayMessage = true) where T : MBObjectBase
        {
            var list = displayMessage ? new List<string>() : null;
            var any = false;
            foreach (var item in items)
            {
                if (item == null) continue;
                action(item);
                if (displayMessage) list.Add(ToString(item));
                else if (!any) any = true;
            }
            if (displayMessage) DisplayMessage(list);
            return (displayMessage ? list.Any() : any) ? Done : NotFound;
        }

        public static string HandleResultBoolean<T>(IEnumerable<T> items, Func<T, bool> func, string tag, string trueString, string falseString) where T : MBObjectBase
        {
            var list_true = new List<string>();
            var list_false = new List<string>();
            foreach (var item in items)
            {
                if (item == null) continue;
                var @bool = func(item);
                (@bool ? list_true : list_false).Add(ToString(item));
            }
            var any_list_true = list_true.Any();
            var any_list_false = list_false.Any();
            var has_tag = !string.IsNullOrWhiteSpace(tag);
            if (list_true.Any())
            {
                DisplayMessage(has_tag ? string.Format(trueString, tag) : trueString, colors.Cyan);
                DisplayMessage(list_true, colors.Cyan);
            }
            if (list_false.Any())
            {
                DisplayMessage(has_tag ? string.Format(falseString, tag) : falseString, colors.Gray);
                DisplayMessage(list_false, colors.Gray);
            }
            return (any_list_true || any_list_false) ? Done : NotFound;
        }

        public static string HandleSearchHeroes(IReadOnlyList<string> args, Func<IEnumerable<Hero>, string> func, bool inMyTroops = true)
        {
            if (Campaign.Current == null) return CampaignIsNull;
            if (!args.Any()) return InvalidArguments;
            IEnumerable<string> inputNames;
            CommandChooseNpcsInMyTroopsArg choose_arg;
            if (GetFirstArg(args, out var arg) && arg.HasValue)
            {
                choose_arg = arg.Value;
                inputNames = args.Count == 1 ? default : args.Skip(1);
            }
            else
            {
                choose_arg = CommandChooseNpcsInMyTroopsArg.all;
                inputNames = args;
            }
            Hero[] heroes;
            if (inputNames == default)
            {
                heroes = GetNpcs(choose_arg.GetNpcType(), inMyTroops);
            }
            else
            {
                heroes = SearchHeroes(inputNames, choose_arg.GetNpcType(), inMyTroops);
            }
            return func(heroes);
        }

        #endregion

        #region TryGetValue

        static bool CanToJsonString(object obj, out Exception exception)
        {
            try
            {
                obj.ToJsonString();
                exception = null;
                return true;
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }

        static object TryGetValue<T, TResult>(this T t, Func<T, TResult> func)
        {
            try
            {
                return func(t);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        static object TryGetValue(this object obj) => CanToJsonString(obj, out var e) ? obj : e.ToString();

        static object TryGetValue<T>(this IEnumerable<T> ts, Func<T, object> func)
        {
            if (ts != null && ts.Any())
            {
                try
                {
                    return ts.Select(x =>
                    {
                        try
                        {
                            return func(x);
                        }
                        catch (Exception e2)
                        {
                            return e2.ToString();
                        }
                    }).ToArray();
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
