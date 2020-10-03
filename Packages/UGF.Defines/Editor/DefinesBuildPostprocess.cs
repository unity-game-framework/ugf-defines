using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace UGF.Defines.Editor
{
    internal class DefinesBuildPostprocess : IPostprocessBuildWithReport
    {
        public int callbackOrder { get; } = int.MaxValue;

        public void OnPostprocessBuild(BuildReport report)
        {
            BuildTargetGroup group = report.summary.platformGroup;

            if (DefinesEditorSettings.RestoreDefinesAfterBuild)
            {
                DefinesBuildEditorUtility.LoadPreviouslySavedDefines(group);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
