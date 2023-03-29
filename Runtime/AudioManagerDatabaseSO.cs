using System.Collections.Generic;
using Padoru.Core;
using UnityEngine;

namespace Padoru.Audio
{
    [CreateAssetMenu(menuName = "Padoru/Audio/AudioManagerDatabase")]
    public class AudioManagerDatabaseSO : ScriptableObject, IAudioManagerDatabase
    {
        [SerializeField] private SerializedDictionary<string, AudioFile> items;

        public Dictionary<string, AudioFile> Items => items;
    }
}