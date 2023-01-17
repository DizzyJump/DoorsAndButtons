using System.Threading;
using CodeBase.GameLogic;
using CodeBase.Services.TimeService;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState, ITickable
    {
        private IGameplayModeService gameplayModeService;
        private IGameStateMachine gameStateMachine;
        private ITimeService timeService;

        public GameLoopState(IGameStateMachine gameStateMachine, IGameplayModeService gameplayModeService, ITimeService timeService)
        {
            this.gameplayModeService = gameplayModeService;
            this.gameStateMachine = gameStateMachine;
            this.timeService = timeService;
        }

        public void Enter()
        {
            timeService.SetScale(1f);
        }

        public void Exit() => 
            gameplayModeService.Cleanup();

        public void Tick()
        {
            gameplayModeService.Tick();

            CheckSessionEnd();
        }

        private void CheckSessionEnd()
        {
            if (gameplayModeService.IsSessionEnd)
                gameStateMachine.Enter<FinishGameSessionState, GameplaySessionResult>(gameplayModeService.GetSessionResult());
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}