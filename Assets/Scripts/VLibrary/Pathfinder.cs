using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WayPointMap;

namespace VLibrary {
    public class Pathfinder : MonoBehaviour {

        public Material lineMaterial;
        private WayPoint[] map;

        void Start() {
            map = GetComponentsInChildren<WayPoint>();
            Debug.Log("Loaded map of " + map.Length + " waypoints");
        }

        void Update() {

        }

        public void VisualizeRoute(List<string> route) {
            LineRenderer line = gameObject.AddComponent<LineRenderer>();
            line.sharedMaterial = lineMaterial;
            line.startWidth = 0.5f;
            line.endWidth = 0.5f;
            line.numPositions = route.Count;
            for (int i = 0; i < route.Count; i++) {
                string shelf = route[i];
                WayPoint node = Array.Find(map, x => x.gameObject.name == shelf);
                if (node != null) {
                    line.SetPosition(i, node.transform.position);
                }
            }
            RemoveLine(30);
        }

        public WayPoint GetClosestTo(Vector3 position) {
            WayPoint closest = null;
            float minDistance = float.MaxValue;
            foreach (WayPoint node in map) {
                float distance = Vector3.Distance(node.transform.position, position);
                if (distance < minDistance) {
                    minDistance = distance;
                    closest = node;
                }
            }
            return closest;
        }

        public void RemoveLine(float delay) {
            StartCoroutine(RemoveLineAfter(delay));
        }

        private IEnumerator RemoveLineAfter(float seconds) {
            yield return new WaitForSeconds(seconds);
            LineRenderer line = gameObject.GetComponent<LineRenderer>();
            if (line != null) Destroy(line);
        }
    }
}
