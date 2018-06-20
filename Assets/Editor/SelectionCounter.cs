using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor.Tools
{
    public class SelectionCounter : EditorWindow
    {
        [MenuItem("Window/Selection Counter")]
        static void Init()
        {
            SelectionCounter window = (SelectionCounter)GetWindow(typeof(SelectionCounter));
            window.Show();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        void OnGUI()
        {
            GUILayout.Label("Objects selected in Hierarchy: " + Selection.gameObjects.Length, EditorStyles.boldLabel);
            GUILayout.Label("Assets selected in Project: " + Selection.assetGUIDs.Length, EditorStyles.boldLabel);
        }
    }
}
