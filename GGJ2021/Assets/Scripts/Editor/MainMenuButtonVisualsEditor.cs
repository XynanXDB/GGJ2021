using Game.Runtime.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Game.Editor
{
    [CustomEditor(typeof(MainMenuButtonVisuals))]
    public class MainMenuButtonVisualsEditor : ButtonEditor
    {
        private SerializedProperty SelectorProp;
        private SerializedProperty TextProp;

        private const string Selector = "Selector";
        private const string Text = "Text";

        private GUIContent SelectorGUI;
        private GUIContent TextGUI;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            SelectorProp = serializedObject.FindProperty(Selector);
            TextProp = serializedObject.FindProperty(Text);

            SelectorGUI = new GUIContent(Selector);
            TextGUI = new GUIContent(Text);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.PropertyField(SelectorProp, SelectorGUI);
            EditorGUILayout.PropertyField(TextProp, TextGUI);

            serializedObject.ApplyModifiedProperties();
        }
    }
}