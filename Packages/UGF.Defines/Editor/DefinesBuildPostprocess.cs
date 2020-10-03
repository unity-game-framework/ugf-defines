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

            if (DefinesEditorSettings.TryGetSettings(group, out DefinesSettings settings) && settings.IncludeInBuild)
            {
                DefinesEditorSettings.ClearAll(group, settings);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
