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
    public float speed = 10f;
    public float runSpeed = 20f;
    Vector3 playerVelocity;

    [Header("Camera")]
    public Camera playerCAM;

    private float gravity = -9.8f;
    private float jumpHeight = 3f;
    private float xRotation = 0f;
    private float xSensitivity = 30f;
    private float ySensitivity = 30f;

    private void Awake()
    {
        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();
        characterController = GetComponent<CharacterController>();
        playerControls.Player.Look.performed += UpdateLookInput;
    }

    private void UpdateLookInput(InputAction.CallbackContext context)
    {
        ProcessLook(context.ReadValue<Vector2>());
    }

    private void ProcessLook(Vector2 lookVector)
    {
        xRotation -= (lookVector.y * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        if(playerCAM != null)
        {
            return;
        }
        playerCAM.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (lookVector.x * Time.deltaTime) * xSensitivity);
    }

    private void Update()
    {
        ProcessMovement();
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
}
