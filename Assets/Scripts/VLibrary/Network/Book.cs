using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace VLibrary {
    public class Book {

        public struct Location {
            public string callNumber;
            public string collection;
        }

        public string title;
        public string cover; // 
        public string author; //
        public string type;
        public string language;
        public string publisher;
        public string description; //
        public string isbn; //
        public List<Location> locations; //

        public Book() {

        }
    }
}
