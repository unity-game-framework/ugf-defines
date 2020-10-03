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

        public bool SetEnable(string name, bool value)
        {
            if (TryGetIndex(name, out int index))
            {
                EnabledProperty<string> define = m_defines[index];

                define.Enabled = value;

                m_defines[index] = define;
                return true;
            }

            return false;
        }

        public EnabledProperty<string> Get(string name)
        {
            return TryGet(name, out EnabledProperty<string> define) ? define : throw new ArgumentException($"Define not found by the specified name: '{name}'.");
        }

        public bool TryGet(string name, out EnabledProperty<string> define)
        {
            if (TryGetIndex(name, out int index))
            {
                define = m_defines[index];
                return true;
            }

            define = default;
            return false;
        }

        public bool TryGetIndex(string name, out int index)
        {
            for (int i = 0; i < m_defines.Count; i++)
            {
                EnabledProperty<string> define = m_defines[i];

                if (define.Value == name)
                {
                    index = i;
                    return true;
                }
            }

            index = default;
            return false;
        }
    }
}
