using System;
using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.SceneManagement.Config
{
    [Serializable]
    public struct PrefabData
    {
        public GameObject prefab;
        public ObjectType type;
    }
}
