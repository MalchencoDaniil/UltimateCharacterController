using UnityEngine;

[RequireComponent(typeof(MathController))]

public class MathJump : MonoBehaviour
{
    private MathController mathController;

    [SerializeField] private float jumpForce = 10f;

    private void Awake()
    {
        mathController = GetComponent<MathController>();
    }

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (mathController.isGround 
            && InputManager.instance.inputActions.Player.Jump.triggered 
            && !mathController.IsFreeUp(mathController.characterController.height * 1.4f, transform))
        {
            mathController.playerVelocity.y = jumpForce;
        }
    }
}