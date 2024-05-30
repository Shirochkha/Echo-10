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

        public virtual void SetDefaultState(int maxCoinsCount)
        {
            _player.SetDefaultState(maxCoinsCount);
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