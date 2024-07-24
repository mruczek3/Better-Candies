# BetterCandies

BetterCandies is a plugin for SCP: Secret Laboratory that enhances the effects of SCP-330 candies. Each time a player consumes a candy, they experience a random effect. This plugin adds new and exciting effects to make the game more unpredictable and fun. You also have the option of setting your own chance for various effects via config and your own hints via translations

## Features

When a player consumes a candy from SCP-330, one of the following effects will occur:

1. **Death**: The player dies instantly.
2. **Random effect**: The player is blinded for 10 seconds.
3. **Health Boost**: The player gains 50 health points.
4. **Random Keycard**: The player receives a random keycard.
5. **Random Firearm**: The player receives a random firearm.
6. **Teleportation**: The player is teleported to a random player.
7. **Size Decrease**: The player shrinks to 50% of their size.


    **! There are many more effects/features planned to be added !**

## Installation

1. Download the latest version of the plugin from the [Releases](https://github.com/Mruczek2137/better-candies/releases) page.
2. Place the downloaded DLL file in your server Exiled `plugins` directory.
3. Restart your server.


## Configuration

A configuration file will be generated on the first run of the plugin. You can modify this file to change various settings.

**! In the next version of the plugin, more configuration options will be added !**

```yaml
better_candies:
  is_enabled: true
  debug: false

  Percentage chance for individual effects.
  show_hints: true
  kill_chance: 10
  gain_health_chance: 10
  random_keycard_chance: 5
  random_firearm_chance: 5
  teleport_chance: 10
  decrease_size_chance: 5
  random_effect_chance: 55
```

## Support 

**If you have any problem with the plugin, please create an issue and I will be happy to help you**

[**ISSUES**](https://github.com/Mruczek2137/Better-Candies/issues)
