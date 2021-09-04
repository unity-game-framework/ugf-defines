using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.Platforms;
using UnityEditor;
using UnityEngine;

namespace UGF.Defines.Editor
{
    [CustomEditor(typeof(DefinesEditorSettingsData), true)]
    internal class DefinesEditorSettingsDataEditor : UnityEditor.Editor
    {
        private readonly DefinesPlatformSettingsDrawer m_drawer = new DefinesPlatformSettingsDrawer();
        private SerializedProperty m_propertyRestoreDefinesAfterBuild;
        private SerializedProperty m_propertyGroups;

        private void OnEnable()
        {
            m_drawer.Enable();

            m_propertyRestoreDefinesAfterBuild = serializedObject.FindProperty("m_restoreDefinesAfterBuild");
            m_propertyGroups = serializedObject.FindProperty("m_settings.m_groups");
        }

        private void OnDisable()
        {
            m_drawer.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyRestoreDefinesAfterBuild);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Scripting Define Symbols", EditorStyles.boldLabel);

                m_drawer.DrawGUILayout(m_propertyGroups);
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Clear"))
                {
                    OnClear();
                }

                if (GUILayout.Button("Apply"))
                {
                    OnApply();
                }

                EditorGUILayout.Space();
            }
        }

        private void OnApply()
        {
            string platformName = m_drawer.GetSelectedPlatformName();
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(platformName);

            if (DefinesEditorSettings.PlatformSettings.TryGetSettings(platform.BuildTargetGroup, out DefinesSettings settings))
            {
                DefinesBuildEditorUtility.ApplyDefinesAll(platform.BuildTargetGroup, settings);
                AssetDatabase.SaveAssets();
            }
        }

        private void OnClear()
        {
            string platformName = m_drawer.GetSelectedPlatformName();
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(platformName);

            if (DefinesEditorSettings.PlatformSettings.TryGetSettings(platform.BuildTargetGroup, out DefinesSettings settings))
            {
                DefinesBuildEditorUtility.ClearDefinesAll(platform.BuildTargetGroup, settings);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
