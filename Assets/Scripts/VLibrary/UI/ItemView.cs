using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class ItemView : MonoBehaviour {

        public Text titleText;
        public Text authorText;
        public Button button;
        public SearchController host;

        private Book content;

        void Start() {

        }

        void Update() {

        }

        public void OnItemClick() {
            Debug.Log("Book with id " + content.bookId + " clicked");
            if (host != null) host.OnBookClicked(content.bookId);
        }

        public void UpdateContent(Book book) {
            content = book;
            titleText.text = (book.title != null ? book.title : "Unknown");
            authorText.text = (book.author != null ? book.author : "Unknown");
        }
    }
}
