using UnityEngine;

public abstract class PlayerData : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run,
        Sprint,
        Crouch,
        Jump
    }

    internal float horizontalInput, verticalInput;

    internal Vector3 movementInput, movementDirection;

    [Header("State")]
    public PlayerState playerState;

    [Header("Movement Stats")]
    public float movementSpeed = 8f;
    public float rotationSpeed = 7f;
    [Range(0, 2)] public float playerHeight = 1;

    public void MyInput()
    {
        Vector2 input = InputManager.instance.inputActions.Player.Movement.ReadValue<Vector2>();

        horizontalInput = input.x;
        verticalInput = input.y;
    }

    public bool IsGrounded(Transform groundCheck, LayerMask whatIsGround, float groundDistance)
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
    }
}