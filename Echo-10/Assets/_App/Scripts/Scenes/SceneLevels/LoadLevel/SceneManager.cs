using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public ConfigObjects objectListSO;
    public ConfigPrefabsTypes prefabTypesSO;

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
            string name = obj.name.Split(' ')[0];

            if (prefabDictionary.ContainsKey(name))
            {
                newData.prefabReference = prefabDictionary[name];

                foreach (PrefabData prefabData in prefabTypesSO.prefabTypes)
                {
                    if (prefabData.prefab == newData.prefabReference)
                    {
                        newData.objectType = prefabData.type;
                        break;
                    }
                }
            }
            else
            {
                continue;
            }

            newData.objectName = obj.name;
            newData.position = obj.transform.position;
            newData.scale = obj.transform.localScale;


            objectListSO.objects.Add(newData);
        }
    }
}
