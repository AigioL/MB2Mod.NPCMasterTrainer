using System;
using System.Linq;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using System.Runtime.InteropServices;
using TaleWorlds.ObjectSystem;
using TaleWorlds.Core;
using colors = TaleWorlds.Library.Colors;
using TaleWorlds.Library;
using System.IO;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

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

        public static string HandleSearchHeroes(IReadOnlyList<string> args, Func<IEnumerable<Hero>, string> func, bool inMyTroops = true, bool excludeMe = false)
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
                heroes = GetNpcs(choose_arg.GetNpcType(), inMyTroops, excludeMe);
            }
            else
            {
                heroes = SearchHeroes(inputNames, choose_arg.GetNpcType(), inMyTroops, excludeMe);
            }
            if (heroes != default && heroes.Any()) return func(heroes);
            return NotFound;
        }

        #endregion

        static readonly string[] separators_SplitArgments = new[] { "|", @"\|" };

        public static IReadOnlyList<IReadOnlyList<string>> SplitArgments(this IReadOnlyList<string> args)
        {
            var result = new List<List<string>>() { new List<string>() };
            var index = 0;
            for (int i = 0; i < args.Count; i++)
            {
                var item = args[i];
                if (separators_SplitArgments.Contains(item))
                {
                    if (i != 0)
                    {
                        index += 1;
                        result.Add(new List<string>());
                    }
                    continue;
                }
                result[index].Add(item);
            }
#if DEBUG
            Console.WriteLine(string.Join(Environment.NewLine, result.Select(x => string.Join(" ", x))));
#endif
            return result;
        }

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

        static readonly Lazy<ApplicationVersion> lazy_game_ver = new Lazy<ApplicationVersion>(() =>
        {
            try
            {
                var moduleNative = ModuleInfo.GetModules()
                    ?.FirstOrDefault(x => x.IsOfficial && x.IsSelected && string.Equals(x.Id, "Native", StringComparison.OrdinalIgnoreCase));
                if (moduleNative != default)
                {
                    return moduleNative.Version;
                }
            }
            catch (DirectoryNotFoundException)
            {

            }
            return default;
        });

        public static ApplicationVersion GameVersion => lazy_game_ver.Value;

        public static bool CheatMode
        {
            get
            {
                if (Game.Current != default)
                {
                    return Game.Current.CheatMode;
                }
                return false;
            }
        }

        public static T GetGameModel<T>(this IEnumerable<GameModel> models) where T : GameModel
        {
            var items = models?.Reverse();
            if (items != default)
            {
                foreach (var item in items)
                {
                    if (item is T t && t != null)
                    {
                        return t;
                    }
                }
            }
            return default;
        }

        public static T GetGameModel<T>(this IGameStarter gameStarter) where T : GameModel
        {
            var items = gameStarter?.Models;
            return items.GetGameModel<T>();
        }

        public static void AddModel<T>(this IGameStarter gameStarter, Func<T> func) where T : GameModel
        {
            T _func(IGameStarter _) => func?.Invoke();
            gameStarter.AddModel(_func);
        }

        public static void AddModel<T>(this IGameStarter gameStarter, Func<IGameStarter, T> func) where T : GameModel
        {
            T _func(Config _, IGameStarter gameStarter1) => func?.Invoke(gameStarter1);
            gameStarter.AddModel(_func);
        }

        public static void AddModel<T>(this IGameStarter gameStarter, Func<Config, T> func) where T : GameModel
        {
            T _func(Config config, IGameStarter _) => func?.Invoke(config);
            gameStarter.AddModel(_func);
        }

        public static void AddModel<T>(this IGameStarter gameStarter, Func<Config, IGameStarter, T> func) where T : GameModel
        {
            var config = Config.Instance;
            var model = func?.Invoke(config, gameStarter);
            if (model != null)
            {
                gameStarter.AddModel(model);
                if (config.HasWin32Console())
                {
                    var name = model.GetType().Name.Replace("NPCMT", string.Empty).Replace("Model", "M");
                    Console.WriteLine($"{name} Init Success.");
                }
            }
        }

        static TService GetGameModel<TService, TImplementation>() where TService : GameModel where TImplementation : TService, new()
        {
            TService model;
            try
            {
                model = Game.Current?.BasicModels.GetGameModels().GetGameModel<TService>();
            }
            catch
            {
                model = default;
            }
            return model ?? new TImplementation();
        }

        public static ClanTierModel ClanTierModel => GetGameModel<ClanTierModel, DefaultClanTierModel>();
    }
}
