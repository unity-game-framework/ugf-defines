using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.Defines.Editor
{
    internal class DefinesEditorSettingsData : CustomSettingsData
    {
        [SerializeField] private DefinesPlatformSettings m_settings = new DefinesPlatformSettings();

        public DefinesPlatformSettings Settings { get { return m_settings; } }
    }
}
