using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.Components.Lifecycle;
using CodeBase.GameLogic.LeoEcs;
using CodeBase.Services.TimeService;
using Zenject;

namespace CodeBase.GameLogic.Systems.Lifecycle
{
    public class CheckLevelSuccessTriggerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter activatedTriggersFilter;
        private EcsPool<LevelSuccessOnActivatedTrigger> triggerPool;
        private EcsPool<SessionResultEvent> resultPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            activatedTriggersFilter = world
                .Filter<LevelSuccessOnActivatedTrigger>()
                .Inc<Activated>()
                .Exc<SessionResultEvent>()
                .End();

            triggerPool = world.GetPool<LevelSuccessOnActivatedTrigger>();
            resultPool = world.GetPool<SessionResultEvent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var trigger in activatedTriggersFilter)   
            {
                triggerPool.Del(trigger);
                ref var result = ref resultPool.Add(trigger);
                result.Value = new GameplaySessionResult() { Win = true};
            }
        }
    }
}