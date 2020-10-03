using System;
using UGF.EditorTools.Editor.Defines;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEditor;

namespace UGF.Defines.Editor
{
    public static partial class DefinesBuildEditorUtility
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
    }
}
