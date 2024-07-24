using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BetterCandies
{
    public class Translations : ITranslation
    {
        [Description("Hint shown when a player dies.")]
        public string DiedHint { get; set; } = "You died!";

        [Description("Hint shown when a player gains health.")]
        public string GainedHealthHint { get; set; } = "You gained {0} health!";

        [Description("Hint shown when a player receives a random keycard.")]
        public string ReceivedKeycardHint { get; set; } = "You received a random keycard!";

        [Description("Hint shown when a player receives a random firearm.")]
        public string ReceivedFirearmHint { get; set; } = "You received a random firearm!";

        [Description("Hint shown when a player is teleported to another player.")]
        public string TeleportedHint { get; set; } = "You were teleported to a random player";

        [Description("Hint shown when a player gets a random effect.")]
        public string RandomEffectHint { get; set; } = "You got the {0} effect for 10 seconds!";

        [Description("Hint shown when a player decreases in size.")]
        public string DecreasedSizeHint { get; set; } = "Your size decreased by 50%!";
    }
}
