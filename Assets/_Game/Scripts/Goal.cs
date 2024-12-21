using _Game.Scripts.sounds;
using UnityEngine;

namespace _Game.Scripts
{
    public class Goal : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Ball>() != null)
            {
                SoundSystem.instance.Play(Sounds.goal);
            }
        }
    }
}
