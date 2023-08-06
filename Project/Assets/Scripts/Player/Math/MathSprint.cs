using UnityEngine;

[RequireComponent(typeof(MathController))]

public class MathSprint : MonoBehaviour
{
    private MathController mathController;

    [SerializeField] private float sprintSpeedMultiplayer = 3f;

    private void Awake()
    {
        mathController = GetComponent<MathController>();
    }

    private void Update()
    {
        Sprint();
    }

    private void Sprint()
    {
        if (InputManager.instance.inputActions.Player.Sprint.IsPressed())
        {
            mathController.characterController.Move(mathController.movementDirection * sprintSpeedMultiplayer * Time.deltaTime);
        }
    }
}