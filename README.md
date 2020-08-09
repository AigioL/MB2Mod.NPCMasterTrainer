# M&BII Mod NPC Master Trainer

[English](./README-EN.md) | 简体中文

[nexusmods](https://www.nexusmods.com/mountandblade2bannerlord/mods/1807)  
[bbs.mountblade.com.cn](https://bbs.mountblade.com.cn/thread-2064895-1-1.html)  

### **Config(配置文件说明)**
<table>
	<tr>
		<th align="left">键</th>
		<th align="left">类型</th>
		<th align="left">默认值</th>
		<th align="left">说明</th>
		<th align="left">在版本 e1.4.0.230377 中的原设定值 / 其他说明</th>
	</tr>
	<tr>
		<td align="left">EnableDeveloperConsole</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">启用开发者控制台</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">ShowWin32Console</td>
		<td align="left">bool</td>
		<td align="left">false</td>
		<td align="left">是否显示Win32控制台</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">ClearItemDifficulty</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">清空物品的熟练度要求</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">UnlockLongBowForUseOnHorseBack</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">解锁长弓在马背上使用</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">UnlockItemCivilian</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">解锁平民装扮</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">FixGetClipboardText</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">修复目前游戏中从剪贴板粘贴的中文文字出现乱码</td>
		<td align="left"></td>
	</tr>
	<tr>
		<th align="left" colspan="5">AddAmmo(添加弹药量)</th>
	</tr>
	<tr>
		<td align="left">AddAmmoByArrow</td>
		<td align="left">ushort</td>
		<td align="left">11</td>
		<td align="left">箭</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">AddAmmoByBolt</td>
		<td align="left">ushort</td>
		<td align="left">6</td>
		<td align="left">弩箭</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">AddAmmoByThrowingAxe</td>
		<td align="left">ushort</td>
		<td align="left">2</td>
		<td align="left">飞斧</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">AddAmmoByThrowingKnife</td>
		<td align="left">ushort</td>
		<td align="left">13</td>
		<td align="left">飞刀</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">AddAmmoByJavelin</td>
		<td align="left">ushort</td>
		<td align="left">1</td>
		<td align="left">标枪</td>
		<td align="left"></td>
	</tr>
	<tr>
		<th align="left" colspan="5">PregnancyModel(妊娠配置)</th>
	</tr>
	<tr>
		<td align="left">EnablePregnancyModel</td>
		<td align="left">bool</td>
		<td align="left">false</td>
		<td align="left">是否开启妊娠配置</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">CharacterFertilityProbability</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">在创建新游戏时设置所有角色可生育的占比</td>
		<td align="left">0.95</td>
	</tr>
	<tr>
		<td align="left">PregnancyDurationInDays</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">妊娠期(天数)</td>
		<td align="left">36</td>
	</tr>
	<tr>
		<td align="left">MaternalMortalityProbabilityInLabor</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">产妇分娩死亡率</td>
		<td align="left">0.015</td>
	</tr>
	<tr>
		<td align="left">StillbirthProbability</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">死胎概率</td>
		<td align="left">0.01</td>
	</tr>
	<tr>
		<td align="left">DeliveringFemaleOffspringProbability</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">生育女性后代率</td>
		<td align="left">0.51</td>
	</tr>
	<tr>
		<td align="left">DeliveringTwinsProbability</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">生双胞胎的概率</td>
		<td align="left">0.03</td>
	</tr>
	<tr>
		<td align="left">MaxPregnancyAge</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">最大孕龄</td>
		<td align="left" rowspan="2">45</td>
	</tr>
	<tr>
		<td align="left">MaxPregnancyAgeForMeOrMySpouse</td>
		<td align="left">float?</td>
		<td align="left">null</td>
		<td align="left">我或我的配偶的最大孕龄</td>
	</tr>
	<tr>
		<td align="left">AddDailyChanceOfPregnancyForHeroMultiple</td>
		<td align="left">ulong</td>
		<td align="left">1</td>
		<td align="left">每日怀孕几率倍数</td>
		<td align="left" rowspan="2">(乘法)此值仅不等于1时生效，如果为0则没有任何几率</td>
	</tr>
	<tr>
		<td align="left">AddDailyChanceOfPregnancyForMeOrMySpouseMultiple</td>
		<td align="left">ulong</td>
		<td align="left">1</td>
		<td align="left">我或我的配偶每日怀孕几率倍数</td>
	</tr>
	<tr>
		<th align="left" colspan="5">ClanTierModel(家族等级配置)</th>
	</tr>
	<tr>
		<td align="left">EnableClanTierModel</td>
		<td align="left">bool</td>
		<td align="left">false</td>
		<td align="left">是否开启家族等级配置</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">CompanionLimit</td>
		<td align="left">int?</td>
		<td align="left">null</td>
		<td align="left">玩家所能拥有的同伴(流浪者)数量</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">UnlockMaxTierCompanionLimit</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">解锁玩家最高家族等级所能拥有的同伴(流浪者)数量</td>
		<td align="left"></td>
	</tr>
	<tr>
		<th align="left" colspan="5">TroopCountLimitModel(藏身处人数配置)</th>
	</tr>
	<tr>
		<td align="left">EnableTroopCountLimitModel</td>
		<td align="left">bool</td>
		<td align="left">false</td>
		<td align="left">是否开启藏身处人数配置</td>
		<td align="left" rowspan="3">由于 e1.4.3 版本删除了原先的藏身处配置代码，此项配置已无法使用，在此Mod版本 1.0.5 中删除</td>
	</tr>
	<tr>
		<td align="left">HideoutBattlePlayerMaxTroopCount</td>
		<td align="left">int?</td>
		<td align="left">null</td>
		<td align="left">藏身处人数最大限制</td>
	</tr>
	<tr>
		<td align="left">UseAllNPCHideoutBattlePlayerMaxTroop</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">使用玩家部队中所有NPC总数作为最大限制</td>
	</tr>
	<tr>
		<th align="left" colspan="5">WorkshopModel(工坊配置)</th>
	</tr>
	<tr>
		<td align="left">EnableWorkshopModel</td>
		<td align="left">bool</td>
		<td align="left">false</td>
		<td align="left">开启工坊配置</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">MaxWorkshopCountForPlayer</td>
		<td align="left">int?</td>
		<td align="left">null</td>
		<td align="left">玩家可拥有的工坊最大数量</td>
		<td align="left" rowspan="3">工坊最大数量选项互斥，优先级从上到下</td>
	</tr>
	<tr>
		<td align="left">RemoveMaxWorkshopCountLimit</td>
		<td align="left">bool</td>
		<td align="left">false</td>
		<td align="left">解除玩家可拥有的工坊最大数量限制</td>
	</tr>
	<tr>
		<td align="left">UnlockMaxTierWorkshopCount</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">解锁玩家最高家族等级所能拥有的工坊数量</td>
	</tr>
	<tr>
		<th align="left" colspan="5">CreateWanderer(创建流浪者)</th>
	</tr>
	<tr>
		<td align="left">OnlyCreateFemaleOrMaleWanderer</td>
		<td align="left">bool?</td>
		<td align="left">null</td>
		<td align="left">仅创建女性或男性流浪者</td>
		<td align="left">true 仅创建女性 / false 仅创建男性</td>
	</tr>
	<tr>
		<td align="left">CreateWandererExcludeCultures</td>
		<td align="left">string[]</td>
		<td align="left">["empire"]</td>
		<td align="left">创建流浪者时排除的文化</td>
		<td align="left">因为帝国文化的流浪者属性比较拉跨，所以默认排除帝国，值为文化的英文名，可使用命令 print.cultures 查询所有的文化英文名</td>
	</tr>
	<tr>
		<th align="left" colspan="5">SetBattlefieldCommander(设置战场指挥官)</th>
	</tr>
	<tr>
		<td align="left">BattlefieldCommanderStringIds</td>
		<td align="left">string[]</td>
		<td align="left">null</td>
		<td align="left" colspan="2">设置战场上的默认指挥官，可设置多个指挥官在使用时循环玩家部队匹配首个，使用StringId指定，可使用 export_csv.all_hero 命令在生成的CSV文件中查看角色的StringId</td>
	</tr>
	<tr>
		<td align="left">EnableAfterDeathControl</td>
		<td align="left">bool</td>
		<td align="left">true</td>
		<td align="left">开启死后控制NPC</td>
		<td align="left">可能与 Mod [Control Your Allies After Death] 或类似功能的冲突，将此选项设为 false 可屏蔽本mod的功能避免冲突</td>
	</tr>
	<tr>
		<td align="left">AfterDeathControlOnly__Noble_Or_Wanderer_Or_NobleOrWanderer</td>
		<td align="left">bool?</td>
		<td align="left">null</td>
		<td align="left">死后控制NPC类型过滤</td>
		<td align="left">null 死后仅控制贵族或流浪者 / true 死后仅控制贵族 / false 死后仅控制流浪者</td>
	</tr>
	<tr>
		<td align="left">AfterDeathControlExcludePlayer</td>
		<td align="left">bool</td>
		<td align="left">false</td>
		<td align="left">死后控制NPC选择中排除玩家</td>
		<td align="left"></td>
	</tr>
</table>


### **Command(命令行使用说明)**
在游戏中 **CTRL 和 ~** 启用开发者控制台，开发者控制台目前仅能输入英文，中文会变成???，可在开发者控制台中输入下面的命令
<table>
	<tr>
		<th align="left">命令</th>
		<th align="left">说明</th>
	</tr>
	<tr>
		<th align="left" colspan="2">技能与专精与属性</th>
	</tr>
	<tr>
		<td align="left">npc.reset_perks [name]</td>
		<td align="left">重置玩家部队中的角色 技能 点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_focus [name]</td>
		<td align="left">重置玩家部队中的角色 专精 点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_attrs [name]</td>
		<td align="left">重置玩家部队中的角色 属性 点</td>
	</tr>
	<tr>
		<td align="left">npc.reset [name]</td>
		<td align="left">重置玩家部队中的角色 技能/专精/属性 点</td>
	</tr>
	<tr>
		<td align="left">npc.remove_attrs [name] | [attrType] | [value]</td>
		<td align="left">移除玩家部队中角色的 属性 并返还到可用点数</td>
	</tr>
	<tr>
		<td align="left">npc.remove_focus [name] | [row] | [column] | [value]</td>
		<td align="left">移除玩家部队中角色的 专精 并返还到可用点数</td>
	</tr>
	<tr>
		<th align="left" colspan="2">铁匠与锻造</th>
	</tr>
	<tr>
		<td align="left">npc.reset_perks_check_smith [bool]</td>
		<td align="left">设置重置技能点时是否检查铁匠系技能，减去技能点添加的专精与属性，默认为 false</td>
	</tr>
	<tr>
		<td align="left">npc.check_legendary_smith [name]</td>
		<td align="left">检查角色是否有 传奇铁匠 技能点</td>
	</tr>
	<tr>
		<td align="left">npc.add_perk_legendary_smith [name]</td>
		<td align="left">检查角色是否有 传奇铁匠 技能点</td>
	</tr>
	<tr>
		<th align="left" colspan="2">信息查询</th>
	</tr>
	<tr>
		<td align="left">npc.refresh_last_seen_location</td>
		<td align="left">刷新所有的流浪者和贵族在百科中显示的最后一次见到的位置</td>
	</tr>
	<tr>
		<td align="left">print.towns_name_prosperity_desc [count]</td>
		<td align="left">根据城镇繁荣度显示最高的 count 个城镇名</td>
	</tr>
	<tr>
		<td align="left">export_csv.query_path</td>
		<td align="left">查询导出csv文件路径</td>
	</tr>
	<tr>
		<td align="left">export_csv.open_dir</td>
		<td align="left">打开导出csv文件所在的文件夹</td>
	</tr>
	<tr>
		<th align="left" colspan="2">导出可视化表格</th>
	</tr>
	<tr>
		<td align="left">export_csv.wanderers</td>
		<td align="left">导出所有 流浪者 数据生成到csv文件中</td>
	</tr>
	<tr>
		<td align="left">export_csv.nobles</td>
		<td align="left">导出所有 贵族 数据生成到csv文件中</td>
	</tr>
	<tr>
		<td align="left">export_csv.all_hero</td>
		<td align="left">导出所有 角色 数据生成到csv文件中</td>
	</tr>
	<tr>
		<td align="left">export_csv.all_towns</td>
		<td align="left">导出所有 城镇 数据生成到csv文件中</td>
	</tr>
	<tr>
		<th align="left" colspan="2">检查与治疗不孕不育</th>
	</tr>
	<tr>
		<td align="left">npc.check_is_fertile [name]</td>
		<td align="left">检查角色是否可生育</td>
	</tr>
	<tr>
		<td align="left">npc.set_is_fertile_true [name]</td>
		<td align="left">设置角色可生育</td>
	</tr>
	<tr>
		<td align="left">npc.set_is_fertile_false [name]</td>
		<td align="left">设置角色不可生育</td>
	</tr>
	<tr>
		<th align="left" colspan="2">美容与整容</th>
	</tr>
	<tr>
		<td align="left">npc.change_body [name]</td>
		<td align="left">更改指定角色的捏脸数据，需将 捏脸数据(BodyProperties) 复制到 剪贴板 后执行</td>
	</tr>
	<tr>
		<td align="left">npc.random_body [name]</td>
		<td align="left">给指定角色重新随机生成一个新的捏脸数据(BodyProperties)</td>
	</tr>
	<tr>
		<th align="left" colspan="2">战场控制NPC</th>
	</tr>
	<tr>
		<td align="left">print.npcs_index</td>
		<td align="left">显示 玩家部队中的npc下标 对应npc名字，在左下角消息窗口中显示</td>
	</tr>
	<tr>
		<td align="left">npc_control.name [name]</td>
		<td align="left">在战场上控制npc(通过npc英文名指定)</td>
	</tr>
	<tr>
		<td align="left">npc_control.index [index]</td>
		<td align="left">在战场上控制指定npc(通过 玩家部队中的npc下标 )</td>
	</tr>
	<tr>
		<td align="left">npc_control.next</td>
		<td align="left">在战场上控制下一个npc</td>
	</tr>
	<tr>
		<td align="left">npc_control.next_noble</td>
		<td align="left">在战场上控制下一个npc(贵族)</td>
	</tr>
	<tr>
		<td align="left">npc_control.next_wanderer</td>
		<td align="left">在战场上控制下一个npc(流浪者)</td>
	</tr>
	<tr>
		<td align="left" colspan="2">使用命令指定的战场指挥官仅本次游戏中有效，加载存档，退出游戏后再进入都将使设定值失效，推荐使用Config配置中 BattlefieldCommanderStringIds 指定</td>
	</tr>
	<tr>
		<td align="left">npc_control.set_battle_commander_name [name]</td>
		<td align="left">设置战场指挥官(通过npc英文名指定)</td>
	</tr>
	<tr>
		<td align="left">npc_control.set_battle_commander_index [index]</td>
		<td align="left">设置战场指挥官(通过 玩家部队中的npc下标 )</td>
	</tr>
	<tr>
		<th align="left" colspan="2">其他杂项</th>
	</tr>
	<tr>
		<td align="left">npc.clone [name] | [count?]</td>
		<td align="left">克隆玩家部队中的角色</td>
	</tr>
	<tr>
		<td align="left">npc.fill_up [name] | [num?]</td>
		<td align="left">加满玩家部队中的角色 技能/专精/属性 点，需开启作弊模式</td>
	</tr>
	<tr>
		<td align="left" colspan="2">因开发者控制台无法输入中文，需将新的名字复制到剪贴板后执行下面的命令</td>
	</tr>
	<tr>
		<td align="left">rename.children [num]</td>
		<td align="left">玩家的第 num 个孩子重命名(num从1开始)</td>
	</tr>
</table>

### **Example(命令行使用示例)**
<table>
	<tr>
		<th align="left">示例</th>
		<th align="left">说明</th>
	</tr>
	<tr>
		<td align="left">npc.reset_perks me</td>
		<td align="left">重置玩家的技能点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_perks all_not_me</td>
		<td align="left">重置玩家部队中除了玩家以外的角色技能点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_perks wanderer</td>
		<td align="left">重置玩家部队中所有流浪者的技能点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_perks noble</td>
		<td align="left">重置玩家部队中所有贵族(前/配偶/子女)的npc技能点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_perks morcon</td>
		<td align="left">重置玩家部队中莫尔孔的技能点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_perks morcon-2</td>
		<td align="left">重置玩家部队中第二个莫尔孔的技能点</td>
	</tr>
	<tr>
		<td align="left">npc.check_legendary_smith me</td>
		<td align="left">检查玩家是否有传奇铁匠技能点</td>
	</tr>
	<tr>
		<td align="left">npc.add_perk_legendary_smith me</td>
		<td align="left">给玩家添加传奇铁匠技能点</td>
	</tr>
	<tr>
		<td align="left">npc.reset_perks khachin_the_swift bilik_the_she-wolf</td>
		<td align="left">重置 喀钦·捷影 和 比力克·母狼 的技能点</td>
	</tr>
	<tr>
		<td align="left">npc.check_is_fertile all_not_me</td>
		<td align="left">检查玩家部队中除了玩家以外的角色是否可生育</td>
	</tr>
	<tr>
		<td align="left">npc.npc.clone all_not_me | 15</td>
		<td align="left">在玩家部队中除了玩家之外的流浪者和贵族数量克隆到15个，如果当前为1个则+14，如果当前为20个则-5</td>
	</tr>
	<tr>
		<td align="left">npc.clone wanderer | 10</td>
		<td align="left">在玩家部队中的流浪者数量克隆到10个</td>
	</tr>
	<tr>
		<td align="left">npc.fill_up all</td>
		<td align="left">加满玩家部队中所有角色的 技能/专精/属性 点</td>
	</tr>
	<tr>
		<td align="left">npc.fill_up all | 999</td>
		<td align="left">加满玩家部队中所有角色的 技能/专精/属性 点，并将所有熟练度设为999</td>
	</tr>
</table>

### **Arguments Or Types(类型或参数名说明)**
<table>
	<tr>
		<th align="left">类型或参数名</th>
		<th align="left">说明</th>
		<th align="left">取值范围</th>
	</tr>
	<tr>
		<td align="left">count</td>
		<td align="left">总数</td>
		<td align="left">正整数</td>
	</tr>
	<tr>
		<td align="left">bool</td>
		<td align="left"></td>
		<td align="left">true 或 false</td>
	</tr>
	<tr>
		<td align="left">row</td>
		<td align="left">技能面板左侧列，属性</td>
		<td align="left">1~6的整数</td>
	</tr>
	<tr>
		<td align="left">column</td>
		<td align="left">技能面板左侧行，技能</td>
		<td align="left">1~3的整数</td>
	</tr>
	<tr>
		<td align="left">index</td>
		<td align="left">下标从0开始, 0和正整数</td>
		<td align="left"></td>
	</tr>
	<tr>
		<td align="left">attrType</td>
		<td align="left">技能面板左侧列，属性</td>
		<td align="left">1~6整数 或 Vigor, Control, Endurance, Cunning, Social, Intelligence</td>
	</tr>
	<tr>
		<td align="left">name</td>
		<td align="left" colspan="2">固定值 me(我),all_not_me(除了我之外),wanderer(流浪者),noble(贵族) 或 角色英文名(在[ESC-选项-游戏设置]更改语言为英语可看到角色的英文名，如果名字存在空格，需要使用下划线(_)替代空格。如果存在多个重名角色，在名字后加上-2，指定第2个)</td>
	</tr>
</table>
