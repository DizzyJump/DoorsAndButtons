using CodeBase.GameLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Services.InputService;
using CodeBase.Services.TimeService;
using CodeBase.Signals;
using CodeBase.UnityRelatedScripts;
using CodeBase.UnityRelatedScripts.ViewFactories;
using Zenject;

namespace CodeBase.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameBootstraperFactory();

            BindCoroutineRunner();

            BindSceneLoader();

            BindLoadingCurtain();

            BindInputService();

            BindTimeService();

            BindGameplayViewsFactory();
            
            BindGameplayEngine();
            
            //BindSignals();

            BindGameStateMachine();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<FinishLevelSignal>().MoveIntoAllSubContainers();
        }

        private void BindGameplayViewsFactory()
        {
            Container
                .BindFactory<string, SceneObjectView, SceneObjectView.Factory>()
                .FromFactory<PrefabResourceFactory<SceneObjectView>>();
            
            Container.BindInterfacesTo<GameplayViewsFactory>().AsSingle();
        }

        private void BindTimeService() => 
            Container.BindInterfacesTo<TimeService>().AsSingle();

        private void BindInputService() => 
            Container.BindInterfacesTo<InputService>().AsSingle();

        private void BindGameplayEngine()
        {
            Container
                .Bind<IGameplayEngine>()
                .FromSubContainerResolve()
                .ByInstaller<GameplayEngineInstaller>()
                .WithKernel()
                .AsSingle();
        }

        private void BindGameBootstraperFactory()
        {
            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstraper);
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
        }

        private void BindSceneLoader() => 
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

        private void BindLoadingCurtain()
        {
            Container
                .Bind<ILoadingCurtain>()
                .To<LoadingCurtain>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CurtainPath)
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind(typeof(IGameStateMachine), typeof(ITickable))
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .WithKernel()
                .AsSingle();
        }
    }
}