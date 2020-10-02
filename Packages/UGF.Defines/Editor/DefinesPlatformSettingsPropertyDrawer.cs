using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEditor;
using UnityEngine;

namespace UGF.Defines.Editor
{
    [CustomPropertyDrawer(typeof(PlatformSettings<DefinesSettings>), true)]
    internal class DefinesPlatformSettingsPropertyDrawer : PlatformSettingsPropertyDrawer
    {
        public DefinesPlatformSettingsPropertyDrawer()
        {
            var drawer = new DefinesPlatformSettingsDrawer();

            Drawer = drawer;
            Drawer.AddPlatformAllAvailable();

            drawer.SetupGroupTypes();
        }
    }
}
