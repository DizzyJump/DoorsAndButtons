using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Zenject;

// System update MoveTo component on Doors according to they activity state
public class UpdateDoorMovingByDoorStateSystem : IEcsInitSystem, IEcsRunSystem
{
EcsWorld world;

    EcsFilter activatedDoorsFilter;
    EcsFilter deactivatedDoorsFilter;

    [Inject] EcsPool<MoveTo> moveToPool;
    [Inject] EcsPool<Position> positionPool;
    [Inject] EcsPool<DoorSettings> doorSettingsPool;

    public void Init(IEcsSystems systems)
    {
        world = systems.GetWorld();

        activatedDoorsFilter = world.Filter<Door>().Inc<DoorSettings>().Inc<Position>().Inc<Activated>().End();
        deactivatedDoorsFilter = world.Filter<Door>().Inc<DoorSettings>().Inc<Position>().Exc<Activated>().End();

        //moveToPool = world.GetPool<MoveTo>();
        //doorSettingsPool = world.GetPool<DoorSettings>();
        //positionPool = world.GetPool<Position>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach(var door in activatedDoorsFilter)
        {
            var targetPosition = doorSettingsPool.Get(door).OpenPosition;
            SetMoveTo(targetPosition, door);
        }

        foreach (var door in deactivatedDoorsFilter)
        {
            var targetPosition = doorSettingsPool.Get(door).ClosedPosition;
            SetMoveTo(targetPosition, door);
        }
    }

    void SetMoveTo(float3 targetPosition, int door)
    {
        var currentPosition = positionPool.Get(door).Value;
        bool isCorrectPosition = math.lengthsq(targetPosition - currentPosition) <= float.Epsilon;
        if (isCorrectPosition)
            return;

        if (moveToPool.Has(door))
        {
            ref var moveTo = ref moveToPool.Get(door);
            moveTo.Value = targetPosition;
        }
        else
        {
            ref var moveTo = ref moveToPool.Add(door);
            moveTo.Value = targetPosition;
        }
    }
}
