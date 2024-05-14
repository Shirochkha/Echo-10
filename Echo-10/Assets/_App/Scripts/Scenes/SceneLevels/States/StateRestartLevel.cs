using System.Threading.Tasks;
using _App.Scripts.Infrastructure.GameCore.States;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateRestartLevel : GameState
    {
        private SystemHealthBarChange _healthBar;
        private SystemColliderRadiusChange _echoBar;
        private SystemPlayerMovement _playerMovement;
        private SystemAddCoin _coinCount;
        private ConfigLevel _configLevel;

        public StateRestartLevel(SystemHealthBarChange healthBar, SystemColliderRadiusChange echoBar, 
            SystemPlayerMovement playerMovement, SystemAddCoin coinCount, ConfigLevel configLevel)
        {
            _healthBar = healthBar;
            _echoBar = echoBar;
            _playerMovement = playerMovement;
            _coinCount = coinCount;
            _configLevel = configLevel;
        }

        public override void OnEnterState()
        {
            Debug.Log("Restart");
            ProcessLevelRestart().Forget();
        }

        private async Task ProcessLevelRestart()
        {
            _healthBar.CurrentHealth = _healthBar.MaxHealth;
            _echoBar.ClickCount = _echoBar.MaxClickCount;
            _playerMovement.PlayerTransform.position = _playerMovement.PlayerPosition;
            _coinCount.CoinCount = _coinCount.CoinMaxCount;

            foreach(var level in _configLevel.levels)
            {
                foreach(var objectData in level.configObjects.objects)
                {
                    if(objectData.objectReference != null)
                        objectData.objectReference.SetActive(true);
                }
            }

            await Task.Yield();
            StateMachine.ChangeState<StateProcessGame>();
        }


    }
}