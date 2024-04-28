using _App.Scripts.Libs.Installer;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemEnemyMovement
    {
        private float _enemySpeed = 15f;


        public SystemEnemyMovement(float enemySpeed)
        {
            _enemySpeed = enemySpeed;
        }

        public void MoveEnemyBackward(ObjectData enemy)
        {
            if (enemy.rigidbody != null)
            {
                enemy.rigidbody.velocity = -enemy.objectReference.transform.forward * _enemySpeed;
            }
        } 
    }
}
