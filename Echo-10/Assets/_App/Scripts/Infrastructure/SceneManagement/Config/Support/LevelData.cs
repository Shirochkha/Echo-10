using Assets._App.Scripts.Infrastructure.CutScene.Config;
using System;
using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.SceneManagement.Config.Support
{
    [Serializable]
    public struct LevelData
    {
        public int id;
        public ConfigObjects configObjects;
        public ConfigCutScene cutScene;
        public Sprite sprite;
        public int coinCount;
        public bool haveAttack;
    }
}
