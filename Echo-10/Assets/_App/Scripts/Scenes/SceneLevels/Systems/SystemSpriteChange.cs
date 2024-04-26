using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemSpriteChange : IUpdatable
    {
        private ConfigSprites _sprites;
        private ConfigObjects _configObjects;
        private SphereCollider _sphereCollider;

        public SystemSpriteChange(ConfigSprites sprites, ConfigObjects configObjects, SphereCollider sphereCollider)
        {
            _sprites = sprites;
            _configObjects = configObjects;
            _sphereCollider = sphereCollider;
        }

        public void Update()
        {
            ChangeSprite();
        }

        private void ChangeSprite()
        {
            foreach (var otherObjectData in _configObjects.objects)
            {
                var renderer = otherObjectData.renderer;
                if (renderer == null) continue;

                var spriteRenderer = renderer.GetComponent<SpriteRenderer>();

                var isInsideSphere = _sphereCollider.bounds.Contains(renderer.transform.position);

                foreach (var sprite in _sprites.sprites)
                {
                    if (isInsideSphere && sprite.oldSprite == spriteRenderer.sprite)
                    {
                        spriteRenderer.sprite = sprite.newSprite;
                    }
                    else if (!isInsideSphere && sprite.newSprite == spriteRenderer.sprite)
                    {
                        spriteRenderer.sprite = sprite.oldSprite;
                    }
                }
            }
        }


    }
}
