using UnityEngine;

namespace Assets._App.Scripts.Libs.SoundsManager
{
    public class SoundMusicManager : MonoBehaviour
    {
        public static SoundMusicManager instance;

        [SerializeField] private AudioSource _musicObject;

        private void Awake()
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
        }

        public void PlayMusicClip(AudioClip audioClip, float volume = 1f)
        {
            _musicObject.clip = audioClip;
            _musicObject.volume = volume;
            _musicObject.Play();
        }
    }
}
