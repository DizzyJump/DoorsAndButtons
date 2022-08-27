using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

public class ActorsFactory
{
    public static int CreateActor(EcsWorld world, Vector3 position, float speed, float radius, bool inputListener, ISceneObjectView view)
    {
        var entity = world.NewEntity();

        var actorsPool = world.GetPool<Actor>();
        var positionPool = world.GetPool<Position>();
        var movementSpeedPool = world.GetPool<MovementSpeed>();
        var inputListenersPool = world.GetPool<InputListener>();
        var radiusPool = world.GetPool<Radius>();
        var viewsPool = world.GetPool<View>();

        actorsPool.Add(entity);

        var positionComponent = positionPool.Add(entity);
        positionComponent.Value = position;

        var speedComponent = movementSpeedPool.Add(entity);
        speedComponent.Value = speed;

        var radiusComponent = radiusPool.Add(entity);
        radiusComponent.Value = radius;

        if(inputListener)
            inputListenersPool.Add(entity);

        if(view!=null)
        {
            var viewComponent = viewsPool.Add(entity);
            viewComponent.Value = view;
        }    
        return entity;
    }
}
