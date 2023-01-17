using CodeBase.GameLogic.LeoEcs;

namespace CodeBase.GameLogic.Components.Buttons
{
    // Store link to button.
    // Add ButtonLinkRequest component to entity if you want to receive this link. It's resolve automaticaly.
    public struct ButtonLink
    {
        public EcsPackedEntity Value;
    }
}
