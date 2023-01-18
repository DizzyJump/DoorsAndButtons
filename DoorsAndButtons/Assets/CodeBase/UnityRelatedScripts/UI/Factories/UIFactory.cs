using CodeBase.UnityRelatedScripts.UI.Overlays.FinishLevelOverlay;
using Zenject;

namespace CodeBase.UnityRelatedScripts.UI
{
    public class UIFactory : IUIFactory
    {
        private FinishLevelOverlay.Factory finishLevelOverlayFactory;

        public UIFactory(FinishLevelOverlay.Factory finishLevelOverlayFactory) => 
            this.finishLevelOverlayFactory = finishLevelOverlayFactory;

        public Overlay CreateFinishLevelOverlay(string msg) => 
            finishLevelOverlayFactory.Create(msg);
    }
}