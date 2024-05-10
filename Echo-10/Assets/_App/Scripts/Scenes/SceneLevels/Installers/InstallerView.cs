using _App.Scripts.Libs.Installer;
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

        [SerializeField] private GameObject _levelsMenuUI;
        [SerializeField] private ConfigLevel _levelList;
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private Transform _parentContainer;
        [SerializeField] private GameObject _restartMenu;
        [SerializeField] private GameObject _pauseMenu;

        public override void InstallBindings(ServiceContainer container)
        {
            var textureScroll = new SystemTextureScroll(_wallOffsets);
            container.SetService<IUpdatable, SystemTextureScroll>(textureScroll);

            var levelSelectionService = new ServiceLevelSelection();
            container.SetServiceSelf(levelSelectionService);

            var levelsMenuUI = new LevelsMenuUI(_levelsMenuUI, _levelList, _buttonPrefab, _parentContainer, 
                levelSelectionService);
            container.SetServiceSelf(levelsMenuUI);

            var restart = new RestartMenu(_restartMenu);
            container.SetService<IUpdatable, RestartMenu>(restart);

            var pause = new PauseMenu(_pauseMenu);
            container.SetService<IUpdatable, PauseMenu>(pause);

        }
    }
}
