using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;

public class Config : IConfig
{
    public bool IsEnabled { get; set; } = true;
    public bool Debug { get; set; } = false;
    public bool ShowHints { get; set; } = true;

    [Description("Percentage chance for individual effects. !!!! Everything added together must equal 100 perfectly.!!!!")]
    public int KillChance { get; set; } = 10;
    public int GainHealthChance { get; set; } = 10;
    public int RandomKeycardChance { get; set; } = 5;
    public int RandomFirearmChance { get; set; } = 5;
    public int TeleportChance { get; set; } = 10;
    public int DecreaseSizeChance { get; set; } = 5;
    public int RandomEffectChance { get; set; } = 10;
    public int TransformToScp0492Chance { get; set; } = 10;
    public int TeleportToRandomScpChance { get; set; } = 10;
    public int SwapRoleChance { get; set; } = 5;
    public int RandomHealthLossChance { get; set; } = 10;
    public int TeleportToRoomChance { get; set; } = 10;
    public int HealToMaxChance { get; set; } = 5;
    public int GiveRandomHealingItemChance { get; set; } = 5;

    [Description("Amount of health to gain when the health gain effect is triggered.")]
    public int HealthGainAmount { get; set; } = 50;

    [Description("List of available keycards that players can receive.")]
    public List<ItemType> AvailableKeycards { get; set; } = new List<ItemType>
    {
        ItemType.KeycardJanitor,
        ItemType.KeycardScientist,
        ItemType.KeycardResearchCoordinator,
        ItemType.KeycardZoneManager,
        ItemType.KeycardGuard,
        ItemType.KeycardMTFPrivate,
        ItemType.KeycardContainmentEngineer,
        ItemType.KeycardMTFOperative,
        ItemType.KeycardMTFCaptain,
        ItemType.KeycardFacilityManager,
        ItemType.KeycardChaosInsurgency,
        ItemType.KeycardO5
    };

    [Description("List of available firearms that players can receive.")]
    public List<ItemType> AvailableFirearms { get; set; } = new List<ItemType>
    {
        ItemType.GunA7,
        ItemType.GunRevolver,
        ItemType.ParticleDisruptor,
        ItemType.GunCom45,
        ItemType.GunFRMG0,
        ItemType.GunCrossvec,
        ItemType.GunE11SR,
        ItemType.GunLogicer,
        ItemType.GunFSP9,
        ItemType.GunShotgun
    };

    [Description("List of available healing items that players can receive.")]
    public List<ItemType> AvailableHealingItems { get; set; } = new List<ItemType>
    {
        ItemType.Medkit,
        ItemType.Adrenaline,
        ItemType.Painkillers
    };

    [Description("Duration of random effects in seconds.")]
    public int RandomEffectDuration { get; set; } = 10;

    [Description("Chance that a candy will do nothing.")]
    public int ChanceToDoNothing { get; set; } = 50;
}
