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
using MapEditorReborn.Events.EventArgs;
using MEC;
using ButtonInteractedEventArgs = ProjectMER.Events.Arguments.ButtonInteractedEventArgs;

namespace Fondation_Delta_DarkRP.PMer
{
    public class reviver_btn
    {
        Dictionary<int, RoleTypeId> deadPlayers = new Dictionary<int, RoleTypeId>();

        public void OnPlayerDied(DyingEventArgs ev)
        {
            if (!deadPlayers.ContainsKey(ev.Player.Id))
                deadPlayers.Add(ev.Player.Id, ev.Player.Role.Type);
        }
        public void OnButtonPressed(ButtonInteractedEventArgs ev)
        {
            if (ev.Button.Base.name != "ReviveButton")
            {
                return;
            }
            
            var exiledPlayer = Exiled.API.Features.Player.Get(ev.Player.ReferenceHub);

            if (exiledPlayer.CurrentItem == null || !CustomItem.TryGet(exiledPlayer.CurrentItem, out var item) || item.Id != 6)
            {
                string Text = "Il vous faut un jeton de réanimation !";
                var dynamicHint = new DynamicHint
                {
                    Id = "rev2",
                    FontSize = 20,
                    LineHeight = 1f,
                    Text = Text,
                    TargetX = 0,
                    TargetY = 750,
                    Priority = HintPriority.Medium,
                };
                PlayerDisplay.Get(ev.Player).AddHint(dynamicHint);
                Timing.CallDelayed(3f, () => PlayerDisplay.Get(ev.Player).RemoveHint("rev2"));
                return;
            }

            if (deadPlayers.Count == 0)
            {
                string Text = "Aucun joueur disponible.";
                var dynamicHint = new DynamicHint
                {
                    Id = "rev3",
                    FontSize = 20,
                    LineHeight = 1f,
                    Text = Text,
                    TargetX = 0,
                    TargetY = 750,
                    Priority = HintPriority.Medium,
                };
                PlayerDisplay.Get(ev.Player).AddHint(dynamicHint);
                Timing.CallDelayed(3f, () => PlayerDisplay.Get(ev.Player).RemoveHint("rev3"));
                return;
            }
            
            var randomEntry = deadPlayers.ElementAt(UnityEngine.Random.Range(0, deadPlayers.Count));
            int playerId = randomEntry.Key;
            RoleTypeId role = randomEntry.Value;

            Player deadPlayer = Player.Get(playerId);
            if (deadPlayer == null) return;

            if (deadPlayer.IsDead)
            {
                deadPlayer.Role.Set(role);
                deadPlayer.Position = new UnityEngine.Vector3(-27.094f, 303.879f, -3.707f);
                deadPlayer.Broadcast(3, $"Vous avez été réanimé par {exiledPlayer.Nickname}");

                deadPlayers.Remove(playerId);

                exiledPlayer.RemoveItem(exiledPlayer.CurrentItem);
                exiledPlayer.Broadcast(3, $"Vous avez réanimé {deadPlayer.Nickname}");
            }
        }
    }
}