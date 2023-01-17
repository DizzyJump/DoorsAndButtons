using CodeBase.GameLogic.Interfaces;
using CodeBase.GameLogic.LeoEcs;

namespace CodeBase.GameLogic.Components.Common
{
    // Store link to the "view" of entity.
    public struct View : IEcsAutoReset<View>
    {
        public ISceneObjectView Value;
        public void AutoReset(ref View c) => 
            c.Value?.DestroyView();
    }
}
