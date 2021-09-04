using System.Collections.Generic;
using UGF.EditorTools.Editor.Defines;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.EnabledProperty;
using UGF.EditorTools.Editor.IMGUI.PlatformSettings;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UGF.EditorTools.Editor.Platforms;
using UnityEditor;
using UnityEngine;

namespace UGF.Defines.Editor
{
    internal class DefinesPlatformSettingsDrawer2 : PlatformSettingsDrawer
    {
        private readonly Dictionary<string, ReorderableListDrawer> m_listDrawers = new Dictionary<string, ReorderableListDrawer>();
        private const float PADDING = 5F;

        private Styles m_styles;

        private class Styles
        {
            public GUIContent IncludeInBuild { get; } = new GUIContent("Include In Build", "Determines whether to include enabled defines in player build.");
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

        public DefinesPlatformSettingsDrawer2()
        {
            AllowEmptySettings = false;
            AutoSettingsInstanceCreation = true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            AddPlatformAllAvailable();

            for (int i = 0; i < PlatformEditorUtility.PlatformsAllAvailable.Count; i++)
            {
                PlatformInfo platform = PlatformEditorUtility.PlatformsAllAvailable[i];

                AddGroupType(platform.Name, typeof(DefinesSettings));
            }

            foreach (KeyValuePair<string, ReorderableListDrawer> pair in m_listDrawers)
            {
                pair.Value.Enable();
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ClearGroups();
            ClearGroupTypes();

            foreach (KeyValuePair<string, ReorderableListDrawer> pair in m_listDrawers)
            {
                pair.Value.Disable();
            }

            m_listDrawers.Clear();
        }

        protected override void OnDrawGUI(Rect position, SerializedProperty propertyGroups)
        {
            m_styles ??= new Styles();

            base.OnDrawGUI(position, propertyGroups);
        }

        protected override void OnDrawSettings(Rect position, SerializedProperty propertyGroups, string name)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            ReorderableListDrawer drawer = GetListDrawer(name, propertySettings);

            using (new LabelWidthChangeScope(-PADDING))
            {
                var rectPlatformName = new Rect(position.x, position.y, position.width, height);
                var rectIncludeInBuild = new Rect(position.x, rectPlatformName.yMax + space, position.width, height);
                var rectDefines = new Rect(position.x, rectIncludeInBuild.yMax + space, position.width, height);

                SerializedProperty propertyIncludeInBuild = propertySettings.FindPropertyRelative("m_includeInBuild");

                OnDrawSettingsPlatformName(rectPlatformName, propertyGroups, name);

                EditorGUI.PropertyField(rectIncludeInBuild, propertyIncludeInBuild);

                drawer.DrawGUI(rectDefines);
            }
        }

        protected override float OnGetSettingsHeight(SerializedProperty propertyGroups)
        {
            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            string name = GetSelectedGroupName();
            SerializedProperty propertySettings = OnGetSettings(propertyGroups, name);
            ReorderableListDrawer drawer = GetListDrawer(name, propertySettings);

            float heightDefines = drawer.SerializedProperty.isExpanded ? drawer.GetHeight() : height;

            return height * 2F + space * 3F + heightDefines + PADDING * 2F;
        }

        protected override void OnCreateSettings(SerializedProperty propertyGroups, string name, SerializedProperty propertySettings)
        {
            base.OnCreateSettings(propertyGroups, name, propertySettings);

            GetListDrawer(name, propertySettings);
        }

        private ReorderableListDrawer GetListDrawer(string name, SerializedProperty propertySettings)
        {
            if (!m_listDrawers.TryGetValue(name, out ReorderableListDrawer drawer))
            {
                SerializedProperty propertyDefines = propertySettings.FindPropertyRelative("m_defines");

                drawer = new ReorderableListDrawer(propertyDefines);
                drawer.List.drawElementCallback = (rect, index, active, focused) => { OnListDrawElement(drawer, name, rect, index); };

                m_listDrawers.Add(name, drawer);
            }

            return drawer;
        }

        private void OnListDrawElement(ReorderableListDrawer drawer, string name, Rect rect, int index)
        {
            PlatformInfo platform = PlatformEditorUtility.GetPlatform(name);

            float height = EditorGUIUtility.singleLineHeight;
            float space = EditorGUIUtility.standardVerticalSpacing;

            rect.y += space;

            SerializedProperty propertyElement = drawer.SerializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty propertyValue = propertyElement.FindPropertyRelative("m_value");

            var rectDefine = new Rect(rect.x, rect.y, rect.width - height + space, height);
            var rectFlag = new Rect(rectDefine.xMax + space, rect.y, height, height);

            GUIContent flagContent = !string.IsNullOrEmpty(propertyValue.stringValue) && DefinesEditorUtility.HasDefine(propertyValue.stringValue, platform.BuildTargetGroup)
                ? m_styles.FlagOnContent
                : m_styles.FlagOffContent;

            EnabledPropertyGUIUtility.EnabledProperty(rectDefine, GUIContent.none, propertyElement, false);
            GUI.Label(rectFlag, flagContent, m_styles.FlagStyle);
        }
    }
}
