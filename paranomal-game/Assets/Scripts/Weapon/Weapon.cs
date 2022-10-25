using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public bool primaryWeapon;
    public bool secondaryWeapon;
    public Transform weaponSlot;

    [Header("Shooting Modes")]
    public bool allowedAutomaticFire; // Will produce more recoil
    public bool allowedBurstFire; // Shots 2 or 3 bullets
    public bool allowedSingleShotFire; // Player must cock gun for next bullet to go through

    [Header("Bullet Type")]
    public bool allowedsoftPoint; // Will be common bullet type for all guns besides shotguns
    public bool allowedAmrourPiercing;

    [Header("Shooting Mode on")]
    public bool isFullAuto;
    public bool isBurstFire;
    public bool isSemiAutomatic;

    [Header("Recoil Stats")]
    public float recoilEnergy; // This will affect the snappiness in Recoil Script 
    public float recoilImpules;

    [Header("Hip Recoil Stats")]
    public float verticalRecoil; // Y axis
    public float horizontalRecoil; // X axis
    public float hipGrip; // Will need to take in affect of grip attachment 

    [Header("Aim Down Sight Recoil Stats")]
    public float downSightVerticalRecoil;
    public float downSightHorizontalRecoil;
    public float downSightGrip;

    [Header("Bullt Types")]
    // bullet
    [SerializeField]
    private GameObject bullet;

    [Header("Bullet Force")]

    // bullet force
    [SerializeField]
    private float muzzleVelocity; // velocity

    [SerializeField]
    private float upwardForceGernades; // For gernades

    [Header("Weapon Mass")]
    [SerializeField]
    private float mass;

    [Header("Statistics")]

    // Weapon statistics 
    [SerializeField]
    private float timeBetweenShooting;

    [SerializeField]
    private float spread;

    [SerializeField]
    private float reloadTime;

    [SerializeField]
    private float timeBetweenShots;

    [SerializeField]
    private float rateOfFire;

    public float effectiveFiringRange;

    [SerializeField]
    private int magazineSize;

    public int bulletsPerTap;

    [Header("Weapon System Checks")]
    public int bulletsLeft;
    public bool readyToShoot;
    public bool reloading;
    public int bulletsShot;
    [HideInInspector]
    public bool triggerPressed; // When player is shooting or shot

    [Header("Aim Reference")]
    // Reference
    public Camera fpsCamera;
    public Transform attackPoint;

    [Header("Weapon UI")]
    // Graphics
    public TextMeshProUGUI ammunitionDisplay;

    [Header("Weapon Muzzle Flash")]
    public GameObject muzzleFlash;

    // Bug fixing
    public bool allowInvoke = true;

    private Recoil recoilScript;

    [Header("Player Prefab")]
    [SerializeField]
    private InputManager playerPrefebInputManger;

    // Might remove some
    [Header("Weapon Sway Stats")]
    private float rotationXMovement = 1f; // For now values will be hard typed 
    private float rotationYMovement = 1.5f; // Same ^
    private float smoothRotation = 0.2f;
    private int counter = 0;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Quaternion rotationX;
    private Quaternion rotationY;

    [SerializeField] private float handleAffect; // Will be used to increase when running or debuffs


    // For when continuous holding down when shooting full auto
    [HideInInspector]
    public float isFullAutoRecoilEnergy;
    [HideInInspector]
    public float isFullAutoGrip;
    [HideInInspector]
    public float isFullAutoVerticalRecoil; // Will need to do more checks
    [HideInInspector]
    public float isFullAutoHorizontalRecoil; // need to do more checks

    [SerializeField] private LineRenderer lineRenderer; // For testing purposes

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        recoilScript = GameObject.Find("Joint").GetComponent<Recoil>();

        isFullAutoRecoilEnergy = recoilEnergy;
        isFullAutoGrip = hipGrip;
        isFullAutoVerticalRecoil = verticalRecoil;
        isFullAutoHorizontalRecoil = horizontalRecoil;

        if(lineRenderer != null)
        {
            WeaponMove();
        }
    }

    private void WeaponMove() // Still work on
    {
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0, 0, 0));

        Vector3 endPosition;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            endPosition = hit.point;
        }
        else
        {
            endPosition = ray.GetPoint(10);
        }

        lineRenderer.SetPosition(1, endPosition);
    }

    // Update is called once per frame
    void Update()
    {                  

        if (!playerPrefebInputManger.GetComponent<WeaponSystem>().triggerDown && playerPrefebInputManger.GetComponent<WeaponSystem>() != null)
        {
            ResestRecoil();
        }

        // Set ammo display if it exists
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }

        // Probably have to use animation
        if (transform.parent == GameObject.Find("RightHand").transform) // Not working look into // Probably have here
        {
            WeaponSway();
        }
    }

    public void ShootPhysics()
    {
        recoilScript.RecoilFire();

        triggerPressed = true;
        readyToShoot = false;

        // Find the exact hit position using a raycast
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray throught the middle of screen
        RaycastHit hit;

        // Check if ray hits something
        Vector3 targetPoint;
        //Must create bullet come out of weapon MAYBE
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); // A point far away from the player
        }

        // Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); // Add spread to last direction

        // Instatiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        // Add force to bullet 
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * muzzleVelocity, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(fpsCamera.transform.up * upwardForce, ForceMode.Impulse); // For bouncing grenades

        // Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsShot++;

        // Invoke resetShot function (if not already invoked)
        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting);
            allowInvoke = false; // Only want to Invoke once
        }

        // if more than one bulletsPerTap make sure to repeat shoot function // For shotgun
        if (isBurstFire && bulletsShot < bulletsPerTap && bulletsLeft > 0) // Make burst mode
        {
            Invoke(nameof(ShootPhysics), timeBetweenShots);
        }

        RecoilIncrease();

        // Might need to put destory gameobject here if not doing collision
        //Destroy(currentBullet, 3f);
    }

    private void ResetShot()
    {
        // Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
        triggerPressed = false;
    }

    public void Reload()
    {
        if (bulletsLeft != magazineSize && !reloading)
        {
            reloading = true;
            Invoke("ReloadFinished", reloadTime);
        }
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void RecoilIncrease()
    {
        if (isFullAuto && playerPrefebInputManger.GetComponent<WeaponSystem>().triggerDown && bulletsLeft != 0)
        {
            // For change with down sight and hip // Valus will change in each part of the different code 
            FullAutoHipFire();
        }
    }

    private void ResestRecoil()
    {
        isFullAutoRecoilEnergy = recoilEnergy;
        isFullAutoGrip = hipGrip;
        isFullAutoVerticalRecoil = verticalRecoil;
        isFullAutoHorizontalRecoil = horizontalRecoil;
    }

    private void FullAutoHipFire()
    {
        isFullAutoRecoilEnergy += recoilImpules / 100;   
        // Once the grips has gone pass the check it will be half so to give the player some control over the gun
        isFullAutoGrip = isFullAutoGrip <= hipGrip / 2.5f ? hipGrip / 2.5f : isFullAutoGrip - mass / recoilEnergy / 5;  
        isFullAutoVerticalRecoil += rateOfFire / 60 / (recoilImpules * muzzleVelocity);
        isFullAutoHorizontalRecoil += ((rateOfFire / 60) * recoilImpules) / muzzleVelocity / verticalRecoil;
        Debug.Log(isFullAutoVerticalRecoil + " test");
    }

    private void WeaponSway() // Must make move in clockwise rotation
    {
        //rotationX = ReturnRandom(rotationX);
        //rotationY = ReturnRandom(rotationY);

        //rotationX = RotationCheck(rotationX, previousRotationX);
        //rotationY = RotationCheck(rotationY, previousRotationY);

        //if (previousRotationXMovement == rotationXMovement && previousRotationYMovement == rotationYMovement)
        //if (counter == 0)
        //{
        //    endPosition = Vector3.Lerp(endPosition, new Vector3(-rotationXMovement, -rotationYMovement, 0f), smoothRotation * Time.fixedDeltaTime);
        //}
        //else if (counter == 1)
        //{
        //    endPosition = Vector3.Lerp(endPosition, new Vector3(rotationXMovement, -rotationYMovement, 0f), smoothRotation * Time.fixedDeltaTime);
        //}
        //else if (counter == 2)
        //{
        //    endPosition = Vector3.Lerp(endPosition, new Vector3(0, 0, 0f), smoothRotation * Time.fixedDeltaTime);
        //}
        //else if (counter == 3)
        //{
        //    endPosition = Vector3.Lerp(endPosition, new Vector3(-rotationXMovement, rotationYMovement, 0f), smoothRotation * Time.fixedDeltaTime);
        //}
        //else if (counter == 4)
        //{
        //    endPosition = Vector3.Lerp(endPosition, new Vector3(rotationXMovement, rotationYMovement, 0f), smoothRotation * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    endPosition = Vector3.Lerp(endPosition, new Vector3(0, 0, 0f), smoothRotation * Time.fixedDeltaTime);
        //}

        //Debug.Log(counter);

        endPosition = Vector3.Lerp(endPosition, new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f) , 0f), 1f * Time.fixedDeltaTime);
        startPosition = Vector3.Slerp(startPosition, endPosition, smoothRotation * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(startPosition);

        //CheckRotation();
        //rotationX = Quaternion.AngleAxis(-rotationXMovement, Vector3.right);
        //rotationY = Quaternion.AngleAxis(-rotationYMovement, Vector3.up

        //transform.localRotation = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(Random.Range(-20f, 20f), Random.Range(-50f, 50f), 0f), 0.1f * Time.deltaTime);

        // targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        // currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        //transform.localRotation = Quaternion.Euler(currentRotation);

        //transform.localRotation = Quaternion.Lerp(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0f);
    }
}
