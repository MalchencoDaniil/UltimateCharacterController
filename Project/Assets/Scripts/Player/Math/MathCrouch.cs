using UnityEngine;

[RequireComponent(typeof(MathController))]

public class MathCrouch : MonoBehaviour
{
    private MathController mathController;

    private float startHeight, currentSpeed;

    [SerializeField] private Transform playerObj;
    [SerializeField] private float crouchSpeed = 5f;
    [SerializeField] private float crouchHeight = 0.5f;

    private void Awake()
    {
        mathController = GetComponent<MathController>();
    }

    private void Start()
    {
        currentSpeed = mathController.movementSpeed;

        startHeight = transform.localScale.y;
    }

    private void Update()
    {
        Crouch();
    }

    private void Crouch()
    {
        if (InputManager.instance.inputActions.Player.Crouch.IsPressed())
        {
            mathController.movementSpeed = crouchSpeed;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, crouchHeight, Time.deltaTime * 6f), transform.localScale.z);
        }
        else
        {
            mathController.movementSpeed = currentSpeed;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, startHeight, Time.deltaTime * 6f), transform.localScale.z);
        }
    }
}