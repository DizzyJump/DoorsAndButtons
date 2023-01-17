using System.Threading.Tasks;
using CodeBase.GameLogic;
using CodeBase.UnityRelatedScripts.UI.Overlays.OverlaysService;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class FinishGameSessionState : IPaylodedState<GameplaySessionResult>
    {
        private IOverlayService overlayService;
        private IGameStateMachine gameStateMachine;
        private IGameplayModeService gameplayModeService;

        public FinishGameSessionState(IGameStateMachine gameStateMachine, IOverlayService overlayService, IGameplayModeService gameplayModeService)
        {
            this.gameStateMachine = gameStateMachine;
            this.overlayService = overlayService;
            this.gameplayModeService = gameplayModeService;
        }
        
        public async void Enter(GameplaySessionResult payload)
        {
            await ShowOverlay(payload);
            Debug.Log("tap!");
            gameStateMachine.Enter<LoadLevelState, string>(SceneNames.GameScene);
        }

        private async Task ShowOverlay(GameplaySessionResult payload)
        {
            string message = payload.Win ? "Congratulations!" : "Level failed";
            await overlayService.ShowFinishLevelOverlay(message);
        }

        public void Exit()
        {
            gameplayModeService.Cleanup();
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, FinishGameSessionState>
        {
        }
    }
}