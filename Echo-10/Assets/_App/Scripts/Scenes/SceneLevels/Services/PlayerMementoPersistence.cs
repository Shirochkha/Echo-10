using Assets._App.Scripts.Scenes.SceneLevels.Features;
using System.IO;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    public class PlayerMementoPersistence : IPersistence<PlayerMemento>
    {
        private string _filePath;

        public PlayerMementoPersistence(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void Save(PlayerMemento obj)
        {
            string json = JsonUtility.ToJson(obj, true);
            File.WriteAllText(_filePath, json);
        }

        public PlayerMemento Load()
        {
            if (!File.Exists(_filePath))
            {
                //create d4efault
                return null;
            }

            string json = File.ReadAllText(_filePath);
            var playerMemento = JsonUtility.FromJson<PlayerMemento>(json);

            return playerMemento;
        }
    }
}
