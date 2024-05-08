using System.Threading.Tasks;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using UnityEngine;

namespace _App.Scripts.Infrastructure.GameCore.States.SetupState
{
    public class StateSetupLevel : GameState
    {
        private readonly IHandlerSetupLevel _handlerSetup;

        public StateSetupLevel(IHandlerSetupLevel handlerSetupLevels)
        {
            _handlerSetup = handlerSetupLevels;
        }

        public override void OnEnterState()
        {
            Debug.Log("Setup");
            ProcessSetupLevel().Forget();
        }

        private async Task ProcessSetupLevel()
        {
            await _handlerSetup.Process();

            StateMachine.ChangeState<StateProcessGame>();
        }
    }
}