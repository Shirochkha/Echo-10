using _App.Scripts.Infrastructure.GameCore.States.LoadState;
using _App.Scripts.Libs.Factory.Mono;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.States.Load
{
    public class HandlerLoadObjects : IHandlerLoadLevel
    {
        private ConfigLevel _level;
        private ServiceLevelSelection _serviceLevelSelection;

        private ConfigObjects _configObjects;

        public HandlerLoadObjects(ConfigLevel level,ServiceLevelSelection serviceLevelSelection)
        {
            _level = level;
            _serviceLevelSelection = serviceLevelSelection;
        }


        public Task Process()
        {
            foreach (var level in _level.levels)
            {
                if (level.id == _serviceLevelSelection.SelectedLevelId)
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
                data.collider = newObject.GetComponent<Collider>();
            }

            return Task.CompletedTask;
        }
    }
}