using Padoru.EnumGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Padoru.Audio
{
    [CustomEditor(typeof(AudioManagerDatabase))]
    public class AudioManagerDatabaseEditor : Editor
    {
        private AudioManagerDatabase t;

        private void OnEnable()
        {
            t = (AudioManagerDatabase)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Apply"))
            {
                ApplyChanges();
            }
        }

        private void ApplyChanges()
        {
            var path = EnumEditorUtils.GetClassPathInProject<AudioManagerDatabaseEditor>();
            var nameSpace = GetType().Namespace;
            var typeName = $"{nameSpace}.{t.EnumName},{nameSpace}";
            var enumType = Type.GetType(typeName);

            if (enumType != null)
            {
                var enumItems = Enum.GetNames(enumType).ToList();
                var itemsToAdd = new List<string>();
                var itemsToRemove = new List<string>();

                foreach (var item in t.Items.Keys)
                {
                    if (!enumItems.Contains(item.ToString()))
                    {
                        itemsToAdd.Add(item.ToString());
                    }
                }

                foreach (var item in enumItems)
                {
                    if (!t.Items.ContainsKey(item))
                    {
                        itemsToRemove.Add(item);
                    }
                }

                foreach (var item in itemsToAdd)
                {
                    //EnumGenerator.AddEnumItem
                }

                foreach (var item in itemsToRemove)
                {
                    //EnumGenerator.RemoveEnumItem
                }
            }
            else
            {
                var newContent = new Dictionary<string, int>();
                var lastIndexAdded = -1;
                foreach (var item in t.Items.Keys)
                {
                    lastIndexAdded++;
                    newContent.Add(item.ToString(), lastIndexAdded);
                }

                EnumGenerator.GenerateEnum(path, nameSpace, t.EnumName, newContent);
            }
        }
    }
}
