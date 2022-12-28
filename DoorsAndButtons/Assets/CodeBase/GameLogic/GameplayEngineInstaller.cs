using CodeBase.GameLogic.LeoEcs;
using CodeBase.GameLogic.Systems;
using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using Zenject;

namespace CodeBase.GameLogic
{
    public class GameplayEngineInstaller : Installer<GameplayEngineInstaller>
    {
        public override void InstallBindings()
        {
            BindEcsSystems();
        
            Container.Bind<IGameplayEngine>().To<GameplayEngine>().AsSingle();
        }

        private void BindEcsSystems()
        {
            BindCommonSystems();
        
#if !SERVER_BUILD
            BindClientSystems();
#endif
        
#if UNITY_EDITOR
            BindDebugSystems();
#endif
        }

        private void BindDebugSystems()
        {
            BindSystem<EcsWorldDebugSystem>();
        }

        private void BindClientSystems()
        {
            BindSystem<UpdateViewPositionSystem>();
        }

        private void BindCommonSystems()
        {
            BindSystem<CheckButtonEnterSystem>();
            BindSystem<CheckButtonLeaveSystem>();
            BindSystem<FindButtonLinkByIdSystem>();
            BindSystem<UpdateDoorMovingByDoorStateSystem>();
            BindSystem<UpdateDoorStateByButtonSystem>();
            BindSystem<UpdateMovingSystem>();
            BindSystem<UserInputRequestProcessingSystem>();
            BindSystem<CreateLevelFromConfigSystem>();
            BindSystem<CheckUserInputSystem>();
        }

        void BindSystem<TSystem>() where TSystem : IEcsSystem
        {
            Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();
        }
    }
}