using Padoru.Core;
using System;
using Padoru.Core.Utils;
using UnityEngine;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Audio
{
    public class PadoruAudioSource : MonoBehaviour
    {
        [SerializeField] private string fileId;
        [SerializeField] private bool trackObject;

        private IAudioManager audioManager;
        private AudioFile audioFile;
        private AudioSource audioSource;
        private bool isPlaying;
        private bool initialized;

        public event Action<PadoruAudioSource> OnAudioFinish;

        private bool CanPlay
        {
            get
            {
                if (audioFile == null)
                {
                    Debug.LogError("Null audio file", Constants.AUDIO_LOG_CHANNEL);
                    return false;
                }

                if (audioFile.Clip == null)
                {
                    Debug.LogWarning("Null audio clip", Constants.AUDIO_LOG_CHANNEL, gameObject);
                    return false;
                }

                if (isPlaying)
                {
                    Debug.LogWarning("Audio is already playing", Constants.AUDIO_LOG_CHANNEL, gameObject);
                    return false;
                }

                if (audioFile.Disabled)
                {
                    Debug.LogWarning("You are trying to play a disabled audio", Constants.AUDIO_LOG_CHANNEL, gameObject);
                    return false;
                }

                return true;
            }
        }

        private void Start()
        {
            if (!initialized)
            {
                Init();
            }

            if (audioFile != null && audioFile.PlayOnAwake)
            {
                Play();
            }
        }

        public void Init()
        {
            audioManager = Locator.Get<IAudioManager>();

            if (audioManager == null)
            {
                Debug.LogError("Could not initialize audio source due to null audio manager", Constants.AUDIO_LOG_CHANNEL);
                return;
            }

            try
            {
                audioFile = audioManager.GetAudioFile(fileId);
            }
            catch (Exception e)
            {
                Debug.LogException("Failed to initialize Audio File", Constants.AUDIO_LOG_CHANNEL, e);
            }

            initialized = true;
        }

        private void OnEnable()
        {
            if (audioFile != null && audioFile.PlayOnAwake)
            {
                Play();
            }
        }

        public void Play()
        {
            if(!initialized)
            {
                Init();
            }

            if (!CanPlay)
            {
                return;
            }

            isPlaying = true;
            
            try
            {
                audioSource = audioManager.GetAudioSource();
            }
            catch (Exception e)
            {
                Debug.LogException($"Failed to initialize audio source", Constants.AUDIO_LOG_CHANNEL, e);
            }

            if(audioSource == null)
            {
                Debug.LogError($"Audio manager failed to return an audio source", Constants.AUDIO_LOG_CHANNEL);
                return;
            }

            SetupAudioSource();

            audioSource.Play();

            if (!audioFile.Loop)
            {
                var countdown = new Countdown(audioFile.Clip.length);
                countdown.OnCountdownEnded += FinishAudio;
                countdown.Start();
            }
        }

        public void Stop()
        {
            if(audioSource != null)
            {
                audioSource.Stop();
            }

            FinishAudio();
        }

        /// <summary>
        /// If you want this change to take effect you need to initialize again
        /// </summary>
        public void SetAudioFileId(string fileId)
        {
            this.fileId = fileId;
        }

        private void FinishAudio()
        {
            if (audioSource != null)
            {
                try
                {
                    audioManager.ReturnAudioSource(audioSource);
                }
                catch (Exception e)
                {
                    Debug.LogException($"Failed to return audio source", Constants.AUDIO_LOG_CHANNEL, e);
                }

                audioSource = null;
            }

            isPlaying = false;

            OnAudioFinish?.Invoke(this);
        }

        private void SetupAudioSource()
        {
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = audioFile.Mixer;
            audioSource.clip = audioFile.Clip;
            audioSource.volume = audioFile.Volume;
            audioSource.loop = audioFile.Loop;

            if (trackObject)
            {
                audioSource.transform.parent = transform;
            }
        }
    }
}
