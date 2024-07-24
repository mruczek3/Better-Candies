using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using MEC;
using UnityEngine;
using Exiled.Events.EventArgs.Player;

namespace BetterCandies
{
    public class EventHandler
    {
        private static readonly System.Random Random = new System.Random();
        private readonly Config config;
        private readonly Translations translations;

        public EventHandler(Config config, Translations translations)
        {
            this.config = config;
            this.translations = translations;
        }

        public void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.SCP330)
            {
                Log.Debug($"{ev.Player.Nickname} used a candy!");

                int totalChance = config.KillChance + config.GainHealthChance + config.RandomKeycardChance +
                                  config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance +
                                  config.RandomEffectChance;

                int roll = Random.Next(totalChance);

                if (roll < config.KillChance)
                {
                    KillPlayer(ev.Player);
                    ShowHint(ev.Player, translations.DiedHint);
                }
                else if (roll < config.KillChance + config.GainHealthChance)
                {
                    AddHealth(ev.Player, 50);
                    ShowHint(ev.Player, string.Format(translations.GainedHealthHint, 50));
                }
                else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance)
                {
                    GiveRandomKeycard(ev.Player);
                    ShowHint(ev.Player, translations.ReceivedKeycardHint);
                }
                else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance)
                {
                    GiveRandomFirearm(ev.Player);
                    ShowHint(ev.Player, translations.ReceivedFirearmHint);
                }
                else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance)
                {
                    TeleportToRandomPlayer(ev.Player);
                    ShowHint(ev.Player, translations.TeleportedHint);
                }
                else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance)
                {
                    DecreaseSize(ev.Player);
                }
                else
                {
                    ApplyRandomEffect(ev.Player);
                }
            }
        }

        public void OnDied(DiedEventArgs ev)
        {
            ResetSize(ev.Player);
        }

        private void ShowHint(Player player, string message)
        {
            if (config.ShowHints)
            {
                player.ShowHint(message, 2f); // Show hint for 2 seconds
            }
        }

        private void KillPlayer(Player player)
        {
            player.Kill(DamageType.SeveredHands);
            Log.Debug($"{player.Nickname} died after eating a candy.");
        }

        private void AddHealth(Player player, int amount)
        {
            player.Health += amount;
            Log.Debug($"{player.Nickname} gained {amount} health points after eating a candy.");
        }

        private void ApplyRandomEffect(Player player)
        {
            EffectType[] effects = new EffectType[]
            {
                EffectType.Scp207,
                EffectType.Invisible,
                EffectType.Burned,
                EffectType.Scp1853,
                EffectType.Bleeding,
                EffectType.CardiacArrest,
                EffectType.Decontaminating,
                EffectType.Hypothermia,
                EffectType.Corroding,
                EffectType.AmnesiaVision,
                EffectType.Poisoned,
                EffectType.Traumatized
            };

            EffectType randomEffect = effects[Random.Next(effects.Length)];
            player.EnableEffect(randomEffect, 15, true); 
            Timing.CallDelayed(10f, () => player.DisableEffect(randomEffect));
            ShowHint(player, string.Format(translations.RandomEffectHint, randomEffect.ToString().ToLower()));
            Log.Debug($"{player.Nickname} received effect {randomEffect} for 10 seconds after eating a candy.");
        }

        private void GiveRandomKeycard(Player player)
        {
            ItemType[] keycards = new ItemType[]
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

            ItemType randomKeycard = keycards[Random.Next(keycards.Length)];
            player.AddItem(randomKeycard);
            Log.Debug($"{player.Nickname} received a random keycard: {randomKeycard} after eating a candy.");
        }

        private void GiveRandomFirearm(Player player)
        {
            ItemType[] firearms = new ItemType[]
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

            ItemType randomFirearm = firearms[Random.Next(firearms.Length)];
            player.AddItem(randomFirearm);
            Log.Debug($"{player.Nickname} received a random firearm: {randomFirearm} after eating a candy.");
        }

        private void TeleportToRandomPlayer(Player player)
        {
            var players = Player.List.Where(p => p != player && !p.IsScp && p.IsAlive).ToList();
            if (players.Count == 0)
            {
                Log.Debug("No available players to teleport to.");
                return;
            }

            Player randomPlayer = players[Random.Next(players.Count)];
            player.Position = randomPlayer.Position;
            Log.Debug($"{player.Nickname} was teleported to {randomPlayer.Nickname} after eating a candy.");
        }

        private void DecreaseSize(Player player)
        {
            float originalScale = player.Scale.x;
            float decreasedScale = originalScale * 0.5f;

            player.Scale = new Vector3(decreasedScale, decreasedScale, decreasedScale);
            Log.Debug($"{player.Nickname}'s size decreased by 50% after eating a candy.");

            ShowHint(player, translations.DecreasedSizeHint);
        }

        private void ResetSize(Player player)
        {
            float originalScale = 1f;
            player.Scale = new Vector3(originalScale, originalScale, originalScale);
            Log.Debug($"{player.Nickname}'s size reset to normal after death.");
        }
    }
}
