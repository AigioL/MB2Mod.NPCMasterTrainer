using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class Perks
        {
            static PerkObject[] GetAllPerks()
            {
                var perks = new HashSet<PerkObject>();
                try
                {
                    var all_po = PerkObject.All;
                    if (all_po != null) perks.AddRange(all_po, true);
                }
                catch (NullReferenceException)
                {

                }
                try
                {
                    var all_def = DefaultPerks.GetAllPerks();
                    if (all_def != null) perks.AddRange(all_def, true);
                }
                catch (NullReferenceException)
                {

                }
                var typeDefaultPerks = typeof(DefaultPerks);
                var typePerkObject = typeof(PerkObject);
                var classs = typeDefaultPerks.GetNestedTypes().Where(x => !x.IsGenericType && x.IsClass && x.IsAbstract && x.IsSealed);
                var fields = classs.SelectMany(x => x.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(y => y.PropertyType == typePerkObject)).ToArray();
                foreach (var item in fields)
                {
                    object value;
                    try
                    {
                        value = item.GetValue(null);
                    }
                    catch (TargetInvocationException e)
                    {
                        if (e.InnerException != null && e.InnerException is NullReferenceException) continue;
                        throw e;
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }
                    if (value != null && value is PerkObject perkObject)
                    {
                        perks.Add(perkObject);
                    }
                }
                return perks.ToArray();
            }

            static readonly Lazy<PerkObject[]> lazy_allPerks = new Lazy<PerkObject[]>(GetAllPerks);

            public static PerkObject[] All => lazy_allPerks.Value;
        }

        public static bool Exist(this PerkObject perk, Hero hero) => perk != null && (hero?.HeroDeveloper.GetPerkValue(perk) ?? false);

        public static bool Add(this PerkObject perk, Hero hero)
        {
            if (hero != null && perk != null)
            {
                if (!perk.Exist(hero))
                {
                    hero.HeroDeveloper.AddPerk(perk);
                    return perk.Exist(hero);
                }
            }
            return false;
        }
    }
}
