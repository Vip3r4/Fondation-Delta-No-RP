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
    [CustomRole(RoleTypeId.NtfSpecialist)]
    public class HealerNtf : CustomRole
    {
        public override uint Id { get; set; } = 2;
        public override RoleTypeId Role { get; set; } = RoleTypeId.NtfSpecialist;
        public override int MaxHealth { get; set; } = 120;
        public override string Name { get; set; } = "<color=blue>Ntf Healeur</color>";
        public override string Description { get; set; } = "";
        public override string CustomInfo { get; set; } = "Ntf Healeur";

        public override List<string> Inventory { get; set; } = new List<string>()
        {
            $"{ItemType.KeycardMTFOperative}",
            $"{ItemType.GunE11SR}",
            $"{ItemType.Medkit}",
            $"{ItemType.Medkit}",
            $"{ItemType.Medkit}",
            $"{ItemType.Adrenaline}",
            $"{ItemType.Radio}",
            $"{ItemType.ArmorCombat}"
        };

        public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato556, 120 },
        };

        public void OnRespawn(RespawningTeamEventArgs ev)
        {
            if (!(ev.NextKnownTeam is Faction.FoundationStaff))
            {
                return;
            }

            var player = ev.Players.FirstOrDefault();

            if (player == null)
            {
                return;
            }

            var players = ev.Players.ToList();

            if (players.Count <= 4)
            {
                return;
            }

            var healerntf = CustomRole.Get(2);
            Timing.CallDelayed(0.5f, () =>
            {
                healerntf.AddRole(player);
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
