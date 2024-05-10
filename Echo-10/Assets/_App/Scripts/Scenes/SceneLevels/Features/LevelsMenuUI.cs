using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class LevelsMenuUI
    {
        private ConfigLevel _levelList;
        private Button _buttonPrefab;
        private Transform _parentContainer;
        private ServiceLevelSelection _levelSelectionService;

        public LevelsMenuUI(ConfigLevel levelList, Button buttonPrefab, Transform parentContainer,
            ServiceLevelSelection levelSelectionService)
        {
            _levelList = levelList;
            _buttonPrefab = buttonPrefab;
            _parentContainer = parentContainer;
            _levelSelectionService = levelSelectionService;

            CreateButtonsPrefabs();
        }

        void CreateButtonsPrefabs()
        {
            foreach (Transform child in _parentContainer)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (var level in _levelList.levels)
            {
                Button instance = GameObject.Instantiate(_buttonPrefab, _parentContainer);
                Text buttonText = instance.GetComponentInChildren<Text>();

                if (buttonText != null)
                {
                    buttonText.text = level.id.ToString();
                }

                instance.onClick.AddListener(() =>
                {
                    _levelSelectionService.SetSelectedLevel(level.id);
                    Debug.Log($"Clicked on level: {_levelSelectionService.SelectedLevelId}");
                });
            }
        }
    }
}
