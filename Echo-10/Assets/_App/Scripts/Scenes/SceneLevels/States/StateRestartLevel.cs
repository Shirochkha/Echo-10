using System.Threading.Tasks;
using _App.Scripts.Infrastructure.GameCore.States.SetupState;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateRestartLevel : GameState
    {
        private readonly SystemHealthBarChange _systemHealthBarChange;

        public StateRestartLevel(SystemHealthBarChange systemHealthBarChange)
        {
            _systemHealthBarChange = systemHealthBarChange;
        }

        public override void OnEnterState()
        {
            Debug.Log("Restart");
            StateMachine.ChangeState<StateSetupLevel>();
        }

        
    }
}