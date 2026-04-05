using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace Fondation_Delta_DarkRP
{
    [CustomItem(ItemType.Adrenaline)]
    public class PhantomInject : CustomItem
    {
        public override uint Id { get; set; } = 3;
        public override string Name { get; set; } = "<color=grey>Injecteur Phantom</color>";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 0.5f;
        public override ItemType Type { get; set; } = ItemType.Adrenaline;

        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new RoomSpawnPoint()
                {
                    Room = Exiled.API.Enums.RoomType.LczArmory,
                    Chance = 100
                },
                new RoomSpawnPoint()
                {
                    Room = Exiled.API.Enums.RoomType.HczArmory,
                    Chance = 100
                },
                new RoomSpawnPoint()
                {
                    Room = Exiled.API.Enums.RoomType.Lcz173,
                    Chance = 100
                }
            }
        };

        public void OnUsing(UsingItemCompletedEventArgs ev)
        {
            if (!Check(ev.Item))
                return;

            ev.Player.EnableEffect(EffectType.Invisible, 5f);
            ev.Player.ChangeEffectIntensity(EffectType.Invisible, 255);

            Timing.CallDelayed(5f, () =>
            {
                ev.Player.EnableEffect(EffectType.Invisible, 0f);
                ev.Player.ChangeEffectIntensity(EffectType.Invisible, 0);
            });
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
