using _App.Scripts.Libs.Installer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemTextureScroll : IUpdatable
    {
        private List<WallMaterialOffset> _wallOffsets;

        public SystemTextureScroll(List<WallMaterialOffset> wallOffsets)
        {
            _wallOffsets = wallOffsets;
        }

        public void Update()
        {
            foreach (WallMaterialOffset wall in _wallOffsets)
            {
                float offsetX = Time.time * wall.textureOffset.x;
                float offsetY = Time.time * wall.textureOffset.y;
                Vector2 textureOffset = new(offsetX, offsetY);

                wall.renderer.material.mainTextureOffset = textureOffset;
            }
        }
    }

    [Serializable]
    public struct WallMaterialOffset
    {
        public Vector2 textureOffset;
        public Renderer renderer;
    }
}
