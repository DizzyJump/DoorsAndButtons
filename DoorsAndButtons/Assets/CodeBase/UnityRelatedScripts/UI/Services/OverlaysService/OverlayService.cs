using System.Threading.Tasks;

namespace CodeBase.UnityRelatedScripts.UI.Overlays.OverlaysService
{
    public class OverlayService : IOverlayService
    {
        private IUIFactory uiFactory;

        public OverlayService(IUIFactory uiFactory) => 
            this.uiFactory = uiFactory;


        public async Task ShowFinishLevelOverlay(string message)
        {
            IOverlay overlay = uiFactory.CreateFinishLevelOverlay(message);
            await ProcessOverlay(overlay);
        }

        private async Task ProcessOverlay(IOverlay overlay)
        {
            await overlay.Show();
            overlay.Destroy();
        }
    }
}