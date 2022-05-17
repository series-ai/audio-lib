﻿using System.Collections.Generic;
using UnityEngine;

namespace Padoru.Audio
{
    [CreateAssetMenu(menuName = "Padoru/Audio/AudioManagerDatabase2")]
    public class AudioManagerDatabase : ScriptableObject
    {
        [SerializeField] private AudioDatabaseDictionary items;

        public Dictionary<string, AudioFile> Items => items;
    }
}