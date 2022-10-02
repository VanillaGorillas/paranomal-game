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

    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        if (contact.otherCollider && contact.otherCollider.gameObject.layer != 7) // Used to check if object collidered with another object // Current detects the gun object so attackpoint might need to move in future
        {
            collisionCount++;
            if (isArmourPiercing)
            {
                hitObjectCollision = true;
            }
            
            Destroy(gameObject, collisionCount == 2 ? 0f : timeBulletGetsDestoryed);
            Debug.Log("hit");
        }
    }
}
