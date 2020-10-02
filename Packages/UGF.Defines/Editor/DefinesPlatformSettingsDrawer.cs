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
            public GUIContent FlagOnContent { get; } = new GUIContent(EditorGUIUtility.FindTexture("Valid"), "Define included in compile symbols.");
            public GUIContent FlagOffContent { get; } = new GUIContent("X", "Define NOT included in compile symbols.");

            public GUIStyle FlagStyle { get; } = new GUIStyle("MiniLabel")
            {
                alignment = TextAnchor.MiddleCenter,
                normal =
                {
                    textColor = Color.grey
                },
                // fontStyle = FontStyle.Bold
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
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");
            SerializedProperty propertyIncludeInBuild = propertySettings.FindPropertyRelative("m_includeInBuild");
            SerializedProperty propertySize = propertyDefines.FindPropertyRelative("Array.size");

            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;
            var buildPosition = new Rect(position.x, position.y, position.width, line);
            var sizePosition = new Rect(position.x, buildPosition.yMax + space, position.width, line);
            var elementPosition = new Rect(position.x, sizePosition.y, position.width - line - space, line);
            var flagPosition = new Rect(elementPosition.xMax + space, sizePosition.y, line, line);

            EditorGUI.PropertyField(buildPosition, propertyIncludeInBuild);
            EditorGUI.PropertyField(sizePosition, propertySize);

            for (int i = 0; i < propertyDefines.arraySize; i++)
            {
                SerializedProperty propertyElement = propertyDefines.GetArrayElementAtIndex(i);
                SerializedProperty propertyValue = propertyElement.FindPropertyRelative("m_value");

                elementPosition.y += line + space;
                flagPosition.y += line + space;

                EnabledPropertyGUIUtility.EnabledProperty(elementPosition, GUIContent.none, propertyElement);

                bool hasValue = !string.IsNullOrEmpty(propertyValue.stringValue);
                bool hasDefine = hasValue && DefinesEditorUtility.HasDefine(propertyValue.stringValue, BuildTargetGroup.Standalone);
                GUIContent flagContent = hasDefine ? m_styles.FlagOnContent : m_styles.FlagOffContent;

                GUI.Label(flagPosition, flagContent, m_styles.FlagStyle);
            }
        }

        protected override float OnGetSettingsHeight(SerializedProperty propertyGroups)
        {
            string name = GetSelectedGroupName();
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");

            float line = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;
            float height = line * 2F + space * 6F;

            height += (line + space) * propertyDefines.arraySize;

            return height;
        }
    }
}
