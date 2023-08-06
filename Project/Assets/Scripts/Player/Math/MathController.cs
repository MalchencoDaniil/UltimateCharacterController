using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class MathController : PlayerData
{
    internal CharacterController characterController;

    private const float GRAVITY_FORCE = -25f;

    internal Vector3 playerVelocity;

    [Header("References")]
    [SerializeField] private Transform orientation;

    [Header("Ground Detection")]
    public bool isGround;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGround = IsGrounded(groundCheck, whatIsGround, groundDistance);

        MyInput();
        PlayerMove();
    }

    private void ApplyGravity()
    {
        if (IsGrounded(groundCheck, whatIsGround, groundDistance) && playerVelocity.y < 0.0f)
        {
            playerVelocity.y = -1;
        }
        else
        {
            playerVelocity.y += GRAVITY_FORCE * Time.deltaTime;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void PlayerMove()
    {
        movementInput = orientation.forward * verticalInput + orientation.right * horizontalInput;
        movementDirection = movementInput.normalized;

        ApplyGravity();

        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
    }

    public bool IsFreeUp(float amount, Transform transform)
    {
        return Physics.Raycast(transform.position, Vector3.up, amount);
    }
}