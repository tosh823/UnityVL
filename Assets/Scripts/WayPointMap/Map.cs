using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WayPointMap {

    public class WayPointModel {
        public string name;
        public Dictionary<string, int> links;
        public WayPointModel() {

        }
    }

    public class Format3 {
        public List<WayPointModel> wayPoints;
        public Format3() {
            wayPoints = new List<WayPointModel>();
        }
    }

    public class Map {

        private Format3 content;
        private string dir;

        public Map(string directory) {
            dir = directory;
        }

        public void Parse() {
            Debug.Log("Navigator started, " + DateTime.Now.ToLongTimeString());
            parseScene();
            JsonSerializerSettings settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore
            };
            Formatting jsonFormatting = Formatting.Indented;
            string json = JsonConvert.SerializeObject(content, jsonFormatting, settings);
            string filename = SceneManager.GetActiveScene().name + "Navigation" + ".json";
            System.IO.File.WriteAllText(dir + filename, json);
            Debug.Log("Navigator completed, " + DateTime.Now.ToLongTimeString());
        }

        private void parseScene() {
            content = new Format3();
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject gameObject in rootObjects) {
                parseObject(gameObject);
            }
        }

        private void parseObject(GameObject gameObject) {
            if (gameObject.GetComponent<WayPoint>() != null) {
                WayPoint wayPoint = gameObject.GetComponent<WayPoint>();
                WayPointModel model = new WayPointModel();
                model.name = wayPoint.name;
                model.links = new Dictionary<string, int>();
                foreach (WayPoint neighbor in wayPoint.neighbors) {
                    model.links.Add(neighbor.name, (int)Vector3.Distance(wayPoint.transform.position, neighbor.transform.position));
                }
                content.wayPoints.Add(model);
            }
            
            if (gameObject.transform.childCount > 0) {
                foreach (Transform child in gameObject.transform) {
                    parseObject(child.gameObject);
                }
            }
        }
    }
}
