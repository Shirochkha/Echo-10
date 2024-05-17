using _App.Scripts.Libs.StateMachine;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateGameOver : GameState
    {
        private GameOverMenu _gameOverMenu;
        

        public StateGameOver(GameOverMenu gameOverMenu)
        {
            _gameOverMenu = gameOverMenu;

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
            
            StateMachine.ChangeState<StateLevelMenu>();
        }
    }
}
