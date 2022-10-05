using UnityEngine;

public class Recoil : MonoBehaviour
{
    // Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    // Settings
    private float snappiness = 0f; // This is fucking up the crosshair // How quick the gun goes to new location

    // The lower the return speed the more it will move upwards
    private float returnSpeed = 0f; // Will also be affective by other stuff // Will be affected by grip of player

    [SerializeField]
    private GameObject rightHand;

    private Weapon weapon;

    [SerializeField]
    private InputManager playerPrefebInputManger;

    [SerializeField]
    private GameObject cameraTransform;


    // Maybe do something with fixed update to get thing to zero or something with camera 

    // Update is called once per frame
    void Update()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        {
            weapon = rightHand.GetComponentInChildren<Weapon>();
            if(weapon.isFullAuto && playerPrefebInputManger.GetComponent<WeaponSystem>().triggerDown && weapon.bulletsLeft > 0)
            {
                snappiness = weapon.isFullAutoRecoilEnergy;
                returnSpeed = weapon.isFullAutoGripStabilizer;
            } 
            else
            {
                snappiness = weapon.recoilEnergy;
                returnSpeed = weapon.gripStabilizer;
            }
        }

        // Still need to add restriction for going to far up
        // JOINT THING
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        //GameObject cam = GameObject.Find("PlayerCamera");
        //Debug.Log(cam.transform.eulerAngles + " ion"); // Yes this
        ////Debug.Log(currentRotation + " current");
        //Debug.Log(targetRotation + " target");
        
        transform.localRotation = Quaternion.Euler(currentRotation);
        //GameObject reticle = GameObject.Find("Reticle");
        //reticle.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        // weapon.verticalRecoil will move the weapon upwards on the X axis with the negative sign
        // weapon.horizontalRecoil will move the weapon along the sides of the Y axis
        if(weapon.isFullAuto && playerPrefebInputManger.GetComponent<WeaponSystem>().triggerDown) // Posible don't use     
        {
            //Debug.Log(weapon.isFullAutoRecoilEnergy + " energy");
            //Debug.Log(weapon.isFullAutoGripStabilizer + " Grip");
            //Debug.Log(weapon.isFullAutoVerticalRecoil + " Vertical");
            //Debug.Log(weapon.isFullAutoHorizontalRecoil + " Horizontal");



            // This will not actually fuck
            //float fullAutoVerticalRecoil = Mathf.Round(cameraTransform.transform.localEulerAngles.x) == 310f ? 0f : weapon.isFullAutoVerticalRecoil;
            // Clamp pitch between lookAngle
            //Debug.Log(Mathf.Round(cameraTransform.transform.localEulerAngles.x) + " cam");
            //Debug.Log(Mathf.Round(transform.localEulerAngles.x) + " local");
            ////if(Mathf.Round(cameraTransform.transform.eulerAngles.x) >= 50f || Mathf.Round(cameraTransform.transform.eulerAngles.x) >= 310f)
            //float cameraAngle = Mathf.Round(cameraTransform.transform.localEulerAngles.x);
            ////if (cameraAngle >= 310f && cameraAngle <= 360f || cameraAngle >= 0f && cameraAngle <= 50f)
            //if (cameraAngle == 310f || cameraAngle <= 310f && cameraAngle > 50f)
            //{
            //    Debug.Log("yes");
            //}
            //float fullAutoVerticalRecoil = Mathf.Clamp(weapon.isFullAutoVerticalRecoil, -maxLookAngleRecoil, maxLookAngleRecoil);
            // Y axis max 17 and -17
            float fullAutoHorizontalRecoil = weapon.isFullAutoHorizontalRecoil > 17f ? 17f : weapon.isFullAutoHorizontalRecoil;
            
            targetRotation += new Vector3(-weapon.isFullAutoVerticalRecoil, Random.Range(-fullAutoHorizontalRecoil, fullAutoHorizontalRecoil), 0);
        }
        else
        {
            targetRotation += new Vector3(-weapon.verticalRecoil, Random.Range(-weapon.horizontalRecoil, weapon.horizontalRecoil), 0);
        }
    }
}
