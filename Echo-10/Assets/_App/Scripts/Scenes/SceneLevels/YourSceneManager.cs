using Assets._App.Scripts.Scenes.SceneLevels.Config;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class YourSceneManager : MonoBehaviour
{
    public ConfigPrefabs objectListSO;

    private void Start()
    {
        PopulateObjectList();
    }

    private void PopulateObjectList()
    {
        objectListSO.objects.Clear();

        // Получаем список всех префабов в проекте
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();
        // Проходим по каждому GUID и загружаем префабы
        foreach (string guid in prefabGuids)
        {
            // Получаем путь к файлу префаба по его GUID
            string prefabPath = AssetDatabase.GUIDToAssetPath(guid);
            // Загружаем префаб по пути
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            // Добавляем имя префаба в список
            if (prefab != null)
            {
                prefabDictionary.Add(prefab.name, prefab);
            }
        }


        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Создаем новый экземпляр ObjectData и заполняем его данными
            ObjectData newData = new ObjectData();

            if (prefabDictionary.ContainsKey(obj.name))
            {
                newData.objectReference = prefabDictionary[obj.name];
            }
            else
            {
                newData.objectReference = null;
            }

            newData.objectName = obj.name;
            newData.position = obj.transform.position;
            newData.scale = obj.transform.localScale;


            objectListSO.objects.Add(newData);
        }
    }
}
