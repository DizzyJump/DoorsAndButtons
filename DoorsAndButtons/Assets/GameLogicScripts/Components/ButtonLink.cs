using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;

// Store link to button. Add ButtonLinkRequest component to entity if you want to receive this link. It's resolve automaticaly.
public struct ButtonLink
{
    public EcsPackedEntity Value;
}
