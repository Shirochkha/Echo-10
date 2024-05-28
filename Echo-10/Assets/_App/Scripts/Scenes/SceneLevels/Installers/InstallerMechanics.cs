﻿using _App.Scripts.Libs.Installer;
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
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private Transform _helthContainer;

        [SerializeField] private Text _coinCountText;
        [SerializeField] private int _maxCoinCount = 10;

        public override void InstallBindings(ServiceContainer container)
        {
            var health = new HealthUI(_helthContainer, _fullHeartSprite, _emptyHeartSprite);
            container.SetServiceSelf(health);

            var healthController = new SystemHealthBarChange(_maxHealth, health);
            container.SetServiceSelf(healthController);

            var coin = new CoinUI(_coinCountText);
            container.SetServiceSelf(coin);

            var coinController = new SystemAddCoin(_maxCoinCount, coin);
            container.SetServiceSelf(coinController);
        }
    }
}
