using _App.Scripts.Libs.Installer;
using UnityEngine;

namespace Assets._App.Scripts.Scenes.SceneLevels.Systems
{
    public class SystemPlayerController : IUpdatable
    {
        private Rigidbody _rb;
        private float _playerSpeed;

        public SystemPlayerController(Rigidbody rb, float playerSpeed)
        {
            _rb = rb;
            _playerSpeed = playerSpeed;
        }

        public void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new(horizontalInput, verticalInput, 0f);
            _rb.velocity = movement * _playerSpeed;
        }
    }
}
