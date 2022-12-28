using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;

// System check are ANY entity with ability to interact is near enough
// to non active button and mark that button as active by adding Activated component
namespace CodeBase.GameLogic.Systems
{
    public class CheckButtonEnterSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter actorsFilter;
        EcsFilter buttonsFilter;

        EcsPool<Position> positionPool;
        EcsPool<Radius> radiusPool;
        EcsPool<Activated> activatedPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            actorsFilter = world.Filter<CanInteractWithButtons>().Inc<Position>().End();
            buttonsFilter = world.Filter<Button>().Inc<Position>().Inc<Radius>().Exc<Activated>().End();

            positionPool = world.GetPool<Position>();
            radiusPool = world.GetPool<Radius>();
            activatedPool = world.GetPool<Activated>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach(var button in buttonsFilter)
                foreach(var actor in actorsFilter)
                {
                    var buttonPosition = positionPool.Get(button).Value;
                    var actorPosition = positionPool.Get(actor).Value;
                    var buttonRadius = radiusPool.Get(button).Value;
                    
                    if(math.lengthsq(buttonPosition-actorPosition) <= (buttonRadius * buttonRadius))
                    {
                        ActivateButton(button);
                        break;
                    }
                }
        }

        private void ActivateButton(int button) => 
            activatedPool.Add(button);
    }
}
