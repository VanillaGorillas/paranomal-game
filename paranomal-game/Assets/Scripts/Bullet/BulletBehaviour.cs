using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private GameObject rightHand;
    private float startRangeOfBulletDrop;

    [SerializeField]
    private float damageDealt;

    private float distanceTravled = 0;
    private Vector3 lastPosition;

    [SerializeField]
    private Rigidbody bulletBody;

    [SerializeField]
    private float bulletWeight; // Weight of bullet might need to be more since bullet weight is low

    private void Awake()
    {
        rightHand = GameObject.Find("RightHand"); // Gets RightHand gameobject on every Instantiate of bullet made
        startRangeOfBulletDrop = rightHand.GetComponentInChildren<Weapon>().effectiveFiringRange;
        lastPosition = transform.position;
    }

    private void Update()
    {
        distanceTravled += Vector3.Distance(lastPosition, transform.position); // Gets the distanced covered from attack point to point on map per frame
        lastPosition = transform.position;

        if (distanceTravled > startRangeOfBulletDrop)
        {
            bulletBody.useGravity = true;
            bulletBody.AddForce(bulletWeight * Vector3.down); // Will need targets to see arch as when muzzle velocity is low it arch quickly
            
            damageDealt -= 2.5f; // This will need to be modified with what damage bullets do and what distance it goes up to and enemy health
            damageDealt = damageDealt < 0 ? 0f : damageDealt;
            Debug.Log(damageDealt);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);

        if (contact.otherCollider) // Used to check if object collidered with another object // Current detects the gun object so attackpoint might need to move in future
        {
            Destroy(gameObject, 0.01f);
            Debug.Log("hit");
            Debug.Log(distanceTravled + " Normal");
            Debug.Log(Math.Round(distanceTravled) + " Rounded"); // Should or Should not use this for distance ## Please test by making velocity of weapon lower
        }
    }
}
