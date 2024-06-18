using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class HealthBarBossUI
    {
        private GameObject _bossUI;
        private Slider _slider;

        public GameObject BossUI { get => _bossUI; set => _bossUI = value; }

        public HealthBarBossUI(Slider slider, GameObject bossUI)
        {
            _slider = slider;
            BossUI = bossUI;
        }
        public void SetActive(bool isActive)
        {
            _bossUI.SetActive(isActive);
        }

        public void SetMaxHealth(int health)
        {
            _slider.maxValue = health;
            _slider.value = health;
        }

        public void SetHealth(int health)
        {
            _slider.value = health;
        }
    }
}
