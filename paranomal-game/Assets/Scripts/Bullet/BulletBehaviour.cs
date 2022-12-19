using Assets.Scripts.Enums;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private GameObject rightHand;
    private float startRangeOfBulletDrop;
    private int collisionCount = 0; // This will be use to see if ricochet hits second game object

    private float timeBulletGetsDestoryed = 0f; // The time for the gameobject to be destoryed

    [SerializeField]
    private float addedRange; // Adds to effective range bullet if armour piercing

    private float distanceTravled = 0;
    private Vector3 lastPosition;

    [SerializeField]
    private bool isArmourPiercing;

    [SerializeField]
    private bool isSoftPoint;

    [SerializeField]
    private Rigidbody bulletBody;

    [SerializeField]
    private float projectileWeight; // Weight of bullet might need to be more since bullet weight is low

    [Header("Bullet Penetration")]

    [SerializeField]
    private float penetrationAmount; // This is the thickness of another gameobject that the bullet can pass through

    [SerializeField]
    private int penetrationContinuousAmount; // The amount of gameobjects the bullet can pass through
    private Vector3? penetrationPoint;
    private Vector3? impactPoint;
    private bool penetratedObject = false;
    private float weaponMuzzleVelocity;
    private float randomDestoryTime = 0;

    [Header("Bullet Damage")]
    private BulletDamage bulletDamage;


    private void Awake()
    {
        bulletDamage = GetComponent<BulletDamage>();
        rightHand = GameObject.Find("RightHand"); // Gets RightHand gameobject on every Creation of bullet
        startRangeOfBulletDrop = rightHand.GetComponentInChildren<Weapon>().effectiveFiringRange; 

        lastPosition = transform.position;

        if (isArmourPiercing) // Gives off ricochet affect to do damage when hitting again or time is ended
        {
            timeBulletGetsDestoryed = Random.Range(0f, 0.1f);
            startRangeOfBulletDrop += addedRange;
        }
    }

    private void Update()
    {
        distanceTravled += Vector3.Distance(lastPosition, transform.position); // Gets the distanced covered from attack point to point on map per frame
        lastPosition = transform.position;

        RayCastDistanceCheck();
    }

    private void LateUpdate()
    {
        if (distanceTravled > startRangeOfBulletDrop || penetratedObject)
        {
            bulletBody.useGravity = true;

            bulletBody.AddForce(projectileWeight * Vector3.down); // Will move the gameobject downwards

            bulletDamage.BulletDamageReduction(startRangeOfBulletDrop);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        // Used to check if object collidered with another object 
        if (contact.otherCollider && contact.otherCollider.gameObject.layer != (int)EnumLayer.Bullet) 
        {
            if (isArmourPiercing && contact.otherCollider.gameObject.layer == (int) EnumLayer.Enemy)
            {
                bulletDamage.BulletDamageOnImpact(collisionCount);

                Penetration();
            }
            else if (isArmourPiercing && collisionCount <= penetrationContinuousAmount && 
                (contact.otherCollider.gameObject.layer == (int) EnumLayer.Wall || contact.otherCollider.gameObject.layer == (int) EnumLayer.Armoured))
            {
                Penetration();
            }
            else
            {
                bulletDamage.BulletDamageOnImpact(collisionCount);
                Destroy(gameObject);
            }
        }
    }

    private void Penetration()
    {
        if (impactPoint != penetrationPoint)
        {
            PentrationCheck(rightHand);

            transform.position = penetrationPoint.Value + transform.forward;

            gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * weaponMuzzleVelocity);

            bulletDamage.BulletDamageOnImpact(collisionCount);

            Destroy(gameObject, randomDestoryTime);
        }
  
        collisionCount++;
    }

    private void RayCastDistanceCheck()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            impactPoint = hit.point;

            Ray penRay = new(hit.point + ray.direction * penetrationAmount, -ray.direction);

            if (hit.collider.Raycast(penRay, out RaycastHit penHit, penetrationAmount))
            {
                penetrationPoint = penHit.point;
            }
            else
            {
                penetrationPoint = impactPoint;
            }
        }
        else
        {
            penetrationPoint = null;
            impactPoint = null;
        }
    }

    private void PentrationCheck(GameObject rightHandGameObject)
    {
        weaponMuzzleVelocity = (float)rightHandGameObject.GetComponentInChildren<Weapon>().muzzleVelocity
                               / 100
                               * 40;
        penetratedObject = true;
        randomDestoryTime = Random.Range(1f, 2f);
    }
}
