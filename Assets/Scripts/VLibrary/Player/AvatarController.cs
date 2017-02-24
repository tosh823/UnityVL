using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class AvatarController : MonoBehaviour {

        public float interactionDistance = 2f;
        public Camera head;

        private FPSControl control;

        void Start() {
            control = GetComponent<FPSControl>();
        }

        void Update() {
            RaycastHit hit;
            Ray ray = head.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out hit, interactionDistance)) {
                Transform objectHit = hit.transform;
                if (objectHit.GetComponent<LampController>() != null) {
                    // Show info message
                    Library.Instance.UI.ShowMessage("Press E to switch a lamp");
                    if (Input.GetKeyUp(KeyCode.E)) {
                        objectHit.GetComponent<LampController>().Switch();
                    }
                }
                else {
                    Library.Instance.UI.HideMessage();
                }
            }
        }

        public void enableControl(bool enabled) {
            Cursor.visible = !enabled;
            control.enabled = enabled;
        }
    }
}
