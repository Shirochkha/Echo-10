using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.Systems;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.States;
using UnityEngine;

namespace _App.Scripts.Infrastructure.GameCore.States
{
    public class StateProcessGame : GameState
    {
        public StateProcessGame()
        {
        }

        public override void OnEnterState()
        {
            Debug.Log("Process");
        }

        public override void Update()
        {
            if (!GameOverMenu.IsGameOverMenuActive() && Input.GetKeyUp(KeyCode.Escape) && !PauseMenu.GameIsPaused)
            {
                StateMachine.ChangeState<StatePauseGame>();
            }
        }

        public override void OnExitState()
        {
            Debug.Log("EndProcess");
        }
    }
}