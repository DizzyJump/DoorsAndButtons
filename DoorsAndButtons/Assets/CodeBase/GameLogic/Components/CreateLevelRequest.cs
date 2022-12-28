using CodeBase.GameLogic.Configs;

namespace CodeBase.GameLogic.Components
{
    // Component trigger building of level entities from config description
    public struct CreateLevelRequest
    {
        public LevelConfig Config;
    }
}