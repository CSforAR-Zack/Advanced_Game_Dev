using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerControl : MonoBehaviour
{
    // Playercontroller settings
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float swimSpeed = 1f;
    [SerializeField] float swimForce = 10f;
    [SerializeField] float buoyantForce = 25f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] bool moveWhileJumping = false;
    [SerializeField] bool swimming = false;

    [SerializeField] float lookSensitivity = 1f;    

    // References
    private PlayerMotor motor;
    private bool jumpingModeChangeTracker = false;


    private void Start()
    {
        // Set motor reference. The motor drives the Player.
        motor = this.GetComponent<PlayerMotor>();

        motor.ChangeJumpingMode(moveWhileJumping);
    }

    private void Update()
    {
        LockCursor();
        Looking();
        Movement();
        Jumping();
        Swimming();
    }

    private static void LockCursor()
    {
        // Hides the cursor in the center of the screen when playing.
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Jumping()
    {
        // Sends jumping information to the motor.
        Vector3 _jumpForce = Vector3.zero;
        if (!swimming)
        {
            if (Input.GetButton("Jump"))
            {
                _jumpForce = Vector3.up * jumpForce;
            }
            if (jumpingModeChangeTracker != moveWhileJumping)
            {
                // Let the motor know if the Player can or cannot move during a jump.
                motor.ChangeJumpingMode(moveWhileJumping);
                jumpingModeChangeTracker = moveWhileJumping;
            }
        }
        motor.ApplyJumpForce(_jumpForce);
    }

    private void Swimming()
    {
        Vector3 _swimForce = Vector3.zero;
        if (swimming)        
        {
            if (Input.GetButton("Jump"))
            {
                _swimForce = Vector3.up * swimForce;
            }
        }
        motor.ApplySwimForce(_swimForce);
    }

    private void Looking()
    {
        // Sends mouse movement to the motor for looking around.
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRotation, 0f) * lookSensitivity;

        motor.Rotate(rotation);

        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;

        motor.RotateCamera(cameraRotationX);
    }

    private void Movement()
    {
        // Sends movement information to the motor.
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = this.transform.right * xMovement;
        Vector3 moveVertical = this.transform.forward * zMovement;
        Vector3 velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocity = (moveHorizontal + moveVertical).normalized * sprintSpeed;
        }
        else if(!swimming)
        {
            velocity = (moveHorizontal + moveVertical).normalized * moveSpeed;
        }
        else if (swimming)
        {
            velocity = (moveHorizontal + moveVertical).normalized * swimSpeed;
        }
        motor.Move(velocity);
    }

    public void ChangeSwimmingMode(bool _swimming)
    {
        swimming = _swimming;
        if (swimming)
        {
            motor.SetBuoyantForce(buoyantForce);
        }
        else
        {
            motor.SetBuoyantForce(0f);
        }

    }

}
