using _App.Scripts.Libs.StateMachine;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateGameOver : GameState
    {
        private GameOverMenu _gameOverMenu;
        private ConfigLevel _configLevel;

        public StateGameOver(GameOverMenu gameOverMenu, ConfigLevel configLevel)
        {
            _gameOverMenu = gameOverMenu;
            _configLevel = configLevel;

            _gameOverMenu.ButtonRetry.onClick.AddListener(Retry);
            _gameOverMenu.ButtonReturnToMenu.onClick.AddListener(LevelMenu);
        }

        public override void OnEnterState()
        {
            Debug.Log("GameOver");
            _gameOverMenu.GameOver(true);
        }

        public override void OnExitState()
        {
            Debug.Log("EndGameOver");
            _gameOverMenu.GameOver(false);
        }

        private void Retry()
        {
            StateMachine.ChangeState<StateRestartLevel>();
        }
        private void LevelMenu()
        {
            foreach (var level in _configLevel.levels)
            {
                foreach (var objectData in level.configObjects.objects)
                {
                    if (objectData.objectReference != null)
                        GameObject.Destroy(objectData.objectReference);
                }
            }
            StateMachine.ChangeState<StateLevelMenu>();
        }
    }
}
