using UnityEngine;
using UnityEngine.Audio;

namespace Padoru.Audio
{
    [System.Serializable]
    public class AudioFile
    {
        public AudioClip Clip { get; set; }
        public AudioMixerGroup Mixer { get;  set;}
        public float Volume { get;  set; }
        public bool PlayOnAwake { get;  set; }
        public bool Loop { get; set; }
        public bool Disabled { get; set; }
    }
}
