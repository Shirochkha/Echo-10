using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.Systems;
using UnityEngine;

namespace _App.Scripts.Infrastructure.GameCore.States
{
    public class StateProcessGame : GameState
    {
        private readonly SystemsGroup _gameSystems;

        public StateProcessGame(SystemsGroup gameSystems)
        {
            _gameSystems = gameSystems;
        }

        public override void OnEnterState()
        {
            Debug.Log("Process");
            _gameSystems.Init();
        }

        public override void Update()
        {
            _gameSystems.Update(Time.deltaTime);
        }

        public override void OnExitState()
        {
            Debug.Log("EndProcess");
            _gameSystems.Cleanup();
        }
    }
}