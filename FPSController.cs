using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;

    //movement parameters
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    //viewing parameters
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    //sprint parameters
    public Slider sprintSlider;
    private float sprintTimeLeft;
    private const float maxSprintTime = 10f;
    private bool isSprinting;
    private bool rechargingSprint;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        //initialize references and locks the cursor
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //initializes sprint parameters
        sprintTimeLeft = maxSprintTime;
        sprintSlider.maxValue = maxSprintTime;
        sprintSlider.value = maxSprintTime;
        isSprinting = false;
        rechargingSprint = false;
    }

    void Update()
    {
        //handles movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //handle sprinting
        bool isRunningInput = Input.GetKey(KeyCode.LeftShift) && sprintTimeLeft > 0;
        isSprinting = isRunningInput && !rechargingSprint;

        float curSpeedX = canMove ? (isSprinting ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isSprinting ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //handle Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        //update sprint time and recharge logic
        if (isSprinting)
        {
            sprintTimeLeft -= Time.deltaTime;
            if (sprintTimeLeft <= 0)
            {
                sprintTimeLeft = 0;
                isSprinting = false;
                rechargingSprint = true;
            }
        }
        else
        {
            RechargeSprint();
        }

        //update the UI slider to reflect the remaining sprint time
        sprintSlider.value = sprintTimeLeft;
    }

    private void RechargeSprint()
    {
        if (!isSprinting)
        {
            sprintTimeLeft += Time.deltaTime;
            if (sprintTimeLeft >= maxSprintTime)
            {
                sprintTimeLeft = maxSprintTime;
                rechargingSprint = false;
            }
        }
    }
}
