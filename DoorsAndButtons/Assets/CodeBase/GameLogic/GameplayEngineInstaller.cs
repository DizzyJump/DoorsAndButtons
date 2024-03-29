using CodeBase.GameLogic.LeoEcs;
using CodeBase.GameLogic.Systems;
using CodeBase.GameLogic.Systems.ButtonsFeature;
using CodeBase.GameLogic.Systems.DoorsFeature;
using CodeBase.GameLogic.Systems.InputFeature;
using CodeBase.GameLogic.Systems.Lifecycle;
using CodeBase.GameLogic.Systems.MovementFeature;
using CodeBase.GameLogic.Systems.ViewFeature;
using CodeBase.UnityRelatedScripts.LeoEcsEditor.Runtime;
using Zenject;

namespace CodeBase.GameLogic
{
    public class GameplayEngineInstaller : Installer<GameplayEngineInstaller>
    {
        public override void InstallBindings()
        {
            BindEcsSystems();
        
            Container.BindInterfacesTo<GameplayModeService>().AsSingle();
        }

        private void BindEcsSystems()
        {
            BindCommonSystems();
        
#if !SERVER_BUILD
            BindClientSystems();
#endif
        
#if UNITY_EDITOR
            //BindDebugSystems();
#endif
        }

        private void BindDebugSystems()
        {
            BindSystem<EcsWorldDebugSystem>();
        }

        private void BindClientSystems()
        {
            BindSystem<CreatePlayerSceneViewSystem>();
            BindSystem<UpdateViewPositionSystem>();
        }

        private void BindCommonSystems()
        {
            BindSystem<CheckButtonEnterSystem>();
            BindSystem<CheckButtonLeaveSystem>();
            BindSystem<FindButtonLinkByIdSystem>();
            BindSystem<UpdateDoorMovingByDoorStateSystem>();
            BindSystem<UpdateTriggerStateByButtonSystem>();
            BindSystem<UpdateMovingSystem>();
            BindSystem<UserInputRequestProcessingSystem>();
            BindSystem<CreateLevelFromConfigSystem>();
            BindSystem<CheckUserInputSystem>();
            BindSystem<CheckLevelFailedTriggerSystem>();
            BindSystem<CheckLevelSuccessTriggerSystem>();
        }

        void BindSystem<TSystem>() where TSystem : IEcsSystem
        {
            Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();
        }
    }
}