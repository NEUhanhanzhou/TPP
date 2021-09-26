using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public CharacterController controller;
    public Transform cam;

    public GameObject playerBody;
    public float speed = 60.0f;
    private float jumpHeight = 6.0f;
    private float gravityValue = -9.81f;
    public bool groundedPlayer;

    public float turnSmoothTime = 0.1f;
    private float directionY;
    private Vector3 moveDir;

    float turnSmoothVelocity;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Debug.LogError(controller.isGrounded);
        if (controller.isGrounded)
        {
            directionY = 0f;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir = moveDir * speed;

            if (controller.isGrounded)
            {

                if (Input.GetButtonDown("Jump"))
                {
                    directionY += jumpHeight;


                }
            }

        }
        else
        {
            moveDir.x = 0;
            moveDir.z = 0;

            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {

                    directionY += jumpHeight;


                }
            }

        }


        directionY += gravityValue * Time.deltaTime;
        moveDir.y = directionY;
        //////playerVelocity.y = playerVelocity.y - 0.0001f;
        controller.Move(moveDir * Time.deltaTime);


    }






}

