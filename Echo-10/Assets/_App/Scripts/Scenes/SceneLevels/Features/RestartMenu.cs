using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class RestartMenu : IUpdatable
    {
        private GameObject _restartMenu;

        public static bool IsRestartMenuActive()
        {
            if (instance != null)
            {
                return instance._restartMenu.activeSelf;
            }
            return false;
        }

        private static RestartMenu instance;

        public RestartMenu(GameObject restartMenu)
        {
            _restartMenu = restartMenu;

            instance = this;
            Time.timeScale = 1.0f;
            _restartMenu.SetActive(false);
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
            _restartMenu.SetActive(false);
        }
    }
}
