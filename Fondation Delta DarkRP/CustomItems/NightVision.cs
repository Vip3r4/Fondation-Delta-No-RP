using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;

namespace Fondation_Delta_DarkRP
{
    [CustomItem(ItemType.SCP1344)]
    public class NightVision : CustomItem
    {
        public override uint Id { get; set; } = 2;
        public override string Name { get; set; } = "<color=green>Lunettes de vision nocturne</color>";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.SCP1344;

        private readonly HashSet<int> _active = new HashSet<int>();

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new RoomSpawnPoint
                {
                    Room = RoomType.HczArmory,
                    Chance = 100
                },
                new RoomSpawnPoint
                {
                    Room = RoomType.LczGlassBox,
                    Chance = 100
                }
            }
        };

        public void OnUsing(UsingItemEventArgs ev)
        {
            if (!Check(ev.Item))
                return;

            ev.IsAllowed = false;

            int id = ev.Player.Id;
            _active.Add(id);

            ev.Player.EnableEffect(EffectType.NightVision, 999999f);
            ev.Player.ChangeEffectIntensity(EffectType.NightVision, 50);
        }

        public void OnDrop(DroppedItemEventArgs ev)
        {
            if (!Check(ev.Pickup))
                return;

            if (!_active.Contains(ev.Player.Id))
                return;

            RemoveEffects(ev.Player);
        }

        private void RemoveEffects(Player player)
        {
            player.DisableEffect(EffectType.NightVision);
            _active.Remove(player.Id);
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsing;
            Exiled.Events.Handlers.Player.DroppedItem += OnDrop;
            Exiled.Events.Handlers.Player.Died += ev => RemoveEffects(ev.Player);
            Exiled.Events.Handlers.Player.ChangingRole += ev => RemoveEffects(ev.Player);
            Exiled.Events.Handlers.Player.Left += ev => _active.Remove(ev.Player.Id);
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsing;
            Exiled.Events.Handlers.Player.DroppedItem -= OnDrop;
            Exiled.Events.Handlers.Player.Died -= ev => RemoveEffects(ev.Player);
            Exiled.Events.Handlers.Player.ChangingRole -= ev => RemoveEffects(ev.Player);
            Exiled.Events.Handlers.Player.Left -= ev => _active.Remove(ev.Player.Id);
        }
    }
}