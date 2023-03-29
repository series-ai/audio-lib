using UnityEngine;
using UnityEngine.Audio;

namespace Padoru.Audio
{
    [System.Serializable]
    public class AudioFile
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private AudioMixerGroup mixer;
        [SerializeField, Range(0, 1)] private float volume = 1f;
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool loop;
        [SerializeField] private bool disabled;

        public AudioClip Clip { get => clip; }
        public AudioMixerGroup Mixer { get => mixer; }
        public float Volume { get => volume; }
        public bool PlayOnAwake { get => playOnAwake; }
        public bool Loop { get => loop; }
        public bool Disabled { get => disabled; }
    }
}
