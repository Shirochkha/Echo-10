using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerView : MonoInstaller
    {
        [SerializeField] private List<WallMaterialOffset> _wallOffsets;

        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private GameObject _restartMenu;
        [SerializeField] private Transform _helthContainer;

        public override void InstallBindings(ServiceContainer container)
        {
            var textureScroll = new SystemTextureScroll(_wallOffsets);
            container.SetService<IUpdatable, SystemTextureScroll>(textureScroll);

            var health = new HealthUI(_helthContainer, _fullHeartSprite, _emptyHeartSprite);
            container.SetServiceSelf<HealthUI>(health);

            var healthController = new SystemHealthBarChange(_maxHealth,health, _restartMenu);
            container.SetServiceSelf<SystemHealthBarChange>(healthController);
        }
    }
}
