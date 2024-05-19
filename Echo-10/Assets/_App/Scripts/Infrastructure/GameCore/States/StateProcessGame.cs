using _App.Scripts.Libs.StateMachine;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.States;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace _App.Scripts.Infrastructure.GameCore.States
{
    public class StateProcessGame : GameState
    {
        private SystemHealthBarChange _healthBar;
        private SystemPlayerInteractions _playerInteractions;
        private SystemPlayerMovement _playerMovement;

        public StateProcessGame(SystemHealthBarChange healthBar, SystemPlayerInteractions playerInteractions, 
            SystemPlayerMovement playerMovement)
        {
            _healthBar = healthBar;
            _playerInteractions = playerInteractions;
            _playerMovement = playerMovement;
        }

        public override void OnEnterState()
        {
            Debug.Log("Process");
            _playerMovement.ChangeSpeed(_playerMovement.DefaultSpeed);
        }

        public override void Update()
        {
            if (!GameOverMenu.IsGameOverMenuActive() && Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.GameIsPaused)
            {
                StateMachine.ChangeState<StatePauseGame>();
            }

            if(_healthBar.CurrentHealth <= 0)
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
            _playerMovement.ChangeSpeed(0f);
        }
    }
}