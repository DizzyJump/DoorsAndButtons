using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsFactory
{
    public static int CreateButton(EcsWorld world, LevelConfig.ButtonConfig config)
    {
        return CreateButton(world, config.Position, config.Radius, config.View);
    }

    public static int CreateButton(EcsWorld world, Vector3 position, float radius, ISceneObjectView view)
    {
        var entity = world.NewEntity();

        var buttonsPool = world.GetPool<Button>();
        var positionPool = world.GetPool<Position>();
        var radiusPool = world.GetPool<Radius>();
        var viewsPool = world.GetPool<View>();

        buttonsPool.Add(entity);

        ref var positionComponent = ref positionPool.Add(entity);
        positionComponent.Value = position;

        ref var radiusComponent = ref radiusPool.Add(entity);
        radiusComponent.Value = radius;

        if (view != null)
        {
            ref var viewComponent = ref viewsPool.Add(entity);
            viewComponent.Value = view;
        }

        return entity;
    }
}
