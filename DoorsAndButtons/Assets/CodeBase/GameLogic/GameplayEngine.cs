using System.Collections;
using System.Collections.Generic;
using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.Factories;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;
using Zenject;

namespace CodeBase.GameLogic
{
    public class GameplayEngine : IGameplayEngine
    {
        private EcsWorld world;
        private EcsSystems systems;
        private IEnumerable<IEcsSystem> bindedSystems;
        private LevelConfig levelConfig;

        public GameplayEngine(IEnumerable<IEcsSystem> bindedSystems)
        {
            this.bindedSystems = bindedSystems;
        }

        public void InitializeLevel(LevelConfig levelConfig)
        {
            Initialize();
            CreateLevelFromConfig(levelConfig);
        }

        public void Update(float dt) => 
            systems?.Run();

        private void Initialize()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            InitializeSystems();
        }

        private void InitializeSystems()
        {
            foreach (var system in bindedSystems)
                systems.Add(system);

            systems.Init();
        }

        public void SetInput(float3 userChoosePosition)
        {
            if(world!=null)
            {
                var requestEntity = world.NewEntity();
                var pool = world.GetPool<MovementRequest>();
                ref var request = ref pool.Add(requestEntity);
                request.Value = userChoosePosition;
            }
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
            }    
        }

        void CreateLevelFromConfig(LevelConfig config) => 
            CreateLevelRequestFactory.Create(world, config);
    }
}