using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerMechanics : MonoInstaller
    {
        [Header("Boss")]
        [SerializeField] private Slider _slider;
        [SerializeField] private GameObject _bossUI;

        [Header("Player")]
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

            var healthBarBoss = new HealthBarBossUI(_slider, _bossUI);
            container.SetServiceSelf(healthBarBoss);
        }
    }
}
