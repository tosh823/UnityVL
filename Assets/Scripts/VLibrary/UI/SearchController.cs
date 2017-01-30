using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class SearchController : MonoBehaviour {

        public GameObject inputField;
        public GameObject scrollView;

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
            GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            inputField.SetActive(false);
            scrollView.SetActive(false);
            folded = true;
            rect.sizeDelta = defaultSize;
        }

        private void unfold() {
            GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.None;
            inputField.SetActive(true);
            folded = false;
            rect.sizeDelta = new Vector2(Mathf.Abs(6f * rect.sizeDelta.x), rect.sizeDelta.y);
        }
    }
}
