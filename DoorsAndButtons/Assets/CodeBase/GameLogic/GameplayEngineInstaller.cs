using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

public class GameplayEngineInstaller : Installer<GameplayEngineInstaller>
{
    public override void InstallBindings()
    {
        BindEcsSystems();
        
        Container.Bind<GameplayEngine>().AsTransient().WithArguments(false);
    }

    private void BindEcsSystems()
    {
        BindSystem<CheckButtonEnterSystem>();
        BindSystem<CheckButtonLeaveSystem>();
        BindSystem<FindButtonLinkByIdSystem>();
        BindSystem<UpdateDoorMovingByDoorStateSystem>();
        BindSystem<UpdateDoorStateByButtonSystem>();
        BindSystem<UpdateMovingSystem>();
        BindSystem<UpdateViewPositionSystem>();
        BindSystem<UserInputRequestProcessingSystem>();
    }

    void BindSystem<TSystem>()
    {
        Container.BindInterfacesTo<TSystem>().AsTransient();
    }
}