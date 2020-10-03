using System;
using UGF.EditorTools.Editor.Defines;
using UGF.EditorTools.Editor.IMGUI.EnabledProperty;
using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UnityEditor;
using UnityEngine;

namespace UGF.Defines.Editor
{
    public class DefinesPlatformSettingsDrawer : PlatformSettingsDrawer
    {
        public event DefineGroupChangeHandler Applied;
        public event DefineGroupChangeHandler Cleared;

        public delegate void DefineGroupChangeHandler(string name, BuildTargetGroup buildTargetGroup, bool onlyEnabled);

        private Styles m_styles;

        private class Styles
        {
            public GUIContent IncludeInBuild { get; } = new GUIContent("Include In Build", "Determines whether to include specified enabled defines in player build.");
            public GUIContent Count { get; } = new GUIContent("Count");
            public GUIContent ApplyContent { get; } = new GUIContent("Apply", "Apply all enabled defines to Project settings compile symbols.");
            public GUIContent ClearContent { get; } = new GUIContent("Clear", "Clear all enabled defines from Project settings compile symbols.");
            public GUIContent ClearAllContent { get; } = new GUIContent("Clear All", "Clear all enabled and disabled defines from Project settings compile symbols.");
            public GUIContent FlagOnContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Valid"), "Define currently included in compile symbols.");
            public GUIContent FlagOffContent { get; } = new GUIContent("X", "Define currently NOT included in compile symbols.");

            public GUIStyle FlagStyle { get; } = new GUIStyle("MiniLabel")
            {
                alignment = TextAnchor.MiddleCenter,
                normal =
                {
                    textColor = Color.grey
                }
            };
        }

        public DefinesPlatformSettingsDrawer()
        {
            AutoSettingsInstanceCreation = true;
            AllowEmptySettings = false;
        }

        public void SetupGroupTypes()
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                string group = Groups[i];

                AddGroupType(group, typeof(DefinesSettings));
            }
        }

        protected override void OnDrawGUI(Rect position, SerializedProperty propertyGroups)
        {
            if (m_styles == null) m_styles = new Styles();

            base.OnDrawGUI(position, propertyGroups);
        }

        protected override void OnDrawSettings(Rect position, SerializedProperty propertyGroups, string name)
        {
            float space = EditorGUIUtility.standardVerticalSpacing;

            float propertiesHeight = OnGetPropertiesHeight(propertyGroups);
            float elementsHeight = OnGetElementsHeight(propertyGroups);
            float controlsHeight = OnGetControlsHeight(propertyGroups);

            var propertiesPosition = new Rect(position.x, position.y, position.width, propertiesHeight);
            var elementsPosition = new Rect(position.x, propertiesPosition.yMax + space, position.width, elementsHeight);
            var controlsPosition = new Rect(position.x, elementsPosition.yMax + space, position.width, controlsHeight);

            OnDrawProperties(propertiesPosition, propertyGroups, name);
            OnDrawElements(elementsPosition, propertyGroups, name);
            OnDrawControls(controlsPosition, propertyGroups, name);
        }

        protected virtual void OnDrawProperties(Rect position, SerializedProperty propertyGroups, string name)
        {
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");
            SerializedProperty propertyIncludeInBuild = propertySettings.FindPropertyRelative("m_includeInBuild");
            SerializedProperty propertySize = propertyDefines.FindPropertyRelative("Array.size");

            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var includeInBuildPosition = new Rect(position.x, position.y, position.width, line);
            var sizePosition = new Rect(position.x, includeInBuildPosition.yMax + space, position.width, line);

            EditorGUI.PropertyField(includeInBuildPosition, propertyIncludeInBuild, m_styles.IncludeInBuild);
            EditorGUI.PropertyField(sizePosition, propertySize, m_styles.Count);
        }

        protected virtual void OnDrawElements(Rect position, SerializedProperty propertyGroups, string name)
        {
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");

            float space = EditorGUIUtility.standardVerticalSpacing;

            for (int i = 0; i < propertyDefines.arraySize; i++)
            {
                float height = OnGetElementHeight(propertyGroups, i);

                position.height = height;

                OnDrawElement(position, propertyGroups, i);

                position.y += height + space;
            }
        }

        protected virtual void OnDrawElement(Rect position, SerializedProperty propertyGroups, int index)
        {
            string name = GetSelectedGroupName();
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");
            SerializedProperty propertyElement = propertyDefines.GetArrayElementAtIndex(index);

            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var propertyPosition = new Rect(position.x, position.y, position.width - line - space, position.height);
            var flagPosition = new Rect(propertyPosition.xMax + space, position.y, line, position.height);

            EnabledPropertyGUIUtility.EnabledProperty(propertyPosition, GUIContent.none, propertyElement);

            DrawDefineFlag(flagPosition, propertyGroups, index);
        }

        protected virtual void OnDrawControls(Rect position, SerializedProperty propertyGroups, string name)
        {
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");

            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;
            float width = 50F;

            position.y += space;

            bool hasApply = Applied != null;
            bool hasClear = Cleared != null;
            bool hasAny = hasApply || hasClear;
            bool hasDefines = propertyDefines.arraySize > 0;

            var applyPosition = new Rect(position.xMax - width - line, position.y, width, line);
            var clearPosition = new Rect(applyPosition.x - width - space, position.y, width, line);
            var clearAllPosition = new Rect(clearPosition.x - width - space - 10F, position.y, width + 10F, line);

            if (hasAny)
            {
                using (new EditorGUI.DisabledScope(!hasApply || !hasDefines))
                {
                    if (GUI.Button(applyPosition, m_styles.ApplyContent))
                    {
                        OnApply(propertyGroups, name, true);
                    }
                }

                using (new EditorGUI.DisabledScope(!hasClear || !hasDefines))
                {
                    if (GUI.Button(clearPosition, m_styles.ClearContent))
                    {
                        OnClearAll(propertyGroups, name, true);
                    }

                    if (GUI.Button(clearAllPosition, m_styles.ClearAllContent))
                    {
                        OnClearAll(propertyGroups, name, false);
                    }
                }
            }
        }

        protected override float OnGetSettingsHeight(SerializedProperty propertyGroups)
        {
            float space = EditorGUIUtility.standardVerticalSpacing;
            float propertiesHeight = OnGetPropertiesHeight(propertyGroups);
            float elementsHeight = OnGetElementsHeight(propertyGroups);
            float controlsHeight = OnGetControlsHeight(propertyGroups);

            if (controlsHeight > 0F)
            {
                controlsHeight += 5F;
            }
            else
            {
                controlsHeight = space;
            }

            return propertiesHeight + elementsHeight + controlsHeight + space * 4F;
        }

        protected virtual float OnGetPropertiesHeight(SerializedProperty propertyGroups)
        {
            return EditorGUIUtility.singleLineHeight * 2F + EditorGUIUtility.standardVerticalSpacing;
        }

        protected virtual float OnGetElementsHeight(SerializedProperty propertyGroups)
        {
            string name = GetSelectedGroupName();
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");

            float space = EditorGUIUtility.standardVerticalSpacing;
            float height = 0F;

            for (int i = 0; i < propertyDefines.arraySize; i++)
            {
                height += OnGetElementHeight(propertyDefines, i) + space;
            }

            return height;
        }

        protected virtual float OnGetElementHeight(SerializedProperty propertyGroups, int index)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        protected virtual float OnGetControlsHeight(SerializedProperty propertyGroups)
        {
            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            bool hasApply = Applied != null;
            bool hasClear = Cleared != null;

            return hasApply || hasClear ? line + space * 2F : 0F;
        }

        protected virtual void OnApply(SerializedProperty propertyGroups, string name, bool onlyEnabled)
        {
            if (Enum.TryParse(name, out BuildTargetGroup group))
            {
                Applied?.Invoke(name, group, onlyEnabled);
            }
        }

        protected virtual void OnClearAll(SerializedProperty propertyGroups, string name, bool onlyEnabled)
        {
            if (Enum.TryParse(name, out BuildTargetGroup group))
            {
                Cleared?.Invoke(name, group, onlyEnabled);
            }
        }

        protected virtual void DrawDefineFlag(Rect position, SerializedProperty propertyGroups, int index)
        {
            string name = GetSelectedGroupName();
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");
            SerializedProperty propertyElement = propertyDefines.GetArrayElementAtIndex(index);
            SerializedProperty propertyValue = propertyElement.FindPropertyRelative("m_value");

            GUIContent flagContent = m_styles.FlagOffContent;

            if (!string.IsNullOrEmpty(propertyValue.stringValue) && Enum.TryParse(name, out BuildTargetGroup value))
            {
                bool hasDefine = DefinesEditorUtility.HasDefine(propertyValue.stringValue, value);

                if (hasDefine)
                {
                    flagContent = m_styles.FlagOnContent;
                }
            }

            GUI.Label(position, flagContent, m_styles.FlagStyle);
        }
    }
}
