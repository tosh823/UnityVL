using System;
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

        private string host = "http://localhost:3000/search?title=";
        private WebClient request;

        public VClient() {
            request = new WebClient();
        }

        public void SearchAsync(string query) {
            Uri uri = new Uri(host + query);
            request.DownloadStringCompleted += onSearchRequestFinished;
            request.DownloadStringAsync(uri);
        }

        public List<Book> Search(string query) {
            Uri uri = new Uri(host + query);
            string json = request.DownloadString(uri);
            return parseJSON(json);
        }

        private void onSearchRequestFinished(object sender, DownloadStringCompletedEventArgs e) {
            request.DownloadStringCompleted -= onSearchRequestFinished;
            List<Book> parsed = parseJSON(e.Result);
            if (OnSearchFinished != null) OnSearchFinished(parsed);
        }

        private List<Book> parseJSON(string json) {
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
