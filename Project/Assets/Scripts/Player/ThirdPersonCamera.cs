using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private enum CameraStyle
    {
        Basic, 
        Combat, 
        Topdown
    }

    [SerializeField] private CameraStyle cameraStyle;
    [SerializeField] private float rotationSpeed = 7f;

    [Space(15)][SerializeField] private GameObject basicCamera;
    [SerializeField] private GameObject combatCamera;
    [SerializeField] private GameObject topdownCamera;
     
    [Space(15)][SerializeField] private Transform orientation;
    [SerializeField] private Transform combatLookAt;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;

    private void Start()
    {
        SwitchCameraStyle(CameraStyle.Basic);
    }

    private void Update()
    {
        MyInput();
        CameraController();
    }

    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCameraStyle(CameraStyle.Basic);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCameraStyle(CameraStyle.Combat);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCameraStyle(CameraStyle.Topdown);
        }
    }

    private void CameraController()
    {
        Vector2 input = InputManager.instance.inputActions.Player.Movement.ReadValue<Vector2>();

        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        if (cameraStyle == CameraStyle.Basic || cameraStyle == CameraStyle.Topdown)
        {
            float horizontalInput = input.x;
            float verticalInput = input.y;

            Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDirection != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (cameraStyle == CameraStyle.Combat)
        {
            Vector3 directionToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = directionToCombatLookAt.normalized;

            playerObj.forward = directionToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCamera.SetActive(false);
        basicCamera.SetActive(false);
        topdownCamera.SetActive(false);

        if (newStyle == CameraStyle.Basic)
        {
            basicCamera.SetActive(true);
        }

        if (newStyle == CameraStyle.Combat)
        {
            combatCamera.SetActive(true);
        }

        if (newStyle == CameraStyle.Topdown)
        {
            topdownCamera.SetActive(true);
        }

        cameraStyle = newStyle;
    }
}