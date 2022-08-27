using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

public class ActorsFactory
{
    public static int CreateActor(EcsWorld world, LevelConfig.Actor actorConfig)
    {
        return CreateActor(world, 
            actorConfig.Position,
            actorConfig.MovementSpeed,
            actorConfig.ListenInput, 
            actorConfig.View);
    }

    public static int CreateActor(EcsWorld world, Vector3 position, float speed, bool inputListener, ISceneObjectView view)
    {
        var entity = world.NewEntity();

        var actorsPool = world.GetPool<Actor>();
        var positionPool = world.GetPool<Position>();
        var movementSpeedPool = world.GetPool<MovementSpeed>();
        var inputListenersPool = world.GetPool<InputListener>();
        var viewsPool = world.GetPool<View>();
        var buttonInteractionPool = world.GetPool<CanInteractWithButtons>();

        actorsPool.Add(entity);

        ref var positionComponent = ref positionPool.Add(entity);
        positionComponent.Value = position;

        ref var speedComponent = ref movementSpeedPool.Add(entity);
        speedComponent.Value = speed;

        if(inputListener)
            inputListenersPool.Add(entity);

        if(view!=null)
        {
            ref var viewComponent = ref viewsPool.Add(entity);
            viewComponent.Value = view;
        }

        buttonInteractionPool.Add(entity);

        return entity;
    }
}
