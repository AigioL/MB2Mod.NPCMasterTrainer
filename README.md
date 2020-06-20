# M&BII Mod NPC Master Trainer [nexusmods](https://www.nexusmods.com/mountandblade2bannerlord/mods/1807) [bbs.mountblade.com.cn](https://bbs.mountblade.com.cn/thread-2064895-1-1.html)

### Command

| 命令 | 说明 |
| ---- | ------------- |
| npc.clone [name] \| [count?] | 克隆玩家部队中的角色 |
| npc.remove_attrs [name] \| [attrType] \| [value] | 移除玩家部队中角色的 **属性** 并返还到可用点数 |
| npc.remove_focus [name] \| [row] \| [column] \| [value] | 移除玩家部队中角色的 **专精** 并返还到可用点数 |
| npc.reset_perks [name] | 重置玩家部队中的角色 **技能** 点 |
| npc.reset_focus [name] | 重置玩家部队中的角色 **专精** 点 |
| npc.reset_attrs [name] | 重置玩家部队中的角色 **属性** 点 |
| npc.reset [name] | 重置玩家部队中的角色 **技能/专精/属性** 点 |
| npc.reset_perks_check_smith [bool] | 设置重置技能点时是否检查铁匠系技能，减去技能点添加的专精与属性，默认为 **false** |
| npc.check_legendary_smith [name] | 检查角色是否有 **传奇铁匠** 技能点 |
| npc.add_perk_legendary_smith [name] | 给指定的角色添加 **传奇铁匠** 技能点 |
| npc.refresh_last_seen_location | 刷新所有的流浪者和贵族在百科中显示的最后一次见到的位置 |
| npc.change_body [name] | 更改指定角色的捏脸数据，需将 **捏脸数据** 复制到 **剪贴板** 后执行 |
| npc.random_body [name] | 给指定角色重新随机生成一个新的捏脸数据 |
| npc.check_is_fertile [name] | 检查角色是否可生育 |
| npc.set_is_fertile_true [name] | 设置角色可生育 |
| npc.set_is_fertile_false [name] | 设置角色不可生育 |
| npc.fill_up [name] \| [num?] | 加满玩家部队中的角色 **技能/专精/属性** 点，需开启作弊模式 |
| npc_control.name [name] | 在战场上控制指定npc |
| npc_control.index [index] | 在战场上控制指定npc |
| npc_control.next | 在战场上控制下一个npc |
| npc_control.next_noble | 在战场上控制下一个npc(贵族) |
| npc_control.next_wanderer | 在战场上控制下一个npc(流浪者) |
| print.towns_name_prosperity_desc [count] | 根据城镇繁荣度显示最高的 count 个城镇名 |
| 目前开发者控制台无法输入中文，需将 **新的名字** 复制到 **剪贴板** 后执行下面的命令 |
| rename.children [num] | 玩家的第 num 个孩子重命名(num从1开始) |
| export_csv.query_path | 查询导出csv文件路径 |
| export_csv.open_dir | 打开导出csv文件所在的文件夹 |
| export_csv.wanderers | 导出所有 **流浪者** 数据生成到csv文件中 |
| export_csv.nobles | 导出所有 **贵族** 数据生成到csv文件中 |
| export_csv.all_hero | 导出所有 **角色** 数据生成到csv文件中 |
| export_csv.all_towns | 导出所有 **城镇** 数据生成到csv文件中 |

### Arguments

| 参数名 | 说明 |
| ---- | ------------- |
| count | 正整数 |
| bool | true 或 false |
| name | 固定值(me,all_not_me,wanderer,noble)或角色英文名(如果名字存在空格，需要<br />使用下划线替代空格。如果存在多个重名角色，在名字后加上-2，指定第2个) |
| attrType | Vigor(1), Control(2), Endurance(3), Cunning(4), Social(5), Intelligence(6) |
| row | 1~6 |
| column | 1~3 |

### Example

| 示例 | 说明 |
| ---- | ------------- |
| npc.clone noble | 玩家部队中的所有贵族克隆1个 |
| npc.clone wanderer \| 10 | 玩家部队中的所有流浪者克隆10个 |
| npc.clone all_not_me \| 15 | 玩家部队中的除了玩家之外的贵族或流浪者克隆15个 |
| npc.remove_attrs me \| 1 \| 1 | 移除玩家1活力属性点并返还到可用点数 |
| npc.remove_attrs me \| Vigor \| 1 | 同上 |
| npc.remove_focus me \| 2 \| 1 \| 1 | 移除玩家1弓专精点并返还到可用点数 |
| npc.reset_perks me | 重置玩家的技能点 |
| npc.reset_perks all_not_me | 重置玩家部队中除了玩家以外的角色技能点 |
| npc.reset_perks wanderer | 重置玩家部队中所有流浪者的技能点 |
| npc.reset_perks noble | 重置玩家部队中所有贵族(前/配偶/子女)的npc技能点 |
| npc.reset_perks morcon | 重置玩家部队中莫尔孔的技能点 |
| npc.reset_perks morcon-2 | 重置玩家部队中第二个莫尔孔的技能点 |
| npc.check_legendary_smith me | 检查玩家是否有传奇铁匠技能点 |
| npc.add_perk_legendary_smith me | 给玩家添加传奇铁匠技能点 |
| npc.check_is_fertile all_not_me | 检查玩家部队中除了玩家以外的角色是否可生育 |
| npc.fill_up all | 加满玩家部队中所有角色的 **技能/专精/属性** 点 |
| npc.fill_up all \| 999 | 加满玩家部队中所有角色的 **技能/专精/属性** 点，并将所有熟练度设为999 |

### Config

| 键 | 类型 | 默认值 | 说明 |
| ---- | ---- | ---- | ------------- |
| EnableDeveloperConsole | bool | true | 启用开发者控制台 |
| ShowWin32Console | bool | false | 是否显示Win32控制台 |
| ClearItemDifficulty | bool | true | 清空物品的熟练度要求 |
| UnlockLongBowForUseOnHorseBack | bool | true | 解锁长弓在马背上使用 |
| UnlockItemCivilian | bool | true | 解锁平民装扮 |
| FixGetClipboardText | bool | true | 修复目前游戏中从剪贴板粘贴的中文文字出现乱码 |

#### AddAmmo

弹药量值范围 0~32767

其他整数值范围 0~65535

| 键 | 类型 | 默认值 | 说明 |
| ---- | ---- | ---- | ------------- |
| AddAmmoByArrow | ushort | 11 | 增加弹药量-**箭** |
| AddAmmoByBolt | ushort | 6 | 增加弹药量-**弩箭** |
| AddAmmoByThrowingAxe | ushort | 2 | 增加弹药量-**飞斧** |
| AddAmmoByThrowingKnife | ushort | 13 | 增加弹药量-**飞刀** |
| AddAmmoByJavelin | ushort | 1 | 增加弹药量-**标枪** |

#### Pregnancy

| 键 | 类型 | 默认值 | 说明 |
| ---- | ---- | ---- | ------------- |
| EnablePregnancyModel | bool | false | 是否开启妊娠配置 |
| CharacterFertilityProbability | float? | null | 在创建新游戏时设置所有角色可生育的占比，当前版本(1.4.0.230377)默认值为 0.95 |
| PregnancyDurationInDays | float? | null | 妊娠期(天数)，当前版本(1.4.0.230377)默认值为 36 |
| MaternalMortalityProbabilityInLabor | float? | null | 产妇分娩死亡率，当前版本(1.4.0.230377)默认值为 0.015 |
| StillbirthProbability | float? | null | 死胎概率，当前版本(1.4.0.230377)默认值为 0.01 |
| DeliveringFemaleOffspringProbability | float? | null | 生育女性后代率，当前版本(1.4.0.230377)默认值为 0.51 |
| DeliveringTwinsProbability | float? | null | 生双胞胎的概率，当前版本(1.4.0.230377)默认值为 0.03 |
| MaxPregnancyAge | ushort? | null | 最大孕龄，当前版本(1.4.0.230377)默认值为 45 |
| MaxPregnancyAgeForMeOrMySpouse | ushort? | null | 我或我的配偶的最大孕龄，当前版本(1.4.0.230377)默认值为 45 |
| AddDailyChanceOfPregnancyForHeroMultiple | ulong | 1 | 增加每日怀孕几率倍数 |
| AddDailyChanceOfPregnancyForMeOrMySpouseMultiple | ulong | 1 | 增加我或我的配偶每日怀孕几率倍数 |

#### ClanTier

| 键 | 类型 | 默认值 | 说明 |
| ---- | ---- | ---- | ------------- |
| EnableClanTierModel | bool | false | 开启家族等级配置 |
| CompanionLimit | int? | null | 玩家所能拥有的同伴(流浪者)数量 |
| UnlockMaxTierCompanionLimit | bool | true | 解锁玩家最高家族等级所能拥有的同伴(流浪者)数量 |

#### TroopCountLimit

| 键 | 类型 | 默认值 | 说明 |
| ---- | ---- | ---- | ------------- |
| EnableTroopCountLimitModel | bool | false | 开启藏身处人数配置 |
| HideoutBattlePlayerMaxTroopCount | int? | null | 藏身处人数最大限制 |
| UseAllNPCHideoutBattlePlayerMaxTroop | bool | true | 使用玩家部队中所有NPC总数作为最大限制 |

#### Workshop

| 键 | 类型 | 默认值 | 说明 |
| ---- | ---- | ---- | ------------- |
| EnableWorkshopModel | bool | false | 开启工坊配置 |
| MaxWorkshopCountForPlayer | int? | null | 玩家可拥有的工坊最大数量 |
| RemoveMaxWorkshopCountLimit | bool | false | 解除玩家可拥有的工坊最大数量限制 |
| UnlockMaxTierWorkshopCount | bool | true | 解锁玩家最高家族等级所能拥有的工坊数量 |