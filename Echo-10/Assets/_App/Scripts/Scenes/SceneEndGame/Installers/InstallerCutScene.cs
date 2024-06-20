using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.SceneManagement.Config;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.CutScene.Config;
using Assets._App.Scripts.Libs.SoundsManager;
using Assets._App.Scripts.Scenes.SceneEndGame.Features;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneEndGame.Installers
{
    public class InstallerCutScene : MonoInstaller
    {
        [SerializeField] private ConfigScenes _scenes;
        [SerializeField] private ConfigCharacters _characterManager;
        [SerializeField] private float _pauseTime = 1f;
        [SerializeField] private float _speedText = 0.05f;
        [SerializeField] private Image _dialogImage;
        [SerializeField] private Text _textArea;
        [SerializeField] private ConfigCutScene _configCutScene;
        [SerializeField] private AudioClip _winMusicClip;

        public override void InstallBindings(ServiceContainer container)
        {
            var cutSceneManager = new CutScene(_characterManager, _pauseTime, _speedText, _dialogImage,
                _textArea, _configCutScene, _scenes);
            container.SetServiceSelf(cutSceneManager);
            container.SetService<IUpdatable, CutScene>(cutSceneManager);
            SoundMusicManager.instance.PlayMusicClip(_winMusicClip);
        }
    }
}
