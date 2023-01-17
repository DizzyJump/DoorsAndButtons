using System.Threading;
using System.Threading.Tasks;
using CodeBase.GameLogic.Configs;

namespace CodeBase.GameLogic
{
    public interface IGameplayModeService
    {
        void InitializeGameplaySession(LevelConfig levelConfig);
        void Cleanup();
        void Tick();
        bool IsSessionEnd { get; }
        GameplaySessionResult GetSessionResult();
    }
}