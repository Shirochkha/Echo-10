using System.Threading.Tasks;
using _App.Scripts.Infrastructure.GameCore.States;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateRestartLevel : GameState
    {
        private ServiceLevelState _levelState;
        private IPlayer _player;
        private Boss _boss;

        public StateRestartLevel(ServiceLevelState levelState, IPlayer player, Boss boss)
        {
            _levelState = levelState;
            _player = player;
            _boss = boss;
        }

        public override void OnEnterState()
        {
            Debug.Log("Restart");
            ProcessLevelRestart().Forget();
        }

        private async Task ProcessLevelRestart()
        {
            await Process();

            StateMachine.ChangeState<StateProcessGame>();
        }

        public Task Process()
        {
            _player.SetDefaultState(_levelState.MaxCoinCount);
            _boss.SetDefault();

            foreach (var objectData in _levelState.ConfigObjects.objects)
            {
                if (objectData.objectReference != null)
                {
                    objectData.collider.enabled = true;
                    objectData.renderer.color = new Color(objectData.renderer.color.r, objectData.renderer.color.g,
                        objectData.renderer.color.b, 255);
                    objectData.objectReference.transform.position = objectData.position;
                }
            }

            return Task.CompletedTask;
        }

    }
}