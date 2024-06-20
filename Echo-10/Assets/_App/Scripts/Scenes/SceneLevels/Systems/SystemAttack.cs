using UnityEngine;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.SceneManagement;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemAttack : IUpdatable
    {
        private SceneNavigatorLoader _scenes;
        private IPlayer _player;
        private Boss _boss;

        private bool _bossAnimationStarted;
        private bool _isAttacking = false;

        public SystemAttack(IPlayer player, Boss boss, SceneNavigatorLoader scenes)
        {
            _player = player;
            _boss = boss;
            _scenes = scenes;
        }

        public void Update()
        {
            if (_isAttacking && _player.PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && 
                !_player.PlayerAnimator.IsInTransition(0))
            {
                _player.PlayerAnimator.SetBool("Attack", false);
                _isAttacking = false;
            }
        }

        public void UserAttack()
        {
            _player.PlayerAnimator.SetBool("Attack", true);
            _isAttacking = true;

            if (IsColliding(_player.AttackCollider, _boss.Collider))
            {
                HandleCollision(_player, _boss);
                if(_boss.Health <= 0)
                {
                    foreach (var scene in _scenes.GetAvailableSwitchScenes())
                    {
                        if (scene.SceneViewName == "EndGame")
                            _scenes.LoadScene(scene.SceneKey);
                    }
                }
            }            
        }

        public void BossAttack()
        {
            if (_boss.AttackAnimator == null) return;
            if (_boss.StartAnimation && !_bossAnimationStarted)
            {
                _bossAnimationStarted = true;
                _boss.AttackAnimator.SetBool("Attack", true);
            }

            if (!_boss.CanAttack) return;

            _bossAnimationStarted = false;
            _boss.AttackAnimator.SetBool("Attack", false);
            _boss.Attack();
            if (IsColliding(_boss.AttackCollider, _player.PlayerCollider))
            {
                HandleCollision(_boss, _player);
            }
        }

        private bool IsColliding(Collider collider1, Collider collider2)
        {
            if (collider1 != null && collider2 != null && collider1.enabled && collider2.enabled)
            {
                return collider1.bounds.Intersects(collider2.bounds);
            }
            else
            {
                Debug.LogWarning("One of the objects does not have a collider or is disabled.");
                return false;
            }
        }

        private void HandleCollision(IPlayer player, Boss boss)
        {
            _boss.TakeDamage(_player.AttackPower);
        }

        private void HandleCollision(Boss boss, IPlayer player)
        {
            _player.TakeDamage(_boss.Damage);
        }
    }
}

