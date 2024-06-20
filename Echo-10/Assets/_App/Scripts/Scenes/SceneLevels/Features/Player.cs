
using _App.Scripts.Libs.Installer;
using System;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{

    public class Player : IUpdatable, IPlayer
    {
        public Action<int, int> OnAddedCoins { get; set; }
        public Action<int, int> OnHealthContainerChanged;
        public Action<int> OnHealthChanged;
        public Action OnPlayerAttacked;

        private GameObject _player;
        private Rigidbody _playerRigidbody;
        private Vector3 _playerPosition;
        private Transform _playerTransform;
        private Collider _playerCollider;
        private Collider _attackCollider;
        private Animator _playerAnimator;
        private float _speedByAxises;
        private float _defaultForwardSpeed;
        private int _coinsCount;
        private int _attackPower;
        private int _maxHealth;
        private int _maxEchoCount;
        private PlayerStateOnLevel _playerStateOnLevel;
        private float _elapsedTime;
        private int _skinId;

        public Player(
            GameObject player,
            Rigidbody playerRigidbody,
            Transform playerTransform,
            Collider playerCollider,
            Collider attackCollider,
            Animator playerAnimator,
            float speedByAxises,
            float forwardSpeed,
            int coinsCount,
            int maxHealth,
            int maxEchoCount,
            Vector3 playerPosition,
            int attackPower,
            int skinId,
            Action<int, int> onAddedCoin,
            Action<int, int> onHealthContainerChanged,
            Action<int> onHealthChanged,
            Action onPlayerAttacked,
            float attackDelay)
        {
            _player = player;
            _playerRigidbody = playerRigidbody;
            _playerTransform = playerTransform;
            _playerCollider = playerCollider;
            _attackCollider = attackCollider;
            _playerAnimator = playerAnimator;
            _speedByAxises = speedByAxises;
            _defaultForwardSpeed = forwardSpeed;
            _coinsCount = coinsCount;
            _maxHealth = maxHealth;
            MaxEchoCount = maxEchoCount;
            _playerPosition = playerPosition;
            AttackPower = attackPower;
            _skinId = skinId;
            OnAddedCoins += onAddedCoin;
            OnHealthContainerChanged += onHealthContainerChanged;
            OnHealthChanged += onHealthChanged;
            OnPlayerAttacked += onPlayerAttacked;
            SetDefaultState(3);

            OnHealthContainerChanged?.Invoke(_playerStateOnLevel.CurrentHealth, MaxHealth);
            OnAddedCoins?.Invoke(_playerStateOnLevel.CoinsCollected, _playerStateOnLevel.MaxCoinsCount);
            AttackDelay = attackDelay;
        }

        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
        public Collider PlayerCollider { get => _playerCollider; set => _playerCollider = value; }
        public PlayerStateOnLevel PlayerStateOnLevel { get => _playerStateOnLevel; set => _playerStateOnLevel = value; }
        public GameObject PlayerGameObject { get => _player; set => _player = value; }
        public Vector3 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }
        public float ForwardSpeed { get => _playerStateOnLevel.ForwardSpeed; set => _playerStateOnLevel.ForwardSpeed = value; }
        public float DefaultForwardSpeed { get => _defaultForwardSpeed; set => _defaultForwardSpeed = value; }
        public bool IsEchoWorking { get; set; } = true;
        public int CoinsCount { get => _coinsCount; }
        public int MaxEchoCount { get => _maxEchoCount; set => _maxEchoCount = value; }
        public Collider AttackCollider { get => _attackCollider; set => _attackCollider = value; }
        public Animator PlayerAnimator { get => _playerAnimator; set => _playerAnimator = value; }
        public float AttackDelay { get; }
        public bool CanAttack => _elapsedTime > AttackDelay && _playerStateOnLevel.EchoCount > 0;

        public int SkinId { get => _skinId; set => _skinId = value; }
        public int AttackPower { get => _attackPower; set => _attackPower = value; }
        public int MaxHealth { get => _maxHealth; }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new(horizontalInput, verticalInput, 0f);
            _playerRigidbody.velocity = movement * _speedByAxises;
            _playerTransform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime);
        }

        public void UseEcho()
        {
            if (CanAttack)
            {
                _playerStateOnLevel.EchoCount--;
                _elapsedTime = 0;
            }
        }

        public void AddCoins(int count = 1)
        {
            _coinsCount += count;
            _playerStateOnLevel.CoinsCollected += count;
            OnAddedCoins?.Invoke(_playerStateOnLevel.CoinsCollected, _playerStateOnLevel.MaxCoinsCount);
        }

        public void TakeDamage(int damageAmount = 1)
        {
            _playerStateOnLevel.CurrentHealth -= damageAmount;
            if (_playerStateOnLevel.CurrentHealth < 0)
            {
                _playerStateOnLevel.CurrentHealth = 0;
            }

            OnHealthChanged?.Invoke(_playerStateOnLevel.CurrentHealth);
        }

        public void AddHealth(int amount = 1)
        {
            _playerStateOnLevel.CurrentHealth += amount;
            if (_playerStateOnLevel.CurrentHealth > _playerStateOnLevel.MaxHealth)
            {
                _playerStateOnLevel.CurrentHealth = _playerStateOnLevel.MaxHealth;
            }

            OnHealthChanged?.Invoke(_playerStateOnLevel.CurrentHealth);
        }

        public void SetDefaultState(int maxCoinsCount)
        {
            _playerStateOnLevel = new PlayerStateOnLevel(false, 0, maxCoinsCount, MaxHealth, MaxHealth,
                MaxEchoCount, 0f);
            OnAddedCoins?.Invoke(0, maxCoinsCount);
            OnHealthChanged?.Invoke(MaxHealth);
            _playerTransform.position = _playerPosition;
            _elapsedTime = AttackDelay;
        }

        public void ChangeSpeed(float newSpeed)
        {
            ForwardSpeed = newSpeed;
        }

        public void SetMemento(PlayerMemento memento)
        {
            _coinsCount = memento.CoinsCount;
            MaxEchoCount = memento.MaxEchoCount;
            _maxHealth = memento.MaxHealth;
            AttackPower = memento.AttackPower;
            SkinId = memento.SkinId;
            PlayerAnimator.SetInteger("Skin", memento.SkinId);
        }

        public PlayerMemento GetMemento()
        {
            return new PlayerMemento(_coinsCount, MaxHealth, MaxEchoCount, AttackPower, SkinId);
        }
    }

    public class PlayerStateOnLevel
    {
        public bool IsWin { get; set; }
        public int CoinsCollected { get; set; }
        public int MaxCoinsCount { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int EchoCount { get; set; }
        public float ForwardSpeed { get; set; }

        public PlayerStateOnLevel(bool isWin, int coinsCollected, int maxCoinsCount, int currentHealth, int maxHealth, int echoCount, float forwardSpeed)
        {
            IsWin = isWin;
            CoinsCollected = coinsCollected;
            MaxCoinsCount = maxCoinsCount;
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
            EchoCount = echoCount;
            ForwardSpeed = forwardSpeed;
        }
    }

    [Serializable]
    public class PlayerMemento
    {
        public int CoinsCount;
        public int MaxHealth;
        public int MaxEchoCount;
        public int AttackPower;
        public int SkinId;

        public PlayerMemento(int coinsCount, int maxHealth, int maxEchoCount, int attackPower,
            int skinId)
        {
            CoinsCount = coinsCount;
            MaxHealth = maxHealth;
            MaxEchoCount = maxEchoCount;
            AttackPower = attackPower;
            SkinId = skinId;
        }
    }
}
