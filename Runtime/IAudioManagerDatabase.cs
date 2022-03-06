using System.Collections.Generic;

namespace Padoru.Audio
{
    public interface IAudioManagerDatabase
    {
        Dictionary<string, AudioFile> Items { get; }
    }
}