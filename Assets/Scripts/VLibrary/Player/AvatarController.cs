using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class AvatarController : MonoBehaviour {

        private FPSControl control;

        void Start() {
            control = GetComponent<FPSControl>();
        }

        void Update() {
            
        }

        public void enableControl(bool enabled) {
            Cursor.visible = !enabled;
            control.enabled = enabled;
        }
    }
}
