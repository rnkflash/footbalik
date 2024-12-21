using _Game.Scripts.sounds;
using UnityEngine;

namespace _Game.Scripts
{
    public class Offside : MonoBehaviour
    {
        public void Sound()
        {
            SoundSystem.instance.Play(Sounds.offside);
        }
    }
}
