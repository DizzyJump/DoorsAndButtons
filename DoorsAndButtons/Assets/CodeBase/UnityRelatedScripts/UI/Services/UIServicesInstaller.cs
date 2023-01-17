using CodeBase.UnityRelatedScripts.UI.Overlays.OverlaysService;
using Zenject;

namespace CodeBase.UnityRelatedScripts.UI.Services
{
    public class UIServicesInstaller : Installer<UIServicesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<OverlayService>().AsSingle();
        }
    }
}