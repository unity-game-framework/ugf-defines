using System;
using UGF.EditorTools.Editor.Defines;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static class DefinesBuildEditorUtility
    {
        public static void ApplyDefinesAll(BuildTargetGroup buildTargetGroup, DefinesSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            for (int i = 0; i < settings.Defines.Count; i++)
            {
                EnabledProperty<string> define = settings.Defines[i];

                if (!string.IsNullOrEmpty(define))
                {
                    if (define)
                    {
                        DefinesEditorUtility.SetDefine(define, buildTargetGroup);
                    }
                    else
                    {
                        DefinesEditorUtility.RemoveDefine(define, buildTargetGroup);
                    }
                }
            }
        }

        public static void ClearDefinesAll(BuildTargetGroup buildTargetGroup, DefinesSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            for (int i = 0; i < settings.Defines.Count; i++)
            {
                EnabledProperty<string> define = settings.Defines[i];

                if (!string.IsNullOrEmpty(define))
                {
                    DefinesEditorUtility.RemoveDefine(define, buildTargetGroup);
                }
            }
        }

        public static void SaveScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup)
        {
            string key = $"{typeof(DefinesBuildEditorUtility).FullName}.{buildTargetGroup}";
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            EditorPrefs.SetString(key, defines);
        }

        public static bool TryLoadScriptingDefineSymbolsForGroup(BuildTargetGroup buildTargetGroup)
        {
            string key = $"{typeof(DefinesBuildEditorUtility).FullName}.{buildTargetGroup}";

            if (EditorPrefs.HasKey(key))
            {
                string defines = EditorPrefs.GetString(key);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);

                return true;
            }

            return false;
        }
    }
}
