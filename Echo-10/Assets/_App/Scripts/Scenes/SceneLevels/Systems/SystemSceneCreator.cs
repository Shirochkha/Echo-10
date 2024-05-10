using _App.Scripts.Libs.Factory.Mono;
using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemSceneCreator : IUpdatable
    {
        private ConfigLevel _level;
        private ServiceLevelSelection _serviceLevelSelection;

        private ConfigObjects _configObjects;
        private bool _hasSceneCreate = false;

        public SystemSceneCreator(ConfigLevel level, ServiceLevelSelection serviceLevelSelection)
        {
            _level = level;
            _serviceLevelSelection = serviceLevelSelection;
        }

        public void Update()
        {
            if (!_hasSceneCreate && _serviceLevelSelection.SelectedLevelId > 0)
            {
                RecreateSceneFromScriptableObject();
                _hasSceneCreate = true;
            }
        }

        public void RecreateSceneFromScriptableObject()
        {
            foreach(var level in _level.levels)
            {
                if(level.id == _serviceLevelSelection.SelectedLevelId)
                    _configObjects = level.configObjects;
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
