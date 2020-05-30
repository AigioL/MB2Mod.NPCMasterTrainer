using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public const char NameIndexAnalysisSeparator = '-';

        public static string TrimQuotationMarks(this string s) => s?.Trim(new char[] { '"', '“', '”', ' ' });

        public static (string name, int index) NameIndexAnalysis(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var indexStr = name.Split(new[] { NameIndexAnalysisSeparator }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                if (!string.IsNullOrWhiteSpace(indexStr) && int.TryParse(indexStr, out var index))
                {
                    return (name.Substring(0, name.Length - (indexStr.Length + 1)), index - 1); // input index starting at1
                }
            }
            return (name, default);
        }

        public static (string name, int index)[] GetNames(IEnumerable<string> args)
          => args.Where(x => !string.IsNullOrWhiteSpace(x))
          .Select(name => NameIndexAnalysis(name.Replace('_', ' ').TrimQuotationMarks()))
          .ToArray();

        public enum CommandChooseNpcsInMyTroopsArg
        {
            all = NpcType.Player | NpcType.Noble | NpcType.Wanderer,
            me = NpcType.Player,
            all_not_me = NpcType.Noble | NpcType.Wanderer,
            wanderer = NpcType.Wanderer,
            noble = NpcType.Noble,
        }

        public static NpcType GetNpcType(this CommandChooseNpcsInMyTroopsArg arg) => (NpcType)(int)arg;

        static bool GetFirstArg(IEnumerable<string> args, out CommandChooseNpcsInMyTroopsArg? arg)
        {
            var value = args.First().TrimQuotationMarks();
            if (Enum.TryParse<CommandChooseNpcsInMyTroopsArg>(value, true, out var result))
            {
                arg = result;
                return true;
            }
            else
            {
                arg = default;
                return false;
            }
        }

        public static Hero[] SearchHeroes(IEnumerable<string> args, NpcType type)
        {
            (string name, int index)[] names = GetNames(args);
            var findHeros = new Hero[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                var name = names[i];
                int currentIndex = 0;
                Hero currentHero = null;
                var npcHeros = GetNpcsInMyTroops(type);
                foreach (var npcHero in npcHeros)
                {
                    if (npcHero == default) continue;
                    if (string.Equals(name.name, npcHero.Name.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        if (currentIndex == name.index)
                        {
                            currentHero = npcHero;
                            break;
                        }
                        currentIndex++;
                    }
                }
                findHeros[i] = currentHero;
            }
            return findHeros;
        }
    }
}
