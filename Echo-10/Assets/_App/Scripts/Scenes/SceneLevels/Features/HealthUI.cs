using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class HealthUI
    {
        private Transform _container;
        private Sprite _fullHeartSprite;
        private Sprite _emptyHeartSprite;
        private List<Image> _heartImages;

        public HealthUI(Transform container, Sprite fullHeartSprite, Sprite emptyHeartSprite)
        {
            _container = container;
            _fullHeartSprite = fullHeartSprite;
            _emptyHeartSprite = emptyHeartSprite;
            _heartImages = new List<Image>();
        }

        public void UpdateHealthUI(int currentHealth, int maxHealth)
        {
            while (_heartImages.Count < maxHealth)
            {
                var newHeart = new GameObject("Heart");
                var imageComponent = newHeart.AddComponent<Image>();
                imageComponent.sprite = _fullHeartSprite;
                newHeart.transform.SetParent(_container, false);
                _heartImages.Add(imageComponent);
            }

            while (_heartImages.Count > maxHealth)
            {
                var heartToRemove = _heartImages[_heartImages.Count - 1];
                _heartImages.RemoveAt(_heartImages.Count - 1);
                GameObject.Destroy(heartToRemove.gameObject);
            }

            for (int i = 0; i < _heartImages.Count; i++)
            {
                _heartImages[i].sprite = i < currentHealth ? _fullHeartSprite : _emptyHeartSprite;
            }
        }
    }
}
