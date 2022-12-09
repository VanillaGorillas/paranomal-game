using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Position")]
    [SerializeField]
    private float amount;

    [SerializeField]
    private float maxAmount;

    [SerializeField]
    private float smoothAmount;

    [Header("Sway Rotation")]
    [SerializeField]
    private float rotationAmount; 
    
    [SerializeField]
    private float maxRotationAmount;
    
    [SerializeField]
    private float smoothRotation;

    [Space]
    public bool rotationX;
    public bool rotationY;
    public bool rotationZ;

    private Vector3 initialPosition; // Must probably get this for script to change then change back
    private Quaternion initialRotation;

    private float inputX;
    private float inputY;

    [SerializeField]
    private float smooth;

    [SerializeField]
    private float swayMultiplier;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update() 
    {
        CalculateSway();

        MoveSway();
        TiltSway();
    }

    private void CalculateSway()
    {
        inputX = -Input.GetAxis("Mouse X");
        inputY = -Input.GetAxis("Mouse Y");
    }

    private void MoveSway()
    {
        float moveX = Mathf.Clamp(inputX * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(inputY * amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }

    private void TiltSway()
    {
        float tiltY = Mathf.Clamp(inputX * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltX = Mathf.Clamp(inputY * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(rotationX ? -tiltX : 0f, rotationY ? tiltY : 0f, rotationZ ? tiltY : 0f));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initialRotation, Time.deltaTime * smoothRotation);
    }
}
