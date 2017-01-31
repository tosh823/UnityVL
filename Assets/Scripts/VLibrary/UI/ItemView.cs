using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class ItemView : MonoBehaviour {

        public Text titleText;
        public Text authorText;

        private Book content;

        void Start() {

        }

        void Update() {

        }

        public void UpdateContent(Book book) {
            titleText.text = book.title;
            authorText.text = string.Join(" ", book.authors.ToArray());
        }
    }
}
