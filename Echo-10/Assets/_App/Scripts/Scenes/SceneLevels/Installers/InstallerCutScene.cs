using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.CutScene.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerCutScene : MonoInstaller
    {
        [SerializeField] private GameObject _cutScene;
        [SerializeField] private ConfigCharacters _characterManager;
        [SerializeField] private float _pauseTime = 1f;
        [SerializeField] private float _speedText = 0.05f;
        [SerializeField] private Image _dialogImage;
        [SerializeField] private Text _textArea;

        [SerializeField] private TextMeshProUGUI _textToChange;
        [SerializeField] private Color32 _targetColor;

        public override void InstallBindings(ServiceContainer container)
        {
            var cutSceneManager = new CutSceneManager(_cutScene, _characterManager, _pauseTime,
                _speedText, _dialogImage, _textArea);
            container.SetServiceSelf< CutSceneManager>(cutSceneManager);
            container.SetService<IUpdatable, CutSceneManager>(cutSceneManager);

            var textColorChanger = new TextColorChanger(_cutScene, _textToChange, _targetColor);
            container.SetServiceSelf<TextColorChanger>(textColorChanger);
            container.SetService<IUpdatable, TextColorChanger>(textColorChanger);

        }
    }
}

