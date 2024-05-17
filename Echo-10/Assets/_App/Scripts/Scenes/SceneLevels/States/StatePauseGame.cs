using _App.Scripts.Infrastructure.GameCore.States;
using _App.Scripts.Libs.StateMachine;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StatePauseGame : GameState
    {
        private PauseMenu _pauseMenu;
        private bool _isPausing = false;

        public StatePauseGame(PauseMenu pauseMenu)
        {
            _pauseMenu = pauseMenu;
            _pauseMenu.ButtonInPauseMenu.onClick.AddListener(LoadMenu);
        }

        public override void OnEnterState()
        {
            Debug.Log("Pause");
            if (!_isPausing)
            {
                _isPausing = true;
                _pauseMenu.Pause();
            }
        }

        public override void Update()
        {
            if (!GameOverMenu.IsGameOverMenuActive() && Input.GetKeyUp(KeyCode.Escape) && PauseMenu.GameIsPaused)
            {
                StateMachine.ChangeState<StateProcessGame>();
            }
        }

        public override void OnExitState()
        {
            Debug.Log("EndPause");
            _pauseMenu.Resume();
            _isPausing = false;
        }

        private void LoadMenu()
        {
            _pauseMenu.Resume();
            StateMachine.ChangeState<StateLevelMenu>();
        }
    }

}
