using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Fondation_Delta_NoRP;
using PlayerRoles;

namespace Fondation_Delta_DarkRP
{
    public class KillCount
    {
        private Dictionary<Player, int> kills = new Dictionary<Player, int>();

        private Dictionary<Player, int> killsSCP = new Dictionary<Player, int>();

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (Plugin.Instance.Config.KillCount == false) { return; }

            if (ev.Attacker != null)
            {
                if (ev.Attacker.IsHuman)
                {
                    if (!kills.ContainsKey(ev.Attacker))
                    {
                        kills[ev.Attacker] = 0;
                    }

                    kills[ev.Attacker]++;
                }

                if (ev.Attacker.IsScp)
                {
                    if (!killsSCP.ContainsKey(ev.Attacker))
                    {
                        killsSCP[ev.Attacker] = 0;
                    }

                    killsSCP[ev.Attacker]++;
                }
            }
        }

        public void ShowMVP(RoundEndedEventArgs ev)
        {
            if (Plugin.Instance.Config.KillCount == false) { return; }

            string message = "";

            if (kills.Count != 0)
            {
                var mj = kills.OrderByDescending(pair => pair.Value).First();
                message += $"👮 Le meilleur joueur de la partie est : <color=red>{mj.Key.Nickname}</color> avec : <color=red>{mj.Value}</color> kills";
            }
            else
            {
                message += "Aucun humain n'a fait de kills cette partie.";
            }

            message += "\n";

            if (killsSCP.Count != 0)
            {
                var mjscp = killsSCP.OrderByDescending(pair => pair.Value).First();
                message += $"💀 Le meilleur SCP de la partie est : <color=red>{mjscp.Key.Nickname}</color> avec : <color=red>{mjscp.Value}</color> kills";
            }
            else
            {
                message += "Aucun SCP n'a fait de kills cette partie.";
            }

            Map.Broadcast(10, message);

            kills.Clear();
            killsSCP.Clear();
        }
    }
}
