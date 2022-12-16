using Assets.Scripts.Enums;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    // Rotations
    private Vector3 currentRotation; // The current rotation of the Player/Camera
    private Vector3 targetRotation; // The next rotation of the Player/Camera when there is recoil

    // Settings
    private float snappiness = 0f; // How quick the gun goes to new location

    // The lower the return speed the more it will move upwards
    private float returnSpeed = 0f; // Will also be affective by other stuff // Will be affected by grip of player

    [SerializeField]
    private GameObject rightHand;

    private Weapon weapon;

    [SerializeField]
    private InputManager playerPrefebInputManger;

    [SerializeField]
    private GameObject cameraTransform;

    private float maxAngle;
    private float cameraLocalEulerAngleX;
    private float totalLocalEulerAngleX;

    [Header("Sway Variables")]
    // Both increase different giving the values added
    public float swayAmountA; // Increase vertical sway
    public float swayAmountB; // Increases horizontal sway
    public float swayLerpSpeed; // This affects how far they go apart more

    private float swayTime;
    private Vector3 swayPosition;

    private void Awake()
    {
        maxAngle = playerPrefebInputManger.GetComponent<FirstPersonController>().maxLookAngle;
    }

    void Update()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        {
            weapon = rightHand.GetComponentInChildren<Weapon>();
            if(weapon.isFullAuto && playerPrefebInputManger.GetComponent<WeaponSystem>().triggerDown && weapon.bulletsLeft > 0)
            {
                snappiness = weapon.isFullAutoRecoilEnergy;
                returnSpeed = weapon.isFullAutoGrip;
            } 
            else
            {
                snappiness = weapon.recoilEnergy;
                returnSpeed = weapon.grip;
            }

            if (playerPrefebInputManger.GetComponent<AimDownSight>().aimPressed)
            {
                CalculateWeaponSway(swayTime, swayAmountA, swayAmountB, transform);

                LocalRotationChange(transform, swayPosition);
            }
            else
            {
                CalculateWeaponSway(swayTime, swayAmountA, swayAmountB, rightHand.transform.GetChild(0).transform);

                rightHand.transform.GetChild(0).transform.localRotation = Quaternion.Euler(swayPosition);

                LocalRotationChange(transform, new Vector3());
            }
        }

        cameraLocalEulerAngleX = cameraTransform.transform.localEulerAngles.x;
        totalLocalEulerAngleX = currentRotation.x + cameraLocalEulerAngleX;
    }

    private void LocalRotationChange(Transform form, Vector3 rotation)
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        form.localRotation = Quaternion.Euler(rotation + currentRotation);
    }

    public void RecoilFire()
    {
        // weapon.verticalRecoil will move the weapon upwards on the X axis with the negative sign
        // weapon.horizontalRecoil will move the weapon along the sides of the Y axis
        if(weapon.isFullAuto && playerPrefebInputManger.GetComponent<WeaponSystem>().triggerDown)     
        {
            // Y axis max 17 and -17 So player doesn't spin around
            float fullAutoHorizontalRecoil = weapon.isFullAutoHorizontalRecoil > 17f ? 17f :  weapon.isFullAutoHorizontalRecoil;

            targetRotation += new Vector3(-CheckVerticalRecoil(weapon.isFullAutoVerticalRecoil), Random.Range(-fullAutoHorizontalRecoil, fullAutoHorizontalRecoil), 0);
            //targetRotation += new Vector3(-Mathf.Clamp(weapon.isFullAutoVerticalRecoil, -maxAngle, maxAngle), Random.Range(-fullAutoHorizontalRecoil, fullAutoHorizontalRecoil), 0);
        }
        else
        {
            // This is when gun is semi-auto or shot once
            targetRotation += new Vector3(-CheckVerticalRecoil(weapon.vertical), Random.Range(-weapon.horizontal, weapon.horizontal), 0);
        }
    }

    // Used for restricting the recoil from going past the max camera angle look
    private float CheckVerticalRecoil(float verticalRecoil)
    {
        return totalLocalEulerAngleX > (float)EnumRecoil.FullRotation - maxAngle || totalLocalEulerAngleX > (float)EnumRecoil.StartRotation && totalLocalEulerAngleX < maxAngle + 1 ? verticalRecoil: 0f;
    }

    // Creates the Weapon Sway
    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }

    private void CalculateWeaponSway(float time, float amountA, float amountB, Transform form)
    {
        Vector3 targetPosition = LissajousCurve(time, amountA, amountB);

        swayPosition = Vector3.Lerp(swayPosition, targetPosition, Time.smoothDeltaTime * swayLerpSpeed);
        swayTime += Time.deltaTime;

        if (swayTime > 6.3f)
        {
            swayTime = 0;
        }
    }
}
