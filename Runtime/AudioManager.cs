using System;
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
                throw new Exception($"Could not get audio file, the database is null");
            }

            if(database.Items == null)
            {
                throw new Exception($"Could not get audio file, the database items collection is null");
            }

            if (!database.Items.ContainsKey(id))
            {
                throw new Exception($"Invalid key {id}");
            }

            return database.Items[id];
        }

        public AudioSource GetAudioSource()
        {
            if (retriever == null)
            {
                throw new Exception($"Could not get audio source, the retriever is null");
            }

            return retriever.GetAudio();
        }

        public void ReturnAudioSource(AudioSource audioSource)
        {
            if (audioSource == null)
            {
                throw new Exception($"Could not return a null audio source");
            }

            if (retriever == null)
            {
                throw new Exception($"Could not return audio source, the retriever is null");
            }

            retriever.ReturnAudio(audioSource);
        }
    }
}
