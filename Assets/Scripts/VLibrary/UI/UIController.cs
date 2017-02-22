using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class UIController : MonoBehaviour {

        public List<GameObject> orbitalUI;
        public InfoController info;
        
        void Start() {

        }

        void Update() {

        }

        public void ShowOrbitalUI() {
            HideMessage();
            foreach (GameObject element in orbitalUI) {
                element.SetActive(true);
            }
        }

        public void HideOrbitalUI() {
            HideMessage();
            foreach (GameObject element in orbitalUI) {
                element.SetActive(false);
            }
        }

        public void ShowMessage(string text) {
            if (!info.isActiveAndEnabled) info.gameObject.SetActive(true);
            info.message.text = text;
        }

        public void HideMessage() {
            if (info.isActiveAndEnabled) {
                info.message.text = string.Empty;
                info.gameObject.SetActive(false);
            }
        }
    }
}
