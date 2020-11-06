using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        partial class Config
        {
            /// <summary>
            /// 战场指挥官 Hero.StringId
            /// </summary>
            public string[] BattlefieldCommanderStringIds { get; set; }

            /// <summary>
            /// 开启死后控制NPC，默认值<see langword="true"/>(开)
            /// </summary>
            public bool EnableAfterDeathControl { get; set; } = true;

            /// <summary>
            /// 死后控制NPC类型过滤
            /// <para><see langword="true" /> 死后仅控制贵族(Noble)</para>
            /// <para><see langword="false" /> 死后仅控制流浪者(Wanderer)</para>
            /// <para><see langword="null" /> 死后仅控制贵族(Noble)或(Or)流浪者(Wanderer)</para>
            /// </summary>
            public bool? AfterDeathControlOnly__Noble_Or_Wanderer_Or_NobleOrWanderer { get; set; }

            /// <summary>
            /// 死后控制NPC选择中排除玩家
            /// </summary>
            public bool AfterDeathControlExcludePlayer { get; set; }
        }

        /// <summary>
        /// 战场控制
        /// </summary>
        public static class BattlefieldControl
        {
#if DEBUG
            static void _(CharacterObject character, Agent agent, int mark)
            {
                var args = new
                {
                    character = character.ToString(),
                    character_a = agent.Character.ToString(),
                    h_character = character.GetHashCode(),
                    h_character_a = agent.Character.GetHashCode(),
                    cName = character.Name.ToString(),
                    aName = agent.Name,
                    mark,
                };
                Console.WriteLine(args.ToJsonString());
            }
#endif

            [Obsolete("use GetAgentsV2", true)]
            public static IEnumerable<Agent> GetAgents(IEnumerable<CharacterObject> characters)
            {
                if (Game.Current != null && Game.Current.CurrentState <= Game.State.Running && Mission.Current != null && Mission.Current.Scene != null)
                {
                    var agents = Mission.Current.Agents;
                    if (characters != default && agents != default)
                    {
                        return _GetAgents();
                        IEnumerable<Agent> _GetAgents()
                        {
                            foreach (var item in characters)
                            {
                                if (item.IsHero)
                                {
                                    foreach (var agent in agents)
                                    {
                                        if (agent.Character == item)
                                        {
#if DEBUG
                                            _(item, agent, 0);
#endif
                                            yield return agent;
                                        }
                                        else
                                        {
                                            var agentName = agent?.Name.ToString();
                                            var heroName = item?.Name.ToString();
                                            if (agentName == heroName)
                                            {
#if DEBUG
                                                _(item, agent, 1);
#endif
                                                yield return agent;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return default;
            }

            public static IEnumerable<Agent> GetAgentsV2(
                IEnumerable<CharacterObject> characters,
                Mission mission = null)
            {
                mission ??= Mission.Current;
                if (characters != default && mission != null)
                {
                    var agents = mission.Agents;
                    if (agents != default)
                    {
                        var query = from x in characters
                                    where x.IsHero
                                    let agent = agents
                                    .Where(x => x.Health > 20f)
                                    .OrderByDescending(x => x.Health)
                                    .FirstOrDefault(y => x == y.Character)
                                    where agent != null
                                    select agent;
                        return query;
                    }
                }
#if DEBUG
                Console.WriteLine("GetAgentsV2 args is null.");
#endif
                return default;
            }

            public static Agent GetAgentV2(
                IEnumerable<CharacterObject> characters,
                Mission mission = null)
            {
                var agents = GetAgentsV2(characters, mission);
                if (agents != null) return agents.FirstOrDefault();
                return default;
            }

            [Obsolete("use ControlV2", true)]
            public static bool Control(Agent agent)
            {
                if (agent == default) return false;
                var main = Agent.Main;
                if (main == default) return false;
                if (agent == main) return true;
                try
                {
                    agent.SetMaximumSpeedLimit(-1f, false);
                }
                catch (Exception e)
                {
                    DisplayMessage(e);
                }
                main.Controller = Agent.ControllerType.AI;
                if (main.AIStateFlags == Agent.AIStateFlag.None) main.AIStateFlags = Agent.AIStateFlag.Alarmed; // Player AI Controller Fix
                agent.Controller = Agent.ControllerType.Player;
                try
                {
                    var battleObserver = Mission.Current.GetMissionBehaviour<BattleObserverMissionLogic>()?.BattleObserver;
                    if (battleObserver != default && battleObserver is ScoreboardVM scoreboard && scoreboard.IsMainCharacterDead)
                    {
                        scoreboard.IsMainCharacterDead = false;
                    }
                }
                catch (Exception e)
                {
                    DisplayMessage(e);
                }
                if (Mission.Current.MainAgent != agent)
                    Mission.Current.MainAgent = agent;
                var agentName = agent.Name?.ToString();
                if (!string.IsNullOrEmpty(agentName))
                {
                    DisplayMessage(Format(Resources.YouAreNowControlling_, agentName));
                    //if (IsDevelopment)
                    //{
                    //    DisplayMessage($"last agent AIStateFlags: {main.AIStateFlags}");
                    //    DisplayMessage($"last agent IsAIControlled: {main.IsAIControlled}");
                    //}
                }
                return true;
            }

            public static bool ControlV2(Agent agent)
            {
                var logic = SetBattlefieldCommanderMissionLogic.Current;
                if (logic != null)
                {
                    return logic.Control(agent);
                }
#if DEBUG
                Console.WriteLine("ControlV2 logic is null.");
#endif
                return false;
            }

#if DEBUG
            public sealed class MissionListener : MissionLogic, IMissionListener
            {
                public /*new*/ Action OnEndMission1 { private get; set; }

                public Action OnResetMission { private get; set; }

                readonly Mission mission;

                public MissionListener(Mission mission)
                {
                    this.mission = mission;
                    mission.OnMainAgentChanged += OnMainAgentChanged;
                }

                private void OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
                {
                    _("OnMainAgentChanged");
                    Console.WriteLine(Environment.StackTrace);
                }

                void _(string mark)
                {
                    var args = new
                    {
                        MainAgent = mission.MainAgent?.Name,
                        PlayerTeam = mission.PlayerTeam?.ToString(),
                        PlayerAllyTeam = mission.PlayerAllyTeam?.ToString(),
                        PlayerTeam2 = mission.PlayerTeam?.PlayerOrderController?.Owner?.Name,
                        PlayerAllyTeam2 = mission.PlayerAllyTeam?.PlayerOrderController?.Owner?.Name,
                        PlayerTeam3 = mission.PlayerTeam?.FormationsIncludingEmpty?.Select(x => x?.PlayerOwner?.Name).Distinct().ToArray(),
                        PlayerAllyTeam3 = mission.PlayerAllyTeam?.FormationsIncludingEmpty?.Select(x => x?.PlayerOwner?.Name).Distinct().ToArray(),
                    };
                    Console.WriteLine($"{mark} {Environment.NewLine + args.ToJsonString()}");
                }

                public override void OnMissionTick(float dt)
                {
                    base.OnMissionTick(dt);
                    _($"OnMissionTick({dt})");
                }

                public override void AfterStart()
                {
                    base.AfterStart();
                    _("AfterStart");
                }

                public override void EarlyStart()
                {
                    base.EarlyStart();
                    _("EarlyStart");
                }

                public override void OnRenderingStarted()
                {
                    base.OnRenderingStarted();
                    _("OnRenderingStarted");
                }

                protected override void OnEndMission()
                {
                    base.OnEndMission();
                    _("OnEndMission");
                }

                public override void OnBattleEnded()
                {
                    base.OnBattleEnded();
                    _("OnBattleEnded");
                }

                public override void OnAfterMissionCreated()
                {
                    base.OnAfterMissionCreated();
                    _("OnAfterMissionCreated");
                }

                public override void OnMissionActivate()
                {
                    base.OnMissionActivate();
                    _("OnMissionActivate");
                }

                public override bool MissionEnded(ref MissionResult missionResult)
                {
                    _("MissionEnded");
                    return base.MissionEnded(ref missionResult);
                }

                public override void OnCreated()
                {
                    base.OnCreated();
                    _("OnCreated");
                }

                public override void OnBehaviourInitialize()
                {
                    base.OnBehaviourInitialize();
                    _("OnBehaviourInitialize");
                }

                public override void OnMissionDeactivate()
                {
                    base.OnMissionDeactivate();
                    _("OnMissionDeactivate");
                }

                public override void HandleOnCloseMission()
                {
                    base.HandleOnCloseMission();
                    _("OnMissionDeactivate");
                }

                void IMissionListener.OnConversationCharacterChanged()
                {
                }

                void IMissionListener.OnEndMission() => OnEndMission1?.Invoke();

                void IMissionListener.OnEquipItemsFromSpawnEquipment(Agent agent, Agent.CreationType creationType)
                {
                }

                void IMissionListener.OnEquipItemsFromSpawnEquipmentBegin(Agent agent, Agent.CreationType creationType)
                {
                }

                void IMissionListener.OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
                {
                }

                void IMissionListener.OnResetMission() => OnResetMission?.Invoke();
            }
#endif

            public sealed class SetBattlefieldCommanderMissionLogic : MissionLogic
            {
                Agent player, sourceCommander;

                public bool IsStartBattle { get; private set; }

                public bool IsDeactivate { get; private set; }

                public static SetBattlefieldCommanderMissionLogic Current => Mission.Current.GetMissionLogic<SetBattlefieldCommanderMissionLogic>();

                public bool Control(Agent destCommander, bool equalsResult = true)
                {
                    if (destCommander == null)
                    {
#if DEBUG
                        Console.WriteLine($"Control Fail, destCommander is null.");
#endif
                        return false;
                    }
                    if (destCommander.Health < 20f)
                    {
#if DEBUG
                        Console.WriteLine($"Control Fail, destCommander Health < 20.");
#endif
                        return false;
                    }
                    if (!IsStartBattle)
                    {
#if DEBUG
                        Console.WriteLine($"Control Fail, IsStartBattle is false.");
#endif
                        return false;
                    }
                    if (IsDeactivate)
                    {
#if DEBUG
                        Console.WriteLine($"Control Fail, IsDeactivate is true.");
#endif
                        return false;
                    }
                    if (Mission == null || destCommander == null || sourceCommander == null)
                    {
#if DEBUG
                        Console.WriteLine($"Control Fail, args is null, " +
                            $"Mission:{Mission} " +
                            $"destCommander:{destCommander?.Name} " +
                            $"sourceCommander:{sourceCommander?.Name}");
#endif
                        return false;
                    }
                    if (sourceCommander == destCommander)
                    {
#if DEBUG
                        Console.WriteLine("Control commander == agent.");
#endif
                        return equalsResult;
                    }
                    var agentName = destCommander.Name?.ToString();
#if DEBUG
                    Console.WriteLine($"Control agentName:{agentName}");
#endif
                    try
                    {
                        destCommander.SetMaximumSpeedLimit(-1f, false);
                        sourceCommander.Controller = Agent.ControllerType.AI;
                        if (sourceCommander.AIStateFlags == Agent.AIStateFlag.None)
                        {
                            // Player AI Controller Fix
                            sourceCommander.AIStateFlags = Agent.AIStateFlag.Alarmed;
                        }
                        TransferFormationsControl(Mission.PlayerTeam, sourceCommander, destCommander);
                        TransferFormationsControl(Mission.PlayerAllyTeam, sourceCommander, destCommander);
                        destCommander.Controller = Agent.ControllerType.Player;
                        var battleObserver = Mission.GetMissionBehaviour<BattleObserverMissionLogic>()?.BattleObserver;
                        if (battleObserver != null && battleObserver is ScoreboardVM scoreboard && scoreboard.IsMainCharacterDead)
                        {
                            scoreboard.IsMainCharacterDead = false;
                        }
                        if (Mission.MainAgent != destCommander)
                        {
                            Mission.MainAgent = destCommander;
                        }
                        if (Config.Instance.EnableAfterDeathControl)
                        {
                            sourceCommander.OnAgentHealthChanged -= OnAgentHealthChanged;
                            destCommander.OnAgentHealthChanged += OnAgentHealthChanged;
                        }
                        sourceCommander = destCommander;
                        if (!string.IsNullOrEmpty(agentName))
                        {
                            DisplayMessage(Format(Resources.YouAreNowControlling_, agentName));
                        }
                        return true;
                    }
                    catch (Exception e)
                    {
                        DisplayMessage(e);
                    }
                    return false;
                }

                void OnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
                {
                    if (newHealth <= 0f)
                    {
                        agent.OnAgentHealthChanged -= OnAgentHealthChanged;
                        NpcType npcType;
                        var config = Config.Instance;
                        var npcTypeArg = config.AfterDeathControlOnly__Noble_Or_Wanderer_Or_NobleOrWanderer;
                        if (npcTypeArg.HasValue)
                        {
                            if (npcTypeArg.Value)
                            {
                                npcType = NpcType.Noble;
                            }
                            else
                            {
                                npcType = NpcType.Wanderer;
                            }
                        }
                        else
                        {
                            npcType = NpcType.Noble | NpcType.Wanderer;
                        }
                        if (!config.AfterDeathControlExcludePlayer)
                        {
                            npcType |= NpcType.Player;
                        }
                        ConsoleCommand.ControlHeroNext(npcType);
                    }
                }

                void DisposeControl()
                {
                    if (sourceCommander != null && player != null && sourceCommander != player)
                    {
                        try
                        {
                            if (Config.Instance.EnableAfterDeathControl)
                            {
                                sourceCommander.OnAgentHealthChanged -= OnAgentHealthChanged;
                            }
                            TransferFormationsControl(Mission.PlayerTeam, sourceCommander, player);
                            TransferFormationsControl(Mission.PlayerAllyTeam, sourceCommander, player);
                        }
                        catch (Exception e)
                        {
                            DisplayMessage(e);
                        }
                    }
                }

                static void TransferFormationsControl(Team team, Agent sourceCommander, Agent destCommander)
                {
                    if (team != null && sourceCommander != null && destCommander != null)
                    {
                        if (team.PlayerOrderController.Owner == sourceCommander)
                        {
                            team.PlayerOrderController.Owner = destCommander;
                        }
                        foreach (var formation in team.FormationsIncludingEmpty)
                        {
                            if (formation.PlayerOwner == sourceCommander)
                            {
                                var isAIControlled = formation.IsAIControlled;
                                formation.PlayerOwner = destCommander;
                                formation.IsAIControlled = isAIControlled;
                            }
                        }
                    }
                }

                bool ControlBattleCommander(params string[] stringIds)
                {
                    var heroes = GetNpcs2(new[] { NpcType.Wanderer, NpcType.Noble, NpcType.Player });
                    var hero = heroes.FirstOrDefault(x => stringIds.Contains(x.StringId, StringComparer.Ordinal));
                    if (hero != null)
                    {
                        var destCommander = GetAgentV2(new[] { hero.CharacterObject }, Mission);
                        return Control(destCommander, false);
                    }
#if DEBUG
                    Console.WriteLine($"ControlBattleCommander, args" +
                        $"hero:{hero?.Name.ToString()} " +
                        $"stringId:{hero?.StringId}");
#endif
                    return false;
                }

                void OnStartBattle()
                {
                    var resultControl = false;
                    var battleCommander = ConsoleCommand.BattleCommander;
                    if (battleCommander != null)
                    {
                        resultControl = ControlBattleCommander(battleCommander);
                    }
                    else
                    {
                        var ids = Config.Instance.BattlefieldCommanderStringIds;
                        if (ids != null && ids.Any())
                        {
                            resultControl = ControlBattleCommander(ids);
                        }
                    }
                    if (!resultControl)
                    {
                        player.OnAgentHealthChanged += OnAgentHealthChanged;
                    }
                }

                public override void OnMissionTick(float dt)
                {
                    base.OnMissionTick(dt);
                    try
                    {
                        if (!IsStartBattle && Mission.IsStartBattle())
                        {
                            player = Mission.MainAgent;
                            sourceCommander = player;
                            IsStartBattle = true;
                            OnStartBattle();
                        }
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        Console.WriteLine("OnMissionTick catch");
                        Console.WriteLine(e.ToString());
#endif
                    }
                }

                public override void OnMissionDeactivate()
                {
                    IsDeactivate = true;
                    base.OnMissionDeactivate();
                    try
                    {
                        DisposeControl();
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        Console.WriteLine("OnMissionDeactivate catch");
                        Console.WriteLine(e.ToString());
#endif
                    }
                }
            }
        }

        static bool HasValue(this IEnumerable<Formation> formations) => formations != null && formations.Any() && formations.All(x => x != null && x.PlayerOwner != null);

        public static bool IsStartBattle(this Mission mission) => mission?.MainAgent != null && mission.PlayerTeam?.PlayerOrderController?.Owner != null && (mission.PlayerTeam?.FormationsIncludingEmpty.HasValue() ?? false);

        public static bool IsBattle(this Mission mission)
           => mission != null && (mission.MissionBehaviours?.Any(x => x is BattleObserverMissionLogic) ?? false);

        static Tuple<bool, T> Is<T>(object o) =>
            o is T t ? new Tuple<bool, T>(true, t) : new Tuple<bool, T>(false, default);

        public static T GetMissionLogic<T>(this Mission mission) where T : MissionLogic
        {
            var logics = mission?.MissionLogics;
            if (logics != null)
            {
                var q = from x in logics
                        let t = Is<T>(x)
                        where t.Item1
                        select t.Item2;
                return q.FirstOrDefault();
            }
            return null;
        }

        public static void AddSetBattlefieldCommander(this Mission mission)
        {
            if (mission.IsBattle())
            {
                var logic = new BattlefieldControl.SetBattlefieldCommanderMissionLogic();
                mission.AddMissionBehaviour(logic);
            }
        }

        //public static bool IsFieldBattle(this Mission mission)
        //    => mission.MissionBehaviours.Any(x => x is FieldBattleController);
    }
}