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
    public float verticalRecoil; // Y axis
    public float horizontalRecoil; // X axis
    public float gripStabilizer; // Will need to take in affect of grip attachment // This is affect the aim wrong
    public float recoilEnergy; // This will affect the snappiness in Recoil Script // This is affect the aim wrong
    public float recoilImpules;

    [Header("Bullt Types")]
    // bullet
    [SerializeField]
    private GameObject bullet;

    [Header("Bullet Force")]

    // bullet force
    [SerializeField]
    private float muzzleVelocity; // velocity

    [SerializeField]
    private float upwardForce; // For gernades

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

    [SerializeField]
    private int bulletsPerTap;

    [Header("Weapon System Checks")]
    public int bulletsLeft;
    // Bools
    public bool readyToShoot;
    public bool reloading;
    public int bulletsShot;

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

    // For when continuous holding down when shooting full auto
    [HideInInspector]
    public float isFullAutoRecoilEnergy;
    [HideInInspector]
    public float isFullAutoGripStabilizer;
    [HideInInspector]
    public float isFullAutoVerticalRecoil; // Will need to do more checks
    [HideInInspector]
    public float isFullAutoHorizontalRecoil; // need to do more checks


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        recoilScript = GameObject.Find("Joint").GetComponent<Recoil>();

        isFullAutoRecoilEnergy = recoilEnergy;
        isFullAutoGripStabilizer = gripStabilizer;
        isFullAutoVerticalRecoil = verticalRecoil;
        isFullAutoHorizontalRecoil = horizontalRecoil;
    }

    // Update is called once per frame
    void Update()
    {
        // Make gun move around here function // Will probably need to create new functin to get weapon to sway smoothly
        //transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-mass, mass), Random.Range(-mass, mass), 0));                   

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

        //GameObject bulletClone;
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
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false; // Only want to Invoke once
        }

        // if more than one bulletsPerTap make sure to repeat shoot function // For shotgun
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("ShootPhysics", timeBetweenShots);
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
            isFullAutoRecoilEnergy += recoilImpules / 100;
            isFullAutoGripStabilizer = isFullAutoGripStabilizer <= 0 ? 0f : isFullAutoGripStabilizer - recoilImpules / 100; // Will still go past zero for the first time but will be okay afterwards
            isFullAutoVerticalRecoil += ((rateOfFire / 60) / magazineSize) / 100;
            isFullAutoHorizontalRecoil += ((rateOfFire / 60) / magazineSize) / 100;
        }
    }

    private void ResestRecoil()
    {
        isFullAutoRecoilEnergy = recoilEnergy;
        isFullAutoGripStabilizer = gripStabilizer;
        isFullAutoVerticalRecoil = verticalRecoil;
        isFullAutoHorizontalRecoil = horizontalRecoil;
    }

}
