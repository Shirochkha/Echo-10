using _App.Scripts.Libs.SceneManagement;
using UnityEngine;

public class MainMenuButtons
{
    private SceneNavigatorLoader _scenes;
    public MainMenuButtons(SceneNavigatorLoader scenes)
    {
        _scenes = scenes;
    }

    public void PlayGame()
    {
        foreach (var scene in _scenes.GetAvailableSwitchScenes())
        {
            if (scene.SceneViewName == "Levels")
                _scenes.LoadScene(scene.SceneKey);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}