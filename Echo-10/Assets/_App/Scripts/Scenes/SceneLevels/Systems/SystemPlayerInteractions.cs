using UnityEngine;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using Assets._App.Scripts.Scenes.SceneLevels.Features;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemPlayerInteractions : IUpdatable
    {
        private IPlayer _player;
        private ServiceLevelState _serviceLevelState;
        private ConfigObjects _configObjects;        

        public SystemPlayerInteractions(IPlayer player, ServiceLevelState serviceLevelState)
        {
            _player = player;
            _serviceLevelState = serviceLevelState;
        }

        public bool IsWin 
        { 
            get => _player.PlayerStateOnLevel.IsWin; 
            set => _player.PlayerStateOnLevel.IsWin = value; 
        }

        public void Update()
        {
            if (!_serviceLevelState.HasLevelCreate)
            {
                _serviceLevelState.HasLevelCreate = true;
                _configObjects = _serviceLevelState.ConfigObjects;
            }

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            if (_configObjects == null) return;
            if (_player.PlayerGameObject == null)
            {
                Debug.LogError("Player object not found in the list of objects.");
            }
            
            foreach (ObjectData otherObjectData in _configObjects.objects)
            {
                if (otherObjectData.objectReference == null
                    || _player.PlayerTransform.position.z > otherObjectData.objectReference.transform.position.z)
                    continue;

                if (IsColliding(otherObjectData))
                {
                    HandleCollision(otherObjectData);
                }
            }
        }

        private bool IsColliding(ObjectData obj2)
        {
            Collider collider1 = _player.PlayerCollider;
            Collider collider2 = obj2.objectReference.GetComponent<Collider>();

            if (collider1 != null && collider2 != null && collider2.enabled == true)
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
                    obj2.collider.enabled = false;
                    obj2.renderer.color = new Color(obj2.renderer.color.r, obj2.renderer.color.g,
                            obj2.renderer.color.b, 0);
                    _player.TakeDamage(1);
                    Debug.Log("Obstacle/Enemy");
                    break;
                case ObjectType.Coin:
                    obj2.collider.enabled = false;
                    obj2.renderer.color = new Color(obj2.renderer.color.r, obj2.renderer.color.g,
                            obj2.renderer.color.b, 0);
                    _player.AddCoins(1);
                    Debug.Log("Coin");
                    break;
                case ObjectType.LevelEnd:
                    Debug.Log("LevelEnd");
                    obj2.collider.enabled = false;
                    _player.PlayerStateOnLevel.IsWin = true;
                    _serviceLevelState.SetLevelWin();
                    break;
                case ObjectType.SpeedBonus:
                    obj2.collider.enabled = false;
                    obj2.renderer.color = new Color(obj2.renderer.color.r, obj2.renderer.color.g,
                            obj2.renderer.color.b, 0);
                    _player = new SpeedPlayerDecorator(_player, 5, 15);
                    break;
                case ObjectType.MuteBonus:
                    obj2.collider.enabled = false;
                    obj2.renderer.color = new Color(obj2.renderer.color.r, obj2.renderer.color.g,
                            obj2.renderer.color.b, 0);
                    _player = new MuteEchoPlayerDecorator(_player, 3);
                    break;
                case ObjectType.AddEchoBonus:
                    obj2.collider.enabled = false;
                    obj2.renderer.color = new Color(obj2.renderer.color.r, obj2.renderer.color.g,
                            obj2.renderer.color.b, 0);
                    _player = new AddEchoPlayerDecorator(_player, 2);
                    break;
                default:
                    Debug.LogWarning("Unknown object type.");
                    break;
            }
        }
    }
}

