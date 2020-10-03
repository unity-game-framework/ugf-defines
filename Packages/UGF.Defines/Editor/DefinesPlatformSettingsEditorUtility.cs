using System;
using UGF.EditorTools.Editor.Defines;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static class DefinesPlatformSettingsEditorUtility
    {
        public static void ApplyAll(BuildTargetGroup buildTargetGroup, DefinesSettings settings, bool onlyEnabled = false)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            for (int i = 0; i < settings.Defines.Count; i++)
            {
                EnabledProperty<string> define = settings.Defines[i];

                if (!onlyEnabled || define)
                {
                    DefinesEditorUtility.SetDefine(define, buildTargetGroup);
                }
            }
        }

        public static void ClearAll(BuildTargetGroup buildTargetGroup, DefinesSettings settings, bool onlyEnabled = false)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            for (int i = 0; i < settings.Defines.Count; i++)
            {
                EnabledProperty<string> define = settings.Defines[i];

                if (!onlyEnabled || define)
                {
                    DefinesEditorUtility.RemoveDefine(define, buildTargetGroup);
                }
            }
        }
    }
}
