using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    public bool holdIn;

    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private new Camera camera;

    public bool aimPressed = false;

    private PlayerLook playerLook;

    // TODO: For charcter when create one
    private readonly float zoomStepTime = 10;

    private void Awake()
    {
        playerLook = GetComponent<PlayerLook>();
    }

    private void Update()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.GetComponentInChildren<Attachment>().sight)
        {   
            ScopeIn();
        }      
    }

    // Works now but still not staying on one value
    private void ScopeIn()
    {
        if (rightHand.GetComponentInChildren<AttachmentSight>().scope)
        {
            if (aimPressed)
            {
                camera.fieldOfView = rightHand.GetComponentInChildren<AttachmentSight>().fieldOfViewScopeLook;        
            }
            else
            {
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, playerLook.fov, zoomStepTime * Time.deltaTime);
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
