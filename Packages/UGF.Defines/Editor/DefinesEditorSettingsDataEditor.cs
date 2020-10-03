using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UnityEditor;

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
            m_drawer.AddPlatformAllAvailable();
            m_drawer.SetupGroupTypes();

            m_propertyRestoreDefinesAfterBuild = serializedObject.FindProperty("m_restoreDefinesAfterBuild");
            m_propertyGroups = serializedObject.FindProperty("m_settings.m_groups");

            m_drawer.Applied += OnApplied;
            m_drawer.Cleared += OnCleared;
        }

        private void OnDisable()
        {
            m_drawer.Applied -= OnApplied;
            m_drawer.Cleared -= OnCleared;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(m_propertyRestoreDefinesAfterBuild);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Scripting Define Symbols", EditorStyles.boldLabel);

            m_drawer.DrawGUILayout(m_propertyGroups);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnApplied(string groupName, BuildTargetGroup buildTargetGroup)
        {
            if (DefinesEditorSettings.Settings.TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                DefinesBuildEditorUtility.ApplyDefinesAll(buildTargetGroup, settings);
                AssetDatabase.SaveAssets();
            }
        }

        private void OnCleared(string groupName, BuildTargetGroup buildTargetGroup)
        {
            if (DefinesEditorSettings.Settings.TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                DefinesBuildEditorUtility.ClearDefinesAll(buildTargetGroup, settings);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
