using _App.Scripts.Infrastructure.ProjectSettings.Config;
using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using UnityEngine;

namespace _App.Scripts.Infrastructure.ProjectSettings.Installers
{
    public class InstallerProjectSettings : MonoInstaller
    {
        [SerializeField] private ConfigProjectSettings configProjectSettings;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var controller = new ControllerSetupProjectSettings(configProjectSettings);

            serviceContainer.SetService<IInitializable, ControllerSetupProjectSettings>(controller);
        }
    }
}