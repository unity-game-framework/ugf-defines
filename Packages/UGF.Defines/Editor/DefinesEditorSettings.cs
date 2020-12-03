using UGF.CustomSettings.Editor;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static class DefinesEditorSettings
    {
        public static bool RestoreDefinesAfterBuild
        {
            get { return Settings.GetData().RestoreDefinesAfterBuild; }
            set
            {
                Settings.GetData().RestoreDefinesAfterBuild = value;
                Settings.SaveSettings();
            }
        }

        public static PlatformSettings<DefinesSettings> PlatformSettings { get { return Settings.GetData().Settings; } }

        public static CustomSettingsEditorPackage<DefinesEditorSettingsData> Settings { get; } = new CustomSettingsEditorPackage<DefinesEditorSettingsData>
        (
            "UGF.Defines",
            "DefinesEditorSettings"
        );

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<DefinesEditorSettingsData>("Project/UGF/Defines", Settings, SettingsScope.Project);
        }
    }
}
