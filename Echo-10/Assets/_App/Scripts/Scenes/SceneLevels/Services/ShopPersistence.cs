using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    public class ShopPersistence : IPersistence<List<Item>>
    {
        private string _filePath;

        public ShopPersistence(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public List<Item> Load()
        {
            if (!File.Exists(_filePath))
            {
                List<Item> defaultItems = CreateDefaultItems();
                Save(defaultItems);
                return defaultItems;
            }

            string json = File.ReadAllText(_filePath);
            Shop shopList = JsonUtility.FromJson<Shop>(json);
            return shopList.Items;
        }

        public void Save(List<Item> items)
        {
            string json = JsonUtility.ToJson(new Shop { Items = items }, true);
            File.WriteAllText(_filePath, json);
        }

        [Serializable]
        public class Shop
        {
            public List<Item> Items = new();
        }

        private List<Item> CreateDefaultItems()
        {
            List<Item> defaultItems = new List<Item>();

            defaultItems.Add(new Item { Name = "Костюм", Cost = 15, ImagePath = "bat", BoughtByUser = false });
            defaultItems.Add(new Item { Name = "Эхо-заряды +", Cost = 1, ImagePath = "bonusEcho", BoughtByUser = false });

            return defaultItems;
        }
    }

    [Serializable]
    public class Item
    {
        public string Name;
        public int Cost;
        public string ImagePath;
        public bool BoughtByUser;
    }
}
