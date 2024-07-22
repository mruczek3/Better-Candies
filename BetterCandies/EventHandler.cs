using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using MEC;
using Exiled.Events;
using Exiled.Events.EventArgs.Player;

namespace BetterCandies
{
    public class EventHandler
    {
        private static readonly Random Random = new Random();
        private readonly Config config;

        public EventHandler(Config config)
        {
            this.config = config;
        }

        public void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type == ItemType.SCP330)
            {
                Log.Debug($"{ev.Player.Nickname} used a candy!");

                // Select effect
                int effect = Random.Next(12);

                if (effect == 0)
                {
                    KillPlayer(ev.Player);
                    ShowHint(ev.Player, "You died!");
                }
                else if (effect == 2)
                {
                    AddHealth(ev.Player, 50);
                    ShowHint(ev.Player, "You gained 50 health!");
                }
                else if (effect >= 9)
                {
                    if (effect == 9)
                    {
                        GiveRandomKeycard(ev.Player);
                        ShowHint(ev.Player, "You received a random keycard!");
                    }
                    else if (effect == 10)
                    {
                        GiveRandomFirearm(ev.Player);
                        ShowHint(ev.Player, "You received a random firearm!");
                    }
                    else if (effect == 11)
                    {
                        TeleportToRandomPlayer(ev.Player);
                        ShowHint(ev.Player, "You were teleported to a random player!");
                    }
                }
                else
                {
                    ApplyRandomEffect(ev.Player);
                }
            }
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
                EffectType.Blinded,
                EffectType.Scp207,
                EffectType.Bleeding,
                EffectType.Burned,
                EffectType.Concussed,
                EffectType.Deafened,
                EffectType.Exhausted
            };

            EffectType randomEffect = effects[Random.Next(effects.Length)];
            player.EnableEffect(randomEffect, 10); // Apply effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(randomEffect));
            ShowHint(player, $"You are {randomEffect.ToString().ToLower()}!");
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
                ItemType.KeycardFacilityManager
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
            };

            ItemType randomFirearm = firearms[Random.Next(firearms.Length)];
            player.AddItem(randomFirearm);
            Log.Debug($"{player.Nickname} received a random firearm: {randomFirearm} after eating a candy.");
        }

        private void TeleportToRandomPlayer(Player player)
        {
            var players = Player.List.Where(p => p != player && !p.IsScp).ToList();
            if (players.Count == 0)
            {
                Log.Debug("No available players to teleport to.");
                return;
            }

            Player randomPlayer = players[Random.Next(players.Count)];
            player.Position = randomPlayer.Position;
            Log.Debug($"{player.Nickname} was teleported to {randomPlayer.Nickname} after eating a candy.");
        }
    }
}
