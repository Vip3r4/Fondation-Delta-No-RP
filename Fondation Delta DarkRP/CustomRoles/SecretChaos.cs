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
    [CustomRole(RoleTypeId.ChaosRifleman)]
    public class SecretChaos : CustomRole
    {
        public override uint Id { get; set; } = 5;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosRifleman;
        public override int MaxHealth { get; set; } = 110;
        public override string Name { get; set; } = "<color=green>Chaos Infiltré</color>";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "Chaos Infiltré";

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            $"{ItemType.KeycardChaosInsurgency}",
            $"{ItemType.GunAK}",
            $"{ItemType.Medkit}",
            $"{ItemType.Medkit}",
            $"{ItemType.Painkillers}",
            $"{ItemType.ArmorCombat}"
        };
        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato762, 90 },
        };

        public void OnRoundStart()
        {
            var guards = Player.List.Where(p => p.Role.Type == RoleTypeId.FacilityGuard).ToList();
            var players = Player.List.Where(o => o.IsAlive).ToList();

            if (players.Count <= 10)
                return;

            if (guards.Count == 0)
                return;

            var player = guards.FirstOrDefault();

            var secretchaos = CustomRole.Get(5);
            Timing.CallDelayed(0.2f, () =>
            {
                secretchaos.AddRole(player);
                player.Position = new UnityEngine.Vector3(0.146f, 300.958f, -51.350f);
            });
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;
        }
    }
}
