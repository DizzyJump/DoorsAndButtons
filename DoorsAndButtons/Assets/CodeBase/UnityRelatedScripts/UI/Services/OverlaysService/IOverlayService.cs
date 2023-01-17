using System.Threading.Tasks;

namespace CodeBase.UnityRelatedScripts.UI.Overlays.OverlaysService
{
    public interface IOverlayService
    {
        Task ShowFinishLevelOverlay(string message);
    }
}