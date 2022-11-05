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

    [SerializeField]
    private float handleAffect; // Will be used to increase when running or debuffs

    // For when continuous holding down when shooting full auto
    [HideInInspector]
    public float isFullAutoRecoilEnergy;
    [HideInInspector]
    public float isFullAutoGrip;
    [HideInInspector]
    public float isFullAutoVerticalRecoil; // Will need to do more checks
    [HideInInspector]
    public float isFullAutoHorizontalRecoil; // need to do more checks

    [Header("For Testing Weapon")]
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

        if (lineRenderer != null)
        {
            WeaponMove();
        }
    }

    private void WeaponMove() // For lineRenderer. Testing purpose
    {
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));       

        Vector3 endPosition;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            endPosition = hit.point;
        }
        else
        {
            endPosition = ray.GetPoint(75);
        }

        Vector3 direction = endPosition - attackPoint.position;

        lineRenderer.SetPosition(1, direction);
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

    }

    public void ShootPhysics()
    {
        recoilScript.RecoilFire();

        triggerPressed = true;
        readyToShoot = false;

        // Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate new direction with spread
        //Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); // Add spread to last direction

        // Instatiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        

        // Add force to bullet  // Can be attackPoint or transform
        currentBullet.GetComponent<Rigidbody>().velocity = attackPoint.TransformDirection(Vector3.forward * muzzleVelocity);        
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

        //Might need to put destory gameobject here if not doing collision
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
    }
}
