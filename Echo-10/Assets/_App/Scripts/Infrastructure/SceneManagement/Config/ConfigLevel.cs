using Assets._App.Scripts.Infrastructure.SceneManagement.Config.Support;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.SceneManagement.Config
{
    [CreateAssetMenu(fileName = "configLevel", menuName = "app/levels/config levels")]
    public class ConfigLevel : ScriptableObject
    {
        public List<LevelData> levels = new();
    }
}
