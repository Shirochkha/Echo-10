using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.SceneManagement.Config;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Libs.SoundsManager;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.Scenes.SceneMainMenu.Installers
{
    public class InstallerMainMenuEntryPoint : MonoInstaller
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private ConfigScenes _scenes;
        [SerializeField] private AudioClip _soundButtonClip;
        [SerializeField] private AudioClip _mainMenuClip;

        public override void InstallBindings(ServiceContainer container)
        {
            var sceneNavigator = new SceneNavigatorLoader(_scenes);
            container.SetServiceSelf(sceneNavigator);

            var settings = container.Get<Settings>();

            var mainMenuButtons = new MainMenuButtons(sceneNavigator, settings, _soundButtonClip);

            _buttonStart.onClick.AddListener(mainMenuButtons.PlayGame);
            _buttonSettings.onClick.AddListener(mainMenuButtons.Settings);
            _buttonExit.onClick.AddListener(mainMenuButtons.ExitGame);

            container.SetServiceSelf(mainMenuButtons);
            SoundMusicManager.instance.PlayMusicClip(_mainMenuClip, 0.8f);
        }
    }
}