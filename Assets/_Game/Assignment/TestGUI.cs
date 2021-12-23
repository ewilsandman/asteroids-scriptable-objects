using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditorInternal;

namespace assignment
{
    public class TestGUI : EditorWindow
    {
        // ReorderableList ???
        string myString = "Hello World";
        bool groupEnabled;
        static bool _Logs;
        bool _Button;
        private Vector2 scroll;
        
        [MenuItem("Window/Test Window")]
        static void Init()
        {
            TestGUI window = (TestGUI)EditorWindow.GetWindow(typeof(TestGUI));
            window.Show();
        }
        void OnGUI()
        {
            _Logs = false;
            scroll = GUILayout.BeginScrollView(scroll);
            _Button = EditorGUILayout.Toggle("Refresh", _Button);
            if (_Button)
            {
                ForEachRootGameObjectInScene(SceneManager.GetActiveScene());
            }
            GUILayout.EndScrollView();
        }
        private static void ForEachRootGameObjectInScene(Scene scene)
        {
            foreach (var go in scene.GetRootGameObjects())
            {
                ForEachChildInTransform(go.transform);
            }
        }
        private static void ForEachChildInTransform(Transform transform)
        { 
            GUILayout.BeginVertical();
           transform.name = EditorGUILayout.TextField("Name", transform.name);
           transform.tag = EditorGUILayout.TextField("Tag", transform.tag);
           foreach (Component c in transform.GetComponents<Component>())
           {
               OnValidFields(c);
           }
           GUILayout.Space(10);
           GUILayout.EndVertical();
            for (int i = 0; i < transform.childCount; i++) {
                var child = transform.GetChild(i);
                ForEachChildInTransform(child);
            }
        }
        private static void OnValidFields(Component component) // based on David's code, checks for and displays events
        {
            const BindingFlags bindingFlags =
                BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Public;

            var type = component.GetType();
            IEnumerable<FieldInfo> fields = type.GetFields(bindingFlags)
                .Where
                (
                    item => typeof(DefaultNamespace.ScriptableEvents.ScriptableEventBase)
                        .IsAssignableFrom(item.FieldType)
                );

            foreach (var field in fields) 
            {
                EditorGUILayout.LabelField("Event", field.Name);
                _Logs = GUILayout.Button("Toggle Event Data");
                if (_Logs)
                {
                    if (field.FieldType.Equals(typeof(Event)))
                    {
                        var next = EditorGUILayout.IntField((int) field.GetValue(component));
                        field.SetValue(component, next);
                        //(event)field.GetValue(component) += OnEventTriggered();
                    //    field.SetValue(); += OnEventTriggered();
                    }
                }
            }
        }

        private static void OnEventTriggered()
        {
            //Debug.Log("Event " + name + " triggered with value " + value);
        }
    }
}