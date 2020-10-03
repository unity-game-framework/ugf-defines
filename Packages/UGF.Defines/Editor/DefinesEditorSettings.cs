using System;
using UGF.CustomSettings.Editor;
using UGF.EditorTools.Editor.Defines;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static class DefinesEditorSettings
    {
        public static PlatformSettings<DefinesSettings> Settings { get { return m_settings.Data.Settings; } }

        private static readonly CustomSettingsEditorPackage<DefinesEditorSettingsData> m_settings = new CustomSettingsEditorPackage<DefinesEditorSettingsData>
        (
            "UGF.Defines",
            "DefinesEditorSettings"
        );

        public static bool TryGetSettings(BuildTargetGroup buildTargetGroup, out DefinesSettings settings)
        {
            string name = buildTargetGroup.ToString();

            return m_settings.Data.Settings.TryGetSettings(name, out settings);
        }

        public static void Save()
        {
            m_settings.SaveSettings();
        }

        public static bool ApplyAll(BuildTargetGroup buildTargetGroup, bool onlyEnabled = false)
        {
            if (TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                ApplyAll(buildTargetGroup, settings, onlyEnabled);
                return true;
            }

            return false;
        }

        public static void ApplyAll(BuildTargetGroup buildTargetGroup, DefinesSettings settings, bool onlyEnabled = false)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            for (int i = 0; i < settings.Defines.Count; i++)
            {
                EnabledProperty<string> define = settings.Defines[i];

                if (!string.IsNullOrEmpty(define) && (!onlyEnabled || define))
                {
                    DefinesEditorUtility.SetDefine(define, buildTargetGroup);
                }
            }
        }

        public static bool ClearAll(BuildTargetGroup buildTargetGroup, bool onlyEnabled = false)
        {
            if (TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                ClearAll(buildTargetGroup, settings, onlyEnabled);
                return true;
            }

            return false;
        }

        public static void ClearAll(BuildTargetGroup buildTargetGroup, DefinesSettings settings, bool onlyEnabled = false)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            for (int i = 0; i < settings.Defines.Count; i++)
            {
                EnabledProperty<string> define = settings.Defines[i];

                if (!string.IsNullOrEmpty(define) && (!onlyEnabled || define))
                {
                    DefinesEditorUtility.RemoveDefine(define, buildTargetGroup);
                }
            }
        }

        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<DefinesEditorSettingsData>("Project/UGF/Defines", m_settings, SettingsScope.Project);
        }
    }
}
