using System;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public abstract class BasePlayerDecorator : IPlayer
    {
        protected readonly IPlayer _player;

        protected BasePlayerDecorator(IPlayer player)
        {
            _player = player;
        }

        public virtual float DefaultForwardSpeed { get => _player.DefaultForwardSpeed; set => _player.DefaultForwardSpeed = value; }
        public virtual float ForwardSpeed { get => _player.ForwardSpeed; set => _player.ForwardSpeed = value; }
        public virtual Collider PlayerCollider { get => _player.PlayerCollider; set => _player.PlayerCollider = value; }
        public virtual GameObject PlayerGameObject { get => _player.PlayerGameObject; set => _player.PlayerGameObject = value; }
        public virtual Vector3 PlayerPosition { get => _player.PlayerPosition; set => _player.PlayerPosition = value; }
        public virtual PlayerStateOnLevel PlayerStateOnLevel { get => _player.PlayerStateOnLevel; set => _player.PlayerStateOnLevel = value; }
        public virtual Transform PlayerTransform { get => _player.PlayerTransform; set => _player.PlayerTransform = value; }
        public virtual bool IsEchoWorking { get => _player.IsEchoWorking; set => _player.IsEchoWorking = value; }
        public virtual Collider AttackCollider { get => _player.AttackCollider; set => _player.AttackCollider = value; }
        public int CoinsCount => _player.CoinsCount;
        public int MaxEchoCount { get => _player.MaxEchoCount; set => _player.MaxEchoCount = value; }
        public Action<int, int> OnAddedCoins { get => _player.OnAddedCoins; set => _player.OnAddedCoins = value; }

        public bool CanAttack => _player.CanAttack;

        public virtual void AddCoins(int count = 1)
        {
            _player.AddCoins(count);
        }

        public virtual void AddHealth(int amount = 1)
        {
            _player.AddHealth(amount);
        }

        public virtual void ChangeSpeed(float newSpeed)
        {
            _player.ChangeSpeed(newSpeed);
        }

        public PlayerMemento GetMemento()
        {
            return _player.GetMemento();
        }

        public virtual void SetDefaultState(int maxCoinsCount)
        {
            _player.SetDefaultState(maxCoinsCount);
        }

        public void SetMemento(PlayerMemento memento)
        {
            _player.SetMemento(memento);
        }

        public virtual void TakeDamage(int damageAmount = 1)
        {
            _player.TakeDamage(damageAmount);
        }

        public virtual void Update()
        {
            _player.Update();
        }

        public virtual void UseEcho()
        {
            _player.UseEcho();
        }
    }
}