using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Components.Buttons;
using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.Components.Doors;
using CodeBase.GameLogic.Components.Movement;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.Interfaces;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;

namespace CodeBase.GameLogic.Factories
{
    public class DoorsFactory
    {
        public static int Create(EcsWorld world, LevelConfig.DoorConfig config)
        {
            return Create(world, config.ID, config.OpenPosition, config.ClosedPosition, config.MovingSpeed, config.ButtonId, config.View);
        }

        public static int Create(EcsWorld world, string id, float3 openPosition, float3 closedPosition, float speed, string buttonId, ISceneObjectView view)
        {
            var entity = world.NewEntity();

            var doorsPool = world.GetPool<Door>();
            var idPool = world.GetPool<GUID>();
            var buttonPool = world.GetPool<ButtonLinkRequest>();
            var positionPool = world.GetPool<Position>();
            var movementSpeedPool = world.GetPool<MovementSpeed>();
            var doorSettingsPool = world.GetPool<DoorSettings>();
            var viewsPool = world.GetPool<View>();
            var triggerPool = world.GetPool<ButtonTrigger>();

            doorsPool.Add(entity);

            ref var guid = ref idPool.Add(entity);
            guid.Value = id;

            ref var buttonIdComponent = ref buttonPool.Add(entity);
            buttonIdComponent.ButtonID = buttonId;

            ref var positionComponent = ref positionPool.Add(entity);
            positionComponent.Value = closedPosition;

            ref var speedComponent = ref movementSpeedPool.Add(entity);
            speedComponent.Value = speed;

            ref var doorSettings = ref doorSettingsPool.Add(entity);
            doorSettings.ClosedPosition = closedPosition;
            doorSettings.OpenPosition = openPosition;

            triggerPool.Add(entity);

            if (view != null)
            {
                ref var viewComponent = ref viewsPool.Add(entity);
                viewComponent.Value = view;
            }

            return entity;
        }
    }
}
