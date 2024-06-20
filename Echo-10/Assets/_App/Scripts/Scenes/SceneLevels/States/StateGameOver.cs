using _App.Scripts.Libs.StateMachine;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Libs.SoundsManager;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateGameOver : GameState
    {
        private GameOverMenu _gameOverMenu;

        private AudioClip _musicGameOver;


        public StateGameOver(GameOverMenu gameOverMenu, AudioClip musicGameOver)
        {
            _gameOverMenu = gameOverMenu;

            _gameOverMenu.ButtonRetry.onClick.AddListener(Retry);
            _gameOverMenu.ButtonReturnToMenu.onClick.AddListener(LevelMenu);
            _musicGameOver = musicGameOver;
        }

        public override void OnEnterState()
        {
            Debug.Log("GameOver");
            if (SoundMusicManager.instance != null)
                SoundMusicManager.instance.PlayMusicClip(_musicGameOver);
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
