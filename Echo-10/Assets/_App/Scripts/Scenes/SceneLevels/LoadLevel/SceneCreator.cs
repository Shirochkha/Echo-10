using UnityEngine;
using _App.Scripts.Libs.Factory.Mono;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using System.Collections.Generic;

namespace Assets._App.Scripts.Scenes.SceneLevels.LoadLevel
{
    public class SceneCreator : MonoBehaviour
    {
        public ConfigObjects _configObjects;

        private void Start()
        {
            RecreateSceneFromScriptableObject();
        }

        public void RecreateSceneFromScriptableObject()
        {
            if (_configObjects == null)
            {
                Debug.LogError("ConfigObjects is not assigned!");
                return;
            }

            Dictionary<GameObject, FactoryMonoPrefab<GameObject>> prefabFactories = 
                new Dictionary<GameObject, FactoryMonoPrefab<GameObject>>();

            foreach (ObjectData data in _configObjects.objects)
            {
                if (data.prefabReference == null)
                {
                    Debug.LogWarning("Prefab reference is null!");
                    continue;
                }

                if (!prefabFactories.ContainsKey(data.prefabReference))
                {
                    var factory = new FactoryMonoPrefab<GameObject>(data.prefabReference);
                    prefabFactories.Add(data.prefabReference, factory);
                }

                var factoryForPrefab = prefabFactories[data.prefabReference];
                GameObject newObject = factoryForPrefab.Create();

                newObject.transform.position = data.position;
                newObject.transform.rotation = Quaternion.identity;
                newObject.transform.localScale = data.scale;

                data.objectReference = newObject;
                data.renderer = newObject.GetComponent<SpriteRenderer>();
                data.animator = newObject.GetComponent<Animator>();
                data.rigidbody = newObject.GetComponent<Rigidbody>();
            }
        }
    }
}
