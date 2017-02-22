using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class LampController : MonoBehaviour {

        private Light lamp;

        void Start() {
            lamp = GetComponentInChildren<Light>();
        }

        void Update() {

        }

        public void Switch() {
            if (lamp.enabled) {
                lamp.enabled = false;
            }
            else {
                lamp.enabled = true;
            }
        }
    }
}
