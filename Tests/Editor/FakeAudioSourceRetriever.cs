using UnityEngine;

namespace Padoru.Audio.Tests
{
    public class FakeAudioSourceRetriever : IAudioSourceRetriever
    {
        public AudioSource GetAudio()
        {
            return null;
        }

        public void ReturnAudio(AudioSource audioSource)
        {

        }
    }
}