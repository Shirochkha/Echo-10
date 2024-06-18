using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class Boss : IUpdatable
    {
        private ServiceLevelState _serviceLevelState;
        private HealthBarBossUI _healthBarBossUI;
        private float _directionChangeInterval;
        private float _rayDistance;
        private float _minTimeBetweenDirectionChanges;

        private GameObject _gameObject;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private Collider _attackCollider;
        private Animator _attackAnimator;
        private float _elapsedTime;
        private float _directionChangeTimer;
        private float _lastDirectionChangeTime;
        private Vector3 _currentDirection;

        public int Damage { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; }
        public int AttackDelay { get; set; }
        public float SpeedByAxises { get; set; }
        public float SpeedForward { get; set; }
        public bool CanAttack => _gameObject is not null && _elapsedTime > AttackDelay;
        public bool StartAnimation => _gameObject is not null && _elapsedTime > AttackDelay - 1;

        public Collider Collider { get => _collider; set => _collider = value; }
        public Collider AttackCollider { get => _attackCollider; set => _attackCollider = value; }
        public Animator AttackAnimator { get => _attackAnimator; set => _attackAnimator = value; }

        public Boss(
            ServiceLevelState serviceLevelState,
            HealthBarBossUI healthBarBossUI,
            float directionChangeInterval,
            float rayDistance,
            float minTimeBetweenDirectionChanges,
            int damage,
            int health,
            int attackDelay,
            float speedByAxises,
            float speedForward)
        {
            _serviceLevelState = serviceLevelState;
            _healthBarBossUI = healthBarBossUI;
            _directionChangeInterval = directionChangeInterval;
            _rayDistance = rayDistance;
            _minTimeBetweenDirectionChanges = minTimeBetweenDirectionChanges;
            Damage = damage;
            MaxHealth = health;
            Health = MaxHealth;
            AttackDelay = attackDelay;
            SpeedByAxises = speedByAxises;
            SpeedForward = speedForward;

            _healthBarBossUI.SetMaxHealth(Health);
            ChangeDirection();
        }

        public void SetDefault()
        {
            Health = MaxHealth;
            _elapsedTime = 0;
            _healthBarBossUI.SetHealth(Health);
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;
            UpdateBossInfo();
            UpdatePosition();
        }

        public void Attack()
        {
            _elapsedTime = 0;
        }

        public void TakeDamage(int damageAmount = 1)
        {
            Health -= damageAmount;
            _healthBarBossUI.SetHealth(Health);
        }

        private void UpdateBossInfo()
        {
            if (_serviceLevelState.ConfigObjects != null)
            {
                foreach (var obj in _serviceLevelState.ConfigObjects.objects)
                {
                    if (obj.objectType == ObjectType.Boss && obj.objectReference != null)
                    {
                        _gameObject = obj.objectReference;
                        _transform = obj.objectReference.transform;
                        _rigidbody = obj.rigidbody;
                        Collider = obj.objectReference.GetComponent<Collider>();
                        var childColliders = obj.objectReference.GetComponentsInChildren<Collider>();
                        foreach (var collider in childColliders)
                        {
                            if (collider.gameObject != obj.objectReference)
                            {
                                AttackCollider = collider;
                                break;
                            }
                        }

                        AttackAnimator = AttackCollider.gameObject.GetComponentInChildren<Animator>();

                        _healthBarBossUI.SetActive(true);
                    }
                    else
                    {
                        _healthBarBossUI.SetActive(false);
                    }
                }
            }
        }

        private void UpdatePosition()
        {
            if (_rigidbody == null || _transform == null) return;

            _directionChangeTimer += Time.deltaTime;
            if (_directionChangeTimer >= _directionChangeInterval)
            {
                _directionChangeTimer = 0f;
                ChangeDirection();
            }

            if ((Time.time - _lastDirectionChangeTime >= _minTimeBetweenDirectionChanges) &&
                (IsObstacleAhead(Vector3.left) || IsObstacleAhead(Vector3.right) || IsObstacleAhead(Vector3.up) ||
                IsObstacleAhead(Vector3.down)))
            {
                ChangeDirection();
                _lastDirectionChangeTime = Time.time;
            }

            Vector3 movement = new Vector3(_currentDirection.x * SpeedByAxises, _currentDirection.y * SpeedByAxises,
                0);
            _rigidbody.velocity = new Vector3(movement.x, movement.y, 0);

            _transform.Translate(Vector3.forward * SpeedForward * Time.deltaTime);
        }

        private void ChangeDirection()
        {
            float randomHorizontal = Random.Range(-1f, 1f);
            float randomVertical = Random.Range(-1f, 1f);

            _currentDirection = new Vector3(randomHorizontal, randomVertical, 0f).normalized;
        }

        private bool IsObstacleAhead(Vector3 direction)
        {
            Ray ray = new Ray(_transform.position, direction);
            return Physics.Raycast(ray, _rayDistance);
        }
    }
}
