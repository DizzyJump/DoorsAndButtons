using UnityEngine;
using Zenject;

public class GameLogicEngineInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameLogicEngine>().AsSingle().WithArguments(false);
    }
}