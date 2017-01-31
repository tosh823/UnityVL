using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class ItemListView : MonoBehaviour {

        public GameObject itemPrefab;

        private Transform content;
        private List<ItemView> items;

        void Start() {
            content = transform.Find("Viewport/Content");
            items = new List<ItemView>();
        }

        void Update() {

        }

        public void Populate(List<Book> books) {
            Clear();
            for (int i = 0; i < books.Count; i++) {
                GameObject newItem = Instantiate(itemPrefab) as GameObject;
                newItem.name = "BookItem " + i;
                newItem.transform.SetParent(content, false);
                ItemView view = newItem.GetComponent<ItemView>();
                view.UpdateContent(books[i]);
                items.Add(view);
            }
        }

        public void Clear() {
            foreach (ItemView item in items) {
                Destroy(item.gameObject);
            }
            items = new List<ItemView>();
        }
    }
}
