using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Roles;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Fondation_Delta_NoRP;
using PlayerRoles;
using PlayerStatsSystem;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using MEC;

namespace Fondation_Delta_DarkRP
{
    public class EventHandlers
    {
        public void NoTeslaForMTF(TriggeringTeslaEventArgs ev)
        {
            if (Plugin.Instance.Config.NoTeslaForMTF == false) { return; }

            if (ev.Player.Role.Side == Side.Mtf)
            {
                ev.IsTriggerable = false;
                ev.DisableTesla = true;
            }

            if (ev.Player.Role.Type == RoleTypeId.Tutorial)
            {
                ev.DisableTesla = true;
                ev.IsTriggerable = false;
            }
        }

        public void BecomeZombie(DiedEventArgs ev)
        {
            if (Plugin.Instance.Config.BecomeZombie == false) { return; }

            if (ev.Attacker == null) { return; }

            if (ev.Attacker.Role.Type is RoleTypeId.Scp0492)
            {
                if (UnityEngine.Random.Range(1, 100) <= Plugin.Instance.Config.BecomeZombiePercentage)
                {
                    ev.Player.Role.Set(RoleTypeId.Scp0492);
                }
            }
        }

        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            if (Plugin.Instance.Config.OnRoundEnd == false) { return; }

            Server.FriendlyFire = true;
        }

        public void OnWaitingForPlayers()
        {
            if (Plugin.Instance.Config.OnWaitingForPlayers == false) { return; }

            Server.FriendlyFire = false;
        }

        public void OnPickingUpCustomItems(PickingUpItemEventArgs ev)
        {
            if (!CustomItem.TryGet(ev.Pickup, out var item))
                return;

            string text = null;

            switch (item.Id)
            {
                case 1:
                    text = "Vous avez récupéré un <color=grey>sniper</color> !";
                    break;

                case 2:
                    text = "Vous avez récupéré des <color=green>lunettes de vision nocturne</color> !";
                    break;

                case 3:
                    text = "Vous avez récupéré une <color=grey>injection phantom</color> !";
                    break;
                
                case 4:
                    text = "Vous avez récupéré un <color=green>pistolet soignant</color> !";
                    break;
                
                case 5:
                    text = "Vous avez récupéré une <color=red>funny pills</color> !";
                    break;
                case 6:
                    text = "Vous avez récupéré un <color=grey>jeton de réanimation</color> !";
                    break;
            }

            if (text == null)
                return;

            string id = "Ci" + item.Id;

            var hint = new DynamicHint
            {
                Id = id,
                FontSize = 20,
                LineHeight = 1f,
                Text = text,
                TargetX = 0,
                TargetY = 750,
                Priority = HintPriority.Medium,
            };

            var display = PlayerDisplay.Get(ev.Player);
            display.AddHint(hint);

            Timing.CallDelayed(3f, () => display.RemoveHint(id));
        }

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (ev.Attacker != null)
                return;
            
            string Text = $"Vous avez tuer {ev.Player.Nickname}";
            var dynamicHint = new DynamicHint
            {
                Id = "kill",
                FontSize = 20,
                LineHeight = 1f,
                Text = Text,
                TargetX = 0,
                TargetY = 750,
                Priority = HintPriority.Medium,
            };
            PlayerDisplay.Get(ev.Attacker).AddHint(dynamicHint);
            Timing.CallDelayed(3f, () =>
            {
                PlayerDisplay.Get(ev.Attacker).RemoveHint("kill");
            });
        }
        
    }
}
