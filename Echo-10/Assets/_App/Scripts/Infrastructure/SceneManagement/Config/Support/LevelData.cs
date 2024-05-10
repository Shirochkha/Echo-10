using Assets._App.Scripts.Infrastructure.CutScene.Config;
using System;

namespace Assets._App.Scripts.Infrastructure.SceneManagement.Config.Support
{
    [Serializable]
    public struct LevelData
    {
        public int id;
        public ConfigObjects configObjects;
        public ConfigCutScene cutScene;
    }
}
