using UnityEngine;
using System;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

namespace GeometryParser {
    public class GeometryParserWindow : EditorWindow {

        [MenuItem("Assets/Parse %#e")]
        static void Init() {
            GetWindow(typeof(GeometryParserWindow), false, "Parser", true);
        }

        void OnEnable() {
            Debug.Log("GeometryParser started, " + DateTime.Now.ToLongTimeString());
        }

        void OnGUI() {
            GUILayout.BeginVertical();
            GUILayout.Label("Geometry Parser", EditorStyles.boldLabel);
            if (GUILayout.Button("Select folder", GUILayout.ExpandWidth(false))) {
                //string dir = EditorUtility.OpenFolderPanel("Choose destination folder", "", "");
                Parser parser = new Parser("D:/Projects/Web/VL/assets/");
                parser.Launch();
            }
            GUILayout.EndVertical();
        }
    }
}

#endif