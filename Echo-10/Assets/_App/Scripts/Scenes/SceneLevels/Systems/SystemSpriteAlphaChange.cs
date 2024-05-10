using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
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
        private ConfigLevel _level;
        private ServiceLevelSelection _serviceLevelSelection;

        private ConfigObjects _configObjects;
        private bool _hasSceneCreate = false;

        private bool _isMaxAlpha;

        public SystemSpriteAlphaChange(SphereCollider sphereCollider, float minAlpha, float maxAlpha, 
            float maxAlphaDuration, SystemColliderRadiusChange colliderRadiusChange, ConfigLevel level,
            ServiceLevelSelection serviceLevelSelection)
        {
            _sphereCollider = sphereCollider;
            _minAlpha = minAlpha;
            _maxAlpha = maxAlpha;
            _maxAlphaDuration = maxAlphaDuration;
            _colliderRadiusChange = colliderRadiusChange;
            _level = level;
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

            HandleMaxAlpha();
            HandleObjectVisibility();
        }

        private void HandleMaxAlpha()
        {
            if (_configObjects == null) return;

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
            if (_configObjects == null) return;

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
