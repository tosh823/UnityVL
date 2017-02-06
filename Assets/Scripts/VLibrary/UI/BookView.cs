using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class BookView : MonoBehaviour {

        public SearchController parent;

        public Image coverImage;
        public Text titleText;
        public Text authorText;
        public Text typeText;
        public Text publisherText;
        public Text languageText;
        public Text descriptionText;
        public Text isbnText;
        public Text locationText;

        private Book content;

        void Start() {

        }

        void Update() {

        }

        public void UpdateContent(Book book) {
            content = book;
            // Set text values
            titleText.text = (book.title != null ? book.title : "Unknown");
            authorText.text = (book.author != null ? book.author : "Unknown");
            typeText.text = (book.type != null ? "Type: " + book.type : "Unknown");
            languageText.text = (book.language != null ? "Language: " + book.language : "Unknown");
            descriptionText.text = (book.description != null ? "Desc: " + book.description : "Unknown");
            publisherText.text = (book.publisher != null ? "Publisher: " + book.publisher : "Unknown");
            isbnText.text = (book.isbn != null ? "ISBN: " + book.isbn: "Unknown");
            locationText.text = "Locations:";
            foreach (Book.Location loc in book.locations) {
                locationText.text += "\n\t" + loc.callNumber + (loc.collection != null ? " " + loc.collection : "");
            }
            // Load cover image
            if (content.cover != null) StartCoroutine(LoadCover());
        }

        private IEnumerator LoadCover() {
            WWW www = new WWW(content.cover);
            yield return www;
            float aspect = www.texture.height / (float) www.texture.width;
            coverImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            coverImage.preserveAspect = true;
            coverImage.rectTransform.sizeDelta = new Vector2(coverImage.rectTransform.sizeDelta.x, coverImage.rectTransform.sizeDelta.x * aspect);
        }

        public void OnButtonClick() {
            parent.OnFindClicked(content.locations[0]);
        }
    }
}
