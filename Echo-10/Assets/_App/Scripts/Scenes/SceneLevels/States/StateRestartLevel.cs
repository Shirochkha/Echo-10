using System.Collections.Generic;
using System.Threading.Tasks;
using _App.Scripts.Infrastructure.GameCore.States;
using _App.Scripts.Libs.Factory.Mono;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.TaskExtensions;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States
{
    public class StateRestartLevel : GameState
    {
        private SystemHealthBarChange _healthBar;
        private HealthUI _healthUI;
        private SystemColliderRadiusChange _echoBar;
        private SystemPlayerMovement _playerMovement;
        private SystemAddCoin _coinCount;
        private ConfigLevel _configLevel;

        public StateRestartLevel(SystemHealthBarChange healthBar, HealthUI healthUI, 
            SystemColliderRadiusChange echoBar,SystemPlayerMovement playerMovement, 
            SystemAddCoin coinCount, ConfigLevel configLevel)
        {
            _healthBar = healthBar;
            _healthUI = healthUI;
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
            await Process();

            StateMachine.ChangeState<StateProcessGame>();
        }

        public Task Process()
        {
            _healthBar.CurrentHealth = _healthBar.MaxHealth;
            _healthUI.UpdateCurrentHealthUI(_healthBar.CurrentHealth);
            _echoBar.ClickCount = _echoBar.MaxClickCount;
            _playerMovement.PlayerTransform.position = _playerMovement.PlayerPosition;
            //_coinCount.CoinCount = 0;
            _coinCount.AddCoins(-_coinCount.CoinCount);

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