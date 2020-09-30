using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static IReadOnlyList<WeaponComponentData> Weapons(this ItemObject itemObject)
        {
            try
            {
                //return this.WeaponComponent.Weapons; // NullReferenceException
                return itemObject.Weapons;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public static IEnumerable<ItemObject> GetItemsByType(ItemObject.ItemTypeEnum type)
        {
            return Items.FindAll(x => x.ItemType == type);
        }

        public static ItemObject.ItemTypeEnum GetItemType(this WeaponComponentData weaponComponentData)
        {
            try
            {
                if (weaponComponentData != null)
                {
                    if (weaponComponentData.IsAmmo)
                    {
                        return WeaponComponentData.GetItemTypeFromWeaponClass(weaponComponentData.AmmoClass);
                    }
                    else
                    {
                        return WeaponComponentData.GetItemTypeFromWeaponClass(weaponComponentData.WeaponClass);
                    }
                }
            }
            catch
            {
            }
            return default;
        }

        public static object Print(this ItemObject item)
        {
            if (item == null) return null;
            var jsonObj = new
            {
                Id = item.Id.ToString(),
                item.StringId,
                Name = item.TryGetValue(x => x.Name?.ToString()),
                ToString = item.ToString(),
                item.ItemType,
                item.IsCraftedWeapon, // 是精心制作的武器
                item.NotMerchandise, // 不是商品
                item.IsUniqueItem, // 是独一无二的物品
                Culture = item.TryGetValue(x => x.Culture?.Name.ToString()), // 文化
                item.ScaleFactor,
                item.IsFood,
                item.MultiplayerItem, // 多人游戏物品
                item.Tierf, // 等级？
                item.Tier,
                item.IsMountable, // 可装配
                item.IsTradeGood, // 是贸易商品
                item.IsAnimal, // 是动物
                RelevantSkill = item.TryGetValue(x => x.RelevantSkill?.Name.ToString()), // 需要相关技能？
                item.IsCivilian, // 平民
                item.BodyName,
                item.CollisionBodyName,
                item.PrefabName,
                item.Value,
                item.Effectiveness,
                item.Weight,
                item.Difficulty,
                item.Appearance,
                item.RecalculateBody,
                PrimaryWeapon = item.PrimaryWeapon == null ? null : new
                {
                    item.PrimaryWeapon.ItemUsage,
                    ItemUsageSetFlags = item.PrimaryWeapon.TryGetValue(x => MBItem.GetItemUsageSetFlags(x.ItemUsage)),
                    item.PrimaryWeapon.MaxDataValue, // 弹药量?

                    item.PrimaryWeapon.IsMeleeWeapon, // 是近战武器
                    item.PrimaryWeapon.IsRangedWeapon, // 是远程武器
                    item.PrimaryWeapon.AmmoClass,
                    item.PrimaryWeapon.WeaponClass,
                    item.PrimaryWeapon.Accuracy, // 准确度
                    item.PrimaryWeapon.WeaponTier, // 武器等级
                    item.PrimaryWeapon.CanHitMultipleTargets, // 是否可以击中多个目标

                    item.PrimaryWeapon.MissileSpeed, // 投掷速,
                    item.PrimaryWeapon.MissileDamage, // 投掷伤害,

                    item.PrimaryWeapon.ThrustSpeed, // 刺
                    item.PrimaryWeapon.ThrustDamage, // 伤害值,
                    item.PrimaryWeapon.ThrustDamageType, // 伤害类型,
                    item.PrimaryWeapon.ThrustDamageFactor, // 伤害倍数,

                    item.PrimaryWeapon.SwingSpeed, // 砍
                    item.PrimaryWeapon.SwingDamage, // 伤害值
                    item.PrimaryWeapon.SwingDamageType, // 伤害类型
                    item.PrimaryWeapon.SwingDamageFactor, // 伤害倍数

                    item.PrimaryWeapon.WeaponLength, // 武器长度
                    item.PrimaryWeapon.WeaponBalance,
                },
            };
            return jsonObj;
        }

        public static void Print(this IEnumerable<ItemObject> items, string tag) => items.Print(tag, "Items", Print);

        private static readonly Lazy<PropertyInfo> lazy_property_Difficulty = new Lazy<PropertyInfo>(() =>
           {
               var property = typeof(ItemObject).GetProperty(
                   name: "Difficulty",
                   bindingAttr: BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
                   returnType: typeof(int),
                   types: Array.Empty<Type>(),
                   binder: null,
                   modifiers: null);
               return property;
           });

        public static void SetDifficulty(this ItemObject obj, int value) => lazy_property_Difficulty.Value.SetValue(obj, value);

        private static readonly Lazy<PropertyInfo> lazy_property_ItemUsage = new Lazy<PropertyInfo>(() =>
           {
               var property = typeof(WeaponComponentData).GetProperty(
                   name: "ItemUsage",
                   bindingAttr: BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
                   returnType: typeof(string),
                   types: Array.Empty<Type>(),
                   binder: null,
                   modifiers: null);
               return property;
           });

        public static void SetItemUsage(this WeaponComponentData obj, string value) => lazy_property_ItemUsage.Value.SetValue(obj, value);

        private static readonly Lazy<PropertyInfo> lazy_property_ItemFlags = new Lazy<PropertyInfo>(() =>
           {
               var property = typeof(ItemObject).GetProperty(
                   name: "ItemFlags",
                   bindingAttr: BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
                   returnType: typeof(ItemFlags),
                   types: Array.Empty<Type>(),
                   binder: null,
                   modifiers: null);
               return property;
           });

        public static void SetItemFlags(this ItemObject obj, ItemFlags value) => lazy_property_ItemFlags.Value.SetValue(obj, value);

        private static readonly Lazy<PropertyInfo> lazy_property_MaxDataValue = new Lazy<PropertyInfo>(() =>
           {
               var property = typeof(WeaponComponentData).GetProperty(
                   name: "MaxDataValue",
                   bindingAttr: BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
                   returnType: typeof(short),
                   types: Array.Empty<Type>(),
                   binder: null,
                   modifiers: null);
               return property;
           });

        public static void SetMaxDataValue(this WeaponComponentData obj, short value) => lazy_property_MaxDataValue.Value.SetValue(obj, value);

        private static bool HasAddAmmoValue(ushort value) => value > 0;

        private static short AddAmmoValue(short left, ushort right)
        {
            var value = left + right;
            if (value > short.MaxValue) value = short.MaxValue;
            return (short)value;
        }

        static bool UnlockItemCivilianV2EnableState;

        [Obsolete("invalid")]
        private static void UnlockItemCivilianV2()
        {
            if (UnlockItemCivilianV2EnableState) return;
            try
            {
                var property = typeof(ItemObject).GetProperty(nameof(ItemObject.IsCivilian), BindingFlags.Public | BindingFlags.Instance);
                if (property.PropertyType == typeof(bool))
                {
                    var destination = typeof(ItemObjectDest).GetMethod(nameof(ItemObjectDest.GetIsCivilianDest), BindingFlags.Public | BindingFlags.Instance);
                    var source = property.GetMethod;
                    if (source == default) return;
                    Hook.ReplaceMethod(source, destination);
                    UnlockItemCivilianV2EnableState = true;
                    if (Config.Instance.HasWin32Console())
                    {
                        Console.WriteLine("UnlockItemCivilianV2 Success.");
                    }
                }
            }
            catch (Exception e)
            {
                DisplayMessage(e);
            }
        }

        private class ItemObjectDest
        {
            private ItemObjectDest() => throw new NotSupportedException();

            [MethodImpl(MethodImplOptions.NoInlining)]
            public bool GetIsCivilianDest() => true;
        }

        public static bool HasCraftedItemObject() => ItemObject.All.Any(x => x.IsCraftedWeapon);

        [Conditional("DEBUG")]
        public static void PrintCraftedWeapons()
        {
            var str = string.Join(Environment.NewLine, ItemObject.All.Where(x => x.IsCraftedWeapon).Select(x => x.Name?.ToString()));
            Console.WriteLine("CraftedWeapons: ");
            Console.WriteLine(str);
        }

        partial class Config
        {
            public void HandleItemObjects(bool onlyCraftedWeapons = false)
            {
                string result;
                try
                {
                    //if (UnlockItemCivilian)
                    //{
                    //    UnlockItemCivilianV2();
                    //}

                    var items = onlyCraftedWeapons ?
                        ItemObject.All.Where(x => x.IsCraftedWeapon) : ItemObject.All.AsEnumerable();
                    if (items != default && items.Any())
                    {
                        var exceptions = new Exception[4];
                        var isAddAmmoByArrow = HasAddAmmoValue(AddAmmoByArrow);
                        var isAddAmmoByBolt = HasAddAmmoValue(AddAmmoByBolt);
                        var isAddAmmoByThrowingAxe = HasAddAmmoValue(AddAmmoByThrowingAxe);
                        var isAddAmmoByThrowingKnife = HasAddAmmoValue(AddAmmoByThrowingKnife);
                        var isAddAmmoByJavelin = HasAddAmmoValue(AddAmmoByJavelin);
                        var isAddAmmo = isAddAmmoByArrow || isAddAmmoByBolt || isAddAmmoByThrowingAxe || isAddAmmoByThrowingKnife || isAddAmmoByJavelin;
                        var isAddAmmoByJavelinSecondary = HasAddAmmoValue(AddAmmoByJavelinSecondary);
                        foreach (var item in items)
                        {
                            if (item == null) continue;

                            #region ClearItemDifficulty 0

                            if (ClearItemDifficulty && item.Difficulty > 0)
                            {
                                try
                                {
                                    item.SetDifficulty(0);
                                }
                                catch (Exception e_SetDifficulty)
                                {
                                    exceptions[0] = e_SetDifficulty;
                                }
                            }

                            #endregion

                            #region UnlockItemCivilian 1

                            if (UnlockItemCivilian && !item.ItemFlags.HasFlag(ItemFlags.Civilian))
                            {
                                try
                                {
                                    item.SetItemFlags(item.ItemFlags | ItemFlags.Civilian);
                                }
                                catch (Exception e_SetItemFlags)
                                {
                                    exceptions[1] = e_SetItemFlags;
                                }
                            }

                            #endregion

                            var hasPrimaryWeapon = item.PrimaryWeapon != null;

                            #region UnlockLongBowForUseOnHorseBack 2

                            if (UnlockLongBowForUseOnHorseBack && item.ItemType == ItemObject.ItemTypeEnum.Bow)
                            {
                                try
                                {
                                    var weapons = item.Weapons();
                                    weapons = weapons != null && weapons.Any() ?
                                        weapons :
                                        (hasPrimaryWeapon ? new[] { item.PrimaryWeapon } : null);
                                    if (weapons != null)
                                    {
                                        foreach (var weapon in weapons)
                                        {
                                            if (weapon.ItemUsage.Contains("long_bow"))
                                            {
                                                var itemUsage = weapon.ItemUsage
                                                    .Replace("long_bow", "bow");
                                                weapon.SetItemUsage(itemUsage);
                                            }
                                        }
                                    }
                                }
                                catch (Exception e_SetItemUsage)
                                {
                                    exceptions[2] = e_SetItemUsage;
                                }
                            }

                            #endregion

                            #region AddAmmo 3

                            // 增加 箭/弩箭/飞刀/标枪/飞斧 数量
                            // EquipmentElement.Ammo
                            // ItemObject.GetAmmoTypeForItemType Arrows箭/Bolts弩箭/Thrown投掷
                            // TaleWorlds.Core.WeaponClass Arrow/Bolt/ThrowingAxe飞斧/ThrowingKnife飞刀/Javelin标枪
                            if (hasPrimaryWeapon && isAddAmmo && item.PrimaryWeapon.IsConsumable)
                            {
                                ushort addValue = default;
                                switch (item.PrimaryWeapon.WeaponClass)
                                {
                                    case WeaponClass.Arrow when isAddAmmoByArrow:
                                        addValue = AddAmmoByArrow;
                                        break;

                                    case WeaponClass.Bolt when isAddAmmoByBolt:
                                        addValue = AddAmmoByBolt;
                                        break;

                                    case WeaponClass.ThrowingAxe when isAddAmmoByThrowingAxe:
                                        addValue = AddAmmoByThrowingAxe;
                                        break;

                                    case WeaponClass.ThrowingKnife when isAddAmmoByThrowingKnife:
                                        addValue = AddAmmoByThrowingKnife;
                                        break;

                                    case WeaponClass.Javelin when isAddAmmoByJavelin:
                                        addValue = AddAmmoByJavelin;
                                        break;
                                }
                                try
                                {
                                    if (addValue != default) item.PrimaryWeapon.SetMaxDataValue(AddAmmoValue(item.PrimaryWeapon.MaxDataValue, addValue));
                                }
                                catch (Exception e_SetMaxDataValue)
                                {
                                    exceptions[3] = e_SetMaxDataValue;
                                }
                            }

                            #endregion

                            #region AddAmmoByJavelinSecondary 3

                            try
                            {
                                if (isAddAmmoByJavelinSecondary)
                                {
                                    var weapons = item.Weapons();
                                    if (weapons != null && weapons.Count > 1)
                                    {
                                        foreach (var weapon in weapons)
                                        {
                                            if (weapon == null || weapon == item.PrimaryWeapon) continue;
                                            if (weapon.WeaponClass == WeaponClass.Javelin && weapon.IsConsumable)
                                            {
                                                var addValue = AddAmmoValue(
                                                    weapon.MaxDataValue,
                                                    AddAmmoByJavelinSecondary);
                                                weapon.SetMaxDataValue(addValue);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception e_SetMaxDataValue2)
                            {
#if DEBUG
                                Console.WriteLine("AddAmmoByJavelinSecondary catch");
                                Console.WriteLine(e_SetMaxDataValue2.Message);
                                Console.WriteLine(e_SetMaxDataValue2.StackTrace);
#endif
                                exceptions[3] = e_SetMaxDataValue2;
                            }

                            #endregion
                        }

                        #region Print Exception

                        var ex_values = exceptions.Where(x => x != null).ToArray();
                        if (ex_values.Any())
                        {
                            foreach (var exception in ex_values)
                            {
                                DisplayMessage(exception);
                            }
                            result = Catch;
                        }
                        else
                        {
                            result = Done;
                        }

                        #endregion

                        #region Print Single Result

                        if (ClearItemDifficulty)
                        {
                            DisplayMessage($"{Resources.ClearItemDifficulty}: {(exceptions[0] == null ? Resources.Done : Resources.Exception)}", Colors.BlueViolet);
                        }
                        if (UnlockItemCivilian)
                        {
                            DisplayMessage($"{Resources.UnlockItemCivilian}: {(exceptions[1] == null ? Resources.Done : Resources.Exception)}", Colors.BlueViolet);
                        }
                        if (UnlockLongBowForUseOnHorseBack)
                        {
                            DisplayMessage($"{Resources.UnlockLongBowForUseOnHorseBack}: {(exceptions[2] == null ? Resources.Done : Resources.Exception)}", Colors.BlueViolet);
                        }
                        if (isAddAmmo)
                        {
                            DisplayMessage($"{Resources.AddAmmo}: {(exceptions[3] == null ? Resources.Done : Resources.Exception)}", Colors.BlueViolet);
                        }

                        #endregion
                    }
                    else
                    {
                        result = NotFound;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine("Config.HandleItems catch");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
#endif
                    DisplayMessage(e);
                    result = Catch;
                }
                if (HasWin32Console()) // Print Total Result
                {
                    Console.WriteLine($"Config.HandleItems: {result}");
                }
            }
        }
    }
}