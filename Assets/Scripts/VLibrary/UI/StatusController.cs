using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class StatusController : MonoBehaviour {

        public Text statusText;
        public Image spinnerImage;
        public float angularSpeed = 1;

        private bool isSpinning = false;

        void Start() {

        }

        void Update() {
            if (isSpinning) {
                Vector3 rotation = spinnerImage.rectTransform.rotation.eulerAngles;
                rotation.z -= angularSpeed * Mathf.Rad2Deg * Time.deltaTime;
                spinnerImage.rectTransform.eulerAngles = Vector3.Lerp(spinnerImage.rectTransform.rotation.eulerAngles, rotation, 0.5f);
            }
        }

        public void Spin() {
            isSpinning = true;
        }

        public void Stop() {
            isSpinning = false;
        }
    }
}
