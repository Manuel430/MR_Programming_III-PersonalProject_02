using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MR_PlayerScript : MonoBehaviour
{
    PlayerControlsScript playerControls;
    CharacterController characterController;

    [Header("Player Health")]
    [SerializeField] int health;
    [SerializeField] int maxHealth = 100;

    [Header("Player Speed")]
    [SerializeField] float speed = 0f;
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float runSpeed = 20f;
    [SerializeField] float crouchSpeed = 5f;
    Vector3 playerVelocity;

    [Header("Camera")]
    [SerializeField] Camera playerCAM;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI healthText;

    [Header("UI")]
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject gameOverUI;

    [SerializeField] bool isRunning;
    [SerializeField] bool isCrouching;

    bool gameOver;
    bool gamePaused;
    bool gameWon;

    private float standingHeight = 2;
    private float crouchingHeight = 0;

    private float gravity = -9.8f;
    private float jumpHeight = 1f;
    private float xRotation = 0f;
    private float xSensitivity = 30f;
    private float ySensitivity = 30f;

    public static MR_PlayerScript Instance { get; private set; }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healthText.text = maxHealth.ToString();
        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();
        characterController = GetComponent<CharacterController>();
        speed = walkSpeed;

        health = maxHealth;

        gamePaused = false;
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);

        playerControls.Player.Pause.performed += PausingGame;
        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Sprint.performed += SprintPace;
        playerControls.Player.Sprint.canceled += WalkPace;
        playerControls.Player.Crouch.performed += CrouchSize;
        playerControls.Player.Crouch.canceled += NormalSize;

    }

    public void GameWon(bool answer)
    {
        gameWon = answer;
    }

    private void Update()
    {
        healthText.text = health.ToString();

        ProcessMovement();
        ProcessCrouch();

        if(isRunning == false && isCrouching == false)
        {
            speed = walkSpeed;
        }

        if(health <= 0)
        {
            gameOver = true;
            GameOver();
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

    public void HealthChange(int change)
    {
        health += change;

        if(health == 0)
        {
            Debug.Log("Player has died. Game Over.");
        }
    }

    private void PausingGame(InputAction.CallbackContext context)
    {
        if (gameOver == true || gameWon == true)
        {
            return;
        }

        if (gamePaused == false)
        {
            gamePaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
        }
        else
        {
            gamePaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            pauseMenuUI.SetActive(false);
        }
    }

    private void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
}
