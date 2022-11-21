using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private GameObject rightHand;
    private float startRangeOfBulletDrop;
    private bool hitObjectCollision = false; // When hitting object or going through will cause bullet drop
    private int collisionCount = 0; // This will be use to see if ricochet hits second game object

    [SerializeField]
    private float damageDealt;
    private float timeBulletGetsDestoryed = 0.01f; // The time for the gameobject to be destoryed

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
    //public float penetrationAmount;
    private GameObject attackPoint;
    private Vector3? endPoint;
    private Vector3? penetrationPoint;
    private Vector3? impactPoint;
    //public Vector3 startPoint; 

    private float weaponMuzzleVelocity;


    private void Awake()
    {
        rightHand = GameObject.Find("RightHand"); // Gets RightHand gameobject on every Instantiate of bullet made
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


        if (distanceTravled > startRangeOfBulletDrop || hitObjectCollision)
        {
            bulletBody.useGravity = true;
            bulletBody.AddForce(projectileWeight * Vector3.down); // Will need targets to see arch as when muzzle velocity is low it arch quickly
            
            damageDealt -= 2.5f; // This will need to be modified with what damage bullets do and what distance it goes up to and enemy health, NEED MORE DIFFERENT CACULATIONS
            damageDealt = damageDealt < 0 ? 0f : damageDealt;
        }

        //Penetration();

    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        if (contact.otherCollider && contact.otherCollider.gameObject.layer != 7) // Used to check if object collidered with another object // Current detects the gun object so attackpoint might need to move in future
        {
            if (isArmourPiercing && contact.otherCollider.gameObject.layer == 8)
            {
                Penetration();
                //Destroy(gameObject);
            }
            else
            {
                collisionCount++;
                if (isArmourPiercing)
                {
                    hitObjectCollision = true;
                }

                Destroy(gameObject, collisionCount == 2 ? 0f : timeBulletGetsDestoryed);
                Debug.Log("hit");
            }
            //collisionCount++;
            //if (isArmourPiercing)
            //{
            //    hitObjectCollision = true;
            //}

            //Destroy(gameObject, collisionCount == 2 ? 0f : timeBulletGetsDestoryed);
            //Debug.Log("hit");
        }
    }

    // Should use this to get bulletpenetration script on weapon
    private void Penetration()
    {
        // Destory object make new one after certain amount and then decrease distance and damage 
        //Vector3 currentDirection = (transform.position - startPoint).normalized;
        //Debug.Log(currentDirection + " test");
        ////Instantiate(gameObject, currentDirection + Vector3.forward, Quaternion.identity);// This bad
        //Destroy(gameObject);
        //float travledDistance = Vector3.Distance(startPoint, transform.position);

        rightHand.GetComponentInChildren<BulletPenetration>().UpdatePenetration();

        endPoint = rightHand.GetComponentInChildren<BulletPenetration>().endPoint;
        penetrationPoint = rightHand.GetComponentInChildren<BulletPenetration>().penetrationPoint; // This is correct value to use

        weaponMuzzleVelocity = rightHand.GetComponentInChildren<Weapon>().muzzleVelocity;
        transform.position = penetrationPoint.Value + transform.forward * weaponMuzzleVelocity;
        //Destroy(gameObject);

        //transform.position = (Vector3)(endPoint + transform.forward * weaponMuzzleVelocity);
        // GameObject currentBullet = Instantiate(gameObject, penetrationPoint.Value, Quaternion.identity);
        //currentBullet.GetComponent<Rigidbody>().velocity = attackPoint.TransformDirection(Vector3.forward * muzzleVelocity);
        //gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * weaponMuzzleVelocity);

        Debug.Log(penetrationPoint + " end");

    }

    private void VelocityReduction()
    {

    }
}
