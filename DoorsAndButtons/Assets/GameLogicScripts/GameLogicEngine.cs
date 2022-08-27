using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicEngine
{
    EcsWorld world;
    EcsSystems systems;
    SharedData sharedData;

    public void Init(LevelConfig levelConfig)
    {
        sharedData = new SharedData();
        world = new EcsWorld();
        systems = new EcsSystems(world, sharedData);

        systems
            .Add(new UserInputRequestProcessingSystem())
            .Add(new UpdateMovingSystem())
            .Add(new CheckButtonLeaveSystem())
            .Add(new CheckButtonEnterSystem())
            .Add(new FindButtonLinkByIdSystem())
            .Add(new UpdateDoorStateByButtonSystem())
            .Add(new UpdateViewPositionSystem())
#if UNITY_EDITOR
        // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
        // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
        .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
        .Init();

        CreateLevelFromConfig(levelConfig);
    }

    public void Update(float dt)
    {
        sharedData.deltaTime = dt;
        systems?.Run();
    }

    public void SetInput(Vector3 userChoosePosition)
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
