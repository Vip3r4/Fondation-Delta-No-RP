using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerRoles;
using UnityEngine;

namespace Fondation_Delta_DarkRP
{
    [CustomItem(ItemType.GunCOM18)]
    public class HealGun : CustomWeapon
    {
        public override uint Id { get; set; } = 4;
        public override string Name { get; set; } = "<color=green>Heal Gun</color>";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 0.72f;
        public override ItemType Type { get; set; } = ItemType.GunCOM18;
        protected override void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null)
            {
                return;
            }

            if (CustomItem.TryGet(ev.Attacker.CurrentItem, out var item) && item.Id == 4)
            {
                if (ev.Player.Role.Type == RoleTypeId.Scp0492)
                {
                    ev.Amount = 21.2f;
                }
                if (ev.Player.IsScp)
                {
                    ev.Amount = 21.2f;
                }
                if (ev.Player.IsHuman)
                {
                    ev.Amount = 0f;
                    ev.Player.Heal(10f);
                }
            }
        }

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new RoomSpawnPoint()
                {
                    Room = Exiled.API.Enums.RoomType.LczPlants,
                    Chance = 100
                }
            }
        };

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
        }
    }
}
