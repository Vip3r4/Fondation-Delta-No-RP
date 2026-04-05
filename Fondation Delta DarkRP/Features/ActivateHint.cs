using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp914;
using Fondation_Delta_NoRP;
using Interactables.Interobjects;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using MEC;

namespace Fondation_Delta_DarkRP
{
    public class ActivateHint
    {
        public void On914Activating(ActivatingEventArgs ev)
        {
            if (!ev.IsAllowed) return;

            foreach (Player player in Player.List)
            {
                if (player is null || player.IsNPC || player.IsHost || player.UserId is null)
                    continue;
                if (Vector3.Distance(player.Position, Exiled.API.Features.Scp914.Transform.position) < 10.0f)
                {
                    string Text = $"<size=18>Le joueur <color=red>{ev.Player.DisplayNickname}</color> a lancé 914 en <color=red>{Exiled.API.Features.Scp914.KnobStatus.ToString()}</color></size>";
                    var dynamicHint = new DynamicHint
                    {
                        Id = "914",
                        FontSize = 20,
                        LineHeight = 1f,
                        Text = Text,
                        TargetX = 0,
                        TargetY = 750,
                        Priority = HintPriority.Medium,
                    };
                    PlayerDisplay.Get(player).AddHint(dynamicHint);
                    Timing.CallDelayed(5f, () =>
                    {
                        PlayerDisplay.Get(player).RemoveHint("914");
                    });
                }
            }
        }

        public void OnPlayerInteractElevator(InteractingElevatorEventArgs ev)
        {
            if (!ev.IsAllowed || (ev.Lift.Status != ElevatorChamber.ElevatorSequence.Ready &&
                                  ev.Elevator.CurSequence != ElevatorChamber.ElevatorSequence.Ready)) return;

            var elevatorPosition = ev.Lift.Position;
            foreach (Player player in Player.List)
            {
                if (player is null || player.IsNPC || player.IsHost || player.UserId is null)
                    continue;
                if (Vector3.Distance(player.Position, elevatorPosition) < 10.0f)
                {
                    string Text = $"Le joueur <color=red>{ev.Player.DisplayNickname}</color> a appuyé sur le bouton";
                    var dynamicHint = new DynamicHint
                    {
                        Id = "elev",
                        FontSize = 20,
                        LineHeight = 1f,
                        Text = Text,
                        TargetX = 0,
                        TargetY = 750,
                        Priority = HintPriority.Medium,
                    };
                    PlayerDisplay.Get(player).RemoveHint("elev");
                    PlayerDisplay.Get(player).AddHint(dynamicHint);
                    Timing.CallDelayed(5f, () =>
                    {
                        PlayerDisplay.Get(player).RemoveHint("elev");
                    });
                }
            }
        }
    }
}
