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

    [Header("Recoil Values To Send")]
    [HideInInspector]
    public float grip;
    [HideInInspector]
    public float vertical;
    [HideInInspector]
    public float horizontal;

    [Header("Hip Recoil Stats")]
    [SerializeField]
    private float verticalRecoil; // Y axis

    [SerializeField]
    private float horizontalRecoil; // X axis

    [SerializeField]
    private float hipGrip; // Will need to take in affect of grip attachment 

    [Header("Aim Down Sight Recoil Stats")]
    [SerializeField]
    private float downSightVerticalRecoil; // TODO: make one that will be sent to the recoil script and others private for both

    [SerializeField]
    private float downSightHorizontalRecoil;

    [SerializeField]
    private float downSightGrip;

    [Header("Bullt Types")]
    // bullet
    [SerializeField]
    private GameObject bullet;

    [Header("Bullet Force")]

    // bullet force
    [SerializeField]
    public float muzzleVelocity; // velocity

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
    public Vector3 aimDownSightPosition;
    public Vector3 defaultHipAim;

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

        if (lineRenderer != null) // MAYBE MOVE THIS INTO UPDATE // DON'T move into update as just weird
        {
            WeaponMove();
        }
    }

    // For lineRenderer. Testing purpose
    private void WeaponMove() 
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

    void Update()
    {
        if (!playerPrefebInputManger.GetComponent<WeaponSystem>().triggerDown && playerPrefebInputManger.GetComponent<WeaponSystem>() != null)
        {
            if (playerPrefebInputManger.GetComponent<AimDownSight>().aimPressed)
            {
                ResestRecoil(downSightGrip, downSightVerticalRecoil, downSightHorizontalRecoil);
            }
            else
            {
                ResestRecoil(hipGrip, verticalRecoil, horizontalRecoil);
            }
        }

        // Set ammo display if it exists
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }

        SendRecoilValuesToSend();
    }

    public void ShootPhysics()
    {
        recoilScript.RecoilFire();

        triggerPressed = true;
        readyToShoot = false;

        // Instatiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, transform.rotation);

        // Add force to bullet  // Can be attackPoint or transform
        currentBullet.GetComponent<Rigidbody>().velocity = attackPoint.TransformDirection(Vector3.forward * muzzleVelocity);        

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
            CheckADSTrigger();
        }
    }

    private void SendRecoilValuesToSend()
    {
        if (playerPrefebInputManger.GetComponent<AimDownSight>().aimPressed)
        {
            grip = downSightGrip;
            vertical = downSightVerticalRecoil;
            horizontal = downSightHorizontalRecoil;
        }
        else
        {
            grip = hipGrip;
            vertical = verticalRecoil;
            horizontal = horizontalRecoil;
        }
    }

    private void CheckADSTrigger()
    {
        if (playerPrefebInputManger.GetComponent<AimDownSight>().aimPressed)
        {
            // For change with down sight and hip // Valus will change in each part of the different code 
            FullAutoHipFire(downSightGrip, downSightVerticalRecoil);
        }
        else
        {
            FullAutoHipFire(hipGrip, verticalRecoil);
        }
        
    }

    private void ResestRecoil(float grip, float vertical, float horizontal) // TODO: must check here also I think
    {
        isFullAutoRecoilEnergy = recoilEnergy;
        isFullAutoGrip = grip;
        isFullAutoVerticalRecoil = vertical;
        isFullAutoHorizontalRecoil = horizontal;
    }

    private void FullAutoHipFire(float grip, float vertical)
    {
        isFullAutoRecoilEnergy += recoilImpules / 100;   
        // Once the grips has gone pass the check it will be half so to give the player some control over the gun
        isFullAutoGrip = isFullAutoGrip <= grip / 2.5f ? grip / 2.5f : isFullAutoGrip - mass / recoilEnergy / 5;  
        isFullAutoVerticalRecoil += rateOfFire / 60 / (recoilImpules * muzzleVelocity);
        isFullAutoHorizontalRecoil += ((rateOfFire / 60) * recoilImpules) / muzzleVelocity / vertical;
    }
}
