using UnityEngine;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemPlayerInteractions : IUpdatable
    {
        private ConfigLevel _level;
        private GameObject _player;        
        private Collider _playerCollider;
        private SystemHealthBarChange _healthController;
        private SystemAddCoin _addCoin;
        private ServiceLevelSelection _serviceLevelSelection;

        private ConfigObjects _configObjects;
        private bool _hasSceneCreate = false;

        public SystemPlayerInteractions(ConfigLevel level, GameObject player, Collider playerCollider, 
            SystemHealthBarChange healthController, SystemAddCoin addCoin, ServiceLevelSelection serviceLevelSelection)
        {
            _level = level;
            _player = player;
            _playerCollider = playerCollider;
            _healthController = healthController;
            _addCoin = addCoin;
            _serviceLevelSelection = serviceLevelSelection;
        }

        public void Update()
        {
            if (!_hasSceneCreate && _serviceLevelSelection.SelectedLevelId > 0)
            {
                foreach (var level in _level.levels)
                {
                    if (level.id == _serviceLevelSelection.SelectedLevelId)
                        _configObjects = level.configObjects;
                }
                _hasSceneCreate = true;
            }
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            if (_configObjects == null) return;
            if (_player != null)
            {
                foreach (ObjectData otherObjectData in _configObjects.objects)
                {
                    if (otherObjectData.objectReference == null
                        || _player.gameObject.transform.position.z > otherObjectData.objectReference.transform.position.z)
                        continue;

                    if (IsColliding(otherObjectData))
                    {
                        HandleCollision(otherObjectData);
                    }
                }
            }
            else
            {
                Debug.LogError("Player object not found in the list of objects.");
            }
        }

        private bool IsColliding(ObjectData obj2)
        {
            Collider collider1 = _playerCollider;
            Collider collider2 = obj2.objectReference.GetComponent<Collider>();

            if (collider1 != null && collider2 != null)
            {
                return collider1.bounds.Intersects(collider2.bounds);
            }
            else
            {
                Debug.LogWarning("One of the objects does not have a collider.");
                return false;
            }
        }

        private void HandleCollision(ObjectData obj2)
        {
            ObjectType objectType = obj2.objectType;

            switch (objectType)
            {
                case ObjectType.Obstacle:
                case ObjectType.Enemy:
                    GameObject.Destroy(obj2.objectReference.gameObject);
                    obj2.objectReference = null;
                    _healthController.PlayerDamaged(1);
                    Debug.Log("Obstacle/Enemy");
                    break;
                case ObjectType.Coin:
                    GameObject.Destroy(obj2.objectReference.gameObject);
                    obj2.objectReference = null;
                    _addCoin.AddCoins(1);
                    Debug.Log("Coin");
                    break;
                case ObjectType.LevelEnd:
                    Debug.Log("LevelEnd");
                    break;
                default:
                    Debug.LogWarning("Unknown object type.");
                    break;
            }
        }
    }
}
