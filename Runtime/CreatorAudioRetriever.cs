using UnityEngine;

namespace Padoru.Audio
{
    public class CreatorAudioRetriever : IAudioSourceRetriever
    {
        public AudioSource GetAudio()
        {
            var go = new GameObject("AudioSource");
            return go.AddComponent<AudioSource>();
        }

        public void ReturnAudio(AudioSource audioSource)
        {
            Object.Destroy(audioSource.gameObject);
        }
    }
}
