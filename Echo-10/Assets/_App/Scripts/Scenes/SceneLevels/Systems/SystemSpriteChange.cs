using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using Assets._App.Scripts.Scenes.SceneLevels.Sevices;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemSpriteChange : IUpdatable
    {
        private ConfigSprites _sprites;
        private ConfigLevel _level;
        private ServiceLevelSelection _serviceLevelSelection;
        private SphereCollider _sphereCollider;
        private SystemEnemyMovement _enemyMovement;

        private ConfigObjects _configObjects;
        private bool _hasSceneCreate = false;

        public SystemSpriteChange(ConfigSprites sprites, ConfigLevel level, ServiceLevelSelection serviceLevelSelection,
            SphereCollider sphereCollider, SystemEnemyMovement enemyMovement)
        {
            _sprites = sprites;
            _level = level;
            _serviceLevelSelection = serviceLevelSelection;
            _sphereCollider = sphereCollider;
            _enemyMovement = enemyMovement;
        }

        public void Update()
        {
            if (!_hasSceneCreate && _serviceLevelSelection.SelectedLevelId > 0)
            {
                foreach (var level in _level.levels)
                {
                    if (level.id == _serviceLevelSelection.SelectedLevelId)
                        _configObjects = level.configObjects;
                }
                _hasSceneCreate = true;
            }
            ChangeView();
        }

        private void ChangeView()
        {
            if (_configObjects == null) return;

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
