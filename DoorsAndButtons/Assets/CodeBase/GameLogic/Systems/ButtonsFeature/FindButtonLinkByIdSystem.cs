using CodeBase.GameLogic.Components.Buttons;
using CodeBase.GameLogic.Components.Common;
using CodeBase.GameLogic.LeoEcs;

// System resolve dependencies to buttons for entities that have ButtonLinkRequest component.
// System find specific button and store link on it in ButtonLink component.
namespace CodeBase.GameLogic.Systems.ButtonsFeature
{
    public class FindButtonLinkByIdSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsWorld world;

        EcsFilter requestsFilter;
        EcsFilter buttonsFilter;

        EcsPool<GUID> IdsPool;
        EcsPool<ButtonLinkRequest> requestsPool;
        EcsPool<ButtonLink> linksPool;

        public void Init(IEcsSystems systems)
        {
            world = systems.GetWorld();

            requestsFilter = world.Filter<ButtonLinkRequest>().Exc<ButtonLink>().End();
            buttonsFilter = world.Filter<GUID>().Inc<Button>().End();

            requestsPool = world.GetPool<ButtonLinkRequest>();
            linksPool = world.GetPool<ButtonLink>();
            IdsPool = world.GetPool<GUID>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach(var request in requestsFilter)
            {
                string targetID = requestsPool.Get(request).ButtonID;

                TryFindButtonByID(targetID, request);
            }
        }

        private void TryFindButtonByID(string targetID, int request)
        {
            foreach (var button in buttonsFilter)
            {
                var buttonId = IdsPool.Get(button).Value;
                if (buttonId == targetID)
                {
                    SetupLink(request, button);
                }
            }
        }

        private void SetupLink(int request, int button)
        {
            ref var link = ref linksPool.Add(request);
            link.Value = world.PackEntity(button);
        }
    }
}
