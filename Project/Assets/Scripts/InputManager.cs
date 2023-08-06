using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector] public InputActions inputActions;

    private void Awake()
    {
        instance = this;

        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}