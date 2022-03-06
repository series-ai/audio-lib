using UnityEngine;
using Padoru.Core;
using Debug = Padoru.Diagnostics.Debug;

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

        internal AudioFile GetAudioFile(string id)
        {
            if (database == null)
            {
                Debug.LogError($"Could not get audio file, the database is null");
                return null;
            }

            if (!database.Items.ContainsKey(id))
            {
                Debug.LogError("Invalid key " + id);
                return null;
            }

            return database.Items[id];
        }

        internal AudioSource GetAudioSource()
        {
            return retriever.GetAudio();
        }

        internal void ReturnAudioSource(AudioSource audioSource)
        {
            retriever.ReturnAudio(audioSource);
        }
    }
}
