using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenuUI : IUpdatable
{
    private GameObject _levelsMenuUI;
    private ConfigLevel _levelList;
    private Button _buttonPrefab;
    private Button _buttonMenu;
    private Button _buttonShop;
    private Button _buttonProfile;
    private Transform _parentContainer;
    private ServiceLevelSelection _levelSelectionService;
    private SceneNavigatorLoader _scenes;
    private ServiceLevelState _levelState;

    private List<Button> _levelButtons = new List<Button>();

    public GameObject LevelsMenuPanel { get => _levelsMenuUI; set => _levelsMenuUI = value; }

    public event Action<int> OnLevelButtonClicked;
    public event Action OnShopButtonClicked;
    public event Action OnProfileButtonClicked;

    public LevelsMenuUI(GameObject levelsMenuUI, ConfigLevel levelList, Button buttonPrefab, Button buttonMenu,
        Button buttonShop, Button buttonProfile, Transform parentContainer, 
        ServiceLevelSelection levelSelectionService, SceneNavigatorLoader scenes, ServiceLevelState levelState)
    {
        LevelsMenuPanel = levelsMenuUI;
        _levelList = levelList;
        _buttonPrefab = buttonPrefab;
        _buttonMenu = buttonMenu;
        _buttonShop = buttonShop;
        _buttonProfile = buttonProfile;
        _parentContainer = parentContainer;
        _levelSelectionService = levelSelectionService;
        _scenes = scenes;
        _levelState = levelState;

        _buttonMenu.onClick.AddListener(ReturnToMainMenu);
        _buttonShop.onClick.AddListener(() => OnShopButtonClicked?.Invoke());
        _buttonProfile.onClick.AddListener(() => OnProfileButtonClicked?.Invoke());
        CreateButtonsPrefabs();
    }

    public void Update()
    {
        UpdateButtonStates();
    }

    public void SubscribeToShopButtonClicked(Action action)
    {
        OnShopButtonClicked += action;
    }

    public void SubscribeToProfileButtonClicked(Action action)
    {
        OnProfileButtonClicked += action;
    }

    public void SetActiveObject(bool isActive)
    {
        LevelsMenuPanel.SetActive(isActive);
    }

    private void CreateButtonsPrefabs()
    {
        foreach (Transform child in _parentContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < _levelList.levels.Count; i++)
        {
            var level = _levelList.levels[i];
            Button instance = GameObject.Instantiate(_buttonPrefab, _parentContainer);
            Image image = instance.image;

            if (image != null)
            {
                image.sprite = level.sprite;
            }

            Text buttonText = instance.GetComponentInChildren<Text>();

            if (buttonText != null)
            {
                buttonText.text = "";
            }

            instance.interactable = false;

            instance.onClick.AddListener(() =>
            {
                LevelsMenuPanel.SetActive(false);
                _levelSelectionService.SetSelectedLevel(level.id);
                Debug.Log($"Clicked on level: {_levelSelectionService.SelectedLevelId}");
                OnLevelButtonClicked?.Invoke(level.id);
            });

            _levelButtons.Add(instance);
        }
    }

    private void UpdateButtonStates()
    {
        for (int i = 0; i < _levelList.levels.Count; i++)
        {
            Button button = _levelButtons[i];

            if (i == 0 || _levelState.IsLevelWin(_levelList.levels[i - 1].id))
            {
                button.interactable = true;
            }
        }
    }

    private void ReturnToMainMenu()
    {
        foreach (var scene in _scenes.GetAvailableSwitchScenes())
        {
            if (scene.SceneViewName == "MainMenu")
                _scenes.LoadScene(scene.SceneKey);
        }
    }
}
