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

        public void Search(string query) {
            Uri uri = new Uri(host + query);
            request.DownloadStringCompleted += onSearchRequestFinished;
            request.DownloadStringAsync(uri);
        }

        private void onSearchRequestFinished(object sender, DownloadStringCompletedEventArgs e) {
            request.DownloadStringCompleted -= onSearchRequestFinished;
            Debug.Log("Received response");
            JObject responseJson = JObject.Parse(e.Result);
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
            if (OnSearchFinished != null) OnSearchFinished(parsed);
        }
    }
}
