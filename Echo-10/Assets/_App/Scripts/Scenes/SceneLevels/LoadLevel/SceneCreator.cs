using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.LoadLevel
{
    public class SceneCreator : MonoBehaviour
    {
        public ConfigObjects _configObjects;

        private void Start()
        {
            RecreateSceneFromScriptableObject();
        }

        public void RecreateSceneFromScriptableObject()
        {
            if (_configObjects == null)
            {
                Debug.LogError("ObjectListSO is not assigned!");
                return;
            }

            foreach (ObjectData data in _configObjects.objects)
            {
                if (data.prefabReference == null)
                {
                    Debug.LogWarning("GameObject is null in ObjectData!");
                    continue;
                }

                GameObject newObject = Instantiate(data.prefabReference, data.position, Quaternion.identity);
                data.objectReference = newObject;
                data.renderer = newObject.GetComponent<SpriteRenderer>();
                data.animator = newObject.GetComponent<Animator>();
                data.rigidbody = newObject.GetComponent<Rigidbody>();
                newObject.transform.localScale = data.scale;
            }
        }
    }
}
