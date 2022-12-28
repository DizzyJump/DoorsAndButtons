using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Helpers;
using CodeBase.GameLogic.LeoEcs;
using CodeBase.Services.TimeService;
using Unity.Mathematics;

// Update Position components on entities if they have MoveTo component
namespace CodeBase.GameLogic.Systems
{
    public class UpdateMovingSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter filter;
        
        EcsPool<MoveTo> moveToPool;
        EcsPool<Position> positionPool;
        EcsPool<MovementSpeed> speedPool;

        private ITimeService timeService;

        public UpdateMovingSystem(ITimeService timeService) => 
            this.timeService = timeService;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            filter = world.Filter<MoveTo>().Inc<Position>().Inc<MovementSpeed>().End();

            moveToPool = world.GetPool<MoveTo>();
            positionPool = world.GetPool<Position>();
            speedPool = world.GetPool<MovementSpeed>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in filter)
            {
                var moveToPosition = moveToPool.Get(entity).Value;
                var speed = speedPool.Get(entity).Value;
                ref var position = ref positionPool.Get(entity);

                position.Value = MathHelpers.MoveTowards(position.Value, moveToPosition, speed * timeService.DeltaTime);
                
                CheckIsArrive(ref position, moveToPosition, entity);
            }
        }

        private void CheckIsArrive(ref Position position, float3 moveToPosition, int entity)
        {
            if (math.lengthsq(position.Value - moveToPosition) < float.Epsilon)
            {
                position.Value = moveToPosition;
                moveToPool.Del(entity);
            }
        }
    }
}
