using System.Collections.Generic;
using _App.Scripts.Libs.SceneManagement.Config;

namespace _App.Scripts.Libs.SceneManagement
{
    public class SceneNavigatorLoader : ISceneNavigator
    {
        private readonly ConfigScenes _configScenes;

        public SceneNavigatorLoader(ConfigScenes configScenes)
        {
            _configScenes = configScenes;
        }

        public void LoadScene(string sceneId)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
        }

        public List<SceneInfo> GetAvailableSwitchScenes()
        {
            var currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            var result = new List<SceneInfo>();

            foreach (var sceneInfo in _configScenes.AvailableScenes)
                if (sceneInfo.SceneKey != currentSceneName)
                    result.Add(sceneInfo);

            return result;
        }
    }
}