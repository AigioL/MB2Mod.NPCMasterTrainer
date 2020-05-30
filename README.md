# M&BII Mod NPC Master Trainer

### Command

| 命令 | 说明 |
| ---- | ------------- |
| npc.reset_perks [name] | 重置玩家部队中的角色 **技能** 点 |
| npc.reset_focus [name] | 重置玩家部队中的角色 **专精** 点 |
| npc.reset_attrs [name] | 重置玩家部队中的角色 **属性** 点 |
| npc.reset [name] | 重置玩家部队中的角色 **技能/专精/属性** 点 |
| npc.reset_perks_check_smith [bool] | 设置重置技能点时是否检查铁匠系技能，减去技能点添加的专精与属性，默认为 **false** |
| npc.check_legendary_smith [name] | 检查角色是否有 **传奇铁匠** 技能点 |
| npc.add_perk_legendary_smith [name] | 给指定的角色添加 **传奇铁匠** 技能点 |
| npc.refresh_last_seen_location | 刷新所有的流浪者和贵族在百科中显示的最后一次见到的位置 |
| print.towns_name_prosperity_desc [count] | 根据城镇繁荣度显示最高的 count 个城镇名 |
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

### Example

| 示例 | 说明 |
| ---- | ------------- |
| npc.reset_perks me | 重置玩家的技能点 |
| npc.reset_perks all_not_me | 重置玩家部队中除了玩家以外的角色技能点 |
| npc.reset_perks wanderer | 重置玩家部队中所有流浪者的技能点 |
| npc.reset_perks noble | 重置玩家部队中所有贵族(前/配偶/子女)的npc技能点 |
| npc.reset_perks morcon | 重置玩家部队中莫尔孔的技能点 |
| npc.reset_perks morcon-2 | 重置玩家部队中第二个莫尔孔的技能点 |
| npc.check_legendary_smith me | 检查玩家是否有传奇铁匠技能点 |
| npc.add_perk_legendary_smith me | 给玩家添加传奇铁匠技能点 |

### Config

| 键 | 默认值 | 说明 |
| ---- | ---- | ------------- |
| EnableDeveloperConsole | true | 启用开发者控制台 |
| ShowWin32Console | false | 是否显示Win32控制台 |
| ClearItemDifficulty | true | 清空物品的熟练度要求 |
| UnlockLongBowForUseOnHorseBack | true | 解锁长弓在马背上使用 |
| UnlockItemCivilian | true | 解锁平民装扮 |
| AddAmmoByArrow | 11 | 增加弹药量-**箭** |
| AddAmmoByBolt | 6 | 增加弹药量-**弩箭** |
| AddAmmoByThrowingAxe | 2 | 增加弹药量-**飞斧** |
| AddAmmoByThrowingKnife | 13 | 增加弹药量-**飞刀** |
| AddAmmoByJavelin | 1 | 增加弹药量-**标枪** |
