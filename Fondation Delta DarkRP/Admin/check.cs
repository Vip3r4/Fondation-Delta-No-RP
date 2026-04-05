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
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Check : ICommand
    {
        public string Command => "check";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Permet à un administrateur d'avoir des informations sur un joueur";

        private readonly HashSet<string> roles = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "owner",
            "ownerr",
            "comite",
            "resp_admin",
            "resp_modo",
            "resp_anim",
            "admin",
            "admin_recrue"
        };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player admin = Player.Get(sender);
            if (admin == null)
            {
                response = "Player not found";
                return false;
            }

            if (admin.GroupName == null || !roles.Contains(admin.GroupName))
            {
                response = "Vous n'avez pas la permission de faire cela";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = "Usage : check <player id>";
                return false;
            }

            if (!int.TryParse(arguments.At(0), out int targetId))
            {
                response = "ID invalide";
                return false;
            }

            Player target = Player.Get(targetId);
            if (target == null)
            {
                response = "Joueur introuvable";
                return false;
            }

            response = $"Nom : {target.Nickname}\n" +
                       $"ID : {target.Id}\n" +
                       $"Rôle : {target.Role}\n" +
                       $"HP : {target.Health}\n" +
                       $"Position : {target.Position}\n" +
                       $"SteamID : {target.UserId}\n";
            return true;
        }
    }
}