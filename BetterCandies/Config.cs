using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BetterCandies
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool ShowHints { get; set; } = true;

        [Description("Percentage chance for individual effects. !!!! Everything added together must equal 100 perfectly, otherwise the plugin will not work !!!!")]

        public int KillChance { get; set; } = 10;
        public int GainHealthChance { get; set; } = 10;
        public int RandomKeycardChance { get; set; } = 5;
        public int RandomFirearmChance { get; set; } = 5;
        public int TeleportChance { get; set; } = 10;
        public int DecreaseSizeChance { get; set; } = 5;
        public int RandomEffectChance { get; set; } = 50; 
    }
}
