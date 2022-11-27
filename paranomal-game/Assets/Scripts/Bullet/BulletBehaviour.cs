using Assets.Scripts.Enums;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private GameObject rightHand;
    private float startRangeOfBulletDrop;
    private int collisionCount = 0; // This will be use to see if ricochet hits second game object

    [SerializeField]
    private float damageDealt;
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
    private Vector3? penetrationPoint;
    private bool penetratedObject = false;
    private float weaponMuzzleVelocity;
    private float randomDestoryTime = 0;


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


        if (distanceTravled > startRangeOfBulletDrop || penetratedObject)
        {
            bulletBody.useGravity = true;
            
            bulletBody.AddForce(projectileWeight * Vector3.down); // Will move the gameobject downwards
            
            damageDealt -= 2.5f; //TODO: This will need to be modified with what damage bullets do and what distance it goes up to and enemy health, NEED MORE DIFFERENT CACULATIONS
            damageDealt = damageDealt < 0 ? 0f : damageDealt;
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
                DoDamage();
                Destroy(gameObject);
            }
            else if (isArmourPiercing && collisionCount == 0 && (contact.otherCollider.gameObject.layer == (int) EnumLayer.Wall || contact.otherCollider.gameObject.layer == (int) EnumLayer.Armoured)) // Must still do more checks for armoured layer(make)
            {
                Penetration();             
            }
            else
            {
                collisionCount++;

                Destroy(gameObject, collisionCount == 2 ? 0f : timeBulletGetsDestoryed);
            }
        }
    }

    private void Penetration()
    {
        collisionCount++;

        if (rightHand.GetComponentInChildren<BulletPenetration>() != null)
        {
            penetrationPoint = rightHand.GetComponentInChildren<BulletPenetration>().penetrationPoint;

            // Layer 10 is Enemy
            if (rightHand.GetComponentInChildren<BulletPenetration>().impactPoint != penetrationPoint)
            {
                PentrationCheck(rightHand);

                // Moves GameObject to point and with forward will point it 1 infront of position
                transform.position = penetrationPoint.Value + transform.forward;

                // Moves GameObject Rigidbody forward with new muzzle velocity
                gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * weaponMuzzleVelocity);

                Destroy(gameObject, randomDestoryTime);
            }
        }
        else
        {
            Destroy(gameObject, Random.Range(0.1f, 0.4f));  
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

    private void DoDamage()
    {
        //TODO: For when hitting enemy
    }
}
