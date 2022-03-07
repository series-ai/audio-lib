using UnityEngine;
using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Audio
{
    public class AudioManager : IAudioManager
    {
        private IAudioManagerDatabase database;
        private IAudioSourceRetriever retriever;

        public AudioManager(IAudioManagerDatabase database, IAudioSourceRetriever retriever)
        {
            this.database = database;
            this.retriever = retriever;
        }

        public AudioFile GetAudioFile(string id)
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
