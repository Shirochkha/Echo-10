using _App.Scripts.Libs.StateMachine;
using Assets._App.Scripts.Libs.SoundsManager;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.States;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace _App.Scripts.Infrastructure.GameCore.States
{
    public class StateProcessGame : GameState
    {
        private SystemPlayerInteractions _playerInteractions;
        private IPlayer _player;

        private AudioClip _musicProcess;

        public StateProcessGame(SystemPlayerInteractions playerInteractions, IPlayer player, AudioClip musicProcess)
        {
            _playerInteractions = playerInteractions;
            _player = player;
            _musicProcess = musicProcess;
        }

        public override void OnEnterState()
        {
            Debug.Log("Process");
            if (SoundMusicManager.instance != null)
                SoundMusicManager.instance.PlayMusicClip(_musicProcess);
            _player.ChangeSpeed(_player.DefaultForwardSpeed);
        }

        public override void Update()
        {
            if (!GameOverMenu.IsGameOverMenuActive() && Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.GameIsPaused)
            {
                StateMachine.ChangeState<StatePauseGame>();
            }

            if(_player.PlayerStateOnLevel.CurrentHealth <= 0)
            {
                StateMachine.ChangeState<StateGameOver>();
            }

            if (_playerInteractions.IsWin)
            {
                _playerInteractions.IsWin = false;
                StateMachine.ChangeState<StateLevelMenu>();
            }
        }

        public override void OnExitState()
        {
            Debug.Log("EndProcess");
            _player.ChangeSpeed(0f);
        }
    }
}