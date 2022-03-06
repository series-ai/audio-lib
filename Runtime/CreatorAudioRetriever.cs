using UnityEngine;

namespace Padoru.Audio
{
    [CreateAssetMenu(menuName = "Padoru/Audio/AudioRetriever/Creator")]
    public class CreatorAudioRetriever : AudioSourceRetriever
    {
        public override AudioSource GetAudio()
        {
            var go = new GameObject("AudioSource");
            return go.AddComponent<AudioSource>();
        }

        public override void ReturnAudio(AudioSource audioSource)
        {
            Destroy(audioSource.gameObject);
        }
    }
}
