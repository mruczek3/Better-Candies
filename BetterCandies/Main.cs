using Exiled.API.Features;
using System;
using Exiled.Events.Handlers;
using Exiled.Events;

namespace BetterCandies
{
    public class Main : Plugin<Config>
    {
        public override string Name => "BetterCandies";
        public override string Author => "mruczek :3";
        public override Version RequiredExiledVersion => new Version(8, 9, 11);
        public override Version Version => new Version(1, 1, 0);

        public static Main Instance { get; private set; }

        private EventHandler eventHandler;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandler = new EventHandler(Config, new Translations());
            Exiled.Events.Handlers.Player.UsedItem += eventHandler.OnUsedItem;
            Exiled.Events.Handlers.Player.Died += eventHandler.OnDied;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.UsedItem -= eventHandler.OnUsedItem;
            Exiled.Events.Handlers.Player.Died -= eventHandler.OnDied;
            eventHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}
