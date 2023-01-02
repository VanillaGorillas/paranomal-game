using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    //[SerializeField]
    public new Camera camera;

    private float xRotation;

    [SerializeField]
    [Range(1, 100)]
    private float xSensitivity;

    [SerializeField]
    [Range(1, 100)]
    private float ySensitivity;

    public float maxLookAngle;
    public float fov;

    private void Awake()
    {
        camera.fieldOfView = fov;
    }

    // Not moving smoothly and going bad at some point when running
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.smoothDeltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        // Apply to camera transform
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player to look left and right
        transform.Rotate((mouseX * Time.smoothDeltaTime) * xSensitivity * Vector3.up);
    }
}
