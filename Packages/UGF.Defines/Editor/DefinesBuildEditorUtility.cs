using System;
using UGF.EditorTools.Editor.Defines;
using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static class DefinesBuildEditorUtility
    {
        public static void ApplyAll(BuildTargetGroup buildTargetGroup, PlatformSettings<DefinesSettings> platformSettings, bool onlyEnabled = false)
        {
            if (platformSettings == null) throw new ArgumentNullException(nameof(platformSettings));

            if (platformSettings.TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                if (!onlyEnabled || settings.IncludeInBuild)
                {
                    ApplyAll(buildTargetGroup, settings, onlyEnabled);
                }
            }
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

        public static void ClearAll(BuildTargetGroup buildTargetGroup, PlatformSettings<DefinesSettings> platformSettings, bool onlyEnabled = false)
        {
            if (platformSettings == null) throw new ArgumentNullException(nameof(platformSettings));

            if (platformSettings.TryGetSettings(buildTargetGroup, out DefinesSettings settings))
            {
                if (!onlyEnabled || settings.IncludeInBuild)
                {
                    ClearAll(buildTargetGroup, settings, onlyEnabled);
                }
            }
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
    }
}
