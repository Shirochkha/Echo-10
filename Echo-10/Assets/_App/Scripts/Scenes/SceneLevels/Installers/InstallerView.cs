using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerView : MonoInstaller
    {
        [SerializeField] private List<WallMaterialOffset> _wallOffsets;

        [Header("LevelMenu")]
        [SerializeField] private GameObject _levelsMenuUI;
        [SerializeField] private ConfigLevel _levelList;
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private Button _buttonMainMenu;
        [SerializeField] private Button _buttonShop;
        [SerializeField] private Button _buttonProfile;
        [SerializeField] private Transform _parentContainer;

        [Header("Shop")]
        [SerializeField] private GameObject _shopMenuObject;
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Button _buttonLevelMenu;
        [SerializeField] private Text _allCoinsCount;
        [SerializeField] private Transform _parentItemContainer;
        [SerializeField] private Button _skinsButton;
        [SerializeField] private Button _bonusesButton;

        [Header("Profile")]
        [SerializeField] private GameObject _profileMenuObject;
        [SerializeField] private Button _buttonReturnLevelMenu;
        [SerializeField] private Text _profileInfo;
        [SerializeField] private Image _skinImage;
        [SerializeField] private Text _skinNameText;
        [SerializeField] private Button _chooseButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _nextButton;

        [Header("GameOver")]
        [SerializeField] private GameObject _gameOverMenu;
        [SerializeField] private Button _buttonRetry;
        [SerializeField] private Button _buttonReturnToMenu;

        [Header("Pause")]
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Button _buttonInPauseMenu;
        [SerializeField] private Button _buttonRestart;
        public override void InstallBindings(ServiceContainer container)
        {
            var textureScroll = new SystemTextureScroll(_wallOffsets);
            container.SetService<IUpdatable, SystemTextureScroll>(textureScroll);


            var levelSelection = container.Get<ServiceLevelSelection>();

            var levelsMenuUI = new LevelsMenuUI(_levelsMenuUI, _levelList, _buttonPrefab, _buttonMainMenu, 
                _buttonShop, _buttonProfile, _parentContainer, levelSelection, container.Get<SceneNavigatorLoader>(), 
                container.Get<ServiceLevelState>());
            container.SetServiceSelf(levelsMenuUI);
            container.SetService<IUpdatable, LevelsMenuUI>(levelsMenuUI);

            var shopPersistence = container.Get<ShopPersistence>();
            var player = container.Get<IPlayer>();
            var playerPersistence = container.Get<PlayerMementoPersistence>();

            var shopUI = new ShopUI(_shopMenuObject, _itemPrefab, _buttonLevelMenu, _allCoinsCount,
                _parentItemContainer, _skinsButton, _bonusesButton, shopPersistence, player, playerPersistence);
            container.SetServiceSelf(shopUI);

            var profileUI = new ProfileUI(_profileMenuObject, _buttonReturnLevelMenu, _profileInfo, 
                _skinImage, _skinNameText, _chooseButton, _prevButton, _nextButton,
                shopPersistence, player, playerPersistence);
            container.SetServiceSelf(shopUI);

            levelsMenuUI.SubscribeToShopButtonClicked(() =>
            {
                levelsMenuUI.SetActiveObject(false);
                shopUI.SetActiveObject(true);
            });

            levelsMenuUI.SubscribeToProfileButtonClicked(() =>
            {
                levelsMenuUI.SetActiveObject(false);
                profileUI.SetActiveObject(true);
            });

            shopUI.SubscribeToLevelMenuButtonClicked(() =>
            {
                shopUI.SetActiveObject(false);
                levelsMenuUI.SetActiveObject(true);
            });

            profileUI.SubscribeToLevelMenuButtonClicked(() =>
            {
                profileUI.SetActiveObject(false);
                levelsMenuUI.SetActiveObject(true);
            });

            var gameOver = new GameOverMenu(_gameOverMenu, _buttonRetry, _buttonReturnToMenu);
            container.SetServiceSelf(gameOver);

            var pause = new PauseMenu(_pauseMenu, _buttonInPauseMenu, _buttonRestart);
            container.SetServiceSelf(pause);
        }

    }
}
