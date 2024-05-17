using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class PauseMenu
    {
        private GameObject _pauseMenu;
        private Button _buttonInPauseMenu;

        private static bool _gameIsPaused = false;

        public static bool GameIsPaused { get => _gameIsPaused; set => _gameIsPaused = value; }
        public Button ButtonInPauseMenu { get => _buttonInPauseMenu; set => _buttonInPauseMenu = value; }

        public PauseMenu(GameObject pauseMenu, Button buttonInPauseMenu)
        {
            _pauseMenu = pauseMenu;
            ButtonInPauseMenu = buttonInPauseMenu;

            _pauseMenu.SetActive(false);
        }

        public void Resume()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
            GameIsPaused = false;
        }

        public void Pause()
        {
            if (!GameOverMenu.IsGameOverMenuActive() && !_pauseMenu.activeSelf)
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
                GameIsPaused = true;
            }
        }
    }
}
