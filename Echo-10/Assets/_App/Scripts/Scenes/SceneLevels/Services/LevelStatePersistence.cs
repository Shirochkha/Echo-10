using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    /*public class LevelStatePersistence
    {
        private string _filePath;

        public LevelStatePersistence(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void SaveLevelStates(List<LevelState> levelStates)
        {
            string json = JsonUtility.ToJson(new LevelStateList { levels = levelStates }, true);
            File.WriteAllText(_filePath, json);
        }

        public List<LevelState> LoadLevelStates()
        {
            if (!File.Exists(_filePath))
            {
                return null;
            }

            string json = File.ReadAllText(_filePath);
            LevelStateList levelStateList = JsonUtility.FromJson<LevelStateList>(json);
            return levelStateList.levels;
        }

        [Serializable]
        private class LevelStateList
        {
            public List<LevelState> levels;
        }
    }*/

    public interface IPersistence<T>
    {
        T Load();
        void Save(T obj);
    }

    public class LevelStatePersistence : IPersistence<List<LevelState>>
    {
        private string _filePath;

        public LevelStatePersistence(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public List<LevelState> Load()
        {
            if (!File.Exists(_filePath))
            {
                //create d4efault
                return null;
            }

            string json = File.ReadAllText(_filePath);
            LevelStateList levelStateList = JsonUtility.FromJson<LevelStateList>(json);
            return levelStateList.levels;
        }

        public void Save(List<LevelState> obj)
        {
            string json = JsonUtility.ToJson(new LevelStateList { levels = obj }, true);
            File.WriteAllText(_filePath, json);
        }

        [Serializable]
        private class LevelStateList
        {
            public List<LevelState> levels;
        }
    }

    [Serializable]
    public class Item
    {
        public string Name;
        public int Cost;
        //public int Level;
        //public int MaxLevel;
        //TODO: картинка по пути
        public Sprite Image;
        public string ImagePath;
        public bool BoughtByUser;
    }

    [Serializable]
    public class Shop
    {
        public List<Item> Items = new();
    }
}
