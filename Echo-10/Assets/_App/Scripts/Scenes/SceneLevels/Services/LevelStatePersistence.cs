using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{

    public class LevelStatePersistence : IPersistence<List<LevelState>>
    {
        private string _filePath;

        public LevelStatePersistence(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void Save(List<LevelState> levelStates)
        {
            string json = JsonUtility.ToJson(new LevelStateList { levels = levelStates }, true);
            File.WriteAllText(_filePath, json);
        }

        public List<LevelState> Load()
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
    }
}
