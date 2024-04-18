using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons
{
    public MainMenuButtons()
    {

    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}