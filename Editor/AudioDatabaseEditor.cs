using Padoru.Core.Editor;
using UnityEditor;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Audio.Editor
{
	[CustomPropertyDrawer(typeof(AudioDatabaseDictionary))]
	public class AudioDatabaseDrawer : SerializedDictionaryDrawer<AudioDatabaseDictionary> { }
}
