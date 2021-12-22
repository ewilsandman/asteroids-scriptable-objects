using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace assignment
{
    public class TestGUI : EditorWindow
    {
        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        bool _Button;
        float myFloat = 1.23f;

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Test Window")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            TestGUI window = (TestGUI)EditorWindow.GetWindow(typeof(TestGUI));
            window.Show();
        }
        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);
            _Button = EditorGUILayout.Toggle("Refresh", _Button);
            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
            if (_Button)
            {
                ForEachRootGameObjectInScene(SceneManager.GetActiveScene());
            }
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
            EditorGUILayout.TextField(transform.name);
            GUILayout.EndVertical();
            for (int i = 0; i < transform.childCount; i++) {
                var child = transform.GetChild(i);
                ForEachChildInTransform(child);
            }
        }

    }
}