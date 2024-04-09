using System;

namespace _App.Scripts.Libs.SceneManagement.Config
{
    [Serializable]
    public class SceneInfo
    {
        public string SceneKey;
        public string SceneViewName;

        public override string ToString()
        {
            return SceneViewName;
        }
    }
}