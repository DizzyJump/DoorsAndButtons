using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Zenject;

public class GameplayEngine : IGameplayEngine
{
    EcsWorld world;
    EcsSystems systems;
    SharedData sharedData;

    public GameplayEngine()
    {
        sharedData = new SharedData();
        world = new EcsWorld();
    }

    [Inject]
    public void Construct(LevelConfig levelConfig,
        IEnumerable<IEcsSystem> bindedSystems)
    {
        systems = new EcsSystems(world, sharedData);

        foreach (var system in bindedSystems)
            systems.Add(system);

        systems.Init();

        //CreateLevelFromConfig(levelConfig);
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
