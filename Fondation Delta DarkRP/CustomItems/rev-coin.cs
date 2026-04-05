using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.API.Features.Toys;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;

namespace Fondation_Delta_DarkRP
{
    [CustomItem(ItemType.Coin)]
    public class revcoin : CustomItem
    {
        public override uint Id { get; set; } = 6;
        public override string Name { get; set; } = "<color=grey>Jeton de réanimation</color>";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.Coin;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new RoomSpawnPoint
                {
                    Room = RoomType.HczHid,
                    Chance = 100
                },
                new RoomSpawnPoint
                {
                    Room = RoomType.HczNuke,
                    Chance = 100
                },
                new RoomSpawnPoint
                {
                    Room = RoomType.LczToilets,
                    Chance = 100
                }
            }
        };

        public void Flipping(FlippingCoinEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.FlippingCoin += Flipping;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.FlippingCoin -= Flipping;
        }
    }
}