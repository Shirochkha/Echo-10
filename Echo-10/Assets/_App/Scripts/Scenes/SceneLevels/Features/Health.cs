using _App.Scripts.Libs.Installer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class Health : IInitializable
    {
        private Image[] _heartImages;
        private Sprite _fullHeartSprite;
        private Sprite _emptyHeartSprite;
        private int _maxHealth;
        private GameObject _restartMenu;

        private int _currentHealth;

        public Health(Image[] heartImages, Sprite fullHeartSprite, Sprite emptyHeartSprite, int maxHealth, 
            GameObject restartMenu)
        {
            _heartImages = heartImages;
            _fullHeartSprite = fullHeartSprite;
            _emptyHeartSprite = emptyHeartSprite;
            _maxHealth = maxHealth;
            _restartMenu = restartMenu;
        }

        public void Init()
        {
            _currentHealth = _maxHealth;
            UpdateHealthUI();
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            if (_currentHealth == 0)
            {
                if (_restartMenu != null)
                {
                    _restartMenu.SetActive(true);
                    Time.timeScale = 0.0f;
                }
            }

            UpdateHealthUI();
            Debug.Log(_currentHealth);
        }

        public void UpdateHealthUI()
        {
            for (int i = 0; i < _heartImages.Length; i++)
            {
                if (i < _currentHealth)
                {
                    _heartImages[i].sprite = _fullHeartSprite;
                }
                else
                {
                    _heartImages[i].sprite = _emptyHeartSprite;
                }
            }
        }
    }
}
