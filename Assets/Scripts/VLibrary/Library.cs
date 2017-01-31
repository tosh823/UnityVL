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
            if (Input.GetKeyUp(KeyCode.Escape) && !orbitCamera.enabled) {
                avatar.enableControl(false);
                orbitCamera.enabled = true;
            }
            Dispatcher.Instance.InvokePending();
        }

        public void changeCamera() {
            if (orbitCamera.enabled) {
                avatar.enableControl(true);
                orbitCamera.enabled = false;
            }
            else {
                avatar.enableControl(false);
                orbitCamera.enabled = true;
            }
        }
    }
}
