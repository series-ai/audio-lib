using UnityEngine;

namespace Padoru.Audio
{
    [System.Serializable]
    public class AudioFile
    {
        [SerializeField] private AudioClip clip;
        [SerializeField, Range(0, 1)] private float volume;
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool loop;
        [SerializeField] private bool disabled;

        public AudioClip Clip { get => clip; }
        public float Volume { get => volume; }
        public bool PlayOnAwake { get => playOnAwake; }
        public bool Loop { get => loop; }
        public bool Disabled { get => disabled; }
    }
}
