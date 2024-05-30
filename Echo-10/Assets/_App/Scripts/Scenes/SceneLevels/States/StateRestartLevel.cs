using System.Threading.Tasks;
using _App.Scripts.Infrastructure.GameCore.States;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateRestartLevel : GameState
    {
        private ConfigLevel _configLevel;
        private IPlayer _player;

        public StateRestartLevel(ConfigLevel configLevel, IPlayer player)
        {
            _configLevel = configLevel;
            _player = player;
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
            // TODO: ПОменять на реальное значение монет в лвле
            _player.SetDefaultState(10);

            foreach (var level in _configLevel.levels)
            {
                foreach (var objectData in level.configObjects.objects)
                {
                    if (objectData.objectReference != null)
                    {
                        objectData.collider.enabled = true;
                        objectData.renderer.color = new Color(objectData.renderer.color.r, objectData.renderer.color.g,
                            objectData.renderer.color.b, 255);
                        objectData.objectReference.transform.position = objectData.position;
                    }
                }
            }

            return Task.CompletedTask;
        }

    }
}