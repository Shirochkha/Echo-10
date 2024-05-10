using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerHelp : MonoInstaller
    {
        [SerializeField] private ConfigLevel _configLevel;
        [SerializeField] private GameObject _camera;

        public override void InstallBindings(ServiceContainer container)
        {
            var serviceLevelSelection = container.Get<ServiceLevelSelection>();

            var sceneCreator = new SystemSceneCreator(_configLevel, serviceLevelSelection);
            container.SetService<IUpdatable, SystemSceneCreator>(sceneCreator);

            var objectsDestroy = new SystemObjectsDestroy(_configLevel, serviceLevelSelection, _camera);
            container.SetService<IUpdatable, SystemObjectsDestroy>(objectsDestroy);
        }
    }
}