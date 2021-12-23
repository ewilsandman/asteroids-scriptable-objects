using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
         /*   if (transform.GetComponent(typeof(Event)) is Event)
           {
               foreach ()
               {
                   EditorGUILayout.TextField("Event", transform.tag)
               }
           }*/
           
           _Logs = GUILayout.Button("Toggle logs from " + transform.name);
           if (_Logs)
           {
               ToggleLogs(transform);
           }
           GUILayout.Space(10);
           GUILayout.EndVertical();
            for (int i = 0; i < transform.childCount; i++) {
                var child = transform.GetChild(i);
                ForEachChildInTransform(child);
            }
        }
        private static void ToggleLogs(Transform transform)
        {
          Debug.Log("Trying to toggle data for id " + transform.gameObject.GetInstanceID());
        }
    }
}