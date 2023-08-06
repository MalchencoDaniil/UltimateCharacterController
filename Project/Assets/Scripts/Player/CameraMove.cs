using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}