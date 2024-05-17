using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class GameOverMenu : IUpdatable
    {
        private GameObject _gameOver;

        public static bool IsGameOverMenuActive()
        {
            if (instance != null)
            {
                return instance._gameOver.activeSelf;
            }
            return false;
        }

        private static GameOverMenu instance;

        public GameOverMenu(GameObject gameOverMenu)
        {
            _gameOver = gameOverMenu;

            instance = this;
            Time.timeScale = 1.0f;
            _gameOver.SetActive(false);
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.R))
            {
                Restart();
            }
        }

        public void Restart()
        {
            // Загружаем текущую сцену заново
        }

        public void LoadMenu()
        {
            _gameOver.SetActive(false);
        }
    }
}
