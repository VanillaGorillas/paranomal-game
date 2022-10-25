using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Setting")]

    [SerializeField]
    private float smooth;

    [SerializeField] 
    private float swayMultiplier;

    private void Sway()
    {
        //rotationX = ReturnRandom(rotationX);
        //rotationY = ReturnRandom(rotationY);

        //rotationX = RotationCheck(rotationX, previousRotationX);
        //rotationY = RotationCheck(rotationY, previousRotationY);


        //endPosition = Vector3.Lerp(endPosition, new Vector3(rotationX, rotationY, 0f), 0.2f * Time.fixedDeltaTime);
        //startPosition = Vector3.Slerp(startPosition, endPosition, 0.2f * Time.fixedDeltaTime);

        //transform.localRotation = Quaternion.Euler(startPosition);

        //previousRotationX = rotationX;
        //previousRotationY = rotationY;

        //transform.localRotation = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(Random.Range(-20f, 20f), Random.Range(-50f, 50f), 0f), 0.1f * Time.deltaTime);

        // targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        // currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        //transform.localRotation = Quaternion.Euler(currentRotation);

        //transform.localRotation = Quaternion.Lerp(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0f);
    }

    private float ReturnRandom(float rotation)
    {
        return Random.Range(-rotation, rotation);
    }

    private float RotationCheck(float current, float previous) // not work
    {
        float newRotation = current;

        if (current != previous)
        {
            newRotation = ReturnRandom(current);
        } 

        if (newRotation < previous)
        {
            //newRotation = Random.Range();
        }
    
        return newRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = -Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = -Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
}
