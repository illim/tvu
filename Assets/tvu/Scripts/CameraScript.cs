using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace tvu
{
    class CameraScript : MonoBehaviour {
        
        public Transform target;
        public float yOffset;
        public float rotateSpeed = 150f;
        public float tpCamDist = 5f;
        public float[] angleLimit = new float[] { -90f, 90f };

        private float currentCamDist;
        private float y = 0f;
        private float x = 0f;

        void Start()
        {
            x = transform.eulerAngles.y;
            y = transform.eulerAngles.x;
        }

	    void Update ()
	    {
            if (Input.GetButtonDown("Third Person Camera"))
            {
                currentCamDist = currentCamDist == 0 ? - tpCamDist : 0;
            }
	    }

        void UpdateCamera(){
            y -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
            y = ClampAngle(y, angleLimit[0], angleLimit[1]);
            var rotation = target.rotation * Quaternion.Euler(y, 0, 0);
            var targetPos = rotation * new Vector3(0f, 0f, currentCamDist) + target.position;
            targetPos.y += yOffset;
            transform.rotation = rotation;
            transform.position = targetPos;
        }

        float ClampAngle (float angle, float min , float max) {
            if (angle < -360)
            angle += 360;
            if (angle > 360)
            angle -= 360;
            return Mathf.Clamp (angle, min, max);
        }

        void LateUpdate()
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                UpdateCamera();
        }
    }
}
