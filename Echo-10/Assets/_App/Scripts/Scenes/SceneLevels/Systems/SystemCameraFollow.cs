using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemCameraFollow : IUpdatable
    {
        private Transform _targetTransform;
        private Transform _cameraTransform;
        private float _cameraSmoothSpeed;
        private float _cameraHeightOffset;

        public SystemCameraFollow(Transform targetTransform, Transform cameraTransform, 
            float cameraSmoothSpeed, float cameraHeightOffset)
        {
            _targetTransform = targetTransform;
            _cameraTransform = cameraTransform;
            _cameraSmoothSpeed = cameraSmoothSpeed;
            _cameraHeightOffset = cameraHeightOffset;
        }

        public void Update()
        {
            Vector3 currentPosition = _cameraTransform.position;
            Vector3 desiredPosition = new(currentPosition.x, currentPosition.y, 
                _targetTransform.position.z + _cameraHeightOffset);

            Vector3 smoothedPosition = Vector3.Lerp(currentPosition, desiredPosition, _cameraSmoothSpeed);
            _cameraTransform.position = smoothedPosition;
        }
    }
}
