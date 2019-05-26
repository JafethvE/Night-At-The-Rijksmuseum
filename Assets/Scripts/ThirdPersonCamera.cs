using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightAtTheRijksmuseum
{ 
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        [SerializeField]
        private float rotateSpeed;

        private Vector3 offset;

        void Start()
        {
            offset = target.transform.position - transform.position;
        }

        void LateUpdate()
        {
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            target.transform.Rotate(0, horizontal, 0);

            float desiredAngle = target.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position - (rotation * offset);

            transform.LookAt(target.transform);
        }
    }
}