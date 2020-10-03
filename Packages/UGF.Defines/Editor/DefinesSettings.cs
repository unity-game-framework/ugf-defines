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

        public bool Contains(string name)
        {
            return TryGet(name, out _);
        }

        public bool IsEnabled(string name)
        {
            return TryGet(name, out EnabledProperty<string> define) && define;
        }

        public EnabledProperty<string> Get(string name)
        {
            return TryGet(name, out EnabledProperty<string> define) ? define : throw new ArgumentException($"Define not found by the specified name: '{name}'.");
        }

        public bool TryGet(string name, out EnabledProperty<string> define)
        {
            for (int i = 0; i < m_defines.Count; i++)
            {
                define = m_defines[i];

                if (define.Value == name)
                {
                    return true;
                }
            }

            define = default;
            return false;
        }
    }
}
