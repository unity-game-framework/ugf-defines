using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Defines.Editor
{
    internal class DefinesBuildPostprocess : IPostprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPostprocessBuild(BuildReport report)
        {
            BuildTargetGroup group = report.summary.platformGroup;

            DefinesBuildEditorUtility.ClearAll(group, DefinesEditorSettings.Settings);
            AssetDatabase.SaveAssets();
        }
    }
}
