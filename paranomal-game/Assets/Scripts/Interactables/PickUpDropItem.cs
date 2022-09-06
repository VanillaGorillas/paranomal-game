using UnityEngine;

public class PickUpDropItem : Interactable
{
    [SerializeField]
    private new Rigidbody rigidbody;
    [SerializeField]
    private new BoxCollider collider;
    [SerializeField]
    private Transform player, parentComponent, fpsCamera, environment;
    [SerializeField]
    private float  dropForwardForce, dropUpwardForce;
    [SerializeField]
    private bool equipped;
    public static bool slotfull;

    // Start is called before the first frame update
    void Start()
    {
        // Setup of each object with the script
        if (!equipped)
        {
            rigidbody.isKinematic = false;
            collider.isTrigger = false;
        }
        else if (equipped)
        {
            rigidbody.isKinematic = true;
            collider.isTrigger = true;
            slotfull = true;
        }
    }

    protected override void Interact()
    {
        if (!equipped && !slotfull)
        {
            PickUp();
        }
        else if (equipped && slotfull)
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotfull = true;

        // Make object a child of the parent Component
        transform.SetParent(parentComponent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        // Make Rigidbody kinematic and BoxCollider true
        rigidbody.isKinematic = true;
        collider.isTrigger = true;

    }

    private void Drop()
    {
        equipped = false;
        slotfull = false;

        // Parent set to null
        transform.SetParent(environment);

        // Make Rigidbody kinematic and BoxCollider false
        rigidbody.isKinematic = false;
        collider.isTrigger = false;

        // AddForce
        rigidbody.AddForce(fpsCamera.forward * dropForwardForce, ForceMode.Impulse);
        rigidbody.AddForce(fpsCamera.up * dropUpwardForce, ForceMode.Impulse);

        // Random rotation
        float random = Random.Range(-1f, 1f);
        rigidbody.AddTorque(new Vector3(random, random, random) * 10);
    }
}
