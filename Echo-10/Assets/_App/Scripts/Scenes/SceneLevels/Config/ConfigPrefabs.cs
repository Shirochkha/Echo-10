using System.Collections.Generic;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Config
{
    [CreateAssetMenu(fileName = "configPrefabs", menuName = "app/levels/config prefabs")]
    public class ConfigPrefabs : ScriptableObject
    {
        public List<ObjectData> objects = new();
    }
}
