using System;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Config
{
    [Serializable]
    public class ObjectData
    {
        public GameObject objectReference;
        public string objectName;
        public Vector3 position;
        public Vector3 scale;
    }
}
