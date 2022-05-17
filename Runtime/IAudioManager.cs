using UnityEngine;

namespace Padoru.Audio
{
    public interface IAudioManager
    {
        AudioFile GetAudioFile(string id);

        AudioSource GetAudioSource();

        void ReturnAudioSource(AudioSource audioSource);
    }
}