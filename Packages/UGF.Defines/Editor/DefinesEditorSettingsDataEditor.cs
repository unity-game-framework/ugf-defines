using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.Defines.Editor
{
    [CustomEditor(typeof(DefinesEditorSettingsData), true)]
    internal class DefinesEditorSettingsDataEditor : UnityEditor.Editor
    {
        private readonly DefinesPlatformSettingsDrawer2 m_drawer = new DefinesPlatformSettingsDrawer2();
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
        }

        private void OnApplied(string groupName, BuildTargetGroup buildTargetGroup)
        {
            if (DefinesEditorSettings.PlatformSettings.TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                DefinesBuildEditorUtility.ApplyDefinesAll(buildTargetGroup, settings);
                AssetDatabase.SaveAssets();
            }
        }

        private void OnCleared(string groupName, BuildTargetGroup buildTargetGroup)
        {
            if (DefinesEditorSettings.PlatformSettings.TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                DefinesBuildEditorUtility.ClearDefinesAll(buildTargetGroup, settings);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
