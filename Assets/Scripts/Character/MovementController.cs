using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private Vector3 movementInputWorldSpace;
    [SerializeField] private Vector3 movementDirection;

    [Header("Movement")]

    public bool PlayerisCrafting = false;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    public float gravityValue = -9.81f;
    public float jumpCount = 1f;
    public bool canJump = true;

    [Header("Camera")]
    [SerializeField] private Camera followCamera;
    public GameObject mainCamera;
    public GameObject craftingCamera;
    public GameObject craftingMenu;
    

    [Header("GameObjects")]
    public GameObject UISelection;
    public CharacterController controller;
    private Vector2 movementInput;
    private Vector3 playerVelocity;
    public PlayerInput PlayerInputMap;

    void Start()
    {
        PlayerInputMap = GetComponent<PlayerInput>();
    }

    void Update()
    {
        Jump();
        if (PlayerInputMap.actions["Craft"].WasPressedThisFrame())
        {
            PlayerisCrafting = !PlayerisCrafting;
            UpdateCamera();
        }
    }
    private void FixedUpdate()
    {


        // Handle player movement if not crafting
        if (!PlayerisCrafting)
        {
            HandleMovement();
        }

    }
    private void HandleMovement()
    {
        // Get player input
        BasicMovement();
        // Rotate player towards movement direction
        RotatetroughtCamera();
        // Calculate player movement speed based on input vector
        InputSlerping();
        // Apply gravity
        Gravity();
    }
    private void BasicMovement()
    {
        GetMovementSpeed();
        // Manage player input
        Vector3 _movemenInput3D = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 _movemenInputWorldSpace = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * _movemenInput3D;
        Vector3 movementDirection = _movemenInputWorldSpace;

        // Rotate player towards movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
        controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (PlayerInputMap.actions["Craft"].WasReleasedThisFrame())
        {
            PlayerisCrafting = true;
        }
    }

    float GetMovementSpeed()
    {
        movementInput = PlayerInputMap.actions["Move"].ReadValue<Vector2>();
        float MovementSpeed;
        MovementSpeed = Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y);
        return MovementSpeed;
    }
    private void UpdateCamera()
    {
        if (PlayerisCrafting)
        {
            mainCamera.SetActive(false);
            craftingCamera.SetActive(true);
            craftingMenu.SetActive(true);
            UISelection.SetActive(true);
        }
        else
        {
            mainCamera.SetActive(true);
            craftingCamera.SetActive(false);
            craftingMenu.SetActive(false);
            UISelection.SetActive(false);
        }
    }
    private void RotatetroughtCamera()
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void InputSlerping()
    {
        // Calculate player movement speed based on input vector
        float movementSpeed = movementInputWorldSpace.magnitude * playerSpeed;
    }
    private void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = gravityValue;
            playerVelocity.x = 0;
        }
        if (controller.isGrounded)
        {
            playerSpeed = 5f;
            StopAllCoroutines();
        }
    }
    public void Jump()
    {
        if (jumpCount > 0 && !controller.isGrounded && canJump && PlayerInputMap.actions["Jump"].WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravityValue);
            jumpCount--;
        }
        if (controller.isGrounded && canJump && PlayerInputMap.actions["Jump"].WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2f * gravityValue);
            jumpCount = 1f;
        }
    }
}