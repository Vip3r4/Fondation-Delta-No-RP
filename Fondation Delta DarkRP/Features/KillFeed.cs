using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Fondation_Delta_NoRP;
using MEC;
using PlayerRoles;
using PlayerStatsSystem;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;

namespace Fondation_Delta_DarkRP
{
    public class KillFeed
    {
        string GetHitPart(Exiled.API.Features.DamageHandlers.DamageHandler handler)
        {
            var firearm = handler.Base as PlayerStatsSystem.FirearmDamageHandler;

            if (firearm == null)
                return "dans le corps";

            if (firearm.Hitbox == HitboxType.Headshot)
                return "dans la tête";

            if (firearm.Hitbox == HitboxType.Limb)
                return "dans les jambes";

            return "dans le corps";
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Attacker == null)
                return;

            string killer = ev.Attacker.Nickname;
            string victim = ev.Player.Nickname;
            string weapon = ev.DamageHandler.Type.ToString();
            string part = GetHitPart(ev.DamageHandler);

            string msg = $"{killer} a tué {victim} avec un(e) {weapon} {part}";

            foreach (var p in Player.List)
            {
                var dynamicHint = new DynamicHint
                {
                    Id = msg,
                    Text = msg,
                    FontSize = 20,
                    LineHeight = 2f,
                    TargetX = 0,
                    TargetY = 100,
                    Priority = HintPriority.Medium
                };

                PlayerDisplay.Get(p).AddHint(dynamicHint);

                Timing.CallDelayed(4f, () =>
                {
                    PlayerDisplay.Get(p).RemoveHint(msg);
                });
            }
        }
    }
}