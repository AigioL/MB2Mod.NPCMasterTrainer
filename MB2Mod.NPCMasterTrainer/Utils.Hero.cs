using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using Helpers;
using MB2Mod.NPCMasterTrainer.Properties;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        [Flags]
        public enum NpcType
        {
            /// <summary>
            /// 玩家
            /// </summary>
            Player = 0x1,

            /// <summary>
            /// 流浪者
            /// </summary>
            Wanderer = 0x2,

            /// <summary>
            /// 贵族(前/配偶/子女)
            /// </summary>
            Noble = 0x4,
            //= 0x8,
            //= 0x10,
            //= 0x20,
            //= 0x40
        }

        public static object Print(this Hero hero)
        {
            if (hero == null) return null;
            var currentLocationComplex = LocationComplex.Current;
            var currentLocationComplexNotNull = currentLocationComplex != null;
            var location = currentLocationComplexNotNull ? currentLocationComplex.GetLocationOfCharacter(hero) : default;
            var locationCharacter = currentLocationComplexNotNull ? currentLocationComplex.GetLocationCharacterOfHero(hero) : default;
            var jsonObj = new
            {
                Id = hero.Id.ToString(),
                hero.StringId,
                Name = hero.TryGetValue(x => x.Name?.ToString()),
                ToString = hero.ToString(),
                NameId = hero.TryGetValue(x => x.Name?.GetID()),
                Age = hero.TryGetValue(x => x.Age),
                Culture = hero.TryGetValue(x => x.Culture.ToString()),
                hero.IsChild,
                hero.IsFemale,
                IsMainPartyBelonged = IsMainPartyBelonged(hero),
                InMainPartyCompanions = InMainPartyCompanions(hero),
                hero.IsPlayerCompanion,
                hero.IsPartyLeader,
                hero.IsAlive,
                Occupation = hero.GetOccupation(),
                FirstName = hero.TryGetValue(x => x.FirstName?.ToString()),
                Location = new
                {
                    Name = location.TryGetValue(x => x?.Name.ToString()),
                    DoorName = location.TryGetValue(x => x?.DoorName.ToString()),
                    currentLocationComplexNotNull,
                    locationCharacter = locationCharacter.TryGetValue(),
                },
            };
            return jsonObj;
        }

        public static void Print(this IEnumerable<Hero> heroes, string tag) => heroes.Print(tag, "Heroes", Print);

        public static bool? IsMainPartyBelonged(this Hero hero)
        {
            if (hero == null || hero.PartyBelongedTo == null) return null;
            return hero.PartyBelongedTo.IsMainParty;
        }

        public static bool? InMainPartyCompanions(this Hero hero)
        {
            var main = Hero.MainHero;
            if (hero == null || main == null || main.CompanionsInParty == null) return null;
            return main.CompanionsInParty.Contains(hero);
        }

        public static void SetHeroLastSeenLocation(this Hero hero, bool willUpdateImmediately)
        {
            try
            {
                HeroHelper.SetLastSeenLocation(hero, willUpdateImmediately);
            }
            catch (Exception e)
            {
                DisplayMessage(e);
            }
        }

        public static string GetHeroLastKnownLocation(this Hero hero, bool refresh = true, bool willUpdateImmediately = true)
        {
            try
            {
                if (refresh)
                {
                    HeroHelper.SetLastSeenLocation(hero, willUpdateImmediately);
                }
                return StringHelpers.GetLastKnownLocation(hero)?.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public static string GetOccupation(this Hero hero)
        {
            if (hero.IsNoble)
            {
                return Resources.Noble;
            }
            else if (hero.IsWanderer)
            {
                return Resources.Wanderer;
            }
            else if (hero.IsArtisan)
            {
                return Resources.Artisan;
            }
            return string.Empty;
        }

        static Hero[] GetNotMeNpcsInMyTroops(IEnumerable<Hero> heroes, Hero player = null, bool? isNoble = null, bool? isWanderer = null)
        {
            if (heroes == null) return null;
            player ??= Hero.MainHero;
            var query = from hero in heroes
                        let isMainPartyBelonged = IsMainPartyBelonged(hero)
                        where hero != null && hero != player && hero.IsAlive && isMainPartyBelonged.HasValue && isMainPartyBelonged.Value
                        && (!isNoble.HasValue || isNoble.Value == hero.IsNoble) && (!isWanderer.HasValue || isWanderer.Value == hero.IsWanderer)
                        select hero;
            return query.ToArray();
        }

        /// <summary>
        /// 获取我部队中的npc
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Hero[] GetNpcsInMyTroops(NpcType type)
        {
            var player = Hero.MainHero;
            var hashSet = new HashSet<Hero>();
            if (type.HasFlag(NpcType.Player))
            {
                hashSet.Add(player);
            }
            if (type.HasFlag(NpcType.Noble))
            {
                var lords = GetNotMeNpcsInMyTroops(player.Clan.Heroes, player, isNoble: true);
                if (lords != null) hashSet.AddRange(lords);
            }
            if (type.HasFlag(NpcType.Wanderer))
            {
                var wanderers = GetNotMeNpcsInMyTroops(player.Clan.Companions, player, isWanderer: true);
                if (wanderers != null) hashSet.AddRange(wanderers);
            }
            return hashSet.ToArray();
        }

        /// <summary>
        /// 获取所有的流浪者
        /// </summary>
        public static IEnumerable<Hero> Wanderers => Hero.FindAll(item => item.IsWanderer);

        public static IEnumerable<Hero> Nobles => Hero.FindAll(item => item.IsNoble);

        #region Reset

        public static bool reset_perks_check_smith;

        public static void ResetPerks([NotNull] this Hero hero)
        {
            if (reset_perks_check_smith)
            {
                if (DefaultPerks.Crafting.VigorousSmith.Exist(hero))
                {
                    // 第一排第六个
                    // 有力的铁匠，立刻提升一点活力
                    int value = hero.GetAttributeValue(CharacterAttributesEnum.Vigor) - 1;
                    if (value > 0) hero.SetAttributeValue(CharacterAttributesEnum.Vigor, value);
                }
                if (DefaultPerks.Crafting.StrongSmith.Exist(hero))
                {
                    // 第二排第六个
                    // 精密锻造，立刻增加一点控制
                    int value = hero.GetAttributeValue(CharacterAttributesEnum.Control) - 1;
                    if (value > 0) hero.SetAttributeValue(CharacterAttributesEnum.Control, value);
                }
                if (DefaultPerks.Crafting.EnduringSmith.Exist(hero))
                {
                    // 第一排倒数第二个
                    // 正规铁匠，立刻提高忍耐1级
                    int value = hero.GetAttributeValue(CharacterAttributesEnum.Endurance) - 1;
                    if (value > 0) hero.SetAttributeValue(CharacterAttributesEnum.Endurance, value);
                }
                if (DefaultPerks.Crafting.WeaponMasterSmith.Exist(hero))
                {
                    // 第二排倒数第二个
                    // 击剑铁匠，单手系和双手系技能各获得1点专精点
                    hero.HeroDeveloper.AddFocus(DefaultSkills.OneHanded, -1, false);
                    hero.HeroDeveloper.AddFocus(DefaultSkills.TwoHanded, -1, false);
                }
            }
            hero.ClearPerks();
            // ↓ 设置初始技能等级，不设置会导致加点无法保存
            foreach (var item in DefaultSkills.GetAllSkills())
            {
                int skillValue = hero.GetSkillValue(item);
                hero.HeroDeveloper.SetInitialSkillLevel(item, skillValue);
            }
        }

        public static void ResetFocus([NotNull] this Hero hero)
        {
            var focus = DefaultSkills.GetAllSkills().Sum(skill => hero.HeroDeveloper.GetFocus(skill));
            hero.HeroDeveloper.UnspentFocusPoints += MBMath.ClampInt(focus, 0, 999);
            hero.HeroDeveloper.ClearFocuses();
        }

        public static void ResetAttrs([NotNull] this Hero hero)
        {
            int attrValues = 0;
            for (var i = CharacterAttributesEnum.First; i < CharacterAttributesEnum.End; i++)
            {
                var currentAttrValue = hero.GetAttributeValue(i);
                attrValues += currentAttrValue - 1;
                hero.SetAttributeValue(i, 1);
            }
            hero.HeroDeveloper.UnspentAttributePoints += MBMath.ClampInt(attrValues, 0, 999);
        }

        public static void Reset([NotNull] this Hero hero)
        {
            DisplayMessage(hero.Name.ToString());
            hero.ResetPerks();
            hero.ResetFocus();
            hero.ResetAttrs();
            DisplayMessage(string.Format(Resources.UnspentStatFocus, hero.HeroDeveloper.UnspentAttributePoints, hero.HeroDeveloper.UnspentFocusPoints));
        }

        #endregion
    }
}
