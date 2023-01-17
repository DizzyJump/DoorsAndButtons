using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Components.Requests;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.LeoEcs;

namespace CodeBase.GameLogic.Factories
{
    public class CreateLevelRequestFactory
    {
        public static int Create(EcsWorld world, LevelConfig config)
        {
            var entity = world.NewEntity();

            var reqeustsPool = world.GetPool<CreateLevelRequest>();

            ref var requestComponent = ref reqeustsPool.Add(entity);
            requestComponent.Config = config;

            return entity;
        }
    }
}