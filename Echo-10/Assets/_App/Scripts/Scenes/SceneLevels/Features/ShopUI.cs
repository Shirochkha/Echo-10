using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
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
        private Button _skinsButton;
        private Button _bonusesButton;
        private IPersistence<List<Item>> _persistence;
        private IPersistence<PlayerMemento> _playerPersistence;
        private IPlayer _player;

        private List<Item> _items;
        private int _currentCoins;
        private string _currentCategory = "Костюмы";

        public GameObject ShopMenuObject { get => _shopMenuObject; set => _shopMenuObject = value; }

        public event Action OnLevelMenuButtonClicked;

        public ShopUI(GameObject shopMenuObject, GameObject itemPrefab, Button buttonLevelMenu, Text allCoinsCount,
            Transform parentContainer, Button skinsButton, Button bonusesButton, IPersistence<List<Item>> persistence, 
            IPlayer player, IPersistence<PlayerMemento> playerPersistence)
        {
            ShopMenuObject = shopMenuObject;
            _itemPrefab = itemPrefab;
            _buttonLevelMenu = buttonLevelMenu;
            _parentContainer = parentContainer;
            _allCoinsCount = allCoinsCount;
            _skinsButton = skinsButton;
            _bonusesButton = bonusesButton;
            _persistence = persistence;
            _items = _persistence.Load() ?? new List<Item>();
            _items = _persistence.Load()?.Skip(1).ToList() ?? new List<Item>();

            SetActiveObject(false);
            _buttonLevelMenu.onClick.AddListener(() => OnLevelMenuButtonClicked?.Invoke());
            _skinsButton.onClick.AddListener(() => SwitchCategory("Костюмы"));
            _bonusesButton.onClick.AddListener(() => SwitchCategory("Бонусы"));
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
                if (item.Category == _currentCategory)
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
                        if (item.Category == "Костюмы")
                        {
                            name.text = item.Name;
                        }
                        else if (item.Category == "Бонусы")
                        {
                            if (item.Level > item.MaxLevel)
                            {
                                name.text = $"{item.Name} \n (Макс. уровень)";
                                button.gameObject.SetActive(false);
                            }
                            else
                            {
                                name.text = $"{item.Name} \n (Уровень {item.Level})";
                            }
                        }
                    }

                    if (cost != null)
                    {
                        cost.text = item.Cost.ToString();
                    }

                    SetupButton(button, item);
                }
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

                button.interactable = !item.BoughtByUser && item.Cost <= _currentCoins ||
                    item.Level <= item.MaxLevel && item.Cost <= _currentCoins;
            }
        }


        private void OnItemButtonClicked(Item item)
        {
            if (!item.BoughtByUser && item.Cost <= _currentCoins ||
                    item.Level <= item.MaxLevel && item.Cost <= _currentCoins)
            {
                _currentCoins -= item.Cost;
                _player.AddCoins(-item.Cost);
                item.BoughtByUser = true;
                AddItemProperty(item.Name, item.Category, item.Id);

                if(item.Category == "Бонусы")
                {
                    item.Level++;
                    item.Cost *= 2;
                    UpdateItemText(item);
                }

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
                Text name = child.GetComponentInChildren<Text>();

                if (button != null && name != null)
                {
                    Item item = _items.Find(i => name.text.Contains(i.Name));
                    if (item != null)
                    {
                        button.interactable = !item.BoughtByUser && item.Cost <= _currentCoins ||
                            item.Category == "Бонусы" && item.Level <= item.MaxLevel && item.Cost <= _currentCoins;

                        if (item.Level > item.MaxLevel && item.Category == "Бонусы")
                        {
                            button.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }


        private void UpdateItemText(Item item)
        {
            foreach (Transform child in _parentContainer)
            {
                Text name = child.GetComponentInChildren<Text>();
                Button button = child.GetComponentInChildren<Button>();
                Text cost = button?.GetComponentInChildren<Text>();

                if (name != null && cost != null && button != null)
                {
                    if (name.text.Contains(item.Name))
                    {
                        if (item.Category == "Бонусы")
                        {
                            if (item.Level > item.MaxLevel)
                            {
                                name.text = $"{item.Name} \n (Макс. уровень)";
                                button.gameObject.SetActive(false);
                            }
                            else
                            {
                                name.text = $"{item.Name} \n (Уровень {item.Level})";
                                cost.text = item.Cost.ToString();
                            }
                        }
                    }
                }
            }
        }


        private Sprite LoadSprite(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        private void AddItemProperty(string nameItem, string category, int id)
        {
            if (nameItem == "Эхо-заряды +")
            {
                _player.MaxEchoCount++;
            }
            if (nameItem == "Атака +")
            {
                _player.AttackPower++;
            }
            else if (category == "Костюмы")
            {
                _player.SkinId = id;

                _player.PlayerAnimator.SetInteger("Skin", _player.SkinId);
            }
            
        }

        private void SwitchCategory(string category)
        {
            _currentCategory = category;
            CreateItemsPrefabs();
        }
    }
}
