using UnityEngine;

namespace Padoru.Audio
{
    public interface IAudioSourceRetriever
    {
        AudioSource GetAudio();

        void ReturnAudio(AudioSource audioSource);
    }
}
