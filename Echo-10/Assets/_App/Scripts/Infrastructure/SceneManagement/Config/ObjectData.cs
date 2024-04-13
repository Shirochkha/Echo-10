using System;
using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.SceneManagement.Config
{
    [Serializable]
    public class ObjectData
    {
        public GameObject objectReference;
        public GameObject prefabReference;
        public string objectName;
        public Vector3 position;
        public Vector3 scale;
        public ObjectType objectType;
    }
}
