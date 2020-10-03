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
        private Styles m_styles;

        private class Styles
        {
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

            EditorGUI.PropertyField(includeInBuildPosition, propertyIncludeInBuild);
            EditorGUI.PropertyField(sizePosition, propertySize);
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
            SerializedProperty propertyValue = propertyElement.FindPropertyRelative("m_value");

            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            var propertyPosition = new Rect(position.x, position.y, position.width - line - space, position.height);
            var flagPosition = new Rect(propertyPosition.xMax + space, position.y, line, position.height);

            GUIContent flagContent = m_styles.FlagOffContent;

            if (!string.IsNullOrEmpty(propertyValue.stringValue) && Enum.TryParse(name, out BuildTargetGroup value))
            {
                bool hasDefine = DefinesEditorUtility.HasDefine(propertyValue.stringValue, value);

                if (hasDefine)
                {
                    flagContent = m_styles.FlagOnContent;
                }
            }

            EnabledPropertyGUIUtility.EnabledProperty(propertyPosition, GUIContent.none, propertyElement);

            GUI.Label(flagPosition, flagContent, m_styles.FlagStyle);
        }

        protected virtual void OnDrawControls(Rect position, SerializedProperty propertyGroups, string name)
        {
            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;
            float width = 50F;

            var applyPosition = new Rect(position.xMax - width - line, position.y + space * 2F, width, line);
            var clearPosition = new Rect(applyPosition.x - width - space, position.y + space * 2F, width, line);
            var clearAllPosition = new Rect(clearPosition.x - width - space - 10F, position.y + space * 2F, width + 10F, line);

            if (GUI.Button(applyPosition, m_styles.ApplyContent))
            {
                OnApply(propertyGroups, name, true);
            }

            if (GUI.Button(clearPosition, m_styles.ClearContent))
            {
                OnClearAll(propertyGroups, name, true);
            }

            if (GUI.Button(clearAllPosition, m_styles.ClearAllContent))
            {
                OnClearAll(propertyGroups, name, false);
            }
        }

        protected override float OnGetSettingsHeight(SerializedProperty propertyGroups)
        {
            float space = EditorGUIUtility.standardVerticalSpacing;
            float propertiesHeight = OnGetPropertiesHeight(propertyGroups);
            float elementsHeight = OnGetElementsHeight(propertyGroups);
            float controlsHeight = OnGetControlsHeight(propertyGroups);

            return propertiesHeight + space + elementsHeight + space + controlsHeight;
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
            return EditorGUIUtility.singleLineHeight * 2F;
        }

        protected virtual void OnApply(SerializedProperty propertyGroups, string name, bool onlyEnabled)
        {
            if (Enum.TryParse(name, out BuildTargetGroup group))
            {
            }
        }

        protected virtual void OnClearAll(SerializedProperty propertyGroups, string name, bool onlyEnabled)
        {
            if (Enum.TryParse(name, out BuildTargetGroup group))
            {
            }
        }
    }
}
