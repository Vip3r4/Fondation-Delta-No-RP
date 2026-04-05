using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace Fondation_Delta_NoRP
{
    public class Config : IConfig
    {
        public bool Debug { get; set; } = false;
        public bool IsEnabled { get; set; } = true;

        public bool NoTeslaForMTF { get; set; } = true;
        public bool BecomeZombie { get; set; } = true;
        public int BecomeZombiePercentage = 30;
        public bool OnRoundEnd { get; set; } = true;
        public bool OnWaitingForPlayers { get; set; } = true;

        public bool KillCount { get; set; } = true;
    }
}
