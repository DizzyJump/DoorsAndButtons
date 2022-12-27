using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

public class GameplayEngineInstaller : Installer<GameplayEngineInstaller>
{
    public override void InstallBindings()
    {
        BindEcsSystems();
        
        Container.Bind<GameplayEngine>().AsTransient();
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
        BindSystem<Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem>();
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
    }

    void BindSystem<TSystem>() where TSystem : IEcsSystem
    {
        Container.Bind<IEcsSystem>().To<TSystem>().AsTransient();
    }
}