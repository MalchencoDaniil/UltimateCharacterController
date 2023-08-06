using UnityEngine;

[RequireComponent(typeof(PhysicsController))]

public class PhysicsSprint : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicsController physicsController;

    [Header("Sprint Stats")]
    [SerializeField] private float sprintSpeed = 8;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        physicsController = GetComponent<PhysicsController>();
    }

    private void FixedUpdate()
    {
        Sprint();
    }

    private void Sprint()
    {
        if (InputManager.instance.inputActions.Player.Sprint.IsPressed() && physicsController.isGround && !physicsController.OnSlope() && physicsController.playerState != PlayerData.PlayerState.Crouch)
        {
            rb.AddForce(physicsController.movementDirection * sprintSpeed * 10f, ForceMode.Force);
            physicsController.playerState = PlayerData.PlayerState.Sprint;
        }
    }
}