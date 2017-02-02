using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class BookView : MonoBehaviour {

        public Image coverImage;
        public Text titleText;
        public Text authorText;
        public Text typeText;
        public Text publisherText;
        public Text languageText;
        public Text descriptionText;
        public Text isbnText;

        private Book content;

        void Start() {

        }

        void Update() {

        }

        public void UpdateContent(Book book) {
            content = book;
            titleText.text = (book.title != null ? book.title : "Unknown");
            authorText.text = (book.author != null ? book.author : "Unknown");
            typeText.text = (book.type != null ? book.type : "Unknown");
            languageText.text = (book.language != null ? book.language : "Unknown");
            descriptionText.text = (book.description != null ? book.description : "Unknown");
            publisherText.text = (book.publisher != null ? book.publisher : "Unknown");
        }

        public void OnButtonClick() {

        }
    }
}
