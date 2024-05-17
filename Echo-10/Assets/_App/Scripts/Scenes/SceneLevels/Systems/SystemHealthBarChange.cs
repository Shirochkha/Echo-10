using Assets._App.Scripts.Scenes.SceneLevels.Features;
using System;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemHealthBarChange
    {
        private int _currentHealth;
        private int _maxHealth;
        private HealthUI _healthUI;

        public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }

        public SystemHealthBarChange(int maxHealth, HealthUI healthUI)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            _healthUI = healthUI;
            CurrentHealth = MaxHealth;
            _healthUI.UpdateHealthUI(CurrentHealth, MaxHealth);
        }       

        public void PlayerDamaged(int damageAmount)
        {
            CurrentHealth -= damageAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            _healthUI.UpdateHealthUI(CurrentHealth, MaxHealth);
            Debug.Log($"Player health: {CurrentHealth}");
        }
    }
}
