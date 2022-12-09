using UnityEngine;

public class AimDownSight : MonoBehaviour
{
    public bool holdIn;

    [SerializeField]
    private GameObject rightHand;

    //[SerializeField]
    //private GameObject aiming;

    [SerializeField]
    private Camera camera;

    public bool aimPressed = false;

    // TODO: For charcter when create one
    private float zoomFov = 30;
    private float zoomStepTime = 5;
    private float fieldOfView = 60;

    // Update is called once per frame
    void LateUpdate()
    {
        //if (rightHand.transform.childCount != 0)
        //{
        //    ChangeAimState("");
        //    //if (!aimPressed)
        //    //{
        //    //    ClickAim();
        //    //}
        //    //else
        //    //{
        //    //    ClickHipAim();
        //    //}
        //}
    }

    // FROM OTHER GAME TO USE
    //void UpdateWeaponAiming() 
    //{
    //    if (m_WeaponSwitchState == WeaponSwitchState.Up)
    //    {
    //        WeaponController activeWeapon = GetActiveWeapon();
    //        if (IsAiming && activeWeapon)
    //        {
    //            m_WeaponMainLocalPosition = Vector3.Lerp(m_WeaponMainLocalPosition,
    //                AimingWeaponPosition.localPosition + activeWeapon.AimOffset,
    //                AimingAnimationSpeed * Time.deltaTime);
    //            SetFov(Mathf.Lerp(m_PlayerCharacterController.PlayerCamera.fieldOfView,
    //                activeWeapon.AimZoomRatio * DefaultFov, AimingAnimationSpeed * Time.deltaTime));
    //        }
    //        else
    //        {
    //            m_WeaponMainLocalPosition = Vector3.Lerp(m_WeaponMainLocalPosition,
    //                DefaultWeaponPosition.localPosition, AimingAnimationSpeed * Time.deltaTime);
    //            SetFov(Mathf.Lerp(m_PlayerCharacterController.PlayerCamera.fieldOfView, DefaultFov,
    //                AimingAnimationSpeed * Time.deltaTime));
    //        }
    //    }
    //}

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

    // Is used for when the user has press to aim down sight enabled
    private void AimPressedCheck()
    {
        aimPressed = !aimPressed;
        Debug.Log("run how");
    }

    public void HoldAim() // Need to make it so it resets when his done holding in
    {
        Vector3 weaponInHand = rightHand.GetComponentInChildren<Weapon>().aimDownSightPosition;

        // THIS needs to be smoothed out
        rightHand.transform.localPosition = Vector3.Lerp(weaponInHand, rightHand.transform.localPosition + weaponInHand, 3 * Time.deltaTime);

        //weaponInHand.position = Vector3.Lerp(weaponInHand.position, aiming.transform.localPosition, 3 * Time.deltaTime);

        //weaponInHand.transform.SetParent(aiming.transform);

        // THIS not working well
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoomFov, zoomStepTime * Time.deltaTime);
    }

    public void ReleaseAim()
    {
        Vector3 weaponInHand = rightHand.GetComponentInChildren<Weapon>().defaultHipAim;

        rightHand.transform.localPosition = Vector3.Lerp(weaponInHand, rightHand.transform.localPosition, 3 * Time.deltaTime);

        //weaponInHand.transform.SetParent(rightHand.transform);

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, fieldOfView, zoomStepTime * Time.deltaTime);
    }
    public void ClickAim()
    {
        HoldAim();
        aimPressed = true;
        Debug.Log(aimPressed + " true");
        //AimPressedCheck();
    }

    public void ClickHipAim()
    {
        ReleaseAim();
        aimPressed = false;
        Debug.Log(aimPressed + " false");
        //AimPressedCheck();
    }

}
