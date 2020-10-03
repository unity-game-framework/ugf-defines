using UnityEditor;

namespace UGF.Defines.Editor
{
    [CustomEditor(typeof(DefinesEditorSettingsData), true)]
    internal class DefinesEditorSettingsDataEditor : UnityEditor.Editor
    {
        private readonly DefinesPlatformSettingsDrawer m_drawer = new DefinesPlatformSettingsDrawer();
        private SerializedProperty m_propertyGroups;

        private void OnEnable()
        {
            m_drawer.AddPlatformAllAvailable();
            m_drawer.SetupGroupTypes();
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

            m_drawer.DrawGUILayout(m_propertyGroups);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnApplied(string groupName, BuildTargetGroup buildTargetGroup, bool onlyEnabled)
        {
            DefinesBuildEditorUtility.ApplyAll(buildTargetGroup, DefinesEditorSettings.Settings, onlyEnabled);
            AssetDatabase.SaveAssets();
        }

        private void OnCleared(string groupName, BuildTargetGroup buildTargetGroup, bool onlyEnabled)
        {
            DefinesBuildEditorUtility.ClearAll(buildTargetGroup, DefinesEditorSettings.Settings, onlyEnabled);
            AssetDatabase.SaveAssets();
        }
    }
}
