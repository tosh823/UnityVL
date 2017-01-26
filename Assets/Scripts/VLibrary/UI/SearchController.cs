using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class SearchController : MonoBehaviour {

        public GameObject searchButton;

        private bool folded = true;
        private RectTransform rect;
        private Vector2 defaultSize;
        private Anchor defaultAnchor;
        
        void Start() {
            rect = GetComponent<RectTransform>();
            defaultSize = rect.sizeDelta;
            defaultAnchor = new Anchor(rect.anchorMin, rect.anchorMax);
        }
        
        void Update() {

        }

        public void onIconClick() {
            if (folded) unfold();
            else fold();
        }

        private void fold() {
            searchButton.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.FitInParent;
            searchButton.GetComponent<RectTransform>().anchorMin = AnchorPresets.StretchAll.min;
            searchButton.GetComponent<RectTransform>().anchorMax = AnchorPresets.StretchAll.max;
            folded = true;
            rect.sizeDelta = defaultSize;
        }

        private void unfold() {
            searchButton.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.None;
            
            folded = false;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x + 100f, rect.sizeDelta.y);
        }
    }
}
