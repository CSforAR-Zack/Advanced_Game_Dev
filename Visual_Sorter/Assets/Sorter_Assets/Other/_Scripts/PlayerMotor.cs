using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    // Tweakable parameters
    [SerializeField] Camera cam;
    [SerializeField] [Range(0,90f)] float cameraRotationLimit = 85f;
    [SerializeField] [Range(0,1f)] float maxSlopeNormal = .55f;
    [SerializeField] PhysicsMaterial walkableFriction;
    [SerializeField] PhysicsMaterial nonWalkableSlopeFriction;

    // Private variables that motor works with
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 jumpForce = Vector3.zero;
    private Vector3 swimForce = Vector3.zero;
    private CapsuleCollider capsule;
    //private bool moving = false;
    private bool jumping = true;
    private bool moveWhileJumping;
    private Vector3 groundContactNormal;
    private bool headAboveWater = true;


    private Rigidbody rb;
    private CharacterController characterChontroller;

    private void Start()
    {
        // Get reference to Rigidbody, capsule collider, and Main Camera 
        rb = this.GetComponent<Rigidbody>();
        capsule = this.GetComponent<CapsuleCollider>();
        cam = Camera.main;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotation)
    {
        cameraRotationX = _cameraRotation;
    }

    public void ApplyJumpForce(Vector3 _jumpForce)
    {
        jumpForce = _jumpForce;
    }

    public void ApplySwimForce(Vector3 _swimForce)
    {
        swimForce = _swimForce;
    }

    private void FixedUpdate()
    {

        JumpCheck();
        SwimCheck();
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        // Check the slope steepness to determine if walkable.
        if (SlopeCheck())
        {
            // Capsule collider physics material set to walkableFriction (MaxFriction) by default.
            // This aids in preventing the player from sliding down small grade slopes.
            this.GetComponent<CapsuleCollider>().material = walkableFriction;
        }
        else if (!GroundCheck() && moveWhileJumping)
        {
            // Capsule collider physics material set to nonWalkableSlopeFriction (ZeroFriction) by default.
            // This prevents the player from climbing slopes that are too steep.
            this.GetComponent<CapsuleCollider>().material = walkableFriction;
        }
        else
        {
            this.GetComponent<CapsuleCollider>().material = nonWalkableSlopeFriction;
        }

        // Perform movment when not jumping by default. Or perform movement when jumping if moveWhileJumping is true.
        if (velocity != Vector3.zero)
        {
            // Move the player in the desired direction based on key input if a key is pressed. Velocity will be .zero if no movement key is pressed.
            if (!jumping || moveWhileJumping || swimForce != Vector3.zero)
            {
                if (!moveWhileJumping)
                { 
                    // Zero out any X and Y RB velocity if on ground to avoid burst of speed.
                    rb.linearVelocity = new Vector3(0f,rb.linearVelocity.y,0f);
                }
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            }
        }
    }

    private void JumpCheck()
    {
        // Check if player has been applied a jump force and that the player is on the ground (or surface).
        if (GroundCheck()) {
            jumping = false;
            if (jumpForce != Vector3.zero)
            {
                jumping = true;
                if (!moveWhileJumping)
                {
                    // Jump without allowing Player movement while jumping.
                    // If player is moving when they jump, maintain their current X and/or Z velocity throughout jump.
                    // Have to store current velocity and zero velocity out so that movement and jump do not both apply an x and/or z velocity.
                    Vector3 currentVelocity = velocity;
                    velocity = Vector3.zero;
                    rb.linearVelocity = currentVelocity + jumpForce;
                }
                else if (moveWhileJumping)
                {
                    // Add jump (Y) velocity to RB. Movement will manage X and Z since moveWhileJumping is true.
                    rb.linearVelocity = jumpForce;
                }
            }
        }
    }

    private void SwimCheck()
    {
        if(swimForce != Vector3.zero)
        {
            if (headAboveWater)
            {

                rb.useGravity = false;
            }
            else
            {
                rb.linearVelocity = new Vector3(0, swimForce.y, 0);
            }
        }
        else
        {
            rb.useGravity = true;
        }
    }

    private void PerformRotation()
    {
        // Rotate the camera based upon mouse input.
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    public bool GroundCheck()
    {
        // Function to check if the ground (or collider) is below the Player.
        // Returns true if the player is on a jumpable surface (Has a collider and isn't too steep.
        RaycastHit hitInfo;
        bool onGround = Physics.SphereCast(this.transform.position, capsule.radius, Vector3.down, out hitInfo, ((capsule.height / 2f) - capsule.radius) + 0.1f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        groundContactNormal = hitInfo.normal;
        return onGround && (SlopeCheck());
    }

    private bool SlopeCheck()
    {
        // Check that the grade of the slope is not too steep to walk on.
        return groundContactNormal.y > maxSlopeNormal;
    }

    public void ChangeJumpingMode(bool mode)
    {
        // Toggles move to where player can or cannot move during a jump.
        moveWhileJumping = mode;
    }
    
    public void SetBuoyantForce(float value)
    {
        // Sets drag to mimic a buoyant force when Player is in water
        rb.linearDamping = value;
    }

    public void HeadAboveWaterCheck(bool value)
    {
        headAboveWater = value;
    }
}
