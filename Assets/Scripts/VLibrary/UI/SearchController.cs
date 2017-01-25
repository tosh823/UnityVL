using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class SearchController : MonoBehaviour {

        private bool folded = true;
        private RectTransform rect;
        private Vector2 defaultSize;
        
        void Start() {
            rect = GetComponent<RectTransform>();
            defaultSize = rect.sizeDelta;
            Debug.Log("Default delta = " + defaultSize);
            Debug.Log("Default delta multiplied = " + defaultSize*2);
        }
        
        void Update() {

        }

        public void onIconClick() {
            if (folded) unfold();
            else fold();
        }

        private void fold() {
            folded = true;
            rect.sizeDelta = defaultSize;
        }

        private void unfold() {
            folded = false;
            rect.sizeDelta = new Vector2(100, 100);
        }
    }
}
