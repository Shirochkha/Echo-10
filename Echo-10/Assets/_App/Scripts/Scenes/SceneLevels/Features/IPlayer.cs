using System;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public interface IPlayer
    {
        Action<int, int> OnAddedCoins { get; set; }

        int CoinsCount { get; }
        int MaxEchoCount { get; set; }
        float DefaultForwardSpeed { get; set; }
        float ForwardSpeed { get; set; }
        bool IsEchoWorking { get; set; }
        Collider PlayerCollider { get; set; }
        GameObject PlayerGameObject { get; set; }
        Vector3 PlayerPosition { get; set; }
        PlayerStateOnLevel PlayerStateOnLevel { get; set; }
        Transform PlayerTransform { get; set; }
        Collider AttackCollider { get; set; }
        bool CanAttack { get; }

        void AddCoins(int count = 1);
        void AddHealth(int amount = 1);
        void ChangeSpeed(float newSpeed);
        void SetDefaultState(int maxCoinsCount);
        void TakeDamage(int damageAmount = 1);
        void Update();
        void UseEcho();
        void SetMemento(PlayerMemento memento);
        PlayerMemento GetMemento();
    }
}