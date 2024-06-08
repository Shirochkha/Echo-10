using System.IO;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    public class ShopPersistence : IPersistence<Shop>
    {
        private string _filePath;

        public ShopPersistence(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public Shop Load()
        {
            if (!File.Exists(_filePath))
            {
                //create d4efault
                return new Shop();
            }

            string json = File.ReadAllText(_filePath);
            var shop = JsonUtility.FromJson<Shop>(json);
            return shop;
        }

        public void Save(Shop obj)
        {
            string json = JsonUtility.ToJson(obj, true);
            File.WriteAllText(_filePath, json);
        }
    }
}
