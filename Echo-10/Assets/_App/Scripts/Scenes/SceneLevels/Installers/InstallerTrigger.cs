using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerTrigger : MonoInstaller
    {
        [SerializeField] private float _maxRadius = 150f;
        [SerializeField] private float _duration = 2f;
        [SerializeField] private SphereCollider _colliderComponent;
        [SerializeField] private Text _clickCountText;

        [SerializeField] private float _minAlpha = 0.03f;
        [SerializeField] private float _maxAlpha = 1f;
        [SerializeField] private ConfigLevel _level;

        [SerializeField] private float _enemySpeed = 15;

        [SerializeField] private ConfigSprites _sprites;

        public override void InstallBindings(ServiceContainer container)
        {
            var colliderRadiusChange = new SystemColliderRadiusChange(_maxRadius, _duration, 
                _colliderComponent, container.Get<IPlayer>());
            container.SetService<IUpdatable, SystemColliderRadiusChange>(colliderRadiusChange);
            container.SetServiceSelf(colliderRadiusChange);

            var clickCountUI = new ClickCountUI(_clickCountText, colliderRadiusChange);
            container.SetService<IUpdatable, ClickCountUI>(clickCountUI);

            var levelSelection = container.Get<ServiceLevelSelection>();
            var levelState = container.Get<ServiceLevelState>();

            var spriteAlphaChange = new SystemSpriteAlphaChange(_colliderComponent, _minAlpha, _maxAlpha, _duration,
                colliderRadiusChange, levelState, container.Get<IPlayer>());
            container.SetService<IUpdatable, SystemSpriteAlphaChange>(spriteAlphaChange);
            container.SetServiceSelf(spriteAlphaChange);

            var enemyMovement = new SystemEnemyMovement(_enemySpeed);
            container.SetServiceSelf(enemyMovement);

            var spriteChange = new SystemSpriteChange(_sprites, _colliderComponent, enemyMovement, levelState);
            container.SetService<IUpdatable, SystemSpriteChange>(spriteChange);
            container.SetServiceSelf(spriteChange);
        }
    }
}
