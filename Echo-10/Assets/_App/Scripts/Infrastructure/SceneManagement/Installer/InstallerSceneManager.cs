using _App.Scripts.Infrastructure.SceneManagement.Controllers;
using _App.Scripts.Infrastructure.SceneManagement.System;
using _App.Scripts.Infrastructure.SceneManagement.View;
using _App.Scripts.Infrastructure.SharedViews.ItemSelector;
using _App.Scripts.Libs.Factory.Mono;
using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using _App.Scripts.Libs.SceneManagement.Config;
using _App.Scripts.Libs.ServiceLocator;
using _App.Scripts.Libs.Systems;
using UnityEngine;

namespace _App.Scripts.Infrastructure.SceneManagement.Installer
{
    public class InstallerSceneManager : MonoInstaller
    {
        [SerializeField] private ConfigScenes configScenes;
        [SerializeField] private ViewSelectorScene viewItemSelector;
        [SerializeField] private ButtonItemLabel prefabButtonItem;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var sceneNavigator = new SceneNavigatorLoader(configScenes);
            serviceContainer.SetService<ISceneNavigator, SceneNavigatorLoader>(sceneNavigator);

            viewItemSelector.Construct(new FactoryMonoPrefab<ButtonItemLabel>(prefabButtonItem));

            var controllerSelectScenes = new SystemChangeScene(sceneNavigator, viewItemSelector);
            serviceContainer.SetService<ISystem, SystemChangeScene>(controllerSelectScenes);

            var controllerInitViewSceneChange = new ControllerInitNavigator(sceneNavigator, viewItemSelector);
            serviceContainer.SetService<IInitializable, ControllerInitNavigator>(controllerInitViewSceneChange);

        }
    }
}