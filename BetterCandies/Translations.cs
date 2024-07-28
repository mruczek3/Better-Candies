using Exiled.API.Interfaces;
using System.ComponentModel;

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
    public string RandomEffectHint { get; set; } = "You got the {0} effect for {1} seconds!";

    [Description("Hint shown when a player decreases in size.")]
    public string DecreasedSizeHint { get; set; } = "Your size decreased by 50%!";

    [Description("Hint shown when the player transforms into scp-049-2.")]
    public string TransformedHint { get; set; } = "You have been turned into a zombie";

    [Description("Hint shown when a player is teleported to scp")]
    public string TeleportedScpHint { get; set; } = "You were teleported to a random scp";

    [Description("Hint shown to the player when nothing happened after eating the candy")]
    public string NothingHint { get; set; } = "You ate the candy and nothing happened";

    [Description("Hint when a player eats a candy and switches roles with another player")]
    public string SwapRoleHint { get; set; } = "You switched roles with a random player";

    [Description("Hint shown to the player who was chosen to switch roles with the one who ate the candy")]
    public string SwappedRoleWithHint { get; set; } = "Your role has been swapped with another player";

    [Description("Hint shown when the player teleports to a random room")]
    public string TeleportedRoomHint { get; set; } = "You teleported to a random room";

    [Description("Hint shown when the player loses HP")]
    public string LostHealthHint { get; set; } = "You have lost {0} health points!";

    [Description("Hint shown when the player is healed to maximum health.")]
    public string HealedToMaxHint { get; set; } = "You were healed to maximum health!";

    [Description("Hint shown when the player receives a random healing item.")]
    public string ReceivedHealingItemHint { get; set; } = "You received a random healing item!";
}
