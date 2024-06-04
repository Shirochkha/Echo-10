using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _App.Scripts.Scenes.SceneMainMenu.Installers
{
    public class InstallerSettings : MonoInstaller
    {
        [SerializeField] private GameObject _settings;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private TMP_Dropdown _resolutionDropdown;
        [SerializeField] private TMP_Dropdown _qualityDropdown;
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _soundFXVolumeSlider;
        [SerializeField] private Toggle _qualityToggle;

        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _exitButton;

        public override void InstallBindings(ServiceContainer container)
        {
            var settings = new Settings(_settings, _audioMixer, _resolutionDropdown, _qualityDropdown, 
                _masterVolumeSlider, _musicVolumeSlider, _soundFXVolumeSlider, _qualityToggle);
            container.SetServiceSelf(settings);

            _resolutionDropdown.onValueChanged.AddListener(settings.SetResolution);
            _qualityToggle.onValueChanged.AddListener(settings.SetFullscreen);
            _qualityDropdown.onValueChanged.AddListener(settings.SetQuality);
            _saveButton.onClick.AddListener(settings.SaveSettings);
            _exitButton.onClick.AddListener(settings.ExitSettings);
            _masterVolumeSlider.onValueChanged.AddListener(settings.SetMasterVolume);
            _musicVolumeSlider.onValueChanged.AddListener(settings.SetMusicVolume);
            _soundFXVolumeSlider.onValueChanged.AddListener(settings.SetSoundFXVolume);
        }
    }
}