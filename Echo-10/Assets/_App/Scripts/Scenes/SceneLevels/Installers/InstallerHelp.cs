using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerHelp : MonoInstaller
    {
        [SerializeField] private ConfigObjects _configObjects;
        [SerializeField] private GameObject _player;

        public override void InstallBindings(ServiceContainer container)
        {
            var objectsDestroy = new SystemObjectsDestroy(_configObjects, _player);
            container.SetService<IUpdatable, SystemObjectsDestroy>(objectsDestroy);
        }
    }
}