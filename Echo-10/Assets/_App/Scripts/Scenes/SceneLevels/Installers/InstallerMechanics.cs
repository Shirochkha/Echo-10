using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerMechanics : MonoInstaller
    {
        [SerializeField] private Sprite _fullHeartSprite;
        [SerializeField] private Sprite _emptyHeartSprite;
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private GameObject _restartMenu;
        [SerializeField] private Transform _helthContainer;

        [SerializeField] private Text _coinCountText;
        [NonSerialized] private int _coinCount = 0;
        [SerializeField] private int _maxCoinCount = 10;

        public override void InstallBindings(ServiceContainer container)
        {
            var health = new HealthUI(_helthContainer, _fullHeartSprite, _emptyHeartSprite);
            container.SetServiceSelf<HealthUI>(health);

            var healthController = new SystemHealthBarChange(_maxHealth, health, _restartMenu);
            container.SetServiceSelf<SystemHealthBarChange>(healthController);

            var coin = new CoinUI(_coinCountText);
            container.SetServiceSelf<CoinUI>(coin);

            var coinController = new SystemAddCoin(_coinCount, _maxCoinCount, coin);
            container.SetServiceSelf<SystemAddCoin>(coinController);
        }
    }
}
