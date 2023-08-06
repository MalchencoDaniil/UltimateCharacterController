using UnityEngine;

[RequireComponent(typeof(PhysicsController))]

public class PhysicsJump : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicsController physicsController;

    [Header("Jump Stats")]
    [SerializeField] private float jumpForce = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        physicsController = GetComponent<PhysicsController>();
    }

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (InputManager.instance.inputActions.Player.Jump.triggered && physicsController.isGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            physicsController.playerState = PlayerData.PlayerState.Jump;
        }
    }
}