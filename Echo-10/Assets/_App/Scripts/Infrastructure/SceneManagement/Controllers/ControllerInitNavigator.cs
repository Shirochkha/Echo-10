using _App.Scripts.Infrastructure.SharedViews.ItemSelector;
using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.SceneManagement.Config;

namespace _App.Scripts.Infrastructure.SceneManagement.Controllers
{
    public class ControllerInitNavigator : IInitializable
    {
        private readonly ISceneNavigator _sceneNavigator;
        private readonly IViewItemSelector<SceneInfo> _viewItemSelector;

        public ControllerInitNavigator(ISceneNavigator sceneNavigator, IViewItemSelector<SceneInfo> viewItemSelector)
        {
            _sceneNavigator = sceneNavigator;
            _viewItemSelector = viewItemSelector;
        }

        public void Init()
        {
            var availableScenes = _sceneNavigator.GetAvailableSwitchScenes();
            _viewItemSelector.UpdateItems(availableScenes);
        }
    }
}