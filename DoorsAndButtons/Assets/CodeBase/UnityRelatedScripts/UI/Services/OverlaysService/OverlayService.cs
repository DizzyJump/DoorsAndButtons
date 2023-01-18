using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UnityRelatedScripts.UI.Overlays.OverlaysService
{
    public class OverlayService : IOverlayService
    {
        private IUIFactory uiFactory;

        public OverlayService(IUIFactory uiFactory) => 
            this.uiFactory = uiFactory;


        public async Task ShowFinishLevelOverlay(string message)
        {
            Overlay overlay = uiFactory.CreateFinishLevelOverlay(message);
            await ProcessOverlay(overlay);
        }

        private async Task ProcessOverlay(Overlay overlay)
        {
            await overlay.Show();
            GameObject.Destroy(overlay.gameObject);
        }
    }
}