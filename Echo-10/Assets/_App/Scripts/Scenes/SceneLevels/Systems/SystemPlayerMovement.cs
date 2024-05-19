using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemPlayerMovement : IUpdatable
    {
        private Transform _playerTransform;
        private Vector3 _playerPosition;
        private float _defaultSpeed;
        private float _currentSpeed;

        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
        public Vector3 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }
        public float DefaultSpeed { get => _defaultSpeed; set => _defaultSpeed = value; }
        public float CurrentSpeed { get => _currentSpeed; set => _currentSpeed = value; }

        public SystemPlayerMovement(Transform playerTransform, float defaultSpeed)
        {
            _playerTransform = playerTransform;
            DefaultSpeed = defaultSpeed;
            CurrentSpeed = 0f;
            PlayerPosition = PlayerTransform.position;
        }

        public void Update()
        {
            _playerTransform.Translate(Vector3.forward * CurrentSpeed * Time.deltaTime);
        }

        public void ChangeSpeed(float newSpeed)
        {
            CurrentSpeed = newSpeed;
        }

        
    }
}
