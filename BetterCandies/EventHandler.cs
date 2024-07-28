using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using MEC;
using UnityEngine;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp330;
using System.Collections.Generic;
using PlayerRoles;

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

        public void OnEatenScp330(EatenScp330EventArgs ev)
        {
            Log.Debug($"{ev.Player.Nickname} used a candy!");

            int totalChance = config.KillChance + config.GainHealthChance + config.RandomKeycardChance +
                              config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance +
                              config.RandomEffectChance + config.TransformToScp0492Chance + config.TeleportToRandomScpChance +
                              config.SwapRoleChance + config.TeleportToRoomChance + config.RandomHealthLossChance +
                              config.HealToMaxChance + config.GiveRandomHealingItemChance;

            // Added chance to do nothing
            int nothingRoll = Random.Next(100);
            if (nothingRoll < config.ChanceToDoNothing)
            {
                ShowHint(ev.Player, translations.NothingHint);
                Log.Debug($"{ev.Player.Nickname} ate a candy but nothing happened.");
                return;
            }

            int roll = Random.Next(totalChance);

            if (roll < config.KillChance)
            {
                KillPlayer(ev.Player);
                ShowHint(ev.Player, translations.DiedHint);
            }
            else if (roll < config.KillChance + config.GainHealthChance)
            {
                AddHealth(ev.Player, config.HealthGainAmount);
                ShowHint(ev.Player, string.Format(translations.GainedHealthHint, config.HealthGainAmount));
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
            else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance + config.RandomEffectChance)
            {
                ApplyRandomEffect(ev.Player);
            }
            else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance + config.RandomEffectChance + config.TransformToScp0492Chance)
            {
                TransformToScp0492(ev.Player);
            }
            else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance + config.RandomEffectChance + config.TransformToScp0492Chance + config.SwapRoleChance)
            {
                SwapRoles(ev.Player);
                ShowHint(ev.Player, translations.SwapRoleHint);
            }
            else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance + config.RandomEffectChance + config.TransformToScp0492Chance + config.SwapRoleChance + config.TeleportToRandomScpChance)
            {
                TeleportToRandomScp(ev.Player);
            }
            else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance + config.RandomEffectChance + config.TransformToScp0492Chance + config.SwapRoleChance + config.TeleportToRandomScpChance + config.TeleportToRoomChance)
            {
                TeleportToRandomRoom(ev.Player);
            }
            else if (roll < config.KillChance + config.GainHealthChance + config.RandomKeycardChance + config.RandomFirearmChance + config.TeleportChance + config.DecreaseSizeChance + config.RandomEffectChance + config.TransformToScp0492Chance + config.SwapRoleChance + config.TeleportToRandomScpChance + config.TeleportToRoomChance + config.HealToMaxChance)
            {
                HealToMax(ev.Player);
                ShowHint(ev.Player, translations.HealedToMaxHint);
            }
            else
            {
                GiveRandomHealingItem(ev.Player);
                ShowHint(ev.Player, translations.ReceivedHealingItemHint);
            }
        }

        private void HealToMax(Player player)
        {
            player.Health = player.MaxHealth;
            Log.Debug($"{player.Nickname} was healed to max health after eating a candy.");
        }

        private void GiveRandomHealingItem(Player player)
        {
            if (config.AvailableHealingItems.Count == 0)
            {
                Log.Debug("No available healing items to give.");
                return;
            }

            ItemType randomHealingItem = config.AvailableHealingItems[Random.Next(config.AvailableHealingItems.Count)];
            player.AddItem(randomHealingItem);
            Log.Debug($"{player.Nickname} received a random healing item: {randomHealingItem} after eating a candy.");
        }

        private void LoseRandomHealth(Player player)
        {
            int lostHealth = Random.Next(1, 99);
            player.Health -= lostHealth;
            ShowHint(player, string.Format(translations.LostHealthHint, lostHealth));
            Log.Debug($"{player.Nickname} lost {lostHealth} health points after eating a candy.");
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
                EffectType.Scp1853,
                EffectType.Invigorated,
                EffectType.CardiacArrest,
                EffectType.Decontaminating,
                EffectType.PocketCorroding,
                EffectType.Ghostly,
                EffectType.Poisoned,
                EffectType.SeveredHands
            };

            EffectType randomEffect = effects[Random.Next(effects.Length)];
            player.EnableEffect(randomEffect, config.RandomEffectDuration, true);
            Timing.CallDelayed(config.RandomEffectDuration, () => player.DisableEffect(randomEffect));
            ShowHint(player, string.Format(translations.RandomEffectHint, randomEffect.ToString().ToLower(), config.RandomEffectDuration));
            Log.Debug($"{player.Nickname} received effect {randomEffect} for {config.RandomEffectDuration} seconds after eating a candy.");
        }

        private void GiveRandomKeycard(Player player)
        {
            if (config.AvailableKeycards.Count == 0)
            {
                Log.Debug("No available keycards to give.");
                return;
            }

            ItemType randomKeycard = config.AvailableKeycards[Random.Next(config.AvailableKeycards.Count)];
            player.AddItem(randomKeycard);
            Log.Debug($"{player.Nickname} received a random keycard: {randomKeycard} after eating a candy.");
        }

        private void GiveRandomFirearm(Player player)
        {
            if (config.AvailableFirearms.Count == 0)
            {
                Log.Debug("No available firearms to give.");
                return;
            }

            ItemType randomFirearm = config.AvailableFirearms[Random.Next(config.AvailableFirearms.Count)];
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

        private void TransformToScp0492(Player player)
        {
            player.Role.Set(RoleTypeId.Scp0492);
            Log.Debug($"{player.Nickname} transformed into SCP-049-2 after eating a candy.");
            ShowHint(player, translations.TransformedHint);
        }

        private void TeleportToRandomScp(Player player)
        {
            var scps = Player.List.Where(p => p.IsScp && p != player).ToList();
            if (scps.Count == 0)
            {
                Log.Debug("No SCP subjects available to teleport to.");
                return;
            }

            Player randomScp = scps[Random.Next(scps.Count)];
            player.Position = randomScp.Position + Vector3.up;
            ShowHint(player, translations.TeleportedScpHint);
            Log.Debug($"{player.Nickname} was teleported to a random SCP subject: {randomScp.Role} after eating a candy.");
        }

        private void SwapRoles(Player player)
        {
            var players = Player.List.Where(p => p != player && p.IsAlive).ToList();
            if (players.Count == 0)
            {
                Log.Debug("No available players to swap roles with.");
                return;
            }

            Player randomPlayer = players[Random.Next(players.Count)];
            RoleTypeId tempRole = player.Role.Type;
            player.Role.Set(randomPlayer.Role.Type);
            randomPlayer.Role.Set(tempRole);

            ShowHint(player, translations.SwapRoleHint);
            ShowHint(randomPlayer, translations.SwappedRoleWithHint);

            Log.Debug($"{player.Nickname} swapped roles with {randomPlayer.Nickname} after eating a candy.");
        }

        private void TeleportToRandomRoom(Player player)
        {
            Room[] rooms = Room.List.ToArray();
            Room randomRoom = rooms[Random.Next(rooms.Length)];
            player.Position = randomRoom.Position + Vector3.up;
            ShowHint(player, translations.TeleportedRoomHint);
            Log.Debug($"{player.Nickname} was teleported to a random room: {randomRoom.Name} after eating a candy.");
        }
    }
}
