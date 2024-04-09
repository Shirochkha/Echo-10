using System.Collections.Generic;
using _App.Scripts.Libs.SceneManagement.Config;

namespace _App.Scripts.Libs.SceneManagement
{
    public interface ISceneNavigator
    {
        void LoadScene(string sceneId);

        public List<SceneInfo> GetAvailableSwitchScenes();
    }
}