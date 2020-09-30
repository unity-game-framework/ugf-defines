using System.Collections.Generic;
using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UnityEditor;

namespace UGF.Defines.Editor
{
    [CustomPropertyDrawer(typeof(DefinesPlatformSettings), true)]
    internal class DefinesPlatformSettingsPropertyDrawer : PlatformSettingsPropertyDrawerBase
    {
        public DefinesPlatformSettingsPropertyDrawer()
        {
            var platforms = new List<BuildTargetGroup>();

            PlatformSettingsEditorUtility.GetPlatformsAvailable(platforms);

            for (int i = 0; i < platforms.Count; i++)
            {
                BuildTargetGroup platform = platforms[i];

                Drawer.AddPlatform(platform);
            }
        }

        protected override void OnDrawerSettingsCreated(string name, SerializedProperty propertySettings)
        {
            propertySettings.managedReferenceValue = new DefinesSettings();
        }
    }
}
