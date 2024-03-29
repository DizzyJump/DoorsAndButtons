using CodeBase.GameLogic.Components.Buttons;
using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.Components.Movement;
using CodeBase.GameLogic.LeoEcs;
using Unity.Mathematics;

// System check are ALL entities with ability to interact are far enough
// from activated button and mark that button as deactivated by removing Activated component
namespace CodeBase.GameLogic.Systems.ButtonsFeature
{
    public class CheckButtonLeaveSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsFilter actorsFilter;
        EcsFilter buttonsFilter;

        EcsPool<Position> positionPool; 
        EcsPool<Size> radiusPool;
        EcsPool<Activated> activatedPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            actorsFilter = world.Filter<CanInteractWithButtons>().Inc<Position>().End();
            buttonsFilter = world.Filter<Button>().Inc<Position>().Inc<Size>().Inc<Activated>().End();

            positionPool = world.GetPool<Position>();
            radiusPool = world.GetPool<Size>();
            activatedPool = world.GetPool<Activated>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var button in buttonsFilter)
            {
                bool nobodyInside = true;
                foreach (var actor in actorsFilter)
                {
                    var buttonPosition = positionPool.Get(button).Value;
                    var actorPosition = positionPool.Get(actor).Value;
                    var buttonRadius = radiusPool.Get(button).Value;
                    
                    if (math.lengthsq(buttonPosition - actorPosition) <= (buttonRadius * buttonRadius))
                    {
                        nobodyInside = false;
                        break;
                    }
                }
                if(nobodyInside) 
                    DeactivateButton(button);
            } 
        }

        private void DeactivateButton(int button) => 
            activatedPool.Del(button);
    }
}
