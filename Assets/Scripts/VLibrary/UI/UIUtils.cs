using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {

    public struct Anchor {
        public Vector2 min;
        public Vector2 max;
        public Anchor(Vector2 min, Vector2 max) {
            this.min = min;
            this.max = max;
        }
    }

    public static class AnchorPresets {

        public static Anchor TopLeft {
            get { return new Anchor(new Vector2(0, 1), new Vector2(0, 1)); }
        }

        public static Anchor TopCenter {
            get { return new Anchor(new Vector2(0.5f, 1), new Vector2(0.5f, 1)); }
        }

        public static Anchor TopRight {
            get { return new Anchor(new Vector2(1, 1), new Vector2(1, 1)); }
        }

        public static Anchor MidLeft {
            get { return new Anchor(new Vector2(0, 0.5f), new Vector2(0, 0.5f)); }
        }

        public static Anchor MidCenter {
            get { return new Anchor(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f)); }
        }

        public static Anchor MidRight {
            get { return new Anchor(new Vector2(1, 0.5f), new Vector2(1, 0.5f)); }
        }

        public static Anchor BotLeft {
            get { return new Anchor(new Vector2(0, 0), new Vector2(0, 0)); }
        }

        public static Anchor BotCenter {
            get { return new Anchor(new Vector2(0.5f, 0), new Vector2(0.5f, 0)); }
        }

        public static Anchor BotRight {
            get { return new Anchor(new Vector2(1, 0), new Vector2(1, 0)); }
        }

        public static Anchor StretchHorTop {
            get { return new Anchor(new Vector2(0, 1), new Vector2(1, 1)); }
        }

        public static Anchor StretchHorMid {
            get { return new Anchor(new Vector2(0, 0.5f), new Vector2(1, 0.5f)); }
        }

        public static Anchor StretchHorBot {
            get { return new Anchor(new Vector2(0, 0), new Vector2(1, 0)); }
        }

        public static Anchor StretchVertLeft {
            get { return new Anchor(new Vector2(0, 0), new Vector2(0, 1)); }
        }

        public static Anchor StretchVertCenter {
            get { return new Anchor(new Vector2(0.5f, 0), new Vector2(0.5f, 1)); }
        }

        public static Anchor StretchVertRight {
            get { return new Anchor(new Vector2(1, 0), new Vector2(1, 1)); }
        }

        public static Anchor StretchAll {
            get { return new Anchor(new Vector2(0, 0), new Vector2(1, 1)); }
        }
    }
}
