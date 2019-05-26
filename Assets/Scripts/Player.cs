using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightAtTheRijksmuseum
{
    public class Player : Character
    {
        // Update is called once per frame
        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (input != Vector3.zero)
            {
                Move(input);
            }
        }

        private void Move(Vector3 input)
        {
            //Calculate movement.
            Vector3 movement = input * movementSpeed;

            //Movement is relative to the camera.
            Vector3 relativeMovement = Camera.main.transform.TransformVector(movement);

            //We want no movement on the y axis.
            relativeMovement.y = 0;

            //Move.
            controller.Move(relativeMovement * Time.deltaTime);
        }
    }
}