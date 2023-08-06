using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 startPosition;

    private float inputMagnitude;

    [Header("Shake Stats")]
    [SerializeField, Range(0, 12)] private float frequency = 10.0f;
    [SerializeField, Range(0, 5)] private float amount = 0.002f;
    [SerializeField, Range(0, 12)] private float smooth = 10.0f;

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        CheckForHeadBobTrigger();

        StopHeadBob();
    }

    private void CheckForHeadBobTrigger()
    {
        Vector2 input = InputManager.instance.inputActions.Player.Movement.ReadValue<Vector2>();

        inputMagnitude = new Vector3(input.x, 0, input.y).magnitude;

        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }

    private Vector3 StartHeadBob()
    {
        Vector3 position = Vector3.zero;

        position.y += Mathf.Lerp(position.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        position.x += Mathf.Lerp(position.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
        transform.localPosition += position;

        return position;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == startPosition)
            return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, 1 * Time.deltaTime);
    }
}