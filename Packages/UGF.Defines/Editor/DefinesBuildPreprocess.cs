using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Defines.Editor
{
    internal class DefinesBuildPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; } = int.MinValue;

        public void OnPreprocessBuild(BuildReport report)
        {
            BuildTargetGroup group = report.summary.platformGroup;

            if (DefinesEditorSettings.RestoreDefinesAfterBuild)
            {
                DefinesBuildEditorUtility.SaveScriptingDefineSymbolsForGroup(group);
            }

            if (DefinesEditorSettings.Settings.TryGetSettings(group, out DefinesSettings settings) && settings.IncludeInBuild)
            {
                DefinesBuildEditorUtility.ApplyDefinesAll(group, settings);
            }

            AssetDatabase.SaveAssets();
        }
    }
}
