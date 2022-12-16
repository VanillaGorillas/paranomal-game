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
    private readonly float zoomFov = 30;
    private readonly float zoomStepTime = 5;
    private readonly float fieldOfView = 60;

    private void Update()
    {
        if (aimPressed)
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoomFov, zoomStepTime * Time.deltaTime);
        }
        else
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, fieldOfView, zoomStepTime * Time.deltaTime);
        }
    }

    public void ChangeAimState(string aimType)
    {
        switch (aimType)
        {
            case "HoldAim":
                HoldAim();
                break;
            case "ClickAim":
                ClickAim();
                break;
            case "ClickHipAim":
                ClickHipAim();
                break;
            default:
                ReleaseAim();
                break;
        }
    }

    public void HoldAim()
    {
        Vector3 weaponInHand = rightHand.GetComponentInChildren<Weapon>().aimDownSightPosition;

        rightHand.transform.localPosition = Vector3.Lerp(weaponInHand, rightHand.transform.localPosition + weaponInHand, 3 * Time.deltaTime);

        aimPressed = true;
    }

    public void ReleaseAim()
    {
        Vector3 weaponInHand = rightHand.GetComponentInChildren<Weapon>().defaultHipAim;

        rightHand.transform.localPosition = Vector3.Lerp(weaponInHand, rightHand.transform.localPosition, 3 * Time.deltaTime);

        aimPressed = false;
    }
    public void ClickAim()
    {
        HoldAim();
    }

    public void ClickHipAim()
    {
        ReleaseAim();
    }

}
