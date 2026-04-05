using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using Fondation_Delta_NoRP;
using PlayerRoles;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using MEC;

namespace Fondation_Delta_DarkRP
{
    public class KillStreak
    {
        private Dictionary<string, int> killstreak = new Dictionary<string, int>();

        public void OnPlayerDied(DiedEventArgs ev)
        {
            if (Plugin.Instance.Config.KillCount == false) { return; }

            if (ev.Attacker == null || !ev.Attacker.IsHuman) return;

            string id = ev.Attacker.UserId;

            if (id != null)
            {
                if (ev.Attacker.IsHuman)
                {
                    if (!killstreak.ContainsKey(id))
                    {
                        killstreak[id] = 0;
                    }

                    killstreak[id]++;
                }
            }

            switch (killstreak[id])
            {
                case 3:
                    {
                        ev.Attacker.EnableEffect(EffectType.MovementBoost);
                        ev.Attacker.ChangeEffectIntensity(EffectType.MovementBoost, 5);
                        string Text = "Streak de 3 kill: +5 % de Vitesse";
                        var dynamicHint = new DynamicHint
                        {
                            Id = "2k",
                            FontSize = 20,
                            LineHeight = 1f,
                            Text = Text,
                            TargetX = 0,
                            TargetY = 750,
                            Priority = HintPriority.Medium,
                        };
                        PlayerDisplay.Get(ev.Attacker).AddHint(dynamicHint);
                        Timing.CallDelayed(5f, () =>
                        {
                            PlayerDisplay.Get(ev.Attacker).RemoveHint("2k");
                        });
                        break;
                    }
                case 6:
                    {
                        ev.Attacker.EnableEffect(EffectType.DamageReduction);
                        ev.Attacker.ChangeEffectIntensity(EffectType.DamageReduction, 5);
                        string Text = "Streak de 6 kill : +5% de dégats réduits";
                        var dynamicHint = new DynamicHint
                        {
                            Id = "4k",
                            FontSize = 20,
                            LineHeight = 1f,
                            Text = Text,
                            TargetX = 0,
                            TargetY = 750,
                            Priority = HintPriority.Medium,
                        };
                        PlayerDisplay.Get(ev.Attacker).AddHint(dynamicHint);
                        Timing.CallDelayed(5f, () =>
                        {
                            PlayerDisplay.Get(ev.Attacker).RemoveHint("4k");
                        });
                        break;
                    }
                case 9:
                    {
                        ev.Attacker.EnableEffect(EffectType.MovementBoost);
                        ev.Attacker.ChangeEffectIntensity(EffectType.MovementBoost, 10);
                        string Text = "Streak de 9 kill : +10% de Vitesse";
                        var dynamicHint = new DynamicHint
                        {
                            Id = "6k",
                            FontSize = 20,
                            LineHeight = 1f,
                            Text = Text,
                            TargetX = 0,
                            TargetY = 750,
                            Priority = HintPriority.Medium,
                        };
                        PlayerDisplay.Get(ev.Attacker).AddHint(dynamicHint);
                        Timing.CallDelayed(5f, () =>
                        {
                            PlayerDisplay.Get(ev.Attacker).RemoveHint("6k");
                        });
                        break;
                    }
                case 12:
                    {
                        ev.Attacker.EnableEffect(EffectType.MovementBoost);
                        ev.Attacker.ChangeEffectIntensity(EffectType.MovementBoost, 20);
                        string Text = "Streak de 12 kill : +20% de Vitesse";
                        var dynamicHint = new DynamicHint
                        {
                            Id = "8k",
                            FontSize = 20,
                            LineHeight = 1f,
                            Text = Text,
                            TargetX = 0,
                            TargetY = 750,
                            Priority = HintPriority.Medium,
                        };
                        PlayerDisplay.Get(ev.Attacker).AddHint(dynamicHint);
                        Timing.CallDelayed(5f, () =>
                        {
                            PlayerDisplay.Get(ev.Attacker).RemoveHint("8k");
                        });
                        break;
                    }
                case 15:
                    {
                        ev.Attacker.EnableEffect(EffectType.DamageReduction);
                        ev.Attacker.ChangeEffectIntensity(EffectType.DamageReduction, 10);
                        ev.Attacker.MaxHealth = 120;
                        string Text = "Streak de 15 kill : +10% de dégats réduits, +20hp permanent";
                        var dynamicHint = new DynamicHint
                        {
                            Id = "10k",
                            FontSize = 20,
                            LineHeight = 1f,
                            Text = Text,
                            TargetX = 0,
                            TargetY = 750,
                            Priority = HintPriority.Medium,
                        };
                        PlayerDisplay.Get(ev.Attacker).AddHint(dynamicHint);
                        Timing.CallDelayed(5f, () =>
                        {
                            PlayerDisplay.Get(ev.Attacker).RemoveHint("10k");
                        });
                        break;
                    }
            }
        }

        public void OnDying(DyingEventArgs ev)
        {
            killstreak.Remove(ev.Player.UserId);
        }
    }
}
