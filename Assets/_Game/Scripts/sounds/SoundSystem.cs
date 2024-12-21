using UnityEngine;
using UnityEngine.Rendering;

namespace _Game.Scripts.sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSystem: MonoBehaviour
    {
        public static SoundSystem instance;

        AudioSource audioSource;

        [SerializeField] SoundsLibrary sounds;

        void Awake()
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }

        public void Play(AudioClip[] audioClip, float volumeScale = 1.0f)
        {
            if (audioClip.Length > 0)
                audioSource.PlayOneShot(audioClip[UnityEngine.Random.Range(0, audioClip.Length)], volumeScale);
        }

        public void Play(Sounds soundName, float volumeScale = 1.0f)
        {
            var audioClips = sounds.sounds.Find(pair => pair.type == soundName).audioClips; 
            Play(audioClips, volumeScale);
        }

    }
}