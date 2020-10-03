using UGF.CustomSettings.Editor;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static class DefinesEditorSettings
    {
        public static bool RestoreDefinesAfterBuild
        {
            get { return m_settings.Data.RestoreDefinesAfterBuild; }
            set
            {
                m_settings.Data.RestoreDefinesAfterBuild = value;
                m_settings.SaveSettings();
            }
        }

        public static PlatformSettings<DefinesSettings> Settings { get { return m_settings.Data.Settings; } }

        private static readonly CustomSettingsEditorPackage<DefinesEditorSettingsData> m_settings = new CustomSettingsEditorPackage<DefinesEditorSettingsData>
        (
            "UGF.Defines",
            "DefinesEditorSettings"
        );

        public static void Save()
        {
            m_settings.SaveSettings();
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<DefinesEditorSettingsData>("Project/UGF/Defines", m_settings, SettingsScope.Project);
        }
    }
}
