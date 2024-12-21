using _Game.Scripts.sounds;
using UnityEngine;

namespace _Game.Scripts
{
    public class RigidBodyHitSound : MonoBehaviour
    {
        public Sounds sound;
        public float impactThreshold = 1.0f;
        public float volume = 1.0f;

        void OnCollisionEnter(Collision other)
        {
            if (other.impulse.magnitude > impactThreshold)
            {
                SoundSystem.instance.Play(sound, volume);
            }
        }
    }
}
