using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerTrigger : MonoInstaller
    {
        [SerializeField] private float _maxRadius = 150f;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private int _clickCount = 10;
        [SerializeField] private SphereCollider _colliderComponent;
        [SerializeField] private Text _clickCountText;

        [SerializeField] private float _minAlpha = 0.03f;
        [SerializeField] private float _maxAlpha = 1f;
        [SerializeField] private ConfigObjects _configObjects;

        [SerializeField] private float _enemySpeed = 15;

        [SerializeField] private ConfigSprites _sprites;

        public override void InstallBindings(ServiceContainer container)
        {
            var colliderRadiusChange = new SystemColliderRadiusChange(_maxRadius, _duration, _clickCount, 
                _colliderComponent);
            container.SetService<IUpdatable, SystemColliderRadiusChange>(colliderRadiusChange);

            var clickCountUI = new ClickCountUI(_clickCountText, colliderRadiusChange);
            container.SetService<IUpdatable, ClickCountUI>(clickCountUI);

            var spriteAlphaChange = new SystemSpriteAlphaChange(_colliderComponent, _minAlpha, _maxAlpha, _duration,
                colliderRadiusChange, _configObjects);
            container.SetService<IUpdatable, SystemSpriteAlphaChange>(spriteAlphaChange);

            var enemyMovement = new SystemEnemyMovement(_enemySpeed);
            container.SetServiceSelf<SystemEnemyMovement>(enemyMovement);

            var spriteChange = new SystemSpriteChange(_sprites, _configObjects, _colliderComponent, enemyMovement);
            container.SetService<IUpdatable, SystemSpriteChange>(spriteChange);
        }
    }
}
