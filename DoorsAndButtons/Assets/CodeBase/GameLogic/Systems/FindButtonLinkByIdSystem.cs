using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using Zenject;

// System resolve dependencies to buttons for entities that have ButtonLinkRequest component.
// System find specific button and store link on it in ButtonLink component.
public class FindButtonLinkByIdSystem : IEcsInitSystem, IEcsRunSystem
{
    EcsWorld world;

    EcsFilter requestsFilter;
    EcsFilter buttonsFilter;

    [Inject] EcsPool<ID> IdsPool;
    [Inject] EcsPool<ButtonLinkRequest> requestsPool;
    [Inject] EcsPool<ButtonLink> linksPool;

    public void Init(IEcsSystems systems)
    {
        world = systems.GetWorld();

        requestsFilter = world.Filter<ButtonLinkRequest>().Exc<ButtonLink>().End();
        buttonsFilter = world.Filter<ID>().Inc<Button>().End();

        //requestsPool = world.GetPool<ButtonLinkRequest>();
        //linksPool = world.GetPool<ButtonLink>();
        //IdsPool = world.GetPool<ID>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach(var request in requestsFilter)
        {
            int targetID = requestsPool.Get(request).ButtonID;

            foreach (var button in buttonsFilter)
            {
                if(IdsPool.Get(button).Value == targetID)
                {
                    ref var link = ref linksPool.Add(request);
                    link.Value = world.PackEntity(button);
                }
            }
        }
    }
}
