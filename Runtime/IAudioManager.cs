using UnityEngine;

namespace Padoru.Audio
{
    public interface IAudioManager
    {
        AudioFile GetAudioFile(string id);

        AudioSource GetAudioSource();

        void ReturnAudioSource(AudioSource audioSource);

        void AddAudioFile(string id, AudioFile audioFile);

        bool TryAddAudioFile(string id, AudioFile audioFile);
        
        void RemoveAudioFile(string id);
        
        bool TryRemoveAudioFile(string id);
    }
}