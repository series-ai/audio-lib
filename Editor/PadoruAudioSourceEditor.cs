using Padoru.Core.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Padoru.Audio.Editor
{
    [CustomEditor(typeof(PadoruAudioSource))]
    public class PadoruAudioSourceEditor : UnityEditor.Editor
    {
        private const string AUDIO_FILE_ID_PROPERTY_NAME = "fileId";
        private const string TRACK_OBJECT_PROPERTY_NAME = "trackObject";

        private SerializedProperty audioFileIdProperty;
        private SerializedProperty trackObjectProperty;

        private void OnEnable()
        {
            audioFileIdProperty = serializedObject.FindProperty(AUDIO_FILE_ID_PROPERTY_NAME);
            trackObjectProperty = serializedObject.FindProperty(TRACK_OBJECT_PROPERTY_NAME);
        }

        public override void OnInspectorGUI()
        {
            DrawFileIdPopup();
            DrawTrackObjectToggle();
        }

        private void DrawFileIdPopup()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Audio File ID", GUILayout.ExpandWidth(false), GUILayout.Width(250));

            if (GUILayout.Button($"{audioFileIdProperty.stringValue}", EditorStyles.popup))
            {
                SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), GetSearchProvider());
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawTrackObjectToggle()
        {
            EditorGUILayout.PropertyField(trackObjectProperty);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnFileIdSelected(string fileId)
        {
            audioFileIdProperty.stringValue = fileId;
            serializedObject.ApplyModifiedProperties();
        }

        private StringListSearchProvider GetSearchProvider()
        {
            var audioManagerDatabases = AssetDatabase.FindAssets($"t:{typeof(AudioManagerDatabase)}");

            if (audioManagerDatabases == null || audioManagerDatabases.Length <= 0)
            {
                return new StringListSearchProvider($"No {typeof(AudioManagerDatabase)} found", new List<string>(), null);
            }

            if(audioManagerDatabases.Length > 1)
            {
                Debug.LogWarning($"There are more than one {typeof(AudioManagerDatabase)} in the project, window will only display values on the first one found");
            }

            var audioManagerDatabasePath = AssetDatabase.GUIDToAssetPath(audioManagerDatabases[0]);
            var audioManagerDatabase = (AudioManagerDatabase)AssetDatabase.LoadAssetAtPath(audioManagerDatabasePath, typeof(AudioManagerDatabase));

            return new StringListSearchProvider("List", audioManagerDatabase.Items.Keys.ToList(), OnFileIdSelected);
        }
    }
}
