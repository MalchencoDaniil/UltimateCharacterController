using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FPSCamera : MonoBehaviour
{
    private float mouseX, mouseY;

    private float xRotation, yRotation;

    [Header("Camera Transform Stats")]
    [SerializeField, Range(-90, 0)] private float minY = -60;
    [SerializeField, Range(0, 90)] private float maxY = 60;

    [Header("Camera settings")]
    [SerializeField] private float mouseSensetivity = 10;
    [SerializeField] private float standartFOV = 70f;
    [SerializeField] private float aimFOV = 20f;

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform orientation;

    private void Update()
    {
        MyInput();
        CameraMovement();
    }

    private void Start()
    {
        mainCamera.fieldOfView = standartFOV;

        mainCamera = GetComponent<Camera>();
    }

    private void MyInput()
    {
        if (Input.GetKey(KeyCode.Mouse1)) 
        { 
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, aimFOV, 3 * Time.deltaTime); 
        }
        else 
        { 
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, standartFOV, 3 * Time.deltaTime); 
        }

        Vector2 cameraLook = InputManager.instance.inputActions.Player.CameraLook.ReadValue<Vector2>();

        mouseX = cameraLook.x * mouseSensetivity * Time.deltaTime;
        mouseY = cameraLook.y * mouseSensetivity * Time.deltaTime;
    }

    private void CameraMovement()
    {
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}