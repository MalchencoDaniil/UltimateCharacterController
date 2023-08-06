using UnityEngine;

[RequireComponent(typeof(PhysicsController))]

public class PhysicsCrouch : MonoBehaviour
{
    private Rigidbody rb;
    private PhysicsController physicsController;

    private float speed;
    private float startYScale = 1f;

    [Header("Crouch Stats")]
    [SerializeField] private float crouchSpeed = 8f;
    [SerializeField] private float crouchYScale = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        physicsController = GetComponent<PhysicsController>();

        speed = physicsController.movementSpeed;
    }

    private void Update()
    {
        Crouch();
    }

    private void Crouch()
    {
        if (InputManager.instance.inputActions.Player.Crouch.IsPressed())
        {
            StartCrouch();
            physicsController.playerState = PlayerData.PlayerState.Crouch;
        }
        else
        {
            StopCrouch();
        }
    }

    private void StartCrouch()
    {
        physicsController.movementSpeed = crouchSpeed;
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, crouchYScale, Time.deltaTime * 6f), transform.localScale.z);
    }

    private void StopCrouch()
    {
        physicsController.movementSpeed = speed;
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, startYScale, Time.deltaTime * 6f), transform.localScale.z);
    }
}