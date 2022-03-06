using Padoru.Core;
using UnityEngine;

namespace Padoru.Audio
{
    public class PadoruAudioSource : MonoBehaviour
    {
        [SerializeField] private string fileId;
        [SerializeField] private bool trackObject;

        private AudioManager audioManager;
        private AudioFile audioFile;
        private AudioSource audioSource;
        private bool isPlaying;

        private float playTime;
        private float audioDuration;
        private bool initialized;

        private bool CanPlay
        {
            get
            {
                if (audioFile == null)
                {
                    Debug.LogError("Null audio file", gameObject);
                    return false;
                }

                if (audioFile.Clip == null)
                {
                    Debug.LogWarning("Null audio clip", gameObject);
                    return false;
                }

                if (isPlaying)
                {
                    Debug.LogWarning("Audio is already playing", gameObject);
                    return false;
                }

                if (audioFile.Disabled)
                {
                    Debug.LogWarning("You are trying to play a disabled audio", gameObject);
                    return false;
                }

                return true;
            }
        }

        private void Start()
        {
            Init();

            if (audioFile != null && audioFile.PlayOnAwake)
            {
                Play();
            }
        }

        private void Init()
        {
            if (initialized) return;

            audioManager = Locator.GetService<AudioManager>();

            if (audioManager == null)
            {
                Debug.LogError("Could not initialize audio source due to null audio manager");
                return;
            }

            audioFile = audioManager.GetAudioFile(fileId);

            initialized = true;
        }

        private void OnEnable()
        {
            if (audioFile != null && audioFile.PlayOnAwake)
            {
                Play();
            }
        }

        private void OnDisable()
        {
            Stop();
        }

        private void Update()
        {
            if(audioFile  != null && isPlaying && !audioFile.Loop)
            {
                if(Time.time - playTime >= audioDuration)
                {
                    OnAudioStop();
                }
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

            audioSource = audioManager.GetAudioSource();

            SetupAudioSource();

            playTime = Time.time;
            audioDuration = audioFile.Clip.length;

            audioSource.Play();
        }

        public void Stop()
        {
            if(audioSource != null)
            {
                audioSource.Stop();
            }

            OnAudioStop();
        }

        private void OnAudioStop()
        {
            if (audioSource != null)
            {
                audioManager.ReturnAudioSource(audioSource);

                audioSource = null;
            }

            isPlaying = false;
        }

        private void SetupAudioSource()
        {
            audioSource.playOnAwake = false;
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
