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
using UnityEditor.Compilation;

namespace assignment
{
    public class TestGUI : EditorWindow
    {
        bool _Button;
        private Vector2 scroll;
        int state;

        [MenuItem("Window/Debug Window")]
        static void Init()
        {
            TestGUI window = (TestGUI)GetWindow(typeof(TestGUI));
            window.Show();
        }
        void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            _Button = EditorGUILayout.Toggle("Log events", _Button);
            EditorGUILayout.TextField("(Can take several seconds to take effect)");
            if (_Button)
            {
                if (state == 0)// an attempt to improve framerate
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "DisplayEvents");
                    CompilationPipeline.RequestScriptCompilation();  // an attempt at decreasing the time untill changes take effect
                }
                state = 1;
            }
            else
            {
                if (state == 1)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
                    CompilationPipeline.RequestScriptCompilation();  // an attempt at decreasing the time untill changes take effect
                }
                state = 0;
            }
            GUILayout.EndScrollView();
        }
    }
}