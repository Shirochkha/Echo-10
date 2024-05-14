using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
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

        public override void InstallBindings(ServiceContainer container)
        {
            var playerController = new SystemPlayerController(_rb, _playerSpeed);
            container.SetService<IUpdatable, SystemPlayerController>(playerController);

            var playerMovement = new SystemPlayerMovement(_playerTransform, _defaultSpeed);
            container.SetService<IUpdatable, SystemPlayerMovement>(playerMovement);
            container.SetServiceSelf<SystemPlayerMovement>(playerMovement);

            var cameraFollow = new SystemCameraFollow(_playerTransform, _cameraTransform, _cameraSmoothSpeed, 
                _cameraHeightOffset);
            container.SetService<IUpdatable, SystemCameraFollow>(cameraFollow);

            var healthController = container.Get<SystemHealthBarChange>();
            var coinController = container.Get<SystemAddCoin>();
            var levelSelection = container.Get<ServiceLevelSelection>();

            var playerInteractions = new SystemPlayerInteractions(_level, _player, _playerCollider, 
                healthController, coinController, levelSelection);
            container.SetService<IUpdatable, SystemPlayerInteractions>(playerInteractions);
        }  
    }
}
