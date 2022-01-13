using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Autohand.Demo
{
    public class LocalOpenXRMover : MonoBehaviour
    {
        [Header("Input Actions")]
        public InputActionProperty moveAction;
        public InputActionProperty turnAction;

        [Header("Body")]
        public GameObject head;
        private CharacterController controller;

        [Header("Settings")]
        public bool snapTurning;
        public float turnAngle;
        public float heightStep;
        public float minHeight, maxHeight;
        public float speed = 5;
        public float gravity = 1;

        private float currentGravity = 0;

        private bool turningReset = true, heightReset = true;

        //Remove jitter when going down slope
        [SerializeField] private float slopeForce;
        [SerializeField] private float slopeForceRayLenght;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            gameObject.layer = LayerMask.NameToLayer("HandPlayer");

            moveAction.action.Enable();
            moveAction.action.performed += Move;
            turnAction.action.Enable();
            turnAction.action.performed += TurnAndHeight;

        }

        private void Move(InputAction.CallbackContext move)
        {
            Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

            Vector2 moveAxis = move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(moveAxis.x, 0, moveAxis.y);

            direction = Quaternion.Euler(headRotation) * direction;

            if (controller.isGrounded)
                currentGravity = 0;
            else
                currentGravity = Physics.gravity.y * gravity;

            controller.Move(new Vector3(direction.x * speed, currentGravity, direction.z * speed) * Time.deltaTime);

            if ((direction.x != 0 || direction.z != 0) && OnSlope())
            {
                controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
            }
        }

        private bool OnSlope()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLenght))
                if (hit.normal != Vector3.up)
                    return true;

            return false;
        }


        private void TurnAndHeight(InputAction.CallbackContext turn)
        {
            Vector2 turningAxis = turn.ReadValue<Vector2>();

            //Snap turning
            if (snapTurning)
            {
                if (turningAxis.x > 0.7f && turningReset)
                {
                    transform.rotation *= Quaternion.Euler(0, turnAngle, 0);
                    turningReset = false;
                }
                else if (turningAxis.x < -0.7f && turningReset)
                {
                    transform.rotation *= Quaternion.Euler(0, -turnAngle, 0);
                    turningReset = false;
                }
                else if (turningAxis.y > 0.7f && heightReset)
                {
                    //if (transform.position.y >= maxHeight){
                    //    transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
                    //    //SetControllerHeight(maxHeight);
                    //}
                    //else{
                    //    transform.position += new Vector3(0, heightStep, 0);
                    //    AddControllerHeight(heightStep);
                    //}

                    heightReset = false;
                }
                else if (turningAxis.y < -0.7f && heightReset)
                {
                    //if (transform.position.y <= minHeight){
                    //    //SetControllerHeight(maxHeight);
                    //    transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
                    //}
                    //else{
                    //    AddControllerHeight(-heightStep);
                    //    transform.position += new Vector3(0, -heightStep, 0);
                    //}

                    heightReset = false;
                }

                if (Mathf.Abs(turningAxis.x) < 0.4f)
                    turningReset = true;
                if (Mathf.Abs(turningAxis.y) < 0.4f)
                    heightReset = true;
            }

            //Smooth turning
            else
            {
                //transform.rotation *= Quaternion.Euler(0, Time.deltaTime * turnAngle * turningAxis.x, 0);
                //transform.position += new Vector3(0, Time.deltaTime * heightStep * turningAxis.y, 0);

                //AddControllerHeight(Time.deltaTime * heightStep * turningAxis.y);

                //if (transform.position.y <= minHeight)
                //    transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
                //else if (transform.position.y >= maxHeight)
                //    transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            }
        }

        private void AddControllerHeight(float height)
        {
            controller.height += height;
            controller.center = new Vector3(0, controller.height / 2f, 0);
        }

        private void SetControllerHeight(float height)
        {
            controller.height = height;
            controller.center = new Vector3(0, height / 2f, 0);
        }
    }
}
