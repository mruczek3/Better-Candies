using Exiled.API.Interfaces;

namespace BetterCandies
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool ShowHints { get; set; } = true;
    }
}
