//-----------------------------------------------------------------------
// <copyright file="EditorBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Unity.CustomEditors
{
    /// <summary>Base class for custom Unity editors</summary>
    public abstract class EditorBase : Editor
    {
        /// <summary>Delimiter for dictionary serialized properties</summary>
        private const string DictionarySerializedPropertyDelimiter = "\u2000";

        /// <summary>Delimiter for values within dictionary serialized properties</summary>
        private const string SerializedDictionaryValueDelimiter = "\u2001";

        /// <summary>Serialized settings</summary>
        private SerializedProperty serializedSettings;

        /// <summary>Unity editor OnEnable handler</summary>
        /// <remarks>Initialization each time the inspector is enabled</remarks>
        public void OnEnable()
        {
            this.serializedSettings = this.serializedObject.FindProperty("Settings");
        }

        /// <summary>Unity editor OnInspectorGUI handler</summary>
        /// <remarks>Refresh each time the inspector is updated</remarks>
        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            var settings = ParseSettings(this.serializedSettings);

            EditorGUILayout.LabelField("Settings");
            foreach (var setting in settings.ToArray())
            {
                EditorGUILayout.BeginHorizontal();
                var modifiedSetting = new KeyValuePair<string, string>(
                    EditorGUILayout.TextField(setting.Key),
                    EditorGUILayout.TextField(setting.Value));
                EditorGUILayout.EndHorizontal();

                if (modifiedSetting.Key != setting.Key)
                {
                    // Remove old setting and add a new one
                    settings.Remove(setting.Key);
                    settings.Add(modifiedSetting);
                }
                else if (modifiedSetting.Value != setting.Value)
                {
                    // Just the value changed
                    settings[setting.Key] = modifiedSetting.Value;
                }
            }

            // Fields for new setting
            EditorGUILayout.BeginHorizontal();
            var newSetting = new KeyValuePair<string, string>(
                EditorGUILayout.TextField(string.Empty),
                EditorGUILayout.TextField(string.Empty));
            EditorGUILayout.EndHorizontal();
            if (!string.IsNullOrEmpty(newSetting.Key))
            {
                settings[newSetting.Key] = newSetting.Value;
            }

            this.serializedSettings.stringValue = SerializeSettings(settings);
            this.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>Parses a dictionary from the serialized property</summary>
        /// <param name="dictionaryProperty">Property containing a dictionary</param>
        /// <returns>The dictionary</returns>
        private static IDictionary<string, string> ParseSettings(SerializedProperty dictionaryProperty)
        {
            return dictionaryProperty.stringValue
                .Split(new[] { DictionarySerializedPropertyDelimiter }, System.StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(new[] { SerializedDictionaryValueDelimiter }, System.StringSplitOptions.RemoveEmptyEntries))
                .Select(kvp => new KeyValuePair<string, string>(kvp.Length > 0 ? kvp[0] : string.Empty, kvp.Length > 1 ? kvp[1] : string.Empty))
                .ToDictionary();
        }

        /// <summary>Serializes a dictionary for storage in a serialized property</summary>
        /// <param name="settings">Dictionary to be serialized</param>
        /// <returns>The serialized dictionary</returns>
        private static string SerializeSettings(IDictionary<string, string> settings)
        {
            var pairs = settings.Select(kvp =>
            {
                var values = new[]
            {
                kvp.Key.Replace(DictionarySerializedPropertyDelimiter, " ").Replace(SerializedDictionaryValueDelimiter, " "),
                kvp.Value.Replace(DictionarySerializedPropertyDelimiter, " ").Replace(SerializedDictionaryValueDelimiter, " ")
            };
                return string.Join(SerializedDictionaryValueDelimiter, values);
            });
            return string.Join(DictionarySerializedPropertyDelimiter, pairs.ToArray());
        }
    }
}