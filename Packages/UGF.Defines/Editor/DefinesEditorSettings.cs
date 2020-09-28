using UGF.CustomSettings.Editor;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static class DefinesEditorSettings
    {
        private static readonly CustomSettingsEditorPackage<DefinesEditorSettingsData> m_settings = new CustomSettingsEditorPackage<DefinesEditorSettingsData>
        (
            "UGF.Defines",
            "DefinesEditorSettings"
        );

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<DefinesEditorSettingsData>("Project/UGF/Defines", m_settings, SettingsScope.Project);
        }
    }
}
