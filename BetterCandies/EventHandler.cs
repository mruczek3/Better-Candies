using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using Exiled.Events;

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
                Log.Info($"{ev.Player.Nickname} used a candy!");

                // Select effect
                int effect = Random.Next(12); 

                switch (effect)
                {
                    case 0:
                        KillPlayer(ev.Player);
                        ShowHint(ev.Player, "You died!");
                        break;
                    case 1:
                        ApplyBlindedEffect(ev.Player);
                        ShowHint(ev.Player, "You are blinded!");
                        break;
                    case 2:
                        AddHealth(ev.Player, 50);
                        ShowHint(ev.Player, "You gained 50 health!");
                        break;
                    case 3:
                        ApplyScp207Effect(ev.Player);
                        ShowHint(ev.Player, "You got SCP-207 effect!");
                        break;
                    case 4:
                        ApplyBleedingEffect(ev.Player);
                        ShowHint(ev.Player, "You are bleeding!");
                        break;
                    case 5:
                        ApplyBurnedEffect(ev.Player);
                        ShowHint(ev.Player, "You are burned!");
                        break;
                    case 6:
                        ApplyConcussedEffect(ev.Player);
                        ShowHint(ev.Player, "You are concussed!");
                        break;
                    case 7:
                        ApplyDeafenedEffect(ev.Player);
                        ShowHint(ev.Player, "You are deafened!");
                        break;
                    case 8:
                        ApplyExhaustedEffect(ev.Player);
                        ShowHint(ev.Player, "You are exhausted!");
                        break;
                    case 9:
                        GiveRandomKeycard(ev.Player);
                        ShowHint(ev.Player, "You received a random keycard!");
                        break;
                    case 10:
                        GiveRandomFirearm(ev.Player);
                        ShowHint(ev.Player, "You received a random firearm!");
                        break;
                    case 11:
                        TeleportToRandomPlayer(ev.Player);
                        ShowHint(ev.Player, "You were teleported to a random player!");
                        break;
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
            Log.Info($"{player.Nickname} died after eating a candy.");
        }

        private void ApplyBlindedEffect(Player player)
        {
            player.EnableEffect(EffectType.Blinded, 10); // Blinded effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(EffectType.Blinded));
            Log.Info($"{player.Nickname} was blinded for 10 seconds after eating a candy.");
        }

        private void AddHealth(Player player, int amount)
        {
            player.Health += amount;
            Log.Info($"{player.Nickname} gained {amount} health points after eating a candy.");
        }

        private void ApplyScp207Effect(Player player)
        {
            player.EnableEffect(EffectType.Scp207, 10); // SCP-207 effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(EffectType.Scp207));
            Log.Info($"{player.Nickname} received SCP-207 effect for 10 seconds after eating a candy.");
        }

        private void ApplyBleedingEffect(Player player)
        {
            player.EnableEffect(EffectType.Bleeding, 10); // Bleeding effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(EffectType.Bleeding));
            Log.Info($"{player.Nickname} is bleeding for 10 seconds after eating a candy.");
        }

        private void ApplyBurnedEffect(Player player)
        {
            player.EnableEffect(EffectType.Burned, 10); // Burned effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(EffectType.Burned));
            Log.Info($"{player.Nickname} is burned for 10 seconds after eating a candy.");
        }

        private void ApplyConcussedEffect(Player player)
        {
            player.EnableEffect(EffectType.Concussed, 10); // Concussed effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(EffectType.Concussed));
            Log.Info($"{player.Nickname} is concussed for 10 seconds after eating a candy.");
        }

        private void ApplyDeafenedEffect(Player player)
        {
            player.EnableEffect(EffectType.Deafened, 10); // Deafened effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(EffectType.Deafened));
            Log.Info($"{player.Nickname} is deafened for 10 seconds after eating a candy.");
        }

        private void ApplyExhaustedEffect(Player player)
        {
            player.EnableEffect(EffectType.Exhausted, 10); // Exhausted effect for 10 seconds
            Timing.CallDelayed(10f, () => player.DisableEffect(EffectType.Exhausted));
            Log.Info($"{player.Nickname} is exhausted for 10 seconds after eating a candy.");
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
            Log.Info($"{player.Nickname} received a random keycard: {randomKeycard} after eating a candy.");
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
            Log.Info($"{player.Nickname} received a random firearm: {randomFirearm} after eating a candy.");
        }

        private void TeleportToRandomPlayer(Player player)
        {
            var players = Player.List.Where(p => p != player && !p.IsScp).ToList();
            if (players.Count == 0)
            {
                Log.Info("No available players to teleport to.");
                return;
            }

            Player randomPlayer = players[Random.Next(players.Count)];
            player.Position = randomPlayer.Position;
            Log.Info($"{player.Nickname} was teleported to {randomPlayer.Nickname} after eating a candy.");
        }
    }
}
