﻿using CodeBase.GameLogic.Components.Actors;
using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.Components.Movement;
using CodeBase.GameLogic.LeoEcs;
using CodeBase.UnityRelatedScripts.ViewFactories;

namespace CodeBase.GameLogic.Systems.ViewFeature
{
    // System demonstrate how we can create views for entities
    public class CreatePlayerSceneViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private EcsFilter actorsWithoutViewsFilter;

        private EcsPool<View> viewPool;
        private EcsPool<Position> positionPool;

        private IGameplayViewsFactory viewsFactory;

        public CreatePlayerSceneViewSystem(IGameplayViewsFactory viewsFactory) => 
            this.viewsFactory = viewsFactory;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();
            
            actorsWithoutViewsFilter = world.Filter<Actor>().Inc<Position>().Exc<View>().End();

            viewPool = world.GetPool<View>();
            positionPool = world.GetPool<Position>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var actor in actorsWithoutViewsFilter)
            {
                var position = positionPool.Get(actor).Value;
                
                ref var view = ref viewPool.Add(actor);
                view.Value = viewsFactory.CreatePlayer(position);
            }
        }
    }
}