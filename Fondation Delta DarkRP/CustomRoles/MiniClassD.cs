using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using InventorySystem.Items.Armor;
using MEC;
using PlayerRoles;
using Respawning.Waves;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fondation_Delta_DarkRP
{
    [CustomRole(RoleTypeId.ClassD)]
    public class MiniClassD : CustomRole
    {
        public override uint Id { get; set; } = 3;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "<color=orange>Mini Class-D</color>";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "Mini Class-D";

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            $"{ItemType.Coin}"
        };

        public void OnRoundStart()
        {
            var classDs = Player.List.Where(p => p.Role.Type == RoleTypeId.ClassD).ToList();

            if (classDs.Count == 0)
                return;

            int max = Mathf.Max(1, classDs.Count / 4);
            int amount = UnityEngine.Random.Range(1, max + 1);

            var selected = classDs.OrderBy(_ => UnityEngine.Random.value)
                                  .Take(amount)
                                  .ToList();

            Timing.CallDelayed(0.5f, () =>
            {
                foreach (var p in selected)
                {
                    if (p.IsAlive)
                        AddRole(p);
                }
            });
        }

        protected override void RoleAdded(Player player)
        {
            base.RoleAdded(player);

            player.Scale = new Vector3(0.7f, 0.7f, 0.7f);

            player.EnableEffect(EffectType.MovementBoost);
            player.ChangeEffectIntensity(EffectType.MovementBoost, 20);
        }

        protected void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (Check(ev.Player))
            {
                ev.Player.Scale = new Vector3(1f, 1f, 1f);
                ev.Player.DisableEffect(EffectType.MovementBoost);
            }
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
        }
    }
}
