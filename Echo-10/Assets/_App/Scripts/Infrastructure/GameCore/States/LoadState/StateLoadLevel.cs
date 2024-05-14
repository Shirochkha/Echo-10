using _App.Scripts.Infrastructure.GameCore.States.LoadState;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateLoadLevel : GameState
    {
        private readonly IHandlerLoadLevel _handlerSetup;

        public StateLoadLevel(IHandlerLoadLevel handlerSetupLevels)
        {
            _handlerSetup = handlerSetupLevels;
        }

        public override void OnEnterState()
        {
            Debug.Log("LoadLevel");
            ProcessSetupLevel().Forget();
        }

        private async Task ProcessSetupLevel()
        {
            await _handlerSetup.Process();

            StateMachine.ChangeState<StateRestartLevel>();
        }
    }
}