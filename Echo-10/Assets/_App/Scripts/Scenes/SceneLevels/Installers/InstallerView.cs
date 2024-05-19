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
        [SerializeField] private Transform _parentContainer;

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

            var levelSelectionService = new ServiceLevelSelection();
            container.SetServiceSelf(levelSelectionService);

            var levelsMenuUI = new LevelsMenuUI(_levelsMenuUI, _levelList, _buttonPrefab, _buttonMainMenu,
                _parentContainer, levelSelectionService, container.Get<SceneNavigatorLoader>(), container.Get<ServiceLevelState>());
            container.SetServiceSelf(levelsMenuUI);
            container.SetService<IUpdatable, LevelsMenuUI>(levelsMenuUI);

            var gameOver = new GameOverMenu(_gameOverMenu, _buttonRetry, _buttonReturnToMenu);
            container.SetServiceSelf(gameOver);

            var pause = new PauseMenu(_pauseMenu, _buttonInPauseMenu, _buttonRestart);
            container.SetServiceSelf(pause);
        }
    }
}
