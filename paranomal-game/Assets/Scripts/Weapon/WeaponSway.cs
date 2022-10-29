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

    private Vector3 initialPosition;
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

    // Update is called once per frame
    void Update() // Needs to be smoother
    {
        // get mouse input
        float mousex = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mousey = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        // calculate target rotation
        Quaternion rotationx = Quaternion.AngleAxis(mousey, Vector3.right);
        Quaternion rotationz = Quaternion.AngleAxis(mousex, Vector3.forward);

        Quaternion targetrotation = rotationx * rotationz;

        // rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetrotation, smooth * Time.deltaTime);

        //CalculateSway();

        //MoveSway();
        //TiltSway();
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
