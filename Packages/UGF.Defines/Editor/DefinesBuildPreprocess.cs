using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Defines.Editor
{
    internal class DefinesBuildPreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            BuildTargetGroup group = report.summary.platformGroup;

            DefinesBuildEditorUtility.ApplyAll(group, DefinesEditorSettings.Settings, true);
            AssetDatabase.SaveAssets();
        }
    }
}
