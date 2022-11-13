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

    // Update is called once per frame
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
                returnSpeed = weapon.hipGrip;
            }
        }
 
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        
        transform.localRotation = Quaternion.Euler(currentRotation);
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
        }
        else
        {
            // This is when gun is semi-auto or shot once
            targetRotation += new Vector3(-CheckVerticalRecoil(weapon.verticalRecoil), Random.Range(-weapon.horizontalRecoil, weapon.horizontalRecoil), 0);
        }
    }

    // Used for restricting the recoil from going past the max camera angle look
    private float CheckVerticalRecoil(float verticalRecoil)
    {
        // TODO: Replace the "UnityEditor.TransformUtils.GetInspectorRotation" with its proper counter part

        // var maxAxisX = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x + UnityEditor.TransformUtils.GetInspectorRotation(cameraTransform.transform).x;

        // return maxAxisX < -playerPrefebInputManger.GetComponent<FirstPersonController>().maxLookAngle ? 0f : verticalRecoil;

        return 0;
    }
}
