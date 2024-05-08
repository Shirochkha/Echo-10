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
        private GameObject _restartMenu;

        public SystemHealthBarChange(int maxHealth, HealthUI healthUI, GameObject restartMenu)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _healthUI = healthUI;
            _restartMenu = restartMenu;
            _currentHealth = _maxHealth;
            _healthUI.UpdateHealthUI(_currentHealth, _maxHealth);
        }

        public void PlayerDamaged(int damageAmount)
        {
            _currentHealth -= damageAmount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            if (_currentHealth == 0)
            {
                if (_restartMenu != null)
                {
                    _restartMenu.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }

            _healthUI.UpdateHealthUI(_currentHealth, _maxHealth);
            Debug.Log($"Player health: {_currentHealth}");
        }
    }
}
