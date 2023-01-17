using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Components.Input;
using CodeBase.GameLogic.Components.Lifecycle;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.Factories;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;
using UnityEngine.Assertions;
using Zenject;

namespace CodeBase.GameLogic
{
    // Gameplay engine is black-box abstraction that implement gameplay model in ecs-way.
    // Gameplay engine is designed to be possible use it in client and server side with different
    // architecture and infrastructure implementations
    public class GameplayModeService : IGameplayModeService
    {
        private EcsWorld world;
        private EcsSystems systems;
        private IEnumerable<IEcsSystem> bindedSystems;
        private LevelConfig levelConfig;

        private EcsFilter resultFilter;
        
        public GameplayModeService(IEnumerable<IEcsSystem> bindedSystems)
        {
            this.bindedSystems = bindedSystems;
        }

        public void InitializeGameplaySession(LevelConfig levelConfig)
        {
            Initialize();
            CreateLevelFromConfig(levelConfig);
        }

        public void Tick() => 
            systems?.Run();

        private void Initialize()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            InitializeSystems();

            CreateResultFilter();
        }

        private void CreateResultFilter() => 
            resultFilter = world.Filter<SessionResultEvent>().End();

        private void InitializeSystems()
        {
            foreach (var system in bindedSystems)
                systems.Add(system);

            systems.Init();
        }

        public bool IsSessionEnd => resultFilter.GetEntitiesCount() > 0;

        public GameplaySessionResult GetSessionResult()
        {
            EcsPool<SessionResultEvent> pool = world.GetPool<SessionResultEvent>();
            foreach (var result in resultFilter)
                return pool.Get(result).Value;
            return null;
        }
        
        public void Cleanup()
        {
            if(systems != null)
            {
                systems.Destroy();
                systems = null;
            }

            if(world != null)
            {
                world.Destroy();
                world = null;
                resultFilter = null;
            }    
        }

        void CreateLevelFromConfig(LevelConfig config) => 
            CreateLevelRequestFactory.Create(world, config);
    }
}