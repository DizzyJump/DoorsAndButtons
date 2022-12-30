using CodeBase.GameLogic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState, ITickable
    {
        private IGameplayEngine gameplayEngine;
        private IGameStateMachine gameStateMachine;

        public GameLoopState(IGameStateMachine gameStateMachine, IGameplayEngine gameplayEngine)
        {
            this.gameplayEngine = gameplayEngine;
            this.gameStateMachine = gameStateMachine;
        }
        
        public void Exit()
        {
            gameplayEngine.Cleanup();
        }

        public void Enter()
        {
            Debug.Log("Game loop state enter");
        }

        public void OnFinishSession(bool isWin)
        {
            Debug.Log($"Level win: {isWin}");
            gameStateMachine.Enter<LoadLevelState, string>(SceneNames.GameScene); // reload level due to we have only one in this game
        }
        
        public void Tick()
        {
            gameplayEngine.Update();
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}