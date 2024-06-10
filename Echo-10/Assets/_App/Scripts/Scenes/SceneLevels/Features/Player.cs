
using _App.Scripts.Libs.Installer;
using System;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class Player : IUpdatable, IPlayer
    {
        public Action<int, int> OnAddedCoins;
        public Action<int> OnCoinsCountChanged;
        public Action<int, int> OnHealthContainerChanged;
        public Action<int> OnHealthChanged;
        public Action OnPlayerAttacked;

        private GameObject _player;
        private Rigidbody _playerRigidbody;
        private Vector3 _playerPosition;
        private Transform _playerTransform;
        private Collider _playerCollider;
        private float _speedByAxises;
        private float _defaultForwardSpeed;
        private int _coinsCount;
        private int _attackPower;
        private int _maxHealth;
        private int _maxEchoCount;
        private PlayerStateOnLevel _playerStateOnLevel;

        public Player(
            GameObject player,
            Rigidbody playerRigidbody,
            Transform playerTransform,
            Collider playerCollider,
            float speedByAxises,
            float forwardSpeed,
            int coinsCount,
            int maxHealth,
            int maxEchoCount,
            Vector3 playerPosition,
            int attackPower,
            Action<int, int> onAddedCoin,
            Action<int> onCoinsCountChanged,
            Action<int, int> onHealthContainerChanged,
            Action<int> onHealthChanged,
            Action onPlayerAttacked)
        {
            _player = player;
            _playerRigidbody = playerRigidbody;
            _playerTransform = playerTransform;
            _playerCollider = playerCollider;
            _speedByAxises = speedByAxises;
            _defaultForwardSpeed = forwardSpeed;
            _coinsCount = coinsCount;
            _maxHealth = maxHealth;
            _maxEchoCount = maxEchoCount;
            _playerPosition = playerPosition;
            _attackPower = attackPower;
            OnAddedCoins += onAddedCoin;
            OnCoinsCountChanged += onCoinsCountChanged;
            OnHealthContainerChanged += onHealthContainerChanged;
            OnHealthChanged += onHealthChanged;
            OnPlayerAttacked += onPlayerAttacked;
            SetDefaultState(3);

            OnHealthContainerChanged?.Invoke(_playerStateOnLevel.CurrentHealth, _maxHealth);
            OnCoinsCountChanged?.Invoke(_coinsCount);
            OnAddedCoins?.Invoke(_playerStateOnLevel.CoinsCollected, _playerStateOnLevel.MaxCoinsCount);
        }

        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }
        public Collider PlayerCollider { get => _playerCollider; set => _playerCollider = value; }
        public PlayerStateOnLevel PlayerStateOnLevel { get => _playerStateOnLevel; set => _playerStateOnLevel = value; }
        public GameObject PlayerGameObject { get => _player; set => _player = value; }
        public Vector3 PlayerPosition { get => _playerPosition; set => _playerPosition = value; }
        public float ForwardSpeed { get => _playerStateOnLevel.ForwardSpeed; set => _playerStateOnLevel.ForwardSpeed = value; }
        public float DefaultForwardSpeed { get => _defaultForwardSpeed; set => _defaultForwardSpeed = value; }
        public bool IsEchoWorking { get; set; } = true;

        public void Update()
        {
            UpdatePosition();
            // AttackIfTime();
        }

        private void UpdatePosition()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new(horizontalInput, verticalInput, 0f);
            _playerRigidbody.velocity = movement * _speedByAxises;
            _playerTransform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime);
        }

        //TODO: и эхо и атака
        public void UseEcho()
        {
            if (_playerStateOnLevel.EchoCount > 0)
            {
                _playerStateOnLevel.EchoCount--;
            }
        }

        public void SubscribeToShop(ShopUI shopUI)
        {
            shopUI.OnItemPurchased += UpdateCoinsAfterPurchase;
        }

        private void UpdateCoinsAfterPurchase(int newCoinsCount)
        {
            _coinsCount = newCoinsCount;
            OnCoinsCountChanged?.Invoke(_coinsCount);
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
            _playerStateOnLevel = new PlayerStateOnLevel(false, 0, maxCoinsCount, _maxHealth, _maxHealth,
                _maxEchoCount, 0f);
            OnAddedCoins?.Invoke(0, maxCoinsCount);
            OnHealthChanged?.Invoke(_maxHealth);
            _playerTransform.position = _playerPosition;
        }

        public void ChangeSpeed(float newSpeed)
        {
            ForwardSpeed = newSpeed;
        }

        public void SetMemento(PlayerMemento memento)
        {
            _coinsCount = memento.CoinsCount;
            _maxEchoCount = memento.MaxEchoCount;
            _maxHealth = memento.MaxHealth;
            _attackPower = memento.AttackPower;
            OnCoinsCountChanged?.Invoke(_coinsCount);
        }

        public PlayerMemento GetMemento()
        {
            return new PlayerMemento(_coinsCount, _maxHealth, _maxEchoCount, _attackPower);
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

        public PlayerMemento(int coinsCount, int maxHealth, int maxEchoCount, int attackPower)
        {
            CoinsCount = coinsCount;
            MaxHealth = maxHealth;
            MaxEchoCount = maxEchoCount;
            AttackPower = attackPower;
        }
    }
}
