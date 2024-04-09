using Assets._App.Scripts.Scenes.SceneLevels.Config;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels
{
    public class SceneCreator : MonoBehaviour
    {
        public ConfigPrefabs objectListSO;

        private void Start()
        {
            RecreateSceneFromScriptableObject();
        }

        public void RecreateSceneFromScriptableObject()
        {
            if (objectListSO == null)
            {
                Debug.LogError("ObjectListSO is not assigned!");
                return;
            }

            foreach (ObjectData data in objectListSO.objects)
            {
                if (data.objectReference == null)
                {
                    Debug.LogWarning("GameObject is null in ObjectData!");
                    continue;
                }

                GameObject newObject = Instantiate(data.objectReference, data.position, Quaternion.identity);
                newObject.transform.localScale = data.scale;
            }
        }
    }
}
