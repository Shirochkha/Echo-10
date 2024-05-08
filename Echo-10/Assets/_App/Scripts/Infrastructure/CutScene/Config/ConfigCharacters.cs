using System.Collections.Generic;
using UnityEngine;

namespace Assets._App.Scripts.Infrastructure.CutScene.Config
{
    [CreateAssetMenu(fileName = "configCharacters", menuName = "app/cutScenes/config characters")]
    public class ConfigCharacters : ScriptableObject
    {
        public List<Character> characters = new();
    }
}
