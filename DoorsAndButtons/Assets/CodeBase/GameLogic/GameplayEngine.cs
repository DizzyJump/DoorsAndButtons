using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Zenject;

public class GameplayEngine
{
    EcsWorld world;
    EcsSystems systems;
    SharedData sharedData;
    bool server;

    public GameplayEngine(bool server)
    {
        sharedData = new SharedData();
        world = new EcsWorld();
        this.server = server;
    }

    [Inject]
    public void Init(LevelConfig levelConfig,
        UserInputRequestProcessingSystem userInputRequestProcessingSystem,
        UpdateMovingSystem updateMovingSystem,
        CheckButtonLeaveSystem checkButtonLeaveSystem)
    {
        CreateBindingsForPools(world, container);
        CreateBindingForSharedData(container);

        systems = new EcsSystems(world, sharedData);

        systems
            .Add(userInputRequestProcessingSystem)
            .Add(updateMovingSystem)
            .Add(checkButtonLeaveSystem)
            .Add(container.Instantiate<CheckButtonEnterSystem>())
            .Add(container.Instantiate<FindButtonLinkByIdSystem>())
            .Add(container.Instantiate<UpdateDoorStateByButtonSystem>())
            .Add(container.Instantiate<UpdateDoorMovingByDoorStateSystem>());

        if(!server)
        {
            systems
            #if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
            #endif
                .Add(container.Instantiate<UpdateViewPositionSystem>());
        }

        systems.Init();

        CreateLevelFromConfig(levelConfig);
    }

    private void CreateBindingForSharedData(DiContainer container)
    {
        container.Bind<SharedData>().FromInstance(sharedData);
    }

    void CreateBindingsForPools(EcsWorld world, DiContainer container)
    {
        container.Bind<EcsPool<MovementRequest>>().FromInstance(world.GetPool<MovementRequest>());
        container.Bind<EcsPool<MoveTo>>().FromInstance(world.GetPool<MoveTo>());
        container.Bind<EcsPool<Position>>().FromInstance(world.GetPool<Position>());
        container.Bind<EcsPool<MovementSpeed>>().FromInstance(world.GetPool<MovementSpeed>());
        container.Bind<EcsPool<Radius>>().FromInstance(world.GetPool<Radius>());
        container.Bind<EcsPool<Activated>>().FromInstance(world.GetPool<Activated>());
        container.Bind<EcsPool<ID>>().FromInstance(world.GetPool<ID>());
        container.Bind<EcsPool<ButtonLinkRequest>>().FromInstance(world.GetPool<ButtonLinkRequest>());
        container.Bind<EcsPool<ButtonLink>>().FromInstance(world.GetPool<ButtonLink>());
        container.Bind<EcsPool<DoorSettings>>().FromInstance(world.GetPool<DoorSettings>());
        container.Bind<EcsPool<View>>().FromInstance(world.GetPool<View>());
    }

    public void Update(float dt)
    {
        sharedData.deltaTime = dt;
        systems?.Run();
    }

    public void SetInput(float3 userChoosePosition)
    {
        if(world!=null)
        {
            var requestEntity = world.NewEntity();
            var pool = world.GetPool<MovementRequest>();
            ref var request = ref pool.Add(requestEntity);
            request.Value = userChoosePosition;
        }
    }

    public void Shootdown()
    {
        if(systems != null)
        {
            systems.Destroy();
            systems = null;
        }

        if(world != null)
        {
            world.Destroy();
            world = null;
        }    
    }

    void CreateLevelFromConfig(LevelConfig config)
    {
        foreach(var actor in config.Actors)
            ActorsFactory.CreateActor(world, actor);

        foreach(var button in config.Buttons)
            ButtonsFactory.CreateButton(world, button);

        foreach (var door in config.Doors)
            DoorsFactory.CreateDoor(world, door);
    }
}
