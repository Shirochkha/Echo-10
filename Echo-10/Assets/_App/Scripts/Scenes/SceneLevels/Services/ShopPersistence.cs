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

            defaultItems.Add(new Item
            {
                Id = 0,
                Category = "Костюмы",
                Name = "Базовый",
                Cost = 0,
                ImagePath = "Skin0",
                BoughtByUser = true,
                Level = 1
            });
            defaultItems.Add(new Item { Id = 1, Category = "Костюмы", Name = "Цилиндр(светлый)", Cost = 10, 
                ImagePath = "Skin1", BoughtByUser = false, Level = 1 });
            defaultItems.Add(new Item {
                Id = 2,
                Category = "Костюмы", Name = "Цилиндр(красный)", Cost = 10, ImagePath = "Skin2",
                BoughtByUser = false, Level = 1 });
            defaultItems.Add(new Item {
                Id = 3,
                Category = "Костюмы", Name = "Цилиндр(темный)", Cost = 10, ImagePath = "Skin3",
                BoughtByUser = false, Level = 1 });
            defaultItems.Add(new Item {
                Id = 4,
                Category = "Костюмы", Name = "Джентельмен", Cost = 15, ImagePath = "Skin4",
                BoughtByUser = false, Level = 1 });
            defaultItems.Add(new Item {
                Id = 5,
                Category = "Костюмы", Name = "Сыщик", Cost = 15, ImagePath = "Skin5",
                BoughtByUser = false, Level = 1 });
            defaultItems.Add(new Item {
                Id = 6,
                Category = "Костюмы", Name = "Викинг", Cost = 20, ImagePath = "Skin6",
                BoughtByUser = false, Level = 1 });
            defaultItems.Add(new Item {
                Id = 7,
                Category = "Костюмы", Name = "Ковбой", Cost = 20, ImagePath = "Skin7",
                BoughtByUser = false, Level = 1 });
            defaultItems.Add(new Item {
                Id = 8,
                Category = "Костюмы", Name = "Наномышь", Cost = 30, ImagePath = "Skin8",
                BoughtByUser = false, Level = 1 });


            defaultItems.Add(new Item { Category = "Бонусы", Name = "Эхо-заряды +", Cost = 2, ImagePath = "bonusEcho",
                BoughtByUser = false, Level = 1, MaxLevel = 5 });
            defaultItems.Add(new Item
            {
                Category = "Бонусы",
                Name = "Атака +",
                Cost = 15,
                ImagePath = "attackBonus",
                BoughtByUser = false,
                Level = 1,
                MaxLevel = 3
            });

            return defaultItems;
        }
    }

    [Serializable]
    public class Item
    {
        public int Id;
        public string Category;
        public string Name;
        public int Cost;
        public string ImagePath;
        public bool BoughtByUser;
        public int Level;
        public int MaxLevel;
    }
}
