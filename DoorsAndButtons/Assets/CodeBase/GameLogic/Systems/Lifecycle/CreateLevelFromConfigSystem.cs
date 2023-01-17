using CodeBase.GameLogic.Components.Requests;
using CodeBase.GameLogic.Configs;
using CodeBase.GameLogic.Factories;
using CodeBase.GameLogic.LeoEcs;

namespace CodeBase.GameLogic.Systems.Lifecycle
{
    public class CreateLevelFromConfigSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        
        private EcsFilter requestsFilter;

        private EcsPool<CreateLevelRequest> requstsPool;
        
        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            
            requestsFilter = world.Filter<CreateLevelRequest>().End();
            
            requstsPool = world.GetPool<CreateLevelRequest>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var request in requestsFilter)
            {
                var config = requstsPool.Get(request).Config;
                CreateLevelFromConfig(config);
                DeleteRequest(request);
            }
        }

        private void DeleteRequest(int request) => 
            requstsPool.Del(request);

        void CreateLevelFromConfig(LevelConfig config)
        {
            // it could be decomposed easily without side effects on few systems if it become appropriate in future
            CreateActors(config);
            CreateButtons(config);
            CreateDoors(config);
            CreateTriggers(config);
        }

        private void CreateTriggers(LevelConfig config)
        {
            foreach (var trigger in config.ButtonTriggers) 
                TriggersFactory.Create(world, trigger);
        }

        private void CreateDoors(LevelConfig config)
        {
            foreach (var door in config.Doors)
                DoorsFactory.Create(world, door);
        }

        private void CreateButtons(LevelConfig config)
        {
            foreach (var button in config.Buttons)
                ButtonsFactory.Create(world, button);
        }

        private void CreateActors(LevelConfig config)
        {
            foreach (var actor in config.Actors)
                ActorsFactory.Create(world, actor);
        }
    }
}