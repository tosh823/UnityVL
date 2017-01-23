using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLibrary {
    public class FPSControl : MonoBehaviour {

        public Camera head;
        public float speed = 5f;
        public float sensitivity = 5f;

        private Rigidbody body;
        private float rotationX;
        private float rotationY;
        
        void Start() {
            body = GetComponent<Rigidbody>();
            rotationX = head.transform.eulerAngles.x;
            rotationY = head.transform.eulerAngles.y;
        }

        void OnEnable() {
            head.enabled = true;
        }

        void OnDisable() {
            head.enabled = false;
        }

        void Update() {
            rotationX += Input.GetAxis("Mouse X") * sensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
            rotationY = ClampAngle(rotationY, -90, 90);
            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
            head.transform.rotation = Quaternion.Lerp(head.transform.rotation, rotation, 0.5f);
        }

        private void FixedUpdate() {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // Moving in the direction of camera
            targetVelocity = head.transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;
            Vector3 velocity = body.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, 2 * speed);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -speed, 2 * speed);
            velocityChange.y = 0;
            body.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        public static float ClampAngle(float angle, float min, float max) {
            if (angle < -360F) angle += 360F;
            if (angle > 360F) angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}
