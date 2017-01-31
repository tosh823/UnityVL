using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {

    public interface IDispatcher {
        void Invoke(Action fn);
    }

    public class Dispatcher : IDispatcher {

        public List<Action> pending = new List<Action>();
        private static Dispatcher instance;

        public static Dispatcher Instance {
            get {
                if (instance == null) {
                    // Instance singleton on first use.
                    instance = new Dispatcher();
                }
                return instance;
            }
        }

        public void Invoke(Action fn) {
            lock (pending) {
                pending.Add(fn);
            }
        }

        public void InvokePending() {
            if (pending.Count > 0) {
                lock (pending) {
                    foreach (var action in pending) {
                        action();
                    }
                    pending.Clear();
                }
            }
        }

    }
}
