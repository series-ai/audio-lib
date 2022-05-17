using System.Collections.Generic;
using UnityEngine;

namespace Padoru.Audio
{
    [CreateAssetMenu(menuName = "Padoru/Audio/AudioManagerDatabase")]
    public class AudioManagerDatabaseSO : ScriptableObject, IAudioManagerDatabase
    {
        [SerializeField] private AudioDatabaseDictionary items;

        public Dictionary<string, AudioFile> Items => items;
    }
}