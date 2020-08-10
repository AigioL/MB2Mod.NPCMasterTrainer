using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static SkillObject GetSkillObject(int row, int column)
        {
            var index = (row - 1) * 3 + column - 1;
            return GetSkillObject(index);
        }

        public static SkillObject GetSkillObject(int index)
        {
            try
            {
                return GetAllSkills().ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return default;
            }
        }

        public static IEnumerable<SkillObject> GetAllSkills()
        {
            yield return DefaultSkills.OneHanded; yield return DefaultSkills.TwoHanded; yield return DefaultSkills.Polearm;
            yield return DefaultSkills.Bow; yield return DefaultSkills.Crossbow; yield return DefaultSkills.Throwing;
            yield return DefaultSkills.Riding; yield return DefaultSkills.Athletics; yield return DefaultSkills.Crafting;
            yield return DefaultSkills.Scouting; yield return DefaultSkills.Tactics; yield return DefaultSkills.Roguery;
            yield return DefaultSkills.Charm; yield return DefaultSkills.Leadership; yield return DefaultSkills.Trade;
            yield return DefaultSkills.Steward; yield return DefaultSkills.Medicine; yield return DefaultSkills.Engineering;
        }
    }
}