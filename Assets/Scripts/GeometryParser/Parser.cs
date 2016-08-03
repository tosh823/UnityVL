using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GeometryParser {
    public class Parser {

        private string outputDir;
        private ParserFormat content;

        public Parser(string destination) {
            outputDir = destination;
        }

        public void Launch() {
            content = new ParserFormat();
            content.metadata.scene = SceneManager.GetActiveScene().name;
            content.metadata.time = DateTime.Now.ToString();
            // Start parsing process
            parseRootObjects();
            // Serialize to JSON and write to file
            string json = JsonConvert.SerializeObject(content, Formatting.Indented);
            string filename = SceneManager.GetActiveScene().name + ".json";
            System.IO.File.WriteAllText(outputDir + filename, json);
            Debug.Log("GeometryParser completed, " + DateTime.Now.ToLongTimeString());
        }

        private void parseRootObjects() {
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject gameObject in rootObjects) {
                parseGameObject(gameObject);
            }
        }

        private void parseGameObject(GameObject gameObject) {
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            if (meshFilter != null) {
                Mesh mesh = meshFilter.sharedMesh;
                if (content.objects.ContainsKey(mesh.name)) {
                    // If we already have mesh in content, add new state to it
                    addState(mesh.name, gameObject);
                }
                else {
                    // Otherwise create mesh in content and add new state to it
                    createStates(mesh.name, gameObject);
                }
            }
            // Parse children of the gameobject
            if (gameObject.transform.childCount > 0) {
                foreach (Transform child in gameObject.transform) {
                    parseGameObject(child.gameObject);
                }
            }
        }

        private void addState(string key, GameObject gameObject) {
            State state = new State();
            Vector3 position = gameObject.transform.position;
            Vector3 rotation = gameObject.transform.rotation.eulerAngles;
            Vector3 scale = gameObject.transform.localScale;
            state.position = new float[3] { position.x, position.y, position.z };
            state.rotation = new float[3] { rotation.x, rotation.y, rotation.z };
            state.scale = new float[3] { scale.x, scale.y, scale.z };

            content.objects[key].Add(state);
        }

        private void createStates(string key, GameObject gameObject) {
            State state = new State();
            Vector3 position = gameObject.transform.position;
            Vector3 rotation = gameObject.transform.rotation.eulerAngles;
            Vector3 scale = gameObject.transform.localScale;
            state.position = new float[3] { position.x, position.y, position.z };
            state.rotation = new float[3] { rotation.x, rotation.y, rotation.z };
            state.scale = new float[3] { scale.x, scale.y, scale.z };

            content.objects.Add(key, new List<State>());
            content.objects[key].Add(state);
        }
    }
}
