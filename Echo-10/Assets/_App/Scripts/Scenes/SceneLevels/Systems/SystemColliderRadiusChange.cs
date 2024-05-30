﻿using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemColliderRadiusChange : IUpdatable
    {
        private float _maxRadius;
        private float _duration;
        private int _maxClickCount;
        private SphereCollider _colliderComponent;
        private IPlayer _player;

        private float _originalRadius;
        private bool _isChanging = false;
        private float _elapsedTime = 0f;

        public int ClickCount { get => _player.PlayerStateOnLevel.EchoCount; set => _player.PlayerStateOnLevel.EchoCount = value; }
        public int MaxClickCount { get => _maxClickCount; set => _maxClickCount = value; }

        public SystemColliderRadiusChange(float maxRadius, float duration,
            SphereCollider colliderComponent, IPlayer player)
        {
            _maxRadius = maxRadius;
            _duration = duration;
            _colliderComponent = colliderComponent;
            _player = player;
            _originalRadius = _colliderComponent.radius;

            MaxClickCount = ClickCount;
        }

        public void Update()
        {
            if (_player.IsEchoWorking)
            {
                if (!_isChanging)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _player.UseEcho();
                        StartRadiusChange();
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        _player.UseEcho();
                        // Че-то другое attack?

                    }
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
