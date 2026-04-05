using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Server;
using InventorySystem.Items.Armor;
using MEC;
using PlayerRoles;
using Respawning.Waves;
using System.Collections.Generic;
using System.Linq;

namespace Fondation_Delta_DarkRP
{
    [CustomRole(RoleTypeId.ChaosMarauder)]
    public class BomberChaos : CustomRole
    {
        public override uint Id { get; set; } = 1;
        public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosMarauder;
        public override int MaxHealth { get; set; } = 120;
        public override string Name { get; set; } = "<color=green>Chaos Bombardier</color>";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "Chaos Bombardier";

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            $"{ItemType.KeycardChaosInsurgency}",
            $"{ItemType.GunShotgun}",
            $"{ItemType.Medkit}",
            $"{ItemType.Painkillers}",
            $"{ItemType.GrenadeHE}",
            $"{ItemType.GrenadeHE}",
            $"{ItemType.ArmorCombat}"
        };

        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Ammo12Gauge, 50 },
        };

        public void OnRespawn(RespawningTeamEventArgs ev)
        {
            if (!(ev.NextKnownTeam is Faction.FoundationEnemy))
            {
                return;
            }

            var player = ev.Players.FirstOrDefault();

            if (player == null)
            {
                return;
            }

            var players = ev.Players.ToList();

            if (players.Count <= 5)
            {
                return;
            }

            var bomberchaos = CustomRole.Get(1);
            Timing.CallDelayed(0.5f, () =>
            {
                bomberchaos.AddRole(player);
            });
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RespawningTeam += OnRespawn;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RespawningTeam -= OnRespawn;
        }
    }
}
