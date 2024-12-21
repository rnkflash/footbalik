using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.sounds
{
    [CreateAssetMenu(fileName = "SoundsLibrary", menuName = "ScriptableObjects/SoundsLibrary", order = 1)]
    public class SoundsLibrary: ScriptableObject
    {
        public List<SoundPair> sounds = new List<SoundPair>();
    }

    [Serializable]
    public class SoundPair
    {
        public Sounds type;
        public AudioClip[] audioClips;
    }
}