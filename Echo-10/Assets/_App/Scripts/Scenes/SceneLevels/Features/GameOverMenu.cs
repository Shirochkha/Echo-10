using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class GameOverMenu
    {
        private GameObject _gameOver;
        private Button _buttonRetry;
        private Button _buttonReturnToMenu;

        public static bool IsGameOverMenuActive()
        {
            if (instance != null)
            {
                return instance._gameOver.activeSelf;
            }
            return false;
        }

        private static GameOverMenu instance;

        public Button ButtonRetry { get => _buttonRetry; set => _buttonRetry = value; }
        public Button ButtonReturnToMenu { get => _buttonReturnToMenu; set => _buttonReturnToMenu = value; }

        public GameOverMenu(GameObject gameOverMenu, Button buttonRetry, Button buttonReturnToMenu)
        {
            _gameOver = gameOverMenu;
            ButtonRetry = buttonRetry;
            ButtonReturnToMenu = buttonReturnToMenu;

            instance = this;
            _gameOver.SetActive(false);
        }

        public void GameOver(bool isOver)
        {
            if (isOver)
            {
                _gameOver.SetActive(true);
            }
            else
            {
                _gameOver.SetActive(false);
            }
        }
    }
}
