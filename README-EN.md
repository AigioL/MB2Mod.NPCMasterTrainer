# M&BII Mod NPC Master Trainer

English | [简体中文](./README.md)

[nexusmods](https://www.nexusmods.com/mountandblade2bannerlord/mods/1807)  
[bbs.mountblade.com.cn](https://bbs.mountblade.com.cn/thread-2064895-1-1.html)

### **Config (configuration file description)**
<table> 
  <tr> 
    <th align="left">key</th>  
    <th align="left">Type</th>  
    <th align="left">default value</th>  
    <th align="left">Description</th>  
    <th align="left">The original settings in version e1.4.0.230377 / other explanations</th> 
  </tr>  
  <tr> 
    <td align="left">EnableDeveloperConsole</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Enable the developer console</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">ShowWin32Console</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">Whether to display Win32 console</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">ClearItemDifficulty</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Proficiency requirements for emptying items</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">UnlockLongBowForUseOnHorseBack</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Unlock the longbow for use on horseback</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">UnlockItemCivilian</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Unlock civilian costumes</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">FixGetClipboardText</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Fixed possible garbled non English characters pasted from the clipboard in the current game</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <th align="left" colspan="5">AddAmmo (Add Ammo)</th> 
  </tr>  
  <tr> 
    <td align="left">AddAmmoByArrow</td>  
    <td align="left">ushort</td>  
    <td align="left">11</td>  
    <td align="left">Arrow</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">AddAmmoByBolt</td>  
    <td align="left">ushort</td>  
    <td align="left">6</td>  
    <td align="left">crossbow arrows</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">AddAmmoByThrowingAxe</td>  
    <td align="left">ushort</td>  
    <td align="left">2</td>  
    <td align="left">Throwing axe</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">AddAmmoByThrowingKnife</td>  
    <td align="left">ushort</td>  
    <td align="left">13</td>  
    <td align="left">Flying knife</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">AddAmmoByJavelin</td>  
    <td align="left">ushort</td>  
    <td align="left">1</td>  
    <td align="left">Javelin</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">AddAmmoByJavelinSecondary</td>  
    <td align="left">ushort</td>  
    <td align="left">0</td>  
    <td align="left">Javelin (Secondary weapon is a long rod weapon that needs to be switched to javelin by pressing X)</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <th align="left" colspan="5">PregnancyModel (pregnancy configuration)</th> 
  </tr>  
  <tr> 
    <td align="left">EnablePregnancyModel</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">Whether to enable pregnancy configuration</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">CharacterFertilityProbability</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Set the proportion of fertility for all characters when creating a new game</td>  
    <td align="left">0.95(This item does not exist in e1.5.2 and is deleted in mod version 1.0.9)</td> 
  </tr>  
  <tr> 
    <td align="left">PregnancyDurationInDays</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Pregnancy period (number of days)</td>  
    <td align="left">36</td> 
  </tr>  
  <tr> 
    <td align="left">MaternalMortalityProbabilityInLabor</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Maternal childbirth mortality rate</td>  
    <td align="left">0.015</td> 
  </tr>  
  <tr> 
    <td align="left">StillbirthProbability</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Probability of stillbirth</td>  
    <td align="left">0.01</td> 
  </tr>  
  <tr> 
    <td align="left">DeliveringFemaleOffspringProbability</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Female child-bearing rate</td>  
    <td align="left">0.51</td> 
  </tr>  
  <tr> 
    <td align="left">DeliveringTwinsProbability</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Probability of having twins</td>  
    <td align="left">0.03</td> 
  </tr>  
  <tr> 
    <td align="left">MaxPregnancyAge</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Maximum gestational age</td>  
    <td align="left" rowspan="2">45</td> 
  </tr>  
  <tr> 
    <td align="left">MaxPregnancyAgeForMeOrMySpouse</td>  
    <td align="left">float?</td>  
    <td align="left">null</td>  
    <td align="left">Maximum gestational age of me or my spouse</td> 
  </tr>  
  <tr> 
    <td align="left">AddDailyChanceOfPregnancyForHeroMultiple</td>  
    <td align="left">ulong</td>  
    <td align="left">1</td>  
    <td align="left">daily pregnancy odds multiples</td>  
    <td align="left" rowspan="2">(Multiplication) This value only takes effect when it is not equal to 1, and if it is 0, there is no chance.</td> 
  </tr>  
  <tr> 
    <td align="left">AddDailyChanceOfPregnancyForMeOrMySpouseMultiple</td>  
    <td align="left">ulong</td>  
    <td align="left">1</td>  
    <td align="left">I or my spouse’s daily pregnancy odds multiples</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="5">ClanTierModel (family level configuration)</th> 
  </tr>  
  <tr> 
    <td align="left">EnableClanTierModel</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">Whether to enable family level configuration</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">CompanionLimit</td>  
    <td align="left">int?</td>  
    <td align="left">null</td>  
    <td align="left">The number of companions (wanderers) a player can have</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">UnlockMaxTierCompanionLimit</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Unlock the number of companions (wanderers) a player can have at the highest family level</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <th align="left" colspan="5">TroopCountLimitModel (the number of people in the hideout)</th> 
  </tr>  
  <tr> 
    <td align="left">EnableTroopCountLimitModel</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">Whether to enable the number allocation in the hideout</td>  
    <td align="left" rowspan="3">Because the e1.4.3 version deleted the original hideout configuration code, this configuration is no longer available, it will be deleted in this Mod version 1.0.5</td> 
  </tr>  
  <tr> 
    <td align="left">HideoutBattlePlayerMaxTroopCount</td>  
    <td align="left">int?</td>  
    <td align="left">null</td>  
    <td align="left">Maximum number of people in the hideout</td> 
  </tr>  
  <tr> 
    <td align="left">UseAllNPCHideoutBattlePlayerMaxTroop</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Use the total number of NPCs in the player’s army as the maximum limit</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="5">WorkshopModel(Workshop configuration)</th> 
  </tr>  
  <tr> 
    <td align="left">EnableWorkshopModel</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">Open workshop configuration</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">MaxWorkshopCountForPlayer</td>  
    <td align="left">int?</td>  
    <td align="left">null</td>  
    <td align="left">Maximum number of workshops a player can own</td>  
    <td align="left" rowspan="3">The maximum number of workshop options are mutually exclusive, with priority from top to bottom</td> 
  </tr>  
  <tr> 
    <td align="left">RemoveMaxWorkshopCountLimit</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">Remove the maximum number of workshops a player can own</td> 
  </tr>  
  <tr> 
    <td align="left">UnlockMaxTierWorkshopCount</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Unlock the number of workshops a player can have at the highest family level</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="5">CreateWanderer (Create Wanderer)</th> 
  </tr>  
  <tr> 
    <td align="left">OnlyCreateFemaleOrMaleWanderer</td>  
    <td align="left">bool?</td>  
    <td align="left">null</td>  
    <td align="left">Create only female or male wanderers</td>  
    <td align="left">true only create female / false only create male</td> 
  </tr>  
  <tr> 
    <td align="left">CreateWandererExcludeCultures</td>  
    <td align="left">string[]</td>  
    <td align="left">["empire"]</td>  
    <td align="left">Culture excluded when creating homeless</td>  
    <td align="left">Because the wanderer attribute of the empire culture is relatively stretched, the empire is excluded by default, and the value is the English name of the culture. You can use the command print.cultures to query all the English names of the culture</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="5">SetBattlefieldCommander (Set Battlefield Commander)</th> 
  </tr>  
  <tr> 
    <td align="left">BattlefieldCommanderStringIds</td>  
    <td align="left">string[]</td>  
    <td align="left">null</td>  
    <td align="left" colspan="2">Set the default commander on the battlefield, you can set multiple commanders to cycle the player’s forces to match the first one when in use, use StringId to specify, you can use export_csv.all_hero command to generate View the StringId of the role in CSV file</td> 
  </tr>  
  <tr> 
    <td align="left">EnableAfterDeathControl</td>  
    <td align="left">bool</td>  
    <td align="left">true</td>  
    <td align="left">Enable NPC control after death</td>  
    <td align="left">It may conflict with Mod [Control Your Allies After Death] or similar functions. Set this option to false to block the mod’s functions to avoid conflicts</td> 
  </tr>  
  <tr> 
    <td align="left">AfterDeathControlOnly__Noble_Or_Wanderer_Or_NobleOrWanderer</td>  
    <td align="left">bool?</td>  
    <td align="left">null</td>  
    <td align="left">Control NPC type filtering after death</td>  
    <td align="left">null only controls the nobles or wanderers after death / true controls only the nobles after death / false controls only the wanderers after death</td> 
  </tr>  
  <tr> 
    <td align="left">AfterDeathControlExcludePlayer</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">Exclude players from control NPC selection after death</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">MySpouseCanDiedInLabor</td>  
    <td align="left">bool</td>  
    <td align="left">false</td>  
    <td align="left">My spouse will die of childbirth. If the player's role is male, the spouse will have a certain probability of dying of dystocia during childbirth. The default value of this option is false to avoid the above situation. If this value is set to true, the original logic will not be modified</td>  
    <td align="left"/> 
  </tr>
  <tr>
  	<th align="left" colspan="4">AgeModel(Age configuration)</th>
  	<th align="left">The original settings in version e1.5.4.245039</th>
  </tr>
  <tr>
  	<td align="left">EnableAgeModel</td>
  	<td align="left">bool</td>
  	<td align="left">false</td>
  	<td align="left"></td>
  	<td align="left"></td>
  </tr>
  <tr>
  	<td align="left">BecomeInfantAge</td>
  	<td align="left">int?</td>
  	<td align="left">null</td>
  	<td align="left"></td>
  	<td align="left">3</td>
  </tr>
  <tr>
  	<td align="left">BecomeChildAge</td>
  	<td align="left">int?</td>
  	<td align="left">null</td>
  	<td align="left"></td>
  	<td align="left">6</td>
  </tr>
  <tr>
  	<td align="left">BecomeTeenagerAge</td>
  	<td align="left">int?</td>
  	<td align="left">null</td>
  	<td align="left"></td>
  	<td align="left">14</td>
  </tr>
  <tr>
  	<td align="left">HeroComesOfAge</td>
  	<td align="left">int?</td>
  	<td align="left">null</td>
  	<td align="left"></td>
  	<td align="left">18</td>
  </tr>
  <tr>
  	<td align="left">BecomeOldAge</td>
  	<td align="left">int?</td>
  	<td align="left">null</td>
  	<td align="left"></td>
  	<td align="left">47</td>
  </tr>
  <tr>
  	<td align="left">MaxAge</td>
  	<td align="left">int?</td>
  	<td align="left">null</td>
  	<td align="left"></td>
  	<td align="left">128</td>
  </tr> 
</table>

### **Command (Command line instructions)**
CTRL and ~ enable the developer console in the game. Currently, the developer console can only type in English. non English char will become ???. You can enter the following commands in the developer console
<table> 
  <tr> 
    <th align="left">Command</th>  
    <th align="left">Description</th> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Skills and specializations and attributes</th> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks [name]</td>  
    <td align="left">Reset the character skill points in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_focus [name]</td>  
    <td align="left">Reset the character in the player’s army. Specialization point</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_attrs [name]</td>  
    <td align="left">Reset the character attribute points in the player’s troops</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset [name]</td>  
    <td align="left">Reset the character skill/specialization/attribute points in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.remove_attrs [name] | [attrType] | [value]</td>  
    <td align="left">Remove the attributes of the characters in the player’s army and return them to the available points. The attributes must be kept at least 1 point</td> 
  </tr>  
  <tr> 
    <td align="left">npc.remove_focus [name] | [row] | [column] | [value]</td>  
    <td align="left">Remove the focus of the character from the player’s army and return it to the available points. All focus can be returned</td> 
  </tr>  
  <tr> 
    <td align="left">npc.remove_focus_by_entire_line [name] | [row] | [value]</td>  
    <td align="left">Remove the specialization of a entire line of a character in the player's army and return it to the available points</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Blacksmiths and Forging</th> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks_check_smith [bool]</td>  
    <td align="left">Set whether to check the blacksmith skills when resetting the skill points, minus the specialization and attributes added by the skill points, the default is false</td> 
  </tr>  
  <tr> 
    <td align="left">npc.check_legendary_smith [name]</td>  
    <td align="left">Check if the character has Legendary Blacksmith skill points</td> 
  </tr>  
  <tr> 
    <td align="left">npc.add_perk_legendary_smith [name]</td>  
    <td align="left">Check if the character has Legendary Blacksmith skill points</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Information query</th> 
  </tr>  
  <tr> 
    <td align="left">npc.refresh_last_seen_location</td>  
    <td align="left">Refresh all the wanderers and nobles’ last seen positions in the encyclopedia</td> 
  </tr>  
  <tr> 
    <td align="left">print.towns_name_prosperity_desc [count]</td>  
    <td align="left">Display the count town names with the highest prosperity according to the town’s prosperity</td> 
  </tr>  
  <tr> 
    <td align="left">export_csv.query_path</td>  
    <td align="left">Query the path of the exported csv file</td> 
  </tr>  
  <tr> 
    <td align="left">export_csv.open_dir</td>  
    <td align="left">Open the folder where the exported csv file is located</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Export visualization table</th> 
  </tr>  
  <tr> 
    <td align="left">export_csv.wanderers</td>  
    <td align="left">Export all the vagrant data to a csv file</td> 
  </tr>  
  <tr> 
    <td align="left">export_csv.nobles</td>  
    <td align="left">Export all the nobles data to a csv file</td> 
  </tr>  
  <tr> 
    <td align="left">export_csv.all_hero</td>  
    <td align="left">Export all character data to a csv file</td> 
  </tr>  
  <tr> 
    <td align="left">export_csv.all_towns</td>  
    <td align="left">Export all town data to a csv file</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Inspection and treatment of infertility</th> 
  </tr>  
  <tr> 
    <td align="left">npc.check_is_fertile [name]</td>  
    <td align="left">Check if the character is fertile</td> 
  </tr>  
  <tr> 
    <td align="left">npc.set_is_fertile_true [name]</td>  
    <td align="left">Set the role to be fertile</td> 
  </tr>  
  <tr> 
    <td align="left">npc.set_is_fertile_false [name]</td>  
    <td align="left">Set the role to be non-fertile</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Beauty and plastic surgery</th> 
  </tr>  
  <tr> 
    <td align="left">npc.change_body [name]</td>  
    <td align="left">To change the pinched face data of the specified character, copy the pinched face data (BodyProperties) to the clipboard and execute it</td> 
  </tr>  
  <tr> 
    <td align="left">npc.random_body [name]</td>  
    <td align="left">Randomly generate a new pinch face data (BodyProperties) for the specified character</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Battlefield control NPC</th> 
  </tr>  
  <tr> 
    <td align="left">print.npcs_index</td>  
    <td align="left">Display the npc subscript in the player’s army corresponding to the npc name, displayed in the message window in the lower left corner</td> 
  </tr>  
  <tr> 
    <td align="left">npc_control.name [name]</td>  
    <td align="left">Control npc on the battlefield (specified by the English name of npc)</td> 
  </tr>  
  <tr> 
    <td align="left">npc_control.index [index]</td>  
    <td align="left">Control the designated npc on the battlefield (via the npc subscript in the player’s army)</td> 
  </tr>  
  <tr> 
    <td align="left">npc_control.next</td>  
    <td align="left">Control the next npc on the battlefield</td> 
  </tr>  
  <tr> 
    <td align="left">npc_control.next_noble</td>  
    <td align="left">Control the next npc (noble) on the battlefield</td> 
  </tr>  
  <tr> 
    <td align="left">npc_control.next_wanderer</td>  
    <td align="left">Control the next npc (wanderer) on the battlefield</td> 
  </tr>  
  <tr> 
    <td align="left" colspan="2">The battlefield commander specified by the command is only valid in this game. Loading the archive, and re-entering after exiting the game will invalidate the set value. It is recommended to use the BattlefieldCommanderStringIds specified in the Config configuration</td> 
  </tr>  
  <tr> 
    <td align="left">npc_control.set_battle_commander_name [name]</td>  
    <td align="left">Set up a battlefield commander (specified by the English name of npc)</td> 
  </tr>  
  <tr> 
    <td align="left">npc_control.set_battle_commander_index [index]</td>  
    <td align="left">Set the battlefield commander (via the npc subscript in the player’s army)</td> 
  </tr>  
  <tr> 
    <th align="left" colspan="2">Other miscellaneous</th> 
  </tr>  
  <tr> 
    <td align="left">npc.clone [name] | [count?]</td>  
    <td align="left">Clone the character in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.clone_fix_empty_wounded</td>  
    <td align="left">Fixed a permanent casualty in a player's unit caused by the use of clone player's character</td> 
  </tr>  
  <tr> 
    <td align="left">npc.fill_up [name] | [num?]</td>  
    <td align="left">To fill up the character’s skill/specialization/attribute points in the player’s army, the cheat mode must be turned on</td> 
  </tr>  
  <tr> 
    <td align="left" colspan="2">Because the developer console cannot input non English Char, you need to copy the new name to the clipboard and execute the following command</td> 
  </tr>  
  <tr> 
    <td align="left">rename.children [num]</td>  
    <td align="left">The player's numth child is renamed (num starts from 1)</td> 
  </tr>
  <tr>
    <td align="left">campaign.kill_player</td>
    <td align="left">Kill the player immediately and choose the heir</td>
  </tr>
  <tr>
    <td align="left">config.handle_crafted_weapon_item_npcmt</td>
    <td align="left">Re-run all forged items according to Config (unlock civilian mode, unlock proficiency, use longbow on horseback, add ammunition). You can use this command after the blacksmith forges new items. If the forged weapon contains ammunition such as javelins Items, execution of this command will cause the ammunition to increase again, and the file can be read back to normal after saving</td>
  </tr>
	<tr>
		<td align="left">npc.set_age [name] | [age]</td>
		<td align="left">set npc age by hero name</td>
	</tr>
</table>

### **Example (Command line usage example)**
<table> 
  <tr> 
    <th align="left">Example</th>  
    <th align="left">Description</th> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks me</td>  
    <td align="left">Reset the player’s skill points</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks all_not_me</td>  
    <td align="left">Reset the skill points of characters other than players in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks wanderer</td>  
    <td align="left">Reset the skill points of all Rangers in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks noble</td>  
    <td align="left">Reset the npc skill points of all nobles (former/spouse/children) in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks morcon</td>  
    <td align="left">Reset Morcon’s skill points in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks morcon-2</td>  
    <td align="left">Reset the skill point of the second Morcon in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.check_legendary_smith me</td>  
    <td align="left">Check if the player has legendary blacksmith skill points</td> 
  </tr>  
  <tr> 
    <td align="left">npc.add_perk_legendary_smith me</td>  
    <td align="left">Add legendary blacksmith skill points to players</td> 
  </tr>  
  <tr> 
    <td align="left">npc.reset_perks khachin_the_swift bilik_the_she-wolf</td>  
    <td align="left">Reset the skill points of Kachin Jie Ying and Bilik Shewolf</td> 
  </tr>  
  <tr> 
    <td align="left">npc.check_is_fertile all_not_me</td>  
    <td align="left">Check whether characters other than players in the player’s army are fertile</td> 
  </tr>  
  <tr> 
    <td align="left">npc.npc.clone all_not_me | 15</td>  
    <td align="left">The number of Wanderers and nobles in the player’s army other than the player is cloned to 15, if it is currently 1, it is +14, if it is currently 20 it is -5</td> 
  </tr>  
  <tr> 
    <td align="left">npc.clone wanderer | 10</td>  
    <td align="left">The number of Rangers in the player’s army is cloned to 10</td> 
  </tr>  
  <tr> 
    <td align="left">npc.fill_up all</td>  
    <td align="left">Fill up the skill/specialization/attribute points of all characters in the player’s army</td> 
  </tr>  
  <tr> 
    <td align="left">npc.fill_up all | 999</td>  
    <td align="left">Fill up the skill/specialization/attribute points of all characters in the player’s army and set all proficiency to 999</td> 
  </tr> 
</table>

### **Arguments Or Types (type or parameter name description)**
<table> 
  <tr> 
    <th align="left">Type or parameter name</th>  
    <th align="left">Description</th>  
    <th align="left">Value range</th> 
  </tr>  
  <tr> 
    <td align="left">count</td>  
    <td align="left">Total</td>  
    <td align="left">positive integer</td> 
  </tr>  
  <tr> 
    <td align="left">bool</td>  
    <td align="left"/>  
    <td align="left">true or false</td> 
  </tr>  
  <tr> 
    <td align="left">row</td>  
    <td align="left">The left column of the skill panel, attributes</td>  
    <td align="left">1~6 integer</td> 
  </tr>  
  <tr> 
    <td align="left">column</td>  
    <td align="left">The left row of the skills panel, skills</td>  
    <td align="left">1~3 integer</td> 
  </tr>  
  <tr> 
    <td align="left">index</td>  
    <td align="left">The subscript starts from 0, 0 and a positive integer</td>  
    <td align="left"/> 
  </tr>  
  <tr> 
    <td align="left">attrType</td>  
    <td align="left">The left column of the skill panel, attributes</td>  
    <td align="left">1~6 integer or Vigor, Control, Endurance, Cunning, Social, Intelligence</td> 
  </tr>  
  <tr> 
    <td align="left">name</td>  
    <td align="left" colspan="2">Fixed value me(me), all_not_me(except me), wanderer(wanderer), noble(noble) or character English name (in [ESC-option-game Settings] Change the language to English to see the English name of the role. If there is a space in the name, you need to use an underscore (_) to replace the space. If there are multiple roles with the same name, add -2 after the name and specify the second one)</td> 
  </tr> 
</table>

### **Change Log**
- v1.1.1
	- **Default Behavior Modify**
		- The spouse does not die of dystocia during childbirth (only when the player's role is male)
	- New Command
		- npc.set_age
		- npc.clone_fix_empty_wounded
	- Config
		- MySpouseCanDiedInLabor
		- EnableAgeModel
		- BecomeInfantAge
		- BecomeChildAge
		- BecomeTeenagerAge
		- HeroComesOfAge
		- BecomeOldAge
		- MaxAge
	- Improve 
		- Selection of NPC sequence for battlefield control
    - **Fix**
        - When controlling NPC, if there is a flash back caused by the wounded
        - Processing equipment (unlock horse longbow, civilian mode, proficiency, ammo, etc.) Takes effect when a new game is created
- v1.1.0
	- New Command
		- config.handle_crafted_weapon_item_npcmt
	- Improve
		- Processing equipment can be used to forge weapons
- v1.0.9
	- Improve
		- Compatible e1.5.2
- v1.0.8
	- New Command
		- campaign.kill_player
		- npc.remove_focus_by_entire_line
	- Improve
	 	- Compatible e1.5.0
- v1.0.7
	- New Command
		- npc.remove_focus
	- Improve
		- Localization (text translation)
- v1.0.6
    - **Default Behavior Modify**
        - Exclude empire culture when creating wanderer
        - Control other NPCs after death on the battlefield
	- Config
		- CreateWandererExcludeCultures
		- OnlyCreateFemaleOrMaleWanderer
		- BattlefieldCommanderStringIds
		- EnableAfterDeathControl
		- AfterDeathControlExcludePlayer
		- AfterDeathControlOnly__Noble_Or_Wanderer_Or_NobleOrWanderer
- v1.0.5
	- New Command
		- npc.clone
	- Improve
		- Compatible e1.4.3
- v1.0.3
	- New Command
		- npc_control.name
		- npc_control.index
		- npc_control.next
		- npc_control.next_noble
		- npc_control.next_wanderer
		- print.npcs_index
- v1.0.2
	- New Command
		- npc.change_body
		- npc.random_body
	- Config
		- FixGetClipboardText
- v1.0.1
	- New Command
		- rename.children
		- npc.check_is_fertile
		- npc.set_is_fertile_true
		- npc.set_is_fertile_false
	- Improve
		- Correction of occupation data in export CSV
		- Check the legend blacksmith command output results text content
- v1.0.0
	- **Default Behavior Modify**
		- AddAmmo
			- Arrow +11
			- Bolt +6
			- ThrowingAxe +2
			- ThrowingKnife +13
			- Javelin +1
    - Clear Item Difficulty
    - Unlock LongBow For Use On Horse Back
    - Unlock Item Civilian
