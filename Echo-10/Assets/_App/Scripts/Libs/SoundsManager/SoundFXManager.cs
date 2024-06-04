using UnityEngine;

namespace Assets._App.Scripts.Libs.SoundsManager
{
    public class SoundFXManager : MonoBehaviour
    {
        public static SoundFXManager instance;

        [SerializeField] private AudioSource _soundFXObject;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        /*private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }*/

        public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform = null, float volume = 1f)
        {
            Vector3 spawnPosition = (spawnTransform != null) ? spawnTransform.position : Vector3.zero;
            AudioSource audioSource = GameObject.Instantiate(_soundFXObject, spawnPosition, Quaternion.identity);

            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;

            GameObject.Destroy(audioSource.gameObject, clipLength);
        }
    }
}
