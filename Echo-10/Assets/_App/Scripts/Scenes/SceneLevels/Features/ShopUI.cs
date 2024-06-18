using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Item = Assets._App.Scripts.Scenes.SceneLevels.Sevices.Item;

namespace Assets._App.Scripts.Scenes.SceneLevels.Features
{
    public class ShopUI
    {
        private GameObject _shopMenuObject;
        private GameObject _itemPrefab;
        private Button _buttonLevelMenu;
        private Text _allCoinsCount;
        private Transform _parentContainer;
        private IPersistence<List<Item>> _persistence;
        private IPersistence<PlayerMemento> _playerPersistence;
        private IPlayer _player;

        private List<Item> _items;
        private int _currentCoins;

        public GameObject ShopMenuObject { get => _shopMenuObject; set => _shopMenuObject = value; }

        public event Action OnLevelMenuButtonClicked;

        public ShopUI(GameObject shopMenuObject, GameObject itemPrefab, Button buttonLevelMenu, Text allCoinsCount,
            Transform parentContainer, IPersistence<List<Item>> persistence, IPlayer player, 
            IPersistence<PlayerMemento> playerPersistence)
        {
            ShopMenuObject = shopMenuObject;
            _itemPrefab = itemPrefab;
            _buttonLevelMenu = buttonLevelMenu;
            _parentContainer = parentContainer;
            _allCoinsCount = allCoinsCount;
            _persistence = persistence;
            _items = _persistence.Load() ?? new List<Item>();

            SetActiveObject(false);
            _buttonLevelMenu.onClick.AddListener(() => OnLevelMenuButtonClicked?.Invoke());
            _player = player;
            UpdateCoinsCount(_player.CoinsCount);
            _player.OnAddedCoins += (collected, max) => UpdateCoinsCount(_player.CoinsCount);
            _playerPersistence = playerPersistence;
        }

        public void SubscribeToLevelMenuButtonClicked(Action action)
        {
            OnLevelMenuButtonClicked += action;
        }

        public void SetActiveObject(bool isActive)
        {
            ShopMenuObject.SetActive(isActive);
            if (isActive)
            {
                CreateItemsPrefabs();
            }
        }

        public void UpdateCoinsCount(int currentCoins)
        {
            _currentCoins = currentCoins;
            if (_allCoinsCount != null)
            {
                _allCoinsCount.text = $"{currentCoins}";
            }
        }

        private void CreateItemsPrefabs()
        {
            foreach (Transform child in _parentContainer)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (var item in _items)
            {
                GameObject instance = GameObject.Instantiate(_itemPrefab, _parentContainer);
                Image image = instance.GetComponentInChildren<Image>();
                Text name = instance.GetComponentInChildren<Text>();
                Button button = instance.GetComponentInChildren<Button>();
                Text cost = button.GetComponentInChildren<Text>();

                if (image != null)
                {
                    Sprite sprite = LoadSprite(item.ImagePath);
                    if (sprite != null)
                    {
                        image.sprite = sprite;
                    }
                }

                if (name != null)
                {
                    name.text = item.Name;
                }

                if (cost != null)
                {
                    cost.text = item.Cost.ToString();
                }

                SetupButton(button, item);
            }
        }

        private void SetupButton(Button button, Item item)
        {
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    OnItemButtonClicked(item);
                });

                button.interactable = !item.BoughtByUser && item.Cost <= _currentCoins;
            }
        }

        private void OnItemButtonClicked(Item item)
        {
            if (!item.BoughtByUser && item.Cost <= _currentCoins)
            {
                _currentCoins -= item.Cost;
                _player.AddCoins(-item.Cost);
                item.BoughtByUser = true;
                AddItemProperty(item.Name);

                UpdateItemStates();
                _persistence.Save(_items);
                _playerPersistence.Save(_player.GetMemento());
            }
        }

        private void UpdateItemStates()
        {
            foreach (Transform child in _parentContainer)
            {
                Button button = child.GetComponentInChildren<Button>();
                if (button != null)
                {
                    Item item = _items.Find(i => i.Name == child.GetComponentInChildren<Text>().text);
                    if (item != null)
                    {
                        button.interactable = !item.BoughtByUser && item.Cost <= _currentCoins;
                    }
                }
            }
        }

        private Sprite LoadSprite(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        private void AddItemProperty(string nameItem)
        {
            if(nameItem == "Эхо-заряды +")
            {
                _player.MaxEchoCount++;
                return;
            }
        }
    }
}
