using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MR_PlayerScript : MonoBehaviour
{
    PlayerControlsScript playerControls;
    CharacterController characterController;

    [Header("Player Speed")]
    public float speed = 0f;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float crouchSpeed = 5f;
    Vector3 playerVelocity;

    [Header("Camera")]
    public Camera playerCAM;

    public bool isRunning;
    public bool isCrouching;
    private float standingHeight = 2;
    private float crouchingHeight = 0;

    private float gravity = -9.8f;
    private float jumpHeight = 1f;
    private float xRotation = 0f;
    private float xSensitivity = 30f;
    private float ySensitivity = 30f;

    private void Awake()
    {
        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();
        characterController = GetComponent<CharacterController>();
        speed = walkSpeed;

        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Sprint.performed += SprintPace;
        playerControls.Player.Sprint.canceled += WalkPace;
        playerControls.Player.Crouch.performed += CrouchSize;
        playerControls.Player.Crouch.canceled += NormalSize;
    }

    private void Update()
    {
        ProcessMovement();
        ProcessCrouch();

        if(isRunning == false && isCrouching == false)
        {
            speed = walkSpeed;
        }
    }

    private void LateUpdate()
    {
        ProcessLook();
    }

    private void ProcessMovement()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        Vector3 movementDir = new Vector3(inputVector.x, 0, inputVector.y);

        playerVelocity.y += gravity * Time.deltaTime;
        if(characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }
        Vector3 moveInputVel = transform.TransformDirection(movementDir) * speed;
        playerVelocity.x = moveInputVel.x;
        playerVelocity.z = moveInputVel.z;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void ProcessLook()
    {
        Vector2 lookVector = playerControls.Player.Look.ReadValue<Vector2>();
        xRotation -= (lookVector.y * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        playerCAM.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (lookVector.x * Time.deltaTime) * xSensitivity);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(characterController.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    private void SprintPace(InputAction.CallbackContext context)
    {
        isRunning = true;
        speed = runSpeed;
    }

    private void WalkPace(InputAction.CallbackContext context)
    {
        isRunning = false;
        speed = walkSpeed;
    }
    private void CrouchSize(InputAction.CallbackContext context)
    {
        isCrouching = true;
    }

    private void NormalSize(InputAction.CallbackContext context)
    {
        isCrouching = false;
    }

    private void ProcessCrouch()
    {
        float heightChange;

        if(isCrouching == true)
        {
            heightChange = crouchingHeight;
            speed = crouchSpeed;
        }
        else
        {
            heightChange = standingHeight;
        }

        if(characterController.height != heightChange)
        {
            characterController.height = Mathf.Lerp(characterController.height, heightChange, crouchSpeed);
        }
    }

}
