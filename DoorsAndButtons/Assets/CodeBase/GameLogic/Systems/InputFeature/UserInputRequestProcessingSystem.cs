using CodeBase.GameLogic.Components.Input;
using CodeBase.GameLogic.Components.Movement;
using CodeBase.GameLogic.Helpers;
using CodeBase.GameLogic.LeoEcs;

// System collect user input requests and update MoveTo component
// on entities marked by InputListener components according this requests.
namespace CodeBase.GameLogic.Systems.InputFeature
{
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
                    ref MoveTo moveTo = ref EcsHelpers.GetOrAddComponent(listener, moveToPool);
                    moveTo.Value = moveToPosition;
                }
                requestsPool.Del(requst);
            }
        }
    }
}
