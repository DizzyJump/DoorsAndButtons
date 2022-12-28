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

        public void Tick()
        {
            gameplayEngine.Update(Time.deltaTime);
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}