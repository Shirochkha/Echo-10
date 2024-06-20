using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class ProfileUI
    {
        private GameObject _profileMenuObject;
        private Button _buttonLevelMenu;
        private Text _profileInfo;
        private Image _skinImage;
        private Text _skinNameText;
        private Button _chooseButton;
        private Button _prevButton;
        private Button _nextButton;

        private IPersistence<List<Item>> _persistence;
        private IPlayer _player;
        private IPersistence<PlayerMemento> _playerPersistence;
        public GameObject ProfileMenuObject { get => _profileMenuObject; set => _profileMenuObject = value; }

        public event Action OnLevelMenuButtonClicked;

        private List<Item> _ownedSkins = new();
        private List<Item> _allItems = new();
        private int _currentSkinIndex = 0;

        public ProfileUI(GameObject profileMenuObject, Button buttonLevelMenu, Text profileInfo,
            Image skinImage, Text skinNameText, Button chooseButton, Button prevButton, Button nextButton,
            IPersistence<List<Item>> persistence, IPlayer player, IPersistence<PlayerMemento> playerPersistence)
        {
            ProfileMenuObject = profileMenuObject;
            _buttonLevelMenu = buttonLevelMenu;
            _profileInfo = profileInfo;
            _skinImage = skinImage;
            _skinNameText = skinNameText;
            _chooseButton = chooseButton;
            _prevButton = prevButton;
            _nextButton = nextButton;
            _persistence = persistence;

            SetActiveObject(false);
            _buttonLevelMenu.onClick.AddListener(() => OnLevelMenuButtonClicked?.Invoke());
            _player = player;
            _playerPersistence = playerPersistence;

            _chooseButton.onClick.AddListener(() => OnChooseButtonClick());
            _prevButton.onClick.AddListener(() => OnPrevButtonClick());
            _nextButton.onClick.AddListener(() => OnNextButtonClick());

            _allItems = _persistence.Load() ?? new List<Item>();
        }

        public void SubscribeToLevelMenuButtonClicked(Action action)
        {
            OnLevelMenuButtonClicked += action;
        }

        public void SetActiveObject(bool isActive)
        {
            ProfileMenuObject.SetActive(isActive);
            if (isActive)
            {
                UpdateProfileInfo();
                LoadOwnedSkins();
                foreach (var item in _allItems)
                {
                    if (!(item.Category == "Костюмы") || !item.BoughtByUser) continue;
                    if (item.Id == _player.SkinId)
                    {
                        foreach (var itemOwned in _ownedSkins)
                        {
                            if (item.Name == itemOwned.Name)
                            {
                                _currentSkinIndex = _ownedSkins.IndexOf(itemOwned);
                                break;
                            }

                        }
                    }

                }
                UpdateUI();
            }
        }

        private void UpdateProfileInfo()
        {
            string currentSkinName = "";

            _ownedSkins = _persistence.Load() ?? new List<Item>();
            foreach (var item in _ownedSkins)
            {
                if (item.Id == _player.SkinId)
                {
                    currentSkinName = item.Name;
                    break;
                }
            }

            _profileInfo.text = $"Монеты: {_player.CoinsCount}\n" +
                $"Здоровье: {_player.MaxHealth}\n" +
                $"Эхо-заряды: {_player.MaxEchoCount}\n" +
                $"Урон: {_player.AttackPower}\n" +
                $"Костюм: {currentSkinName}\n";
        }

        private void LoadOwnedSkins()
        {
            _allItems = _persistence.Load() ?? new List<Item>();
            _ownedSkins.Clear();

            foreach (var item in _allItems)
            {
                if (item.Category == "Костюмы" && item.BoughtByUser)
                {
                    _ownedSkins.Add(item);
                }
            }
            
        }

        private void UpdateUI()
        {
            if (_ownedSkins.Count == 0)
            {
                Debug.LogError("No items loaded.");
                return;
            }

            UpdateSkinInfo();
            UpdateButtons();
        }

        private void UpdateSkinInfo()
        {
            if (_currentSkinIndex < 0 || _currentSkinIndex >= _ownedSkins.Count)
            {
                Debug.LogError("Invalid skin index.");
                return;
            }

            Item currentSkin = _ownedSkins[_currentSkinIndex];
            _skinNameText.text = currentSkin.Name;
            Sprite sprite = LoadSprite(currentSkin.ImagePath);
            if (sprite != null)
            {
                _skinImage.sprite = sprite;
            }

            bool isSelected = currentSkin.Id == _player.SkinId;
            _chooseButton.interactable = !isSelected;
            _chooseButton.GetComponentInChildren<Text>().text = isSelected ? "Выбрано" : "Выбрать";
        }

        private void UpdateButtons()
        {
            _prevButton.interactable = _currentSkinIndex > 0;
            _nextButton.interactable = _currentSkinIndex < _ownedSkins.Count - 1;

            _prevButton.gameObject.SetActive(_currentSkinIndex > 0);
            _nextButton.gameObject.SetActive(_currentSkinIndex < _ownedSkins.Count - 1);
        }

        private Sprite LoadSprite(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        public void OnPrevButtonClick()
        {
            if (_currentSkinIndex > 0)
            {
                _currentSkinIndex--;
                UpdateUI();
            }
        }

        public void OnNextButtonClick()
        {
            if (_currentSkinIndex < _ownedSkins.Count - 1)
            {
                _currentSkinIndex++;
                UpdateUI();
            }
        }

        public void OnChooseButtonClick()
        {
            if (_currentSkinIndex >= 0 && _currentSkinIndex < _ownedSkins.Count)
            {
                Item selectedSkin = _ownedSkins[_currentSkinIndex];
                _player.SkinId = selectedSkin.Id;
                _player.PlayerAnimator.SetInteger("Skin", _player.SkinId);

                UpdateUI();

                _playerPersistence.Save(_player.GetMemento());
            }
            else
            {
                Debug.LogError("Invalid skin index.");
            }
        }
    }
}
