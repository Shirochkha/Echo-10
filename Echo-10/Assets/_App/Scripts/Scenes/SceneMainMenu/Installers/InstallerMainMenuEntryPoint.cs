using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.SceneManagement.Config;
using _App.Scripts.Libs.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.Scenes.SceneMainMenu.Installers
{
    public class InstallerMainMenuEntryPoint : MonoInstaller
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private ConfigScenes _scenes;

        public override void InstallBindings(ServiceContainer container)
        {
            var sceneNavigator = new SceneNavigatorLoader(_scenes);
            container.SetServiceSelf(sceneNavigator);

            var mainMenuButtons = new MainMenuButtons(sceneNavigator);

            _buttonStart.onClick.AddListener(mainMenuButtons.PlayGame);
            _buttonExit.onClick.AddListener(mainMenuButtons.ExitGame);

            container.SetServiceSelf(mainMenuButtons);
        }
    }
}