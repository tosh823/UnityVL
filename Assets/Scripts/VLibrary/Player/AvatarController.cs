﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class AvatarController : MonoBehaviour {

        public float interactionDistance = 10f;

        private FPSControl control;
        private Camera head;

        void Start() {
            control = GetComponent<FPSControl>();
            head = GetComponentInChildren<Camera>();
        }

        void Update() {
            RaycastHit hit;
            Ray ray = head.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out hit, interactionDistance)) {
                Transform objectHit = hit.transform;
                if (objectHit.GetComponent<LampController>() != null && Input.GetKeyUp(KeyCode.E)) {
                    objectHit.GetComponent<LampController>().Switch();
                }
            }
        }

        public void enableControl(bool enabled) {
            Cursor.visible = !enabled;
            control.enabled = enabled;
        }
    }
}
