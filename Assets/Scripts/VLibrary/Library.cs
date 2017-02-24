using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class Library : MonoBehaviour {

        public static Library Instance = null;
        public Camera activeCamera; 
        public Camera orbitCamera;
        public AvatarController avatar;
        public Pathfinder navigator;
        public UIController UI;

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
            UI.ShowOrbitalUI();
            avatar.enableControl(false);
            orbitCamera.enabled = true;
            activeCamera = orbitCamera;
        }

        private void enableAvatarControl() {
            UI.HideOrbitalUI();
            avatar.enableControl(true);
            orbitCamera.enabled = false;
            activeCamera = avatar.head;
        }
    }
}
