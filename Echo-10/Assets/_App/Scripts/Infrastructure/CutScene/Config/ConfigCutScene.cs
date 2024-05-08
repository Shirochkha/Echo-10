using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.CutScene.Config
{
    [CreateAssetMenu(fileName = "configCutScene", menuName = "app/cutScenes/config CutScene")]
    public class ConfigCutScene : ScriptableObject
    {
        public DialogLine[] dialogLines;
    }
}
