using _App.Scripts.Libs.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemPlayerMovement : IUpdatable
    {
        private Transform _playerTransform;
        private float _defaultSpeed = 15f;
        private float _currentSpeed;

        public SystemPlayerMovement(Transform playerTransform, float defaultSpeed)
        {
            _playerTransform = playerTransform;
            _defaultSpeed = defaultSpeed;
            _currentSpeed = defaultSpeed;
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
