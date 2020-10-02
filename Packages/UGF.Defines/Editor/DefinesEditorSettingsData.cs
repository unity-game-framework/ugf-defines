using UGF.CustomSettings.Runtime;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEngine;

namespace UGF.Defines.Editor
{
    internal class DefinesEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private PlatformSettings<DefinesSettings> m_settings = new PlatformSettings<DefinesSettings>();

        public PlatformSettings<DefinesSettings> Settings { get { return m_settings; } }
    }
}
