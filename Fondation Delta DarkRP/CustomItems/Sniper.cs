using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerRoles;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using UnityEngine;
using MEC;

namespace Fondation_Delta_DarkRP
{
    [CustomItem(ItemType.GunE11SR)]
    public class Sniper : CustomWeapon
    {
        public override uint Id { get; set; } = 1;
        public override string Name { get; set; } = "<color=grey>Sniper</color>";
        public override string Description { get; set; } = "";
        public override float Weight { get; set; } = 11.3f;
        public override byte ClipSize { get; set; } = 1;
        public override ItemType Type { get; set; } = ItemType.GunE11SR;
        protected override void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null)
            {
                return;
            }

            if (CustomItem.TryGet(ev.Attacker.CurrentItem, out var item) && item.Id == 1)
            {
                if (ev.Player.Role.Type == RoleTypeId.Scp0492)
                {
                    ev.Amount = 400f;
                }
                if (ev.Player.IsScp)
                {
                    ev.Amount = 100f;
                }
                if (ev.Player.IsHuman)
                {
                    ev.Amount = 400f;
                }
            }
        }
        public override AttachmentName[] Attachments { get; set; } = new[]
        {
            AttachmentName.MuzzleBrake,
            AttachmentName.RifleBody,
            AttachmentName.Laser,
            AttachmentName.ScopeSight,
            AttachmentName.RecoilReducingStock
        };
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            RoomSpawnPoints = new List<RoomSpawnPoint>
            {
                new RoomSpawnPoint()
                {
                    Room = Exiled.API.Enums.RoomType.LczArmory,
                    Chance = 100
                }
            }
        };
        public void CantModify(ChangingAttachmentsEventArgs ev)
        {
            if (Check(ev.Item))
            {
                if (CustomItem.TryGet(ev.Player.CurrentItem, out var item))
                {
                    ev.IsAllowed = false;
                    string Text = "Vous ne pouvez pas modifier cette arme";
                    var dynamicHint = new DynamicHint
                    {
                        Id = "snip",
                        FontSize = 20,
                        LineHeight = 1f,
                        Text = Text,
                        TargetX = 0,
                        TargetY = 750,
                        Priority = HintPriority.Medium,
                    };
                    PlayerDisplay.Get(ev.Player).AddHint(dynamicHint);
                    Timing.CallDelayed(3f, () =>
                    {
                        PlayerDisplay.Get(ev.Player).RemoveHint("snip");
                    });
                }
            }
        }
        protected override void OnReloaded(ReloadedWeaponEventArgs ev)
        {
            if (CustomItem.TryGet(ev.Player.CurrentItem, out var item))
            {
                ev.Firearm.MagazineAmmo = 0;
            }
        }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Item.ChangingAttachments += CantModify;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.ReloadedWeapon += OnReloaded;
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Item.ChangingAttachments -= CantModify;
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
            Exiled.Events.Handlers.Player.ReloadedWeapon -= OnReloaded;
        }
    }
}
