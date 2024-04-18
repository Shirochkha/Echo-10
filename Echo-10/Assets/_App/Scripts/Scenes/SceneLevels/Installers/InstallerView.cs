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

        [SerializeField] private Image[] _heartImages;
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private GameObject _restartMenu;

        public override void InstallBindings(ServiceContainer container)
        {
            var textureScroll = new SystemTextureScroll(_wallOffsets);
            container.SetService<IUpdatable, SystemTextureScroll>(textureScroll);

            var health = new Health(_heartImages, _fullHeartSprite, _emptyHeartSprite, _maxHealth, _restartMenu);
            container.SetService<IInitializable, Health>(health);
        }
    }
}
