using _App.Scripts.Infrastructure.SharedViews.ItemSelector;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.SceneManagement.Config;
using _App.Scripts.Libs.Systems;

namespace _App.Scripts.Infrastructure.SceneManagement.System
{
    public class SystemChangeScene : ISystem
    {
        private readonly ISceneNavigator _sceneNavigator;
        private readonly IViewItemSelector<SceneInfo> _viewItemSelector;
        private SceneInfo _selectedScene;

        public SystemChangeScene(ISceneNavigator sceneNavigator, IViewItemSelector<SceneInfo> viewItemSelector)
        {
            _sceneNavigator = sceneNavigator;
            _viewItemSelector = viewItemSelector;
        }

        public void Init()
        {
            _viewItemSelector.OnItemSelected += OnItemSelected;
        }

        public void Update(float dt)
        {
            CheckChangeScene();
        }

        private void CheckChangeScene()
        {
            if (_selectedScene is null)
            {
                return;
            }

            _sceneNavigator.LoadScene(_selectedScene.SceneKey);
            _selectedScene = null;
        }

        public void Cleanup()
        {
            _selectedScene = null;
            _viewItemSelector.OnItemSelected -= OnItemSelected;
        }

        private void OnItemSelected(SceneInfo sceneInfo)
        {
            _selectedScene = sceneInfo;
        }
    }
}