using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemPlayerMovement : IUpdatable
    {
        private Transform _playerTransform;
        private float _defaultSpeed = 15f;
        private float _currentSpeed;
        private Vector3 _playerPosition;

        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
        public Vector3 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }

        public SystemPlayerMovement(Transform playerTransform, float defaultSpeed)
        {
            _playerTransform = playerTransform;
            _defaultSpeed = defaultSpeed;
            _currentSpeed = defaultSpeed;
            PlayerPosition = PlayerTransform.position;
        }

        public void Update()
        {
            _playerTransform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
        }

        public void ChangeSpeed(float newSpeed)
        {
            _currentSpeed = newSpeed;
        }

        
    }
}
