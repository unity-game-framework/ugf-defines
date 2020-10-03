using UGF.CustomSettings.Runtime;
using UGF.EditorTools.Runtime.IMGUI.PlatformSettings;
using UnityEngine;

namespace UGF.Defines.Editor
{
    internal class DefinesEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private bool m_restoreDefinesAfterBuild = true;
        [SerializeField] private PlatformSettings<DefinesSettings> m_settings = new PlatformSettings<DefinesSettings>();

        public bool RestoreDefinesAfterBuild { get { return m_restoreDefinesAfterBuild; } set { m_restoreDefinesAfterBuild = value; } }
        public PlatformSettings<DefinesSettings> Settings { get { return m_settings; } }
    }
}
