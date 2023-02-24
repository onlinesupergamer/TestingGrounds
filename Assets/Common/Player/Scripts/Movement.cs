using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/* Check This code For Optimization
* When Possible



*/
public class Movement : MonoBehaviour
{
    PlayerInput playerInput;

    CharacterController characterController;


    public float jumpSpeed;

    public float speed;
    public float rotationSpeed;
    public float jumpButtonGracePeriod;

    public float gravityAmount;

    [SerializeField]
    Transform cameraTransform; //Check to Get main Camera Automatically

    float ySpeed;
    float originalStepOffset;
    float? lastGroundedTime;
    float? jumpButtonPressedTime;

    private void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;

        cameraTransform = Camera.main.transform;

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        
        movementDirection.Normalize();

        movementDirection = Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) * movementDirection;

        ySpeed += gravityAmount * Time.deltaTime;

        if (characterController.isGrounded) 
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump")) 
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else 
        {
            characterController.stepOffset = 0;
        }

        Vector3 Velocity = movementDirection * magnitude;
        Velocity.y = ySpeed;

        characterController.Move(Velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
