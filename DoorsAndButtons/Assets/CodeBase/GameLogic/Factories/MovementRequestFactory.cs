using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Components.Input;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;

namespace CodeBase.GameLogic.Factories
{
    public class MovementRequestFactory
    {
        public static int Create(EcsWorld world, float3 moveToPosition)
        {
            var requestEntity = world.NewEntity();
            
            var pool = world.GetPool<MovementRequest>();
            
            ref var request = ref pool.Add(requestEntity);
            request.Value = moveToPosition;

            return requestEntity;
        }
    }
}