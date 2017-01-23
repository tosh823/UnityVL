using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class Library : MonoBehaviour {

        public Camera orbitCamera;
        public AvatarController avatar;

        void Start() {

        }

        void Update() {

        }

        public void changeCamera() {
            if (orbitCamera.enabled) {
                orbitCamera.enabled = false;
                avatar.enableControl(true);
            }
            else {
                avatar.enableControl(false);
                orbitCamera.enabled = true;
            }
        }
    }
}
