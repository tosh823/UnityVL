using UnityEngine;
using System.Collections;

namespace WayPointMap {
    public class NavDrawer : MonoBehaviour {

        private WayPoint point;
   
        void Start() {
            point = GetComponentInParent<WayPoint>();
        }

        void Update() {
            if (point != null) {
                foreach (WayPoint linkedNode in point.neighbors) {
                    if (linkedNode != null) {
                        Debug.DrawLine(transform.position, linkedNode.transform.position, Color.cyan);
                    }
                }
            }
        }
    }
}
