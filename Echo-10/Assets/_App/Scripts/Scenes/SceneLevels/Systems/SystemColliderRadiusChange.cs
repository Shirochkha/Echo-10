using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemColliderRadiusChange : IUpdatable
    {
        private float _maxRadius;
        private float _duration;
        private int _clickCount;
        private SphereCollider _colliderComponent;

        private float _originalRadius;
        private bool _isChanging = false;
        private float _elapsedTime = 0f;

        public int ClickCount { get => _clickCount; set => _clickCount = value; }

        public SystemColliderRadiusChange(float maxRadius, float duration, int clickCount, 
            SphereCollider colliderComponent)
        {
            _maxRadius = maxRadius;
            _duration = duration;
            _clickCount = clickCount;
            _colliderComponent = colliderComponent;
            _originalRadius = _colliderComponent.radius;
        }

        public void Update()
        {
            if (_clickCount > 0 && Input.GetMouseButtonDown(0))
            {
                if (!_isChanging)
                {
                    _clickCount--;
                    StartRadiusChange();
                }
            }

            if (_isChanging)
            {
                AnimateRadiusChange();
            }
        }

        private void AnimateRadiusChange()
        {
            _elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(_elapsedTime / _duration);
            float newRadius;

            if (t <= 0.5f)
            {
                newRadius = Mathf.Lerp(_originalRadius, _maxRadius, t * 2f);
            }
            else
            {
                newRadius = Mathf.Lerp(_maxRadius, _originalRadius, (t - 0.5f) * 2f);
            }

            _colliderComponent.radius = newRadius;

            if (t >= 1f)
            {
                _isChanging = false;
                _elapsedTime = 0f;
            }
        }

        public void StartRadiusChange()
        {
            _isChanging = true;
            _elapsedTime = 0f;
        }
    }
}
