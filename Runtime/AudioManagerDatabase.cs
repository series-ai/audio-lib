using Padoru.EnumGeneration;
using System.Collections.Generic;
using UnityEngine;

namespace Padoru.Audio
{
    public abstract class AudioManagerDatabase : EnumBasedDatabase<AudioFile>
    {
        [SerializeField] private AudioDatabaseDictionary items;

        public sealed override Dictionary<string, AudioFile> Items => items;
    }
}