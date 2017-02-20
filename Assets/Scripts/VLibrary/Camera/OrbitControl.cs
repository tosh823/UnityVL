using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VLibrary {
    public class OrbitControl : MonoBehaviour {

        public float smooth = 0.5f;
        public float speedX = 1f;
        public float speedY = 1f;
        public float minX = 0f;
        public float maxX = 360f;
        public float minY = 10f;
        public float maxY = 80f;
        public float minZoom = 4f;
        public float maxZoom = 0.5f;

        private Vector3 target;
        private float distance;
        private float distanceMin;
        private float distanceMax;
        private float rotationX;
        private float rotationY;

        void Start() {
            target = Vector3.zero;
            rotationX = transform.eulerAngles.y;
            rotationY = transform.eulerAngles.x;
            CalculateDistances();
        }

        void Update() {
            // Don't record if user is working with UI
            if (EventSystem.current.IsPointerOverGameObject()) return;
            float zoom = Input.GetAxis("Mouse ScrollWheel");
            if (zoom != 0f) {
                distance = Mathf.Clamp(distance - (distance * zoom), distanceMin, distanceMax);
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = transform.rotation * negDistance + target;
                transform.position = position;
            }
            // Left mouse button controls rotation
            if (Input.GetMouseButton(0)) {
                rotationX += Input.GetAxis("Mouse X") * speedX;
                rotationY -= Input.GetAxis("Mouse Y") * speedY;
                rotationY = ClampAngle(rotationY, minY, maxY);
                rotationX = ClampAngle(rotationX, minX, maxX);
                Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
                
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = rotation * negDistance + target;
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smooth);
                transform.position = Vector3.Lerp(transform.position, position, smooth);
            }
            // Right mouse button controls position
            if (Input.GetMouseButton(1)) {
                target += transform.right * Input.GetAxis("Mouse X") * speedX;
                target += transform.forward * Input.GetAxis("Mouse Y") * speedY;
                target.y = 0f;
                CalculateDistances();
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                Vector3 position = transform.rotation * negDistance + target;
                transform.position = Vector3.Lerp(transform.position, position, smooth);
            }
        }

        private float ClampAngle(float angle, float min, float max) {
            if (angle <= -360f) angle += 360f;
            if (angle >= 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }

        private void CalculateDistances() {
            distance = Vector3.Distance(target, transform.position);
            distanceMin = distance / minZoom;
            distanceMax = distance / maxZoom;
        }
    }
}
