using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerPlayer : MonoInstaller
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _playerSpeed = 25f;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _defaultSpeed = 15f;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _cameraSmoothSpeed = 0.125f;
        [SerializeField] private float _cameraHeightOffset = -35f;
        [SerializeField] private ConfigLevel _level;
        [SerializeField] private GameObject _player;
        [SerializeField] private Collider _playerCollider;
        [SerializeField] private Vector3 _playerPosition;

        [SerializeField] private int _coinsCount = 0;
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private int _echoCount = 10;

        public override void InstallBindings(ServiceContainer container)
        {
            var cameraFollow = new SystemCameraFollow(_playerTransform, _cameraTransform, _cameraSmoothSpeed, 
                _cameraHeightOffset);
            container.SetService<IUpdatable, SystemCameraFollow>(cameraFollow);

            var levelState = container.Get<ServiceLevelState>();

            var healthUI = container.Get<HealthUI>();
            var coinUI = container.Get<CoinUI>();
            var player = new Player(_player, _rb, _playerTransform, _playerCollider, _playerSpeed,
                _defaultSpeed, _coinsCount, _maxHealth, _echoCount, _playerPosition, 
                coinUI.UpdateCoinCountUI, healthUI.UpdateHealthUI, healthUI.UpdateCurrentHealthUI);

            container.SetServiceSelf(player);
            container.SetService<IUpdatable, Player>(player);
            container.SetService<IPlayer, Player>(player);

            var playerInteractions = new SystemPlayerInteractions(player, levelState);
            container.SetServiceSelf(playerInteractions);
            container.SetService<IUpdatable, SystemPlayerInteractions>(playerInteractions);
        }  
    }
}
