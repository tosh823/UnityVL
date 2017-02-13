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

        public delegate void RouteRequestHandler(List<string> route);
        public event RouteRequestHandler OnRouteFound;

        public delegate void BookLocationRequestHandler(List<string> shelfs);
        public event BookLocationRequestHandler OnBookLocationFound;

        public delegate void SearchRequestHandler(List<Book> results);
        public event SearchRequestHandler OnSearchFinished;

        public delegate void FetchRequesthandler(Book book);
        public event FetchRequesthandler OnFetchFinished;

        private string localHost = "http://localhost:3000";
        private string host = "http://95.85.62.204:3000";
        private string search = "/search?title=";
        private string fetch = "/search/book?id=";
        private string route = "/map?from=";
        private string book = "/map/book?call=";
        private WebClient request;

        public VClient() {
            request = new WebClient();
            request.Encoding = Encoding.UTF8;
        }

        public void Search(string query) {
            string title = query.Replace(' ', '+');
            Uri uri = new Uri(host + search + title);
            request.DownloadStringCompleted += OnSearchRequestFinished;
            request.DownloadStringAsync(uri);
        }

        public void Fetch(string id) {
            Uri uri = new Uri(host + fetch + id);
            request.DownloadStringCompleted += OnFetchRequestFinished;
            request.DownloadStringAsync(uri);
        }

        public void FindPathToBook(Book.Location location) {
            string call = location.callNumber.Replace(' ', '+');
            string collection = (location.collection != null ? location.collection.Replace(' ', '+') : null);
            string activePart = call;
            if (collection != null) activePart += "&collection=" + collection;
            Uri uri = new Uri(host + book + activePart);
            request.DownloadStringCompleted += OnBookLocationRequestFinished;
            request.DownloadStringAsync(uri);
        }

        public void FindRoute(string from, string to) {
            Uri uri = new Uri(host + route + from + "&to=" + to);
            request.DownloadStringCompleted += OnRouteRequestFinished;
            request.DownloadStringAsync(uri);
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

        private void OnRouteRequestFinished(object sender, DownloadStringCompletedEventArgs e) {
            request.DownloadStringCompleted -= OnRouteRequestFinished;
            List<string> route = ParseStringsJSON(e.Result);
            if (OnRouteFound != null) OnRouteFound(route);
        }

        private void OnBookLocationRequestFinished(object sender, DownloadStringCompletedEventArgs e) {
            request.DownloadStringCompleted -= OnBookLocationRequestFinished;
            List<string> possibleShelfs = ParseStringsJSON(e.Result);
            if (OnBookLocationFound != null) OnBookLocationFound(possibleShelfs);
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

        private List<string> ParseStringsJSON(string json) {
            JObject responseJson = JObject.Parse(json);
            List<string> strings = new List<string>();
            try {
                strings = JsonConvert.DeserializeObject<List<string>>(responseJson["data"].ToString());
            }
            catch (JsonSerializationException exception) {
                Debug.Log(exception.Message);
            }
            return strings;
        }
    }
}
