using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditorInternal;
using Variables;
using DefaultNamespace.ScriptableEvents;

namespace assignment
{
    public class TestGUI : EditorWindow
    {
        // ReorderableList ???
        bool _Button;
        private Vector2 scroll;
        private String[] ScriptableEventsList;
        bool _ListMade = false;

        [MenuItem("Window/Debug Window")]
        static void Init()
        {
            TestGUI window = (TestGUI)GetWindow(typeof(TestGUI));
            window.Show();
            Scene currentScene = SceneManager.GetActiveScene(); // currently unused
        }
        void OnGUI()
        {
            if (!_ListMade)
            {
                 ScriptableEventsList = AssetDatabase.FindAssets("t:ScriptableEventBase");
                 DisplayEvents();
                _ListMade = true;
            }
            scroll = GUILayout.BeginScrollView(scroll);
            _Button = EditorGUILayout.Toggle("Log events", _Button);
            if (_Button)
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "DisplayEvents");
            }
            else
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
            }
            GUILayout.EndScrollView();
        }

        private void DisplayEvents()
        {
            ScriptableEventBase test;
            string EventPath;
            foreach (string st in ScriptableEventsList) // This was a huge waste of time
            {
                EventPath = AssetDatabase.GUIDToAssetPath(st);
                test = (ScriptableEventBase)AssetDatabase.LoadAssetAtPath(EventPath, typeof(ScriptableEventBase));
                Debug.Log("Path " + EventPath);
                Debug.Log("OBJ " + test.name);
            }
        }

      /*  private static void ForEachRootGameObjectInScene(Scene scene)
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
              // OnValidFields(c);
           }
           GUILayout.Space(10);
           GUILayout.EndVertical();
            for (int i = 0; i < transform.childCount; i++) 
            {
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
                AddListener(field.GetValue(field));
                EditorGUILayout.LabelField("Event", field.Name);
                _Logs = GUILayout.Button("Toggle Event Data");
                if (_Logs)
                {
                    // Toggle something relatated to the debug logs, also supposed to use subscribedEvents
                }
            }
        }*/
        private void WhenEvent()
        {
            Debug.Log("thing happen");
        }
    }
}