using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _App.Scripts.Scenes.SceneMainMenu.Installers
{
    public class InstallerMainMenuEntryPoint : MonoInstaller
    {
        // TODO: Я тупик


        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonExit;

        public override void InstallBindings(ServiceContainer container)
        {
            var mainMenuButtons = new MainMenuButtons();

            _buttonStart.onClick.AddListener(mainMenuButtons.PlayGame);
            _buttonExit.onClick.AddListener(mainMenuButtons.ExitGame);

            container.SetServiceSelf(mainMenuButtons);
        }
    }
}