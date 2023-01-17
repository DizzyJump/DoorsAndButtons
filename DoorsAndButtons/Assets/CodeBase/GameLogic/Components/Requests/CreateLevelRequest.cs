using CodeBase.GameLogic.Configs;

namespace CodeBase.GameLogic.Components.Requests
{
    // Component trigger building of level entities from config description
    public struct CreateLevelRequest
    {
        public LevelConfig Config;
    }
}