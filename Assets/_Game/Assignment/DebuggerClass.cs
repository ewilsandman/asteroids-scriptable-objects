using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace.ScriptableEvents;

namespace assignment
{
    public class DebuggerClass : MonoBehaviour
    {
        void OnEnable()
        {
           // Debug.Log("Test");
            var all = Resources.FindObjectsOfTypeAll<ScriptableEvent>();
            foreach (var scriptableObj in all)
            {
                Debug.Log(scriptableObj);
            }
        }
    }
}