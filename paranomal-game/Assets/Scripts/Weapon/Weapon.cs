using UnityEngine;
using TMPro;
using System.Collections;

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
    private float smoothRotation = 2f; // later put in inspector
    private float weaponRotationSpeed = 1f; // later put in inspector

    private float currentRotationX = 10f; // later put in inspector
    private float currentRotationY = 15f; // later put in inspector
    private float previousRotationX;
    private float previousRotationY;
    private float maxRotationX;
    private float maxRotationY;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Transform attackPointAdjust;

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

    [Header("Testing")]
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

        maxRotationX = currentRotationX;
        maxRotationY = currentRotationY;


        if (lineRenderer != null)
        {
            WeaponMove();
        }
    }

    private void WeaponMove() // Still work on
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

    private void FixedUpdate()
    {
        if (transform.parent == GameObject.Find("RightHand").transform)
        {
            WeaponSway();
        }
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

        if (transform.parent == GameObject.Find("RightHand").transform) // Might remove
        {
            //transform.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));

            //attackPoint.TransformDirection(0.5f, 0.5f, 0f);

            //Vector3 lookAtPosition = fpsCamera.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, fpsCamera.nearClipPlane));

            //transform.LookAt(lookAtPosition);
            //transform.LookAt(fpsCamera.transform.forward);

            //attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
            //attackPoint.LookAt(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
            //attackPoint.TransformPoint(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
            //attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
            //transform.TransformPoint(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
            //attackPoint.TransformPoint(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));



            // Failed
            ////transform.TransformDirection(fpsCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f)));
            ////attackPoint.TransformDirection(fpsCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f)));
            //transform.TransformDirection(fpsCamera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
            //attackPoint.TransformDirection(fpsCamera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));


            //Find the exact hit position using a raycast
            //Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray through the middle of screen

            ////Check if ray hits something
            //Vector3 targetPoint;
            ////Must create bullet come out of weapon MAYBE
            //if (Physics.Raycast(ray, out RaycastHit hit))
            //{
            //    attackPoint.LookAt(hit.point);
            //    //targetPoint = hit.point;
            //    //targetPoint = hit.transform.position - attackPoint.position;
            //    //Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
            //    //attackPoint.TransformDirection(targetPoint);
            //    //transform.TransformDirection(targetPoint);
            //    //attackPoint.TransformDirection(targetPoint);

            //    //transform.TransformPoint(directionWithoutSpread);
            //    //attackPoint.TransformPoint(directionWithoutSpread);
            //}
            //else
            //{
            //    attackPoint.LookAt(ray.GetPoint(75));
            //    //targetPoint = ray.GetPoint(75); // A point far away from the player
            //    //targetPoint = attackPoint.transform.forward;
            //    //targetPoint = new Vector3(0.5f, 0.5f, 0f);
            //    //attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
            //}

            //Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

            //Vector3 newDirection = Vector3.RotateTowards(attackPoint.transform.forward, targetPoint, muzzleVelocity * Time.deltaTime, 0.0f);

            //attackPoint.transform.forward = newDirection;

            //attackPoint.transform.forward = directionWithoutSpread.normalized;

            //float yes = Vector3.Distance(targetPoint, attackPoint.position);
            //Debug.Log(yes);
            ////attackPoint.TransformDirection(ray.GetPoint(yes));
            ////attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(directionWithoutSpread.normalized));
            //attackPoint.LookAt(ray.GetPoint(yes));


        }

        //if (lineRenderer != null)
        //{
        //    WeaponMove();
        //}
    }

    public void ShootPhysics()
    {
        RightHandDistanceCheck();
        recoilScript.RecoilFire();
        //Vector3 aimSpot = fpsCamera.transform.position;

        //aimSpot += fpsCamera.transform.forward * 50.0f;
        //transform.LookAt(aimSpot);

        triggerPressed = true;
        readyToShoot = false;

        // Rotation need to be smaller // Need to work on this a lot more
        float inspectorRotationX = UnityEditor.TransformUtils.GetInspectorRotation(transform).x;
        float inspectorRotationY = UnityEditor.TransformUtils.GetInspectorRotation(transform).y;
        float inspectorRotationZ = UnityEditor.TransformUtils.GetInspectorRotation(transform).z; // Still work on

        // Debug.Log(inspectorRotationZ + " Z");

        // Find the exact hit position using a raycast
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray through the middle of screen

        // Check if ray hits something
        Vector3 targetPoint;
        //Must create bullet come out of weapon MAYBE
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); // A point far away from the player
        }

       

        // Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        //Vector3 directionWithoutSpread = targetPoint - transform.position;

        // Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate new direction with spread
        //Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); // Add spread to last direction

        //Debug.Log(inspectorRotationX + " start");
        //Debug.Log(inspectorRotationY + " start");
        //inspectorRotationX /= 2;
        //inspectorRotationY /= 2;
        //Debug.Log(inspectorRotationX + " end");
        //Debug.Log(inspectorRotationY + " end");

        // Look at recoil and see if can do like that but without moving screen
        // This not working when really close or if it far away
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); // Add spread to last direction
        //Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x + inspectorRotationY, y + inspectorRotationX, 0 ); // Add spread to last direction

        // Instatiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        

        // Rotate bullet to shoot direction
        //currentBullet.transform.forward = directionWithSpread.normalized;


        //Debug.Log(attackPointAdjust + " adj");
        //Debug.Log(attackPoint + " point");

        // Add force to bullet  // Can be attackPoint or transform
        currentBullet.GetComponent<Rigidbody>().velocity = attackPoint.TransformDirection(Vector3.forward * muzzleVelocity);
        
        //currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread * muzzleVelocity, ForceMode.Impulse);
        //currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * muzzleVelocity, ForceMode.Impulse);
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
    }

    private void WeaponSway() // Must get this to move around more away from middle of screen
    {
        RotationChange();

        endPosition = Vector3.Lerp(endPosition, Vector3.zero, weaponRotationSpeed * Time.fixedDeltaTime);
        startPosition = Vector3.Slerp(startPosition, endPosition, smoothRotation * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(startPosition);
    }

    private void RotationChange()
    {
        currentRotationX = CheckRotationState(currentRotationX, previousRotationX, maxRotationX);
        currentRotationY = CheckRotationState(currentRotationY, previousRotationY, maxRotationY);

        previousRotationX = currentRotationX;
        previousRotationY = currentRotationY;
        
        endPosition = Vector3.Lerp(endPosition, new Vector3(currentRotationX, currentRotationY , 0f), weaponRotationSpeed * Time.fixedDeltaTime);
    }

    private float CheckRotationState(float currentRotation, float previousRotation, float maxRotation)
    {
        currentRotation = Random.Range(-currentRotation, currentRotation);

        // Might need to play around more with this to get something good and smooth
        if (currentRotation >= previousRotation - 1 && currentRotation <= previousRotation + 1)
        {
            if (previousRotation < 0f)
            {
                currentRotation = Random.Range(1, maxRotation - 1);
            }
            else
            {
                currentRotation = Random.Range(-maxRotation + 1, -1);
            }
        }
     
        return currentRotation;
    }

    private void RightHandDistanceCheck()
    {
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray through the middle of screen

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); 
        }

        Vector3 directionWithoutSpread = targetPoint - GameObject.Find("RightHand").transform.position;

        GameObject.Find("RightHand").transform.forward = directionWithoutSpread.normalized;
    }
}
