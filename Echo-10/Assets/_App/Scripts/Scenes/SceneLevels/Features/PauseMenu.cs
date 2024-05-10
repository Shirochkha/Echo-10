using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class PauseMenu : IUpdatable
    {
        private GameObject _pauseMenu;

        private static bool _gameIsPaused = false;
        private bool _isResuming = false;
        private float _resumeCooldown = 0.5f;
        private float _resumeTimer = 0f;

        public PauseMenu(GameObject pauseMenu)
        {
            _pauseMenu = pauseMenu;

            _pauseMenu.SetActive(false);
        }

        public void Update()
        {
            if (_isResuming)
            {
                _resumeTimer += Time.deltaTime; 
                if (_resumeTimer >= _resumeCooldown)
                {
                    _isResuming = false; 
                    _resumeTimer = 0f;
                }
            }

            if (!RestartMenu.IsRestartMenuActive() && Input.GetKeyUp(KeyCode.Escape))
            {
                if (_gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            if (!_isResuming)
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                _gameIsPaused = false;

                _isResuming = true;
            }
        }

        void Pause()
        {
            if (!RestartMenu.IsRestartMenuActive() && !_pauseMenu.activeSelf)
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
                _gameIsPaused = true;
            }
        }

        public void LoadMenu()
        {
            Time.timeScale = 1.0f;
        }
    }
}
