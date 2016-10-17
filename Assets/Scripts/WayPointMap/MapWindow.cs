using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

namespace WayPointMap {

    public class MapWindow : EditorWindow {

        private string directory;

        [MenuItem("ThreeJS/Build navs %#n")]
        static void Init() {
            MapWindow window = (MapWindow)GetWindow(typeof(MapWindow));
            window.titleContent = new GUIContent("Navigation creator");
            window.Show();
        }

        void OnEnable() {
            directory = string.Empty;
        }

        void OnGUI() {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            directory = GUILayout.TextField(directory);
            if (GUILayout.Button("...", GUILayout.ExpandWidth(false))) {
                directory = EditorUtility.OpenFolderPanel("Choose destination folder", "", "");
                directory += "/";
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Export", GUILayout.ExpandWidth(false))) {
                Map navigator = new Map(directory);
                navigator.Parse();
            }
            GUILayout.EndVertical();
        }

    }
}

#endif
