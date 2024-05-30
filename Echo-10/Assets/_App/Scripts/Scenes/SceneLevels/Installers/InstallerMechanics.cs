using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerMechanics : MonoInstaller
    {
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;
        [SerializeField] private Transform _helthContainer;

        [SerializeField] private Text _coinCountText;

        public override void InstallBindings(ServiceContainer container)
        {
            var health = new HealthUI(_helthContainer, _fullHeartSprite, _emptyHeartSprite);
            container.SetServiceSelf(health);

            var coin = new CoinUI(_coinCountText);
            container.SetServiceSelf(coin);
        }
    }
}
