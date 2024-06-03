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

        public bool TryAddAudioFile(string id, AudioFile audioFile)
        {
            if (audioFiles == null)
            {
                Debug.LogError($"Could not add audio file, the database is null");
            }

            return audioFiles.TryAdd(id, audioFile);
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
        
        public bool TryRemoveAudioFile(string id)
        {
            if (audioFiles == null)
            {
                Debug.LogError($"Could not remove audio file, the database is null");
            }

            if (!audioFiles.ContainsKey(id))
            {
                return false;
            }
            
            audioFiles.Remove(id);
            return true;
        }
		
		/// <summary>
        /// This removes all audio files with null clip references
        /// </summary>
        /// <returns></returns>
        public void ClenupAudioClips()
        {
            var keysToRemove = new List<string>();
            foreach (var key in audioFiles.Keys)
            {
                if (audioFiles[key].Clip == null)
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                audioFiles.Remove(key);
            }
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
