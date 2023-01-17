using CodeBase.GameLogic.Components.Buttons;
using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.LeoEcs;

// System set activity state of doors according to activity state of linked button
namespace CodeBase.GameLogic.Systems.ButtonsFeature
{
    public class UpdateTriggerStateByButtonSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsWorld world;

        EcsFilter activatedTriggerFilter;
        EcsFilter deactivatedTriggersFilter;

        EcsPool<Activated> activatedPool;
        EcsPool<ButtonLink> linksPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            activatedTriggerFilter = world.Filter<ButtonTrigger>().Inc<ButtonLink>().Inc<Activated>().End();
            deactivatedTriggersFilter = world.Filter<ButtonTrigger>().Inc<ButtonLink>().Exc<Activated>().End();

            activatedPool = world.GetPool<Activated>();
            linksPool = world.GetPool<ButtonLink>();
        }

        public void Run(IEcsSystems systems)
        {
            CheckActivatedTriggers();
            CheckDeactivatedTriggers();
        }

        private void CheckDeactivatedTriggers()
        {
            foreach (var trigger in deactivatedTriggersFilter)
            {
                var link = linksPool.Get(trigger).Value;
                if (link.Unpack(world, out var buttonEntity))
                {
                    bool isButtonActivated = activatedPool.Has(buttonEntity);
                    // if button is activated then activate trigger too
                    if (isButtonActivated)
                        activatedPool.Add(trigger);
                }
                else
                    linksPool.Del(trigger); // brocken link - try to refresh
            }
        }

        private void CheckActivatedTriggers()
        {
            foreach (var trigger in activatedTriggerFilter)
            {
                var link = linksPool.Get(trigger).Value;
                if (link.Unpack(world, out var buttonEntity))
                {
                    bool isButtonActivated = activatedPool.Has(buttonEntity);
                    // if button is not activated then deactivate trigger too
                    if (!isButtonActivated)
                        activatedPool.Del(trigger);
                }
                else
                    linksPool.Del(trigger); // brocken link - try to refresh
            }
        }
    }
}
