using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings
{
    private GameObject _settings;
    private AudioMixer _audioMixer;
    private TMP_Dropdown _resolutionDropdown;
    private TMP_Dropdown _qualityDropdown;
    private Slider _masterVolumeSlider;
    private Slider _musicVolumeSlider;
    private Slider _soundFXVolumeSlider;
    private Toggle _fullscreenToggle;

    private float _currentMasterVolume;
    private float _currentMusicVolume;
    private float _currentSoundFXVolume;

    private float _tempMasterVolume;
    private float _tempMusicVolume;
    private float _tempSoundFXVolume;

    private bool _currentFullscreen;
    private bool _tempFullscreen;
    private int _currentResolutionIndex;
    private int _tempResolutionIndex;

    private Resolution[] _resolutions;

    public Settings(GameObject settings, AudioMixer audioMixer, TMP_Dropdown resolutionDropdown,
        TMP_Dropdown qualityDropdown, Slider masterVolumeSlider, Slider musicVolumeSlider, Slider soundFXVolumeSlider, 
        Toggle fullscreenToggle)
    {
        _settings = settings;
        _audioMixer = audioMixer;
        _resolutionDropdown = resolutionDropdown;
        _qualityDropdown = qualityDropdown;
        _masterVolumeSlider = masterVolumeSlider;
        _musicVolumeSlider = musicVolumeSlider;
        _soundFXVolumeSlider = soundFXVolumeSlider;
        _fullscreenToggle = fullscreenToggle;

        _settings.SetActive(false);
    }

    public void SettingsStart()
    {
        _settings.SetActive(true);

        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        _resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height + " " +
                _resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (_resolutions[i].width == Screen.currentResolution.width
                  && _resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.RefreshShownValue();

        _currentResolutionIndex = currentResolutionIndex;
        _tempResolutionIndex = currentResolutionIndex;

        LoadSettings(currentResolutionIndex);
    }

    private void SetVolume(string volumeParameter, float volume, ref float currentVolume)
    {
        float dbVolume = Mathf.Lerp(-80f, 20f, volume);
        _audioMixer.SetFloat(volumeParameter, dbVolume);
        currentVolume = volume;
    }

    public void SetMasterVolume(float volume)
    {
        SetVolume("MasterVolume", volume, ref _tempMasterVolume);
    }

    public void SetMusicVolume(float volume)
    {
        SetVolume("MusicVolume", volume, ref _tempMusicVolume);
    }

    public void SetSoundFXVolume(float volume)
    {
        SetVolume("SoundFXVolume", volume, ref _tempSoundFXVolume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        _tempFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        _tempResolutionIndex = resolutionIndex;
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, _tempFullscreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ExitSettings()
    {
        SetMasterVolume(_currentMasterVolume);
        SetMusicVolume(_currentMusicVolume);
        SetSoundFXVolume(_currentSoundFXVolume);
        Screen.SetResolution(_resolutions[_currentResolutionIndex].width, _resolutions[_currentResolutionIndex].height, 
            _currentFullscreen);
        Screen.fullScreen = _currentFullscreen;
        _settings.SetActive(false);
    }

    public void SaveSettings()
    {
        _currentMasterVolume = _tempMasterVolume;
        _currentMusicVolume = _tempMusicVolume;
        _currentSoundFXVolume = _tempSoundFXVolume;
        _currentResolutionIndex = _tempResolutionIndex;
        _currentFullscreen = _tempFullscreen;

        PlayerPrefs.SetInt("QualitySettingPreference", _qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", _resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(_currentFullscreen));
        PlayerPrefs.SetFloat("MasterVolumePreference", _currentMasterVolume);
        PlayerPrefs.SetFloat("MusicVolumePreference", _currentMusicVolume);
        PlayerPrefs.SetFloat("SoundFXVolumePreference", _currentSoundFXVolume);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        _qualityDropdown.value = PlayerPrefs.HasKey("QualitySettingPreference") ?
            PlayerPrefs.GetInt("QualitySettingPreference") : 3;

        _resolutionDropdown.value = PlayerPrefs.HasKey("ResolutionPreference") ?
            PlayerPrefs.GetInt("ResolutionPreference") : currentResolutionIndex;

        _currentFullscreen = PlayerPrefs.HasKey("FullscreenPreference") ?
            System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference")) : true;

        _currentMasterVolume = PlayerPrefs.HasKey("MasterVolumePreference") ?
            PlayerPrefs.GetFloat("MasterVolumePreference") : 0.8f;

        _currentMusicVolume = PlayerPrefs.HasKey("MusicVolumePreference") ?
            PlayerPrefs.GetFloat("MusicVolumePreference") : 0.8f;

        _currentSoundFXVolume = PlayerPrefs.HasKey("SoundFXVolumePreference") ?
            PlayerPrefs.GetFloat("SoundFXVolumePreference") : 1f;

        _masterVolumeSlider.value = _currentMasterVolume;
        _musicVolumeSlider.value = _currentMusicVolume;
        _soundFXVolumeSlider.value = _currentSoundFXVolume;

        _tempMasterVolume = _currentMasterVolume;
        _tempMusicVolume = _currentMusicVolume;
        _tempSoundFXVolume = _currentSoundFXVolume;

        _tempFullscreen = _currentFullscreen;
        Screen.fullScreen = _currentFullscreen;

        _fullscreenToggle.isOn = _currentFullscreen;
    }
}
