using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace Fondation_Delta_DarkRP
{
    public class LastPlayer
    {
        private CoroutineHandle flickerHandle;

        public void Count(DiedEventArgs ev)
        {
            int aliveCount = Player.List.Count(p => p.IsAlive && p.IsHuman);

            if (aliveCount == 1)
            {
                var lastPlayer = Player.List.FirstOrDefault(p => p.IsAlive && p.IsHuman);
                if (lastPlayer == null)
                    return;

                string zoneName = lastPlayer.Zone.ToString();

                if (!flickerHandle.IsRunning)
                {
                    Cassie.MessageTranslated(
                        "$PITCH_0.3 .G4 .G4 $PITCH_0.95 Last human standing",
                        $"<b><color=red>Dernier humain restant en {zoneName}.</color></b>"
                    );

                    flickerHandle = Timing.RunCoroutine(FlickerLightsLoop(lastPlayer));
                }
            }
        }

        public void OnRoundStarted()
        {
            if (flickerHandle.IsRunning)
                Timing.KillCoroutines(flickerHandle);
        }

        private IEnumerator<float> FlickerLightsLoop(Player lastPlayer)
        {
            while (Player.List.Count(p => p.IsAlive && p.IsHuman) == 1)
            {
                Map.ChangeLightsColor(Color.red);
                yield return Timing.WaitForSeconds(1f);

                Map.ResetLightsColor();
                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
