using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    public bool holdIn;

    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private new Camera camera;

    public bool aimPressed = false;

    // TODO: For charcter when create one
    private readonly float zoomStepTime = 5;
    private readonly float fieldOfView = 60;

    private void Update()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.GetComponentInChildren<Attachment>().sight)
        {   
                ScopeIn();
        }      
    }

    private void ScopeIn()
    {
        if (rightHand.GetComponentInChildren<AttachmentSight>().scope)
        {
            if (aimPressed)
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, rightHand.GetComponentInChildren<AttachmentSight>().fieldOfViewScopeLook, zoomStepTime * Time.deltaTime);
            }
            else
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, fieldOfView, zoomStepTime * Time.deltaTime);
            }
        }
    }

    public void ChangeAimState()
    {
        if (aimPressed)
        {
            ReleaseAim();
        } 
        else
        {
            Aim();
        }
    }

    public void Aim()
    {
        Vector3 weaponInHand = rightHand.GetComponentInChildren<Weapon>().sendAimDownSightPosition;

        rightHand.transform.localPosition = Vector3.Lerp(weaponInHand, rightHand.transform.localPosition + weaponInHand, 3 * Time.deltaTime);

        aimPressed = true;
    }

    public void ReleaseAim()
    {
        Vector3 weaponInHand = rightHand.GetComponentInChildren<Weapon>().defaultHipAim;

        rightHand.transform.localPosition = Vector3.Lerp(weaponInHand, rightHand.transform.localPosition, 3 * Time.deltaTime);

        aimPressed = false;
    }
}