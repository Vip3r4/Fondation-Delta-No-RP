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
    public class Boom : ICommand
    {
        public string Command => "boom";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Permet au Chaos Bombardier d'exploser";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "Player not found.";
                return false;
            }
            
            var bomberchaos = CustomRole.Get(1);
            if (bomberchaos == null || !bomberchaos.Check(player))
            {
                response = "Vous devez être un Chaos Bombardier pour faire cela.";
                return false;
            }
            else
            {
                player.Explode();
                
                response = "Boom.";
                return true;
            }
        }
    }
}