using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

public class UserInputRequestProcessingSystem : IEcsInitSystem, IEcsRunSystem
{
    EcsFilter listenersFilter;
    EcsFilter requestsFilter;

    EcsPool<MovementRequest> requestsPool;
    EcsPool<MoveTo> moveToPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        listenersFilter = world.Filter<InputListener>().End();
        requestsFilter = world.Filter<MovementRequest>().End();
        requestsPool = world.GetPool<MovementRequest>();
        moveToPool = world.GetPool<MoveTo>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach(var requst in requestsFilter)
        {
            var moveToPosition = requestsPool.Get(requst).Value;
            foreach(var listener in listenersFilter)
            {
                if(moveToPool.Has(listener))
                {
                    ref var moveTo = ref moveToPool.Get(listener);
                    moveTo.Value = moveToPosition;
                }
                else
                {
                    ref var moveTo = ref moveToPool.Add(listener);
                    moveTo.Value = moveToPosition;
                }
            }
            requestsPool.Del(requst);
        }
    }
}
