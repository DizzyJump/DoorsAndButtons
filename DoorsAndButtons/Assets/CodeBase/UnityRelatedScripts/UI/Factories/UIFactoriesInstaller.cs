using CodeBase.UnityRelatedScripts.UI.Overlays.FinishLevelOverlay;
using Zenject;

namespace CodeBase.UnityRelatedScripts.UI
{
    public class UIFactoriesInstaller : Installer<UIFactoriesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();

            Container
                .BindFactory<string, FinishLevelOverlay, FinishLevelOverlay.Factory>()
                .FromComponentInNewPrefabResource(UiResPaths.FinishLevelOverlayPrefab);
        }
    }
}