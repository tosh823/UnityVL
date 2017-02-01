﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VLibrary {

    public class SearchController : MonoBehaviour {

        public GameObject inputField;
        public ItemListView scrollView;

        private bool folded = true;
        private RectTransform rect;
        private Vector2 defaultSize;
        private VClient client;
        
        void Start() {
            rect = GetComponent<RectTransform>();
            defaultSize = rect.sizeDelta;
            client = new VClient();
            inputField.SetActive(false);
            scrollView.gameObject.SetActive(false);
        }
        
        void Update() {

        }

        public void OnQueryEntered(string query) {
            client.SearchAsync(query);
            client.OnSearchFinished += OpenResults;
        }

        public void OnIconClick() {
            if (folded) Unfold();
            else Fold();
        }

        private void Fold() {
            GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            inputField.SetActive(false);
            scrollView.gameObject.SetActive(false);
            folded = true;
            rect.sizeDelta = defaultSize;
        }

        private void Unfold() {
            GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.None;
            inputField.SetActive(true);
            folded = false;
            rect.sizeDelta = new Vector2(Mathf.Abs(6f * rect.sizeDelta.x), rect.sizeDelta.y);
        }

        private void OpenResults(List<Book> books) {
            client.OnSearchFinished -= OpenResults;
            Dispatcher.Instance.Invoke(() => {
                scrollView.gameObject.SetActive(true);
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, 6f * rect.sizeDelta.y);
                scrollView.Populate(books);
            });
        }
    }
}
