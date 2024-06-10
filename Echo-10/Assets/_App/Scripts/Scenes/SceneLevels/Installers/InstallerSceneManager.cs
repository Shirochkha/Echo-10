using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement.Config;
using _App.Scripts.Libs.SceneManagement;
using UnityEngine;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System.Collections.Generic;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    public class InstallerSceneManager : MonoInstaller
    {
        [SerializeField] private ConfigScenes _scenes;
        [SerializeField] private string _fileNameLevelState;
        [SerializeField] private string _fileNameShopInfo;

        public override void InstallBindings(ServiceContainer container)
        {
            var sceneNavigator = new SceneNavigatorLoader(_scenes);
            container.SetServiceSelf(sceneNavigator);

            var levelStatePersistence = new LevelStatePersistence(_fileNameLevelState);
            container.SetServiceSelf(levelStatePersistence);
            container.SetService<IPersistence<List<LevelState>>, LevelStatePersistence>(levelStatePersistence);

            var shopPersistence = new ShopPersistence(_fileNameShopInfo);
            container.SetServiceSelf(shopPersistence);
            container.SetService<IPersistence<List<Item>>, ShopPersistence>(shopPersistence);
        }
    }
}
