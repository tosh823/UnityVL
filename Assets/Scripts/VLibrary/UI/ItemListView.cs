using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class ItemListView : MonoBehaviour {

        public GameObject itemPrefab;

        private List<ItemView> items;

        void Start() {
            items = new List<ItemView>();
        }

        void Update() {

        }

        public void Populate(List<Book> books) {
            Clear();
            items = new List<ItemView>();
            RectTransform content = GetComponent<ScrollRect>().content;
            float itemHeight = itemPrefab.GetComponent<LayoutElement>().preferredHeight;
            float width = content.sizeDelta.x;
            content.sizeDelta = new Vector2(width, itemHeight * books.Count);
            for (int i = 0; i < books.Count; i++) {
                GameObject newItem = Instantiate(itemPrefab) as GameObject;
                newItem.name = "BookItem " + i;
                newItem.transform.SetParent(content, false);

                /*RectTransform rectTransform = newItem.GetComponent<RectTransform>();
                float vertShift = rectTransform.rect.height * i;
                rectTransform.offsetMin = new Vector2(0, vertShift);*/

                ItemView view = newItem.GetComponent<ItemView>();
                view.UpdateContent(books[i]);
                items.Add(view);
            }
        }

        public void Clear() {
            if (items != null) {
                foreach (ItemView item in items) {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}
