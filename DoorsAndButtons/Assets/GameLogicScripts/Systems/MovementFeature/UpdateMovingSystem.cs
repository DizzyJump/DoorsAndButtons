using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMovingSystem : IEcsInitSystem, IEcsRunSystem
{
    EcsFilter filter;
    EcsPool<MoveTo> moveToPool;
    EcsPool<Position> positionPool;
    EcsPool<MovementSpeed> speedPool;

    SharedData sharedData;
    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        filter = world.Filter<MoveTo>().Inc<Position>().Inc<MovementSpeed>().End();

        moveToPool = world.GetPool<MoveTo>();
        positionPool = world.GetPool<Position>();
        speedPool = world.GetPool<MovementSpeed>();

        sharedData = systems.GetShared<SharedData>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach(var entity in filter)
        {
            var moveToPosition = moveToPool.Get(entity).Value;
            var speed = speedPool.Get(entity).Value;
            ref var position = ref positionPool.Get(entity);

            position.Value = Vector3.MoveTowards(position.Value, moveToPosition, speed * sharedData.deltaTime);
            if((position.Value - moveToPosition).sqrMagnitude < float.Epsilon)
            {
                position.Value = moveToPosition;
                moveToPool.Del(entity);
            }
        }
    }
}