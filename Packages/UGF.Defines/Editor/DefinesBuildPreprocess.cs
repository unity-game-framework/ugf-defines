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

            if (DefinesEditorSettings.TryGetSettings(group, out DefinesSettings settings) && settings.IncludeInBuild)
            {
                DefinesEditorSettings.ApplyAll(group, settings, true);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
