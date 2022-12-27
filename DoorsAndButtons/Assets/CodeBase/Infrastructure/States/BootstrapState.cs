using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            gameStateMachine.Enter<LoadLevelState, string>(InfrastructureAssetPath.GameScene);
        }

        public void Exit()
        {
            
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
        {
        }
    }
}