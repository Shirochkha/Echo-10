using _App.Scripts.Libs.Installer;
using UnityEngine;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerLevelManager : MonoInstaller
    {
        [SerializeField] private ConfigLevel _levels;

        public override void InstallBindings(ServiceContainer container)
        {
            var levelStatePersistence = container.Get<LevelStatePersistence>();

            var levelSelectionService = new ServiceLevelSelection();
            container.SetServiceSelf(levelSelectionService);

            var serviceLevelState = new ServiceLevelState(_levels, levelStatePersistence, levelSelectionService);
            container.SetServiceSelf(serviceLevelState);
        }
    }
}
