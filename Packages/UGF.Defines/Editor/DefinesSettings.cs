using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEngine;

namespace UGF.Defines.Editor
{
    [Serializable]
    public class DefinesSettings
    {
        [SerializeField] private bool m_includeInBuild = true;
        [SerializeField] private List<EnabledProperty<string>> m_defines = new List<EnabledProperty<string>>();

        public bool IncludeInBuild { get { return m_includeInBuild; } set { m_includeInBuild = value; } }
        public List<EnabledProperty<string>> Defines { get { return m_defines; } }
    }
}
