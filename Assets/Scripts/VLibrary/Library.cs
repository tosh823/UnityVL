using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class Library : MonoBehaviour {

        public static Library Instance = null;
        public Camera orbitCamera;
        public AvatarController avatar;
        public Pathfinder navigator;
        public GameObject UI;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else if (Instance != this) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        void Start() {
            
        }

        void Update() {
            if (Input.GetKeyUp(KeyCode.Escape) && !orbitCamera.enabled) {
                enableOrbitControl();
            }
            Dispatcher.Instance.InvokePending();
        }

        public void changeCamera() {
            if (orbitCamera.enabled) {
                enableAvatarControl();
            }
            else {
                enableOrbitControl();
            }
        }

        private void enableOrbitControl() {
            UI.SetActive(true);
            avatar.enableControl(false);
            orbitCamera.enabled = true;
        }

        private void enableAvatarControl() {
            UI.SetActive(false);
            avatar.enableControl(true);
            orbitCamera.enabled = false;
        }
    }
}
