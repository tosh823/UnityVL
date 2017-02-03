using System;
using System.Text;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VLibrary {
    public class VClient {

        public delegate void SearchRequestHandler(List<Book> results);
        public event SearchRequestHandler OnSearchFinished;

        public delegate void FetchRequesthandler(Book book);
        public event FetchRequesthandler OnFetchFinished;

        private string host = "http://localhost:3000";
        private string search = "/search?title=";
        private string fetch = "/search/book?id=";
        private WebClient request;

        public VClient() {
            request = new WebClient();
            request.Encoding = Encoding.UTF8;
        }

        public void SearchAsync(string query) {
            Uri uri = new Uri(host + search + query);
            request.DownloadStringCompleted += OnSearchRequestFinished;
            request.DownloadStringAsync(uri);
        }

        public void FetchAsync(string id) {
            Uri uri = new Uri(host + fetch + id);
            request.DownloadStringCompleted += OnFetchRequestFinished;
            request.DownloadStringAsync(uri);
        }

        public List<Book> Search(string query) {
            Uri uri = new Uri(host + search + query);
            string json = request.DownloadString(uri);
            return ParseSearchResultsJSON(json);
        }

        private void OnFetchRequestFinished(object sender, DownloadStringCompletedEventArgs e) {
            request.DownloadStringCompleted -= OnFetchRequestFinished;
            Book parsed = ParseBookDetailsJSON(e.Result);
            if (OnFetchFinished != null) OnFetchFinished(parsed);
        }

        private void OnSearchRequestFinished(object sender, DownloadStringCompletedEventArgs e) {
            request.DownloadStringCompleted -= OnSearchRequestFinished;
            List<Book> parsed = ParseSearchResultsJSON(e.Result);
            if (OnSearchFinished != null) OnSearchFinished(parsed);
        }

        private Book ParseBookDetailsJSON(string json) {
            JObject responseJson = JObject.Parse(json);
            Book result = new Book();
            try {
                result = JsonConvert.DeserializeObject<Book>(responseJson["data"].ToString());
            }
            catch (JsonSerializationException exception) {
                Debug.Log(exception.Message);
            }
            return result;
        }

        private List<Book> ParseSearchResultsJSON(string json) {
            JObject responseJson = JObject.Parse(json);
            List<Book> parsed = new List<Book>();
            foreach (JToken token in responseJson["data"].Children()) {
                try {
                    Book book = JsonConvert.DeserializeObject<Book>(token.ToString());
                    parsed.Add(book);
                }
                catch (JsonSerializationException exception) {
                    Debug.Log(exception.Message);
                }
            }
            return parsed;
        }
    }
}
