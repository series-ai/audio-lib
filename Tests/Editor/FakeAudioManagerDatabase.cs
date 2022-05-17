using System.Collections.Generic;

namespace Padoru.Audio.Tests
{
    public class FakeAudioManagerDatabase : IAudioManagerDatabase
    {
        public Dictionary<string, AudioFile> Items { get; }

        public FakeAudioManagerDatabase(Dictionary<string, AudioFile> items)
        {
            Items = items;
        }
    }
}