using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Audio
{
    public class AudioManager : IAudioManager
    {
        private IDictionary<string, AudioFile> audioFiles;
        private IAudioSourceRetriever retriever;

        public AudioManager(IAudioSourceRetriever retriever):
            this(new Dictionary<string, AudioFile>(), retriever)
        {
            
        }
        
        public AudioManager(IDictionary<string, AudioFile> audioFiles, IAudioSourceRetriever retriever)
        {
            this.audioFiles = audioFiles;
            this.retriever = retriever;
        }

        public AudioFile GetAudioFile(string id)
        {
            if (audioFiles == null)
            {
                throw new Exception($"Could not get audio file, the database is null");
            }

            if (!audioFiles.ContainsKey(id))
            {
                throw new Exception($"Invalid key {id}");
            }

            return audioFiles[id];
        }

        public void AddAudioFile(string id, AudioFile audioFile)
        {
            if (audioFiles == null)
            {
                throw new Exception($"Could not add audio file, the database is null");
            }

            if (audioFiles.ContainsKey(id))
            {
                throw new Exception($"Database already contains an audio file with id {id}");
            }
            
            audioFiles[id] = audioFile;
        }

        public void RemoveAudioFile(string id)
        {
            if (audioFiles == null)
            {
                throw new Exception($"Could not remove audio file, the database is null");
            }

            if (!audioFiles.ContainsKey(id))
            {
                throw new Exception($"Invalid key {id}");
            }
            
            audioFiles.Remove(id);
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
