using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemSpriteAlphaChange : IUpdatable
    {
        private SphereCollider _sphereCollider;
        private float _minAlpha;
        private float _maxAlpha;
        private float _maxAlphaDuration;
        private SystemColliderRadiusChange _colliderRadiusChange;
        private ConfigObjects _configObjects;

        private bool _isMaxAlpha;

        public SystemSpriteAlphaChange(SphereCollider sphereCollider, float minAlpha, float maxAlpha, 
            float maxAlphaDuration, SystemColliderRadiusChange colliderRadiusChange, ConfigObjects configObjects)
        {
            _sphereCollider = sphereCollider;
            _minAlpha = minAlpha;
            _maxAlpha = maxAlpha;
            _maxAlphaDuration = maxAlphaDuration;
            _colliderRadiusChange = colliderRadiusChange;
            _configObjects = configObjects;
        }

        public void Update()
        {
            HandleMaxAlpha();
            HandleObjectVisibility();
        }

        private void HandleMaxAlpha()
        {
            if (_isMaxAlpha)
            {
                _maxAlphaDuration -= Time.deltaTime;
                if (_maxAlphaDuration <= 0f)
                {
                    _isMaxAlpha = false;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_colliderRadiusChange.ClickCount < 10 && _colliderRadiusChange.ClickCount > 0)
                {
                    _isMaxAlpha = true;
                    _maxAlphaDuration = 2f;
                }
            }
        }
        
        private void HandleObjectVisibility()
        {
            foreach (var otherObjectData in _configObjects.objects)
            {
                var renderer = otherObjectData.renderer;
                if (renderer == null) continue;

                var shaderName = renderer.material.shader.name;
                if (shaderName == "Unlit/Texture") continue;

                var isInsideSphere = _sphereCollider.bounds.Contains(renderer.transform.position);
                var targetAlpha = _isMaxAlpha ? _maxAlpha : (isInsideSphere ? 1f : _minAlpha);

                var currentColor = renderer.material.color;
                if (Mathf.Approximately(currentColor.a, targetAlpha)) continue;

                currentColor.a = targetAlpha;
                renderer.material.color = currentColor;
            }
        }

    }
}
