using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    public PlayerInput PlayerInputMap;
    public Vector3 newPosition;
    public Vector3 CamOffset;
    public float zoomValue;

    void OnEnable()
    {
        UpdateCameraPosition();
    }

    void Start()
    {
        newPosition = transform.position;
        PlayerInputMap = GetComponent<PlayerInput>();
    }

    void Update()
    {
        HandleMovementInput();
    }

    void UpdateCameraPosition()
    {
        if (PlayerMovement.currentTilePosition != Vector3.zero)
        {
            this.transform.position = PlayerMovement.currentTilePosition + (CamOffset + transform.forward * -zoomValue);
            newPosition = transform.position; // Actualiza newPosition para evitar desajustes en HandleMovementInput
        }
    }

    void HandleMovementInput()
    {
        Vector2 zoomInput = PlayerInputMap.actions["Zoom"].ReadValue<Vector2>();
        Vector2 panInput = PlayerInputMap.actions["Pam"].ReadValue<Vector2>();

        if (zoomInput.y > 0)
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (zoomInput.y < 0)
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (panInput.x > 0)
        {
            newPosition += (transform.right * movementSpeed / 10);
        }
        if (panInput.x < 0)
        {
            newPosition += (transform.right * -movementSpeed / 10);
        }
        if (panInput.y > 0)
        {
            newPosition += (transform.up * movementSpeed / 10);
        }
        if (panInput.y < 0)
        {
            newPosition += (transform.up * -movementSpeed / 10);
        }

        transform.position = newPosition;
    }
}
