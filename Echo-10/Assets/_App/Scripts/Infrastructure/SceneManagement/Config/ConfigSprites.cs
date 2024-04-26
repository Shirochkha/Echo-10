using System.Collections.Generic;
using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.SceneManagement.Config
{
    [CreateAssetMenu(fileName = "cConfigSprites", menuName = "app/levels/config sprites")]
    public class ConfigSprites : ScriptableObject
    {
        public List<SpriteChangeData> sprites = new();
    }
}

