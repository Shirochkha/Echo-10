using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemObjectsDestroy : IUpdatable
    {
        private ConfigLevel _level;
        private ServiceLevelSelection _serviceLevelSelection;
        private GameObject _camera;

        private ConfigObjects _configObjects;
        private bool _hasSceneCreate = false;

        public SystemObjectsDestroy(ConfigLevel level, ServiceLevelSelection serviceLevelSelection, GameObject camera)
        {
            _level = level;
            _serviceLevelSelection = serviceLevelSelection;
            _camera = camera;
        }

        public void Update()
        {
            if (!_hasSceneCreate && _serviceLevelSelection.SelectedLevelId > 0)
            {
                foreach(var level in _level.levels)
                {
                    if (level.id == _serviceLevelSelection.SelectedLevelId)
                        _configObjects = level.configObjects;
                }
                _hasSceneCreate = true;
            }

            ObjectsDestroy();
        }

        private void ObjectsDestroy()
        {
            if (_configObjects == null) return;

            foreach (var objectData in _configObjects.objects)
            {
                if(objectData.objectReference != null && 
                    objectData.objectReference.transform.position.z < _camera.transform.position.z)
                {
                    //GameObject.Destroy(objectData.objectReference);
                    //objectData.objectReference.SetActive(false);
                }
            }
        }
    }
}
