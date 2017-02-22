using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VLibrary {
    public class BookViewInGame : MonoBehaviour {

        public Image coverImage;
        public Text titleText;
        public Text authorText;

        private Book content;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            transform.LookAt(Library.Instance.orbitCamera.transform.position);
        }

        public void UpdateContent(Book book) {
            content = book;
            // Set text values
            titleText.text = (book.title != null ? book.title : "Unknown");
            authorText.text = (book.author != null ? book.author : "Unknown");
            // Load cover image
            if (content.cover != null) StartCoroutine(LoadCover());
        }

        private IEnumerator LoadCover() {
            WWW www = new WWW(content.cover);
            yield return www;
            float aspect = www.texture.height / (float)www.texture.width;
            coverImage.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            coverImage.preserveAspect = true;
            coverImage.rectTransform.sizeDelta = new Vector2(coverImage.rectTransform.sizeDelta.x, coverImage.rectTransform.sizeDelta.x * aspect);
        }
    }
}
