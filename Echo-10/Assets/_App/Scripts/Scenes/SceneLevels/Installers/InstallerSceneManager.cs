using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement.Config;
using _App.Scripts.Libs.SceneManagement;
using UnityEngine;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerSceneManager : MonoInstaller
    {
        [SerializeField] private ConfigScenes _scenes;
        [SerializeField] private string _fileName;

        public override void InstallBindings(ServiceContainer container)
        {
            var sceneNavigator = new SceneNavigatorLoader(_scenes);
            container.SetServiceSelf(sceneNavigator);

            var levelStatePersistence = new LevelStatePersistence(_fileName);
            container.SetServiceSelf(levelStatePersistence);
        }
    }
}
