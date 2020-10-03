using UnityEditor;

namespace UGF.Defines.Editor
{
    public static partial class DefinesBuildEditorUtility
    {
        internal static void SaveCurrentDefines(BuildTargetGroup buildTargetGroup)
        {
            string key = $"{typeof(DefinesBuildEditorUtility).FullName}.{buildTargetGroup}";
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

            EditorPrefs.SetString(key, defines);
        }

        internal static void LoadPreviouslySavedDefines(BuildTargetGroup buildTargetGroup)
        {
            string key = $"{typeof(DefinesBuildEditorUtility).FullName}.{buildTargetGroup}";

            if (EditorPrefs.HasKey(key))
            {
                string defines = EditorPrefs.GetString(key);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, defines);
            }
        }
    }
}
