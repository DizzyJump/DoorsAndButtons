using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Components.Buttons;
using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.Components.Movement;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.Interfaces;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;

namespace CodeBase.GameLogic.Factories
{
    public class ButtonsFactory
    {
        public static int Create(EcsWorld world, LevelConfig.ButtonConfig config)
        {
            return Create(world, config.ID, config.Position, config.Radius, config.View);
        }

        public static int Create(EcsWorld world, string id, float3 position, float radius, ISceneObjectView view)
        {
            var entity = world.NewEntity();

            var buttonsPool = world.GetPool<Button>();
            var IdsPool = world.GetPool<GUID>();
            var positionPool = world.GetPool<Position>();
            var radiusPool = world.GetPool<Size>();
            var viewsPool = world.GetPool<View>();

            buttonsPool.Add(entity);

            ref var IdComponent = ref IdsPool.Add(entity);
            IdComponent.Value = id;

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
}
