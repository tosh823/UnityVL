using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VLibrary {

    public class SearchController : MonoBehaviour {

        public GameObject inputField;
        public ItemListView scrollView;
        public StatusController statusView;
        public BookView bookView;

        private bool folded = true;
        private bool searching = false;
        private RectTransform rect;
        private Vector2 defaultSize;
        private VClient client;
        private Book activeBook;
        
        void Start() {
            rect = GetComponent<RectTransform>();
            defaultSize = rect.sizeDelta;
            client = new VClient();
            inputField.SetActive(false);
            scrollView.gameObject.SetActive(false);
        }
        
        void Update() {
            if (!folded && Input.GetKeyUp(KeyCode.Escape)) {
                Fold();
            }
        }

        public void OnQueryEntered(string query) {
            if (searching) return;
            // Set other widgets invisible
            scrollView.gameObject.SetActive(false);
            bookView.gameObject.SetActive(false);
            // Launch search
            client.Search(query);
            client.OnSearchFinished += OpenResults;
            searching = true;
            // Show status bar
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 3f * defaultSize.y);
            statusView.gameObject.SetActive(true);
            statusView.Spin();
        }

        public void OnBookClicked(string bookId) {
            if (searching) return;
            // Launch fetch
            client.Fetch(bookId);
            client.OnFetchFinished += OpenBook;
            searching = true;
            // Show status bar
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 3f * defaultSize.y);
            scrollView.gameObject.SetActive(false);
            statusView.gameObject.SetActive(true);
            statusView.Spin();
        }

        public void OnFindClicked(Book book, int locNumber) {
            activeBook = book;
            client.FindPathToBook(book.locations[locNumber]);
            client.OnBookLocationFound += OnShelfsFound;
        }

        public void OnIconClick() {
            if (folded) Unfold();
            else Fold();
        }

        public void OnCloseClick() {
            // Close BookView and show Results
            bookView.gameObject.SetActive(false);
            scrollView.gameObject.SetActive(true);
        }

        private void Fold() {
            folded = true;
            inputField.SetActive(false);
            scrollView.gameObject.SetActive(false);
            statusView.gameObject.SetActive(false);
            bookView.gameObject.SetActive(false);
            GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            rect.sizeDelta = defaultSize;
        }

        private void Unfold() {
            folded = false;
            GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.None;
            rect.sizeDelta = new Vector2(Mathf.Abs(6f * rect.sizeDelta.x), rect.sizeDelta.y);
            inputField.SetActive(true);
        }

        private void OpenBook(Book book) {
            searching = false;
            client.OnFetchFinished -= OpenBook;
            if (folded) return;
            Dispatcher.Instance.Invoke(() => {
                // Stop the spinner
                statusView.Stop();
                statusView.gameObject.SetActive(false);
                // Show results
                bookView.gameObject.SetActive(true);
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, 6f * defaultSize.y);
                bookView.UpdateContent(book);
            });
        }

        private void OpenResults(List<Book> books) {
            searching = false;
            client.OnSearchFinished -= OpenResults;
            if (folded) return;
            Dispatcher.Instance.Invoke(() => {
                // Stop the spinner
                statusView.Stop();
                statusView.gameObject.SetActive(false);
                // Show results
                scrollView.gameObject.SetActive(true);
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, 6f * defaultSize.y);
                scrollView.Populate(books);
            });
        }

        private void OnShelfsFound(List<string> shelfs) {
            client.OnBookLocationFound -= OnShelfsFound;
            if (shelfs.Count > 0) {
                string from = "F1P1";
                string to = shelfs[0];
                client.FindRoute(from, to);
                client.OnRouteFound += OnRouteFound;
            }
            else {
                Debug.Log("No possible shelfs");
            }
        }

        private void OnRouteFound(List<string> route) {
            client.OnRouteFound -= OnRouteFound;
            Debug.Log(string.Join(" ", route.ToArray()));
            Dispatcher.Instance.Invoke(() => {
                float time = 60f;
                Vector3 destination = Library.Instance.navigator.VisualizeRouteAndRemove(route, time);
                // Show InGame UI
                destination.y += 3f;
                GameObject ingame = Instantiate(Resources.Load("UI/BookViewInGame") as GameObject, destination, Quaternion.identity);
                ingame.GetComponent<BookViewInGame>().ShowContent(activeBook, time);
            });
        }
    }
}
