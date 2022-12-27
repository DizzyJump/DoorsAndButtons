using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class DoorsFactory
{
    public static int CreateDoor(EcsWorld world, LevelConfig.DoorConfig config)
    {
        return CreateDoor(world, config.OpenPosition, config.ClosedPosition, config.MovingSpeed, config.ButtonId, config.View);
    }

    public static int CreateDoor(EcsWorld world, float3 openPosition, float3 closedPosition, float speed, int button, ISceneObjectView view)
    {
        var entity = world.NewEntity();

        var doorsPool = world.GetPool<Door>();
        var buttonPool = world.GetPool<ButtonLinkRequest>();
        var positionPool = world.GetPool<Position>();
        var movementSpeedPool = world.GetPool<MovementSpeed>();
        var doorSettingsPool = world.GetPool<DoorSettings>();
        var viewsPool = world.GetPool<View>();

        doorsPool.Add(entity);

        ref var buttonIdComponent = ref buttonPool.Add(entity);
        buttonIdComponent.ButtonID = button;


        ref var positionComponent = ref positionPool.Add(entity);
        positionComponent.Value = closedPosition;

        ref var speedComponent = ref movementSpeedPool.Add(entity);
        speedComponent.Value = speed;

        ref var doorSettings = ref doorSettingsPool.Add(entity);
        doorSettings.ClosedPosition = closedPosition;
        doorSettings.OpenPosition = openPosition;

        if (view != null)
        {
            ref var viewComponent = ref viewsPool.Add(entity);
            viewComponent.Value = view;
        }

        return entity;
    }
}
