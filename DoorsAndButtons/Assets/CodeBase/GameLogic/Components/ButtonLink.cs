using CodeBase.GameLogic.LeoEcs;

// Store link to button. Add ButtonLinkRequest component to entity if you want to receive this link. It's resolve automaticaly.
namespace CodeBase.GameLogic.Components
{
    public struct ButtonLink
    {
        public EcsPackedEntity Value;
    }
}
