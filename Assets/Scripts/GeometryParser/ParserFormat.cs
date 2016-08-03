using UnityEngine;
using System.Collections.Generic;

namespace GeometryParser {

    public class ParserMetadata {
        public string name;
        public string scene;
        public string time;
        public ParserMetadata() {
            name = "Geometry Parser";
        }
    }

    public class State {
        public float[] position;
        public float[] rotation;
        public float[] scale;

        public State() {
            position = new float[3];
            rotation = new float[3];
            scale = new float[3];
        }
    }

    public class ParserFormat {

        public ParserMetadata metadata;
        public Dictionary<string, List<State>> objects;

        public ParserFormat() {
            metadata = new ParserMetadata();
            objects = new Dictionary<string, List<State>>();
        }
    }
}
