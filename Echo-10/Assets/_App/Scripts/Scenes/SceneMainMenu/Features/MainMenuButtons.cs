using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons
{
    public MainMenuButtons()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрылась");
        Application.Quit();
    }
}