using Padoru.Core.Editor;
using UnityEditor;

namespace Padoru.Audio.Editor
{
	[CustomPropertyDrawer(typeof(AudioDatabaseDictionary))]
	public class AudioDatabaseDrawer : SerializedDictionaryDrawer<AudioDatabaseDictionary> { }
}
