using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class ItemListView : MonoBehaviour {

        public GameObject itemPrefab;

        private Transform content;

        void Start() {
            content = transform.Find("Viewport/Content");
        }

        void Update() {

        }

        private void populate(int count) {
            for (int i = 0; i < count; i++) {
                GameObject newItem = Instantiate(itemPrefab) as GameObject;
                newItem.name = "BookItem " + i;
                newItem.transform.SetParent(content, false);
            }
        }
    }
}
