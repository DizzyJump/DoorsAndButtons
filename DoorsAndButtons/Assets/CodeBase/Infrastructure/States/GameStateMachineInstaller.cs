using CodeBase.Signals;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IGameStateMachine, BootstrapState, BootstrapState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadLevelState, LoadLevelState.Factory>();
            Container.BindFactory<IGameStateMachine, GameLoopState, GameLoopState.Factory>();

            Container.BindSignal<FinishLevelSignal>()
                .ToMethod<GameLoopState>((state, signal)=>state.OnFinishSession(signal.isWin))
                .FromResolveAll();
            
            Container.Bind(typeof(IGameStateMachine), typeof(ITickable)).To<GameStateMachine>().AsSingle();
        }
    }
}