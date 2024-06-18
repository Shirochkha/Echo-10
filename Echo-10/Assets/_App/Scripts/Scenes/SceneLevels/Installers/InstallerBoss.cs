using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerBoss : MonoInstaller
    {
        [SerializeField] private float _directionChangeInterval = 2f;
        [SerializeField] private float _rayDistance = 1f;
        [SerializeField] private float _minTimeBetweenDirectionChanges = 0.5f;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private int _attackDelay;
        [SerializeField] private float _speedByAxises;
        [SerializeField] private float _speedForward;

        public override void InstallBindings(ServiceContainer container)
        {
            var levelState = container.Get<ServiceLevelState>();
            var healthUI = container.Get<HealthBarBossUI>();

            var boss = new Boss(levelState, healthUI, _directionChangeInterval, _rayDistance,
                _minTimeBetweenDirectionChanges, _damage, _health, _attackDelay, _speedByAxises, _speedForward);
            container.SetServiceSelf(boss);
            container.SetService<IUpdatable, Boss>(boss);
        }
    }
}
