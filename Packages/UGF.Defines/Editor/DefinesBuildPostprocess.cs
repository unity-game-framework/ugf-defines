using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Defines.Editor
{
    internal class DefinesBuildPostprocess : IPostprocessBuildWithReport
    {
        public int callbackOrder { get; } = 1000000;

        public void OnPostprocessBuild(BuildReport report)
        {
            BuildTargetGroup group = report.summary.platformGroup;

            if (DefinesEditorSettings.RestoreDefinesAfterBuild && DefinesBuildEditorUtility.TryLoadScriptingDefineSymbolsForGroup(group))
            {
                AssetDatabase.SaveAssets();
            }
        }
    }
}
