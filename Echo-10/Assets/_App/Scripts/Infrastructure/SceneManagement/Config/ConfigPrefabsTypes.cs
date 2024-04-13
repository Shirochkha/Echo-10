using System.Collections.Generic;
using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.SceneManagement.Config
{
    [CreateAssetMenu(fileName = "configPrefabsTypes", menuName = "app/levels/config prefabs types")]
    public class ConfigPrefabsTypes : ScriptableObject
    {
        public List<PrefabData> prefabTypes = new();
    }
}
