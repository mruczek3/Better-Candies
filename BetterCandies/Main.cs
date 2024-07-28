using Exiled.API.Features;
using Exiled.API.Interfaces;
using System;
using Exiled.Events.Handlers;
using PlayerEvents = Exiled.Events.Handlers.Player;
using Scp330Events = Exiled.Events.Handlers.Scp330;

namespace BetterCandies
{
    public class Main : Plugin<Config, Translations>
    {
        public override string Name => "BetterCandies";
        public override string Author => "mruczek :3";
        public override Version RequiredExiledVersion => new Version(8, 9, 11);
        public override Version Version => new Version(1, 2, 0);

        public static Main Instance { get; private set; }

        private EventHandler eventHandler;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandler(Config, Translation);

            Scp330Events.EatenScp330 += eventHandler.OnEatenScp330;
            PlayerEvents.Died += eventHandler.OnDied;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Scp330Events.EatenScp330 -= eventHandler.OnEatenScp330;
            PlayerEvents.Died -= eventHandler.OnDied;

            eventHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}
