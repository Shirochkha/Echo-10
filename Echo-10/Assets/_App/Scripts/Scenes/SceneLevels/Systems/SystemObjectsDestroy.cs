using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemObjectsDestroy : IUpdatable
    {
        private ConfigObjects _configObjects;
        private GameObject _camera;

        public SystemObjectsDestroy(ConfigObjects configObjects, GameObject player)
        {
            _configObjects = configObjects;
            _camera = player;
        }

        public void Update()
        {
            ObjectsDestroy();
        }

        private void ObjectsDestroy()
        {
            foreach(var objectData in _configObjects.objects)
            {
                if(objectData.objectReference != null && 
                    objectData.objectReference.transform.position.z < _camera.transform.position.z)
                {
                    GameObject.Destroy(objectData.objectReference);
                }
            }
        }
    }
}
