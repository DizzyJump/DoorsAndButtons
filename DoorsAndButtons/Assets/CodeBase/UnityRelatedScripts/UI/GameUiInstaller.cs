using CodeBase.UnityRelatedScripts.UI.Overlays.OverlaysService;
using CodeBase.UnityRelatedScripts.UI.Services;
using Zenject;

namespace CodeBase.UnityRelatedScripts.UI
{
    public class GameUiInstaller : Installer<GameUiInstaller>
    {
        public override void InstallBindings()
        {
            InstallFactories();

            InstallServices();
        }

        private void InstallFactories()
        {
            Container
                .Bind<IUIFactory>()
                .FromSubContainerResolve()
                .ByInstaller<UIFactoriesInstaller>()
                .AsSingle();
        }

        private void InstallServices()
        {
            UIServicesInstaller.Install(Container);
        }
    }
}