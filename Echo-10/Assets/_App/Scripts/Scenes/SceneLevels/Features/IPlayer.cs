using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public interface IPlayer
    {
        float DefaultForwardSpeed { get; set; }
        float ForwardSpeed { get; set; }
        bool IsEchoWorking { get; set; }
        Collider PlayerCollider { get; set; }
        GameObject PlayerGameObject { get; set; }
        Vector3 PlayerPosition { get; set; }
        PlayerStateOnLevel PlayerStateOnLevel { get; set; }
        Transform PlayerTransform { get; set; }

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