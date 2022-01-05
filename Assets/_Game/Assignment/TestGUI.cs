using UnityEditor;
using UnityEngine;

namespace assignment
{
    public class TestGUI : EditorWindow
    {
        bool _IsLogCheckbox;
        private Vector2 _scrollVector;
        bool _state;

        [MenuItem("Window/Debug Window")]
        static void Init()
        {
            TestGUI window = (TestGUI)GetWindow(typeof(TestGUI));
            window.Show();
        }
        void OnGUI()
        {
            _scrollVector = GUILayout.BeginScrollView(_scrollVector);
            _IsLogCheckbox = EditorGUILayout.Toggle("Log events", _IsLogCheckbox);
            EditorGUILayout.LabelField("(Can take several seconds to take effect)");
            if (_IsLogCheckbox)
            {
                if (!_state)// an attempt to improve framerate
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "DisplayEvents");
                }
                _state = true;
            }
            else
            {
                if (_state)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, "");
                }
                _state = false;
            }
            GUILayout.EndScrollView();
        }
    }
}