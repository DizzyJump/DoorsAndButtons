using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Zenject;

// System set activity state of doors according to activity state of linked button
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
        foreach(var door in activatedDoorsFilter)
        {
            var link = linksPool.Get(door).Value;
            int buttonEntity;
            if (link.Unpack(world, out buttonEntity))
            {
                bool isButtonActivated = activatedPool.Has(buttonEntity);
                // if button is not activated then deactivate door too
                if (!isButtonActivated)
                    activatedPool.Del(door);
            }
            else
                linksPool.Del(door); // brocken link - try to refresh
        }

        foreach (var door in deactivatedDoorsFilter)
        {
            var link = linksPool.Get(door).Value;
            int buttonEntity;
            if (link.Unpack(world, out buttonEntity))
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
}
