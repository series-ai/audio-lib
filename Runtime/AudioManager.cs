using UnityEngine;
using Padoru.Core;

namespace Padoru.Audio
{
    public class AudioManager : MonoBehaviour, IInitializable, IShutdowneable
    {
        [SerializeField] private AudioManagerDatabase database;
        [SerializeField] private AudioSourceRetriever retriever;

        public void Init()
        {
            Locator.RegisterService(this);
        }

        public void Shutdown()
        {
            Locator.UnregisterService<AudioManager>();
        }

        public AudioFile GetAudioFile(string id)
        {
            if (!database.Items.ContainsKey(id))
            {
                Debug.LogError("Invalid key " + id);
                return null;
            }

            return database.Items[id];
        }

        public AudioSource GetAudioSource()
        {
            return retriever.GetAudio();
        }

        public void ReturnAudioSource(AudioSource audioSource)
        {
            retriever.ReturnAudio(audioSource);
        }
    }
}
