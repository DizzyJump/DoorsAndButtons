using CodeBase.GameLogic.LeoEcs;

namespace CodeBase.GameLogic.Helpers
{
    public class EcsHelpers
    {
        public static ref TComponent GetOrAddComponent<TComponent>(int entity, EcsPool<TComponent> pool) where TComponent:struct
        {
            if (pool.Has(entity))
                return ref pool.Get(entity);
            else
                return ref pool.Add(entity);
        }
    }
}