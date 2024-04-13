using UnityEngine;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using _App.Scripts.Libs.Installer;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemPlayerInteractions : IUpdatable
    {
        private ConfigObjects _configObjects;
        private GameObject _player;

        public SystemPlayerInteractions(ConfigObjects configObjects, GameObject player)
        {
            _configObjects = configObjects;
            _player = player;
        }

        public void Update()
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            if (_player != null)
            {
                foreach (ObjectData otherObjectData in _configObjects.objects)
                {
                    if (otherObjectData.objectReference == null || 
                        _player.gameObject.transform.position.z > otherObjectData.objectReference.transform.position.z) 
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
            Collider collider1 = _player.GetComponent<Collider>();
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
                    Debug.Log("Obstacle/Enemy");
                    break;
                case ObjectType.Coin:
                    GameObject.Destroy(obj2.objectReference.gameObject);
                    obj2.objectReference = null;
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
