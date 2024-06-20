using _App.Scripts.Libs.SceneManagement;
using Assets._App.Scripts.Libs.SoundsManager;
using UnityEngine;

public class MainMenuButtons
{
    private SceneNavigatorLoader _scenes;
    private Settings _settings;

    private AudioClip _soundButtonClip;
    public MainMenuButtons(SceneNavigatorLoader scenes, Settings settings, AudioClip soundButtonClip)
    {
        _scenes = scenes;
        _settings = settings;
        _soundButtonClip = soundButtonClip;
    }

    public void PlayGame()
    {
        foreach (var scene in _scenes.GetAvailableSwitchScenes())
        {
            if (scene.SceneViewName == "Levels")
            {
                PlaySoundClip();
                _scenes.LoadScene(scene.SceneKey);
            }
        }
    }

    public void Settings()
    {
        PlaySoundClip();
        _settings.SettingsStart();
    }

    public void ExitGame()
    {
        PlaySoundClip();
        Debug.Log("Игра закрылась");
        Application.Quit();
    }

    private void PlaySoundClip()
    {
        SoundFXManager.instance.PlaySoundFXClip(_soundButtonClip);
    }
}