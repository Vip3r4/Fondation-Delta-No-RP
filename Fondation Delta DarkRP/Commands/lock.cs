using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Interactables.Interobjects.DoorUtils;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace Fondation_Delta_NoRP
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Lock : ICommand
    {
        public string Command => "lock";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Permet au Chaos Hackeur de lock une porte pendant 10 secondes.";

        private static readonly Dictionary<string, float> Cooldowns = new Dictionary<string, float>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Commande invalide : joueur introuvable.";
                return false;
            }

            float now = Time.time;

            if (Cooldowns.TryGetValue(player.UserId, out float readyTime))
            {
                if (now < readyTime)
                {
                    float remaining = readyTime - now;
                    response = $"Cooldown actif : {Mathf.Ceil(remaining)} secondes restantes.";
                    return false;
                }
            }

            var hackerchaos = CustomRole.Get(4);
            if (hackerchaos == null || !hackerchaos.Check(player))
            {
                response = "Vous devez être un Chaos Hackeur pour faire cela.";
                return false;
            }

            if (Physics.Raycast(player.CameraTransform.position,
                            player.CameraTransform.forward,
                            out RaycastHit hit,
                            4f))
            {
                var doorVariant = hit.collider.GetComponentInParent<DoorVariant>();

                if (doorVariant == null)
                {
                    response = "Vous devez regarder une porte.";
                    return false;
                }

                var door = Exiled.API.Features.Doors.Door.Get(doorVariant);

                if (door == null)
                {
                    response = "Porte invalide.";
                    return false;
                }

                if (door.IsLocked)
                {
                    response = "La porte est déjà verrouillée.";
                    return false;
                }

                if (door.Type != DoorType.HeavyContainmentDoor &&
                    door.Type != DoorType.LightContainmentDoor &&
                    door.Type != DoorType.EntranceDoor)
                {
                    response = "Vous ne pouvez pas verrouiller cette porte.";
                    return false;
                }

                door.Lock(10f, DoorLockType.AdminCommand);
                Cooldowns[player.UserId] = Time.time + 90f;
                response = "Porte verrouillée.";
                return true;
            }

            response = "Vous devez regarder une porte pour faire ça.";
            return false;
        }
    }
}