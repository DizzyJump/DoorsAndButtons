using CodeBase.GameLogic.Components;
using CodeBase.GameLogic.LeoEcs;

// System set activity state of doors according to activity state of linked button
namespace CodeBase.GameLogic.Systems
{
    public class UpdateDoorStateByButtonSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsWorld world;

        EcsFilter activatedDoorsFilter;
        EcsFilter deactivatedDoorsFilter;

        EcsPool<Activated> activatedPool;
        EcsPool<ButtonLink> linksPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            activatedDoorsFilter = world.Filter<Door>().Inc<ButtonLink>().Inc<Activated>().End();
            deactivatedDoorsFilter = world.Filter<Door>().Inc<ButtonLink>().Exc<Activated>().End();

            activatedPool = world.GetPool<Activated>();
            linksPool = world.GetPool<ButtonLink>();
        }

        public void Run(IEcsSystems systems)
        {
            CheckActivatedDoors();
            CheckDeactivatedDoors();
        }

        private void CheckDeactivatedDoors()
        {
            foreach (var door in deactivatedDoorsFilter)
            {
                var link = linksPool.Get(door).Value;
                if (link.Unpack(world, out var buttonEntity))
                {
                    bool isButtonActivated = activatedPool.Has(buttonEntity);
                    // if button is activated then activate door too
                    if (isButtonActivated)
                        activatedPool.Add(door);
                }
                else
                    linksPool.Del(door); // brocken link - try to refresh
            }
        }

        private void CheckActivatedDoors()
        {
            foreach (var door in activatedDoorsFilter)
            {
                var link = linksPool.Get(door).Value;
                if (link.Unpack(world, out var buttonEntity))
                {
                    bool isButtonActivated = activatedPool.Has(buttonEntity);
                    // if button is not activated then deactivate door too
                    if (!isButtonActivated)
                        activatedPool.Del(door);
                }
                else
                    linksPool.Del(door); // brocken link - try to refresh
            }
        }
    }
}
