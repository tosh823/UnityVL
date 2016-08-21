﻿using UnityEngine;
using System.Collections.Generic;

namespace Export3JS.Model {

    public class Geometry3JS {

        public string uuid;
        public string name;
        public string type;
        public Geometry3JSMeta metadata;
        public Geometry3JSData data;

        public Geometry3JS() {
            uuid = System.Guid.NewGuid().ToString().ToUpper();
            type = "Geometry";
            metadata = new Geometry3JSMeta();
            data = new Geometry3JSData();
        }
    }

    public class Geometry3JSMeta {
        public string version;
        public string type;
        public string generator;
        public int vertices;
        public int normals;
        public int uvs;
        public int faces;

        public Geometry3JSMeta() {
            version = "4.0";
            type = "Geometry";
            generator = "Unity Export3JS";
        }
    }

    public class Geometry3JSData {
        public float[] vertices;
        public float[] normals;
        public float[,] uvs;
        public int[] faces;
        public float[] colors;

        public Geometry3JSData() {
            
        }
    }
}
