using System;
using UGF.EditorTools.Runtime.IMGUI.EnabledProperty;
using UnityEngine;

namespace UGF.Defines.Editor
{
    [Serializable]
    public class DefinesSettings
    {
        [SerializeField] private EnabledProperty<string> m_define1;
        [SerializeField] private EnabledProperty<string> m_define2;
        [SerializeField] private EnabledProperty<string> m_define3;

        public EnabledProperty<string> Define1 { get { return m_define1; } set { m_define1 = value; } }
        public EnabledProperty<string> Define2 { get { return m_define2; } set { m_define2 = value; } }
        public EnabledProperty<string> Define3 { get { return m_define3; } set { m_define3 = value; } }
    }
}
