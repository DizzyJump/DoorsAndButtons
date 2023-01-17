using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Components.Actors;
using CodeBase.GameLogic.Components.Buttons;
using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.Components.Input;
using CodeBase.GameLogic.Components.Movement;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.Interfaces;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;

namespace CodeBase.GameLogic.Factories
{
    public class ActorsFactory
    {
        public static int Create(EcsWorld world, LevelConfig.ActorConfig actorConfigConfig)
        {
            return Create(world, 
                actorConfigConfig.Position,
                actorConfigConfig.MovementSpeed,
                actorConfigConfig.ListenInput);
        }

        public static int Create(EcsWorld world, float3 position, float speed, bool inputListener)
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

            buttonInteractionPool.Add(entity);

            return entity;
        }
    }
}
