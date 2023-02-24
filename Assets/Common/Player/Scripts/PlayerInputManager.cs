using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerInput playerInput;

    public Vector2 movementInput;

    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        if (playerInput == null) 
        {
            playerInput = new PlayerInput();

            playerInput.PlayerControls.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerInput.Enable();

    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void HandleMovementInput() 
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

}
