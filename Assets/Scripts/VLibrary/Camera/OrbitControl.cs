using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class OrbitControl : MonoBehaviour {

        public float smooth = 0.5f;
        public float speedX = 1f;
        public float speedY = 1f;
        public float minY = 1f;
        public float maxY = 90f;
        public float minZoom = 4f;
        public float maxZoom = 0.5f;

        private Vector3 target;
        private float distance;
        private float distanceMin;
        private float distanceMax;
        private float rotationX;
        private float rotationY;

        void Start() {
            rotationX = transform.eulerAngles.x;
            rotationY = transform.eulerAngles.y;
            distance = Vector3.Distance(target, transform.position);
            distanceMin = distance / minZoom;
            distanceMax = distance / maxZoom;
        }

        void Update() {

            float zoom = Input.GetAxis("Mouse ScrollWheel");
            if (zoom != 0f) {
                distance = Mathf.Clamp(distance - (distance * zoom), distanceMin, distanceMax);
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = transform.rotation * negDistance + target;
                transform.position = position;
            }

            if (Input.GetMouseButton(0)) {
                rotationX += Input.GetAxis("Mouse X") * speedX;
                rotationY -= Input.GetAxis("Mouse Y") * speedY;
                rotationY = ClampAngle(rotationY, minY, maxY);
                Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
                
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target;
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smooth);
                transform.position = Vector3.Lerp(transform.position, position, smooth);
            }
        }

        public static float ClampAngle(float angle, float min, float max) {
            if (angle < -360F) angle += 360F;
            if (angle > 360F) angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
