using MB2Mod.NPCMasterTrainer.Properties;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection;

namespace MB2Mod.NPCMasterTrainer
{
    partial class Utils
    {
        public static class BattlefieldControl
        {
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
                                        var agentName = agent?.Name.ToString();
                                        var heroName = item?.Name.ToString();
                                        if (agentName == heroName)
                                        {
                                            yield return agent;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return default;
            }

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
                    DisplayMessage(string.Format(Resources.YouAreNowControlling_, agentName));
                    //if (IsDevelopment)
                    //{
                    //    DisplayMessage($"last agent AIStateFlags: {main.AIStateFlags}");
                    //    DisplayMessage($"last agent IsAIControlled: {main.IsAIControlled}");
                    //}
                }
                return true;
            }
        }
    }
}