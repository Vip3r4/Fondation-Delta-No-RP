using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomRoles.API;
using Exiled.Loader.Models;
using Fondation_Delta_DarkRP;
using Fondation_Delta_DarkRP.PMer;
using static PlayerList;

namespace Fondation_Delta_NoRP
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "Site-Delta NoRP";
        public override string Author => "Vip3r";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion => new Version(9, 13, 1);

        private EventHandlers EventHandler;
        private KillCount KillCount;
        private LastPlayer LastPlayer;
        private ActivateHint ActivateHint;
        private KillStreak KillStreak;
        private KillFeed KillFeed;
        private reviver_btn Reviver;

        public static Plugin Instance { get; private set; }

        private Exiled.CustomRoles.API.Features.CustomRole bomberchaos;
        private Exiled.CustomRoles.API.Features.CustomRole healerntf;
        private Exiled.CustomRoles.API.Features.CustomRole miniclassd;
        private Exiled.CustomRoles.API.Features.CustomRole hackerchaos;
        private Exiled.CustomRoles.API.Features.CustomRole secretchaos;

        private Exiled.CustomItems.API.Features.CustomWeapon sniper;
        private Exiled.CustomItems.API.Features.CustomItem nightvision;
        private Exiled.CustomItems.API.Features.CustomItem phantominject;
        private Exiled.CustomItems.API.Features.CustomWeapon healgun;
        private Exiled.CustomItems.API.Features.CustomItem dice;
        private Exiled.CustomItems.API.Features.CustomItem coin;

        public override void OnEnabled()
        {
            EventHandler = new EventHandlers();
            KillCount = new KillCount();
            LastPlayer = new LastPlayer();
            ActivateHint = new ActivateHint();
            KillStreak = new KillStreak();
            KillFeed = new KillFeed();
            Reviver = new reviver_btn();

            Exiled.Events.Handlers.Player.TriggeringTesla += EventHandler.NoTeslaForMTF;
            Exiled.Events.Handlers.Player.Died += EventHandler.BecomeZombie;
            Exiled.Events.Handlers.Server.RoundEnded += EventHandler.OnRoundEnd;
            Exiled.Events.Handlers.Server.WaitingForPlayers += EventHandler.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.PickingUpItem += EventHandler.OnPickingUpCustomItems;
            Exiled.Events.Handlers.Player.Died += EventHandler.OnPlayerDied;

            Exiled.Events.Handlers.Player.Dying += KillFeed.OnDying;

            Exiled.Events.Handlers.Player.Died += KillCount.OnPlayerDied;
            Exiled.Events.Handlers.Server.RoundEnded += KillCount.ShowMVP;

            Exiled.Events.Handlers.Player.Died += LastPlayer.Count;
            Exiled.Events.Handlers.Server.RoundStarted += LastPlayer.OnRoundStarted;

            Exiled.Events.Handlers.Player.InteractingElevator += ActivateHint.OnPlayerInteractElevator;
            Exiled.Events.Handlers.Scp914.Activating += ActivateHint.On914Activating;

            Exiled.Events.Handlers.Player.Died += KillStreak.OnPlayerDied;
            Exiled.Events.Handlers.Player.Dying += KillStreak.OnDying;
            
            Exiled.Events.Handlers.Player.Dying += Reviver.OnPlayerDied;
            ProjectMER.Events.Handlers.Schematic.ButtonInteracted += Reviver.OnButtonPressed;

            bomberchaos = new BomberChaos();
            bomberchaos.Register();
            healerntf = new HealerNtf();
            healerntf.Register();
            miniclassd = new MiniClassD();
            miniclassd.Register();
            hackerchaos = new HackerChaos();
            hackerchaos.Register();
            secretchaos = new SecretChaos();
            secretchaos.Register();

            sniper = new Sniper();
            sniper.Register();
            nightvision = new NightVision();
            nightvision.Register();
            phantominject = new PhantomInject();
            phantominject.Register();
            healgun = new HealGun();
            healgun.Register();
            dice = new Dice();
            dice.Register();
            coin = new revcoin();
            coin.Register();

            Instance = this;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.TriggeringTesla -= EventHandler.NoTeslaForMTF;
            Exiled.Events.Handlers.Player.Died -= EventHandler.BecomeZombie;
            Exiled.Events.Handlers.Server.RoundEnded -= EventHandler.OnRoundEnd;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= EventHandler.OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.PickingUpItem -= EventHandler.OnPickingUpCustomItems;
            Exiled.Events.Handlers.Player.Died -= EventHandler.OnPlayerDied;

            Exiled.Events.Handlers.Player.Dying -= KillFeed.OnDying;

            Exiled.Events.Handlers.Player.Died -= KillCount.OnPlayerDied;
            Exiled.Events.Handlers.Server.RoundEnded -= KillCount.ShowMVP;

            Exiled.Events.Handlers.Player.Died += LastPlayer.Count;
            Exiled.Events.Handlers.Server.RoundStarted -= LastPlayer.OnRoundStarted;

            Exiled.Events.Handlers.Player.InteractingElevator -= ActivateHint.OnPlayerInteractElevator;
            Exiled.Events.Handlers.Scp914.Activating -= ActivateHint.On914Activating;

            Exiled.Events.Handlers.Player.Died -= KillStreak.OnPlayerDied;
            Exiled.Events.Handlers.Player.Dying -= KillStreak.OnDying;

            Exiled.Events.Handlers.Player.Dying -= Reviver.OnPlayerDied;
            ProjectMER.Events.Handlers.Schematic.ButtonInteracted -= Reviver.OnButtonPressed;

            bomberchaos.Unregister();
            healerntf.Unregister();
            miniclassd .Unregister();
            hackerchaos.Unregister();
            secretchaos.Unregister();

            sniper.Unregister();
            nightvision.Unregister();
            phantominject.Unregister();
            healgun.Unregister();
            dice.Unregister();
            coin.Unregister();

            Instance = null;
            EventHandler = null;
            KillCount = null;
            LastPlayer = null;
            ActivateHint = null;
            KillStreak = null;
            KillFeed = null;
            Reviver = null;
            base.OnDisabled();
        }
    }
}
