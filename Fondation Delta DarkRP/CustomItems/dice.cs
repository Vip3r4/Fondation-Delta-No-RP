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
    [CustomItem(ItemType.SCP500)]
    public class Dice : CustomItem
    {
        public override uint Id { get; set; } = 5;
        public override string Name { get; set; } = "<color=red>Funny pills</color>";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 1f;
        public override ItemType Type { get; set; } = ItemType.SCP500;

        private readonly HashSet<int> _active = new HashSet<int>();

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new RoomSpawnPoint
                {
                    Room = RoomType.Hcz079,
                    Chance = 100
                },
                new RoomSpawnPoint
                {
                    Room = RoomType.HczHid,
                    Chance = 100
                },
                new RoomSpawnPoint
                {
                    Room = RoomType.LczToilets,
                    Chance = 100
                },
                new RoomSpawnPoint
                {
                    Room = RoomType.EzCafeteria,
                    Chance = 100
                }
            }
        };

        public void OnUsing(UsingItemCompletedEventArgs ev)
        {
            if (!Check(ev.Item))
            {
                return;
            }

            int roll = UnityEngine.Random.Range(1, 101);

            if (roll <= 5)
            {
                if (Warhead.IsInProgress)
                {
                    Warhead.Stop();
                }
                else
                {
                    Warhead.Start();
                }
                
            }
            else if (roll <= 15)
            {
                Map.TurnOffAllLights(10f);
            }
            else if (roll <= 30)
            {
                ev.Player.EnableEffect(EffectType.MovementBoost, 15f);
                ev.Player.ChangeEffectIntensity(EffectType.MovementBoost, 30);
            }
            else if (roll <= 50)
            {
                ev.Player.Hurt(30f);
            }
            else if (roll <= 70)
            {
                ev.Player.Heal(20f);
            }
            else if (roll <= 90)
            {
                var playerid = ev.Player.Id;
                Server.ExecuteCommand($"/rtp {playerid}");
            }
            else
            {
                return;
            }

            ev.Player.RemoveItem(ev.Item);

        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItemCompleted += OnUsing;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItemCompleted -= OnUsing;
        }
    }
}