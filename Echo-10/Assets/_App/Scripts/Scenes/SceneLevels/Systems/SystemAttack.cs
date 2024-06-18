using UnityEngine;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using _App.Scripts.Libs.Installer;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemAttack : IUpdatable
    {
        private IPlayer _player;
        private Boss _boss;
        private Animator _playerAnimator;

        private bool _bossAnimationStarted;
        private bool _isAttacking = false;

        public SystemAttack(IPlayer player, Boss boss, Animator playerAnimator)
        {
            _player = player;
            _boss = boss;
            _playerAnimator = playerAnimator;
        }

        public void Update()
        {
            if (_isAttacking && _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && 
                !_playerAnimator.IsInTransition(0))
            {
                _playerAnimator.SetBool("Attack", false);
                _isAttacking = false;
            }
        }

        public void UserAttack()
        {
            _playerAnimator.SetBool("Attack", true);
            _isAttacking = true;

            if (IsColliding(_player.AttackCollider, _boss.Collider))
            {
                HandleCollision(_player, _boss);
            }            
        }

        public void BossAttack()
        {
            if (_boss.StartAnimation && !_bossAnimationStarted)
            {
                _bossAnimationStarted = true;
                _boss.AttackAnimator.SetBool("Attack", true);
                //Debug.Log("Начинаем анимацию");
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
            _boss.TakeDamage(1);
            //TODO: КОНЕЦ ИГРЫ
        }

        private void HandleCollision(Boss boss, IPlayer player)
        {
            _player.TakeDamage(_boss.Damage);
        }
    }
}

