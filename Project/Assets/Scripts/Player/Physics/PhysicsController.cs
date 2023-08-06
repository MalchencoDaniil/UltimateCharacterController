using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PhysicsController : PlayerData
{
    private Rigidbody rb;

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform orientation;

    [Header("Drag Stats")]
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airMultiplayer = 0.4f;

    [Header("Ground Detection")]
    public bool isGround;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    [Header("Slope Handling")]
    [SerializeField] private float maxSlopeAngle = 40;
    private RaycastHit slopeHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGround = IsGrounded(groundCheck, whatIsGround, groundDistance);

        MyInput();
        ApplyDrag();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void ApplyDrag()
    {
        if (IsGrounded(groundCheck, whatIsGround, groundDistance))
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void ApplyMove()
    {
        if (OnSlope())
        {
            rb.AddForce(GetSlopeMoveDirection(movementDirection) * movementSpeed * 20f, ForceMode.Force);
        }

        if (IsGrounded(groundCheck, whatIsGround, groundDistance))
        {
            rb.AddForce(movementDirection * movementSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(movementDirection * movementSpeed * 10f * airMultiplayer, ForceMode.Force);
        }
    }

    private void PlayerMove()
    {
        movementInput = orientation.forward * verticalInput + orientation.right * horizontalInput;
        movementDirection = movementInput.normalized;

        if (movementDirection.magnitude > 0f && playerState != PlayerState.Sprint && playerState != PlayerState.Jump && playerState != PlayerState.Crouch)
        {
            playerState = PlayerState.Run;
        }
        else if (movementDirection.magnitude <= 0f)
        {
            playerState = PlayerState.Idle;
        }

        ApplyMove();
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle > maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}