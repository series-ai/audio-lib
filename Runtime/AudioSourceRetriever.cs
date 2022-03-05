using UnityEngine;

namespace Padoru.Audio
{
    public abstract class AudioSourceRetriever : ScriptableObject
    {
        public abstract AudioSource GetAudio();

        public abstract void ReturnAudio(AudioSource audioSource);
    }
}
