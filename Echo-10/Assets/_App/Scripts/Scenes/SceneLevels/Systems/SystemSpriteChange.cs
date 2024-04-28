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
        private SystemEnemyMovement _enemyMovement;

        public SystemSpriteChange(ConfigSprites sprites, ConfigObjects configObjects, SphereCollider sphereCollider, 
            SystemEnemyMovement enemyMovement)
        {
            _sprites = sprites;
            _configObjects = configObjects;
            _sphereCollider = sphereCollider;
            _enemyMovement = enemyMovement;
        }

        public void Update()
        {
            ChangeView();
        }

        private void ChangeView()
        {
            foreach (var objectData in _configObjects.objects)
            {
                if(objectData.objectReference == null) continue;
                var isInsideSphere = _sphereCollider.bounds.Contains(objectData.objectReference.transform.position);

                if (objectData.objectType == ObjectType.Enemy)
                {
                    ChangeEnemyAnimation(objectData, isInsideSphere);
                    
                }
                else
                {
                    ChangeSpriteForObject(objectData.renderer, isInsideSphere);
                }
            }
        }

        private void ChangeEnemyAnimation(ObjectData objectData, bool isInsideSphere)
        {
            var animator = objectData.animator;
            if (animator == null) return;

            bool currentAnimState = animator.GetBool("WhiteAnim");

            if (isInsideSphere && !currentAnimState)
            {
                animator.SetBool("WhiteAnim", true);
                _enemyMovement.MoveEnemyBackward(objectData);
            }
            else if (!isInsideSphere && currentAnimState)
            {
                animator.SetBool("WhiteAnim", false);
            }
        }

        private void ChangeSpriteForObject(SpriteRenderer spriteRenderer, bool isInsideSphere)
        {
            if (spriteRenderer == null) return;
            foreach (var sprite in _sprites.sprites)
            {
                if (isInsideSphere && sprite.oldSprite == spriteRenderer.sprite)
                {
                    spriteRenderer.sprite = sprite.newSprite;
                    break;
                }
                else if (!isInsideSphere && sprite.newSprite == spriteRenderer.sprite)
                {
                    spriteRenderer.sprite = sprite.oldSprite;
                    break;
                }
            }
        }

    }
}
