using CodeBase.GameLogic.Configs;

namespace CodeBase.GameLogic
{
    public interface IGameplayEngine
    {
        void InitializeLevel(LevelConfig levelConfig);
        void Update(float dt);
        void Cleanup();
    }
}