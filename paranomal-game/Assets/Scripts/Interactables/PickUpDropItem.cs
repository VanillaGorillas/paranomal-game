using UnityEngine;

public class PickUpDropItem : Interactable
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private new BoxCollider collider;
    [SerializeField] private Transform player, parentComponent, fpsCamera, environment;  
    [SerializeField] private float  dropForwardForce, dropUpwardForce;
    [SerializeField] private bool equipped;

    [Header("ItemHand")]
    public bool isLeftHandItem; // Item or gameobject must be equipped to LeftHand GameObject
    public bool isRightHandItem; // Item or gameobject must be equipped to RightHand GameObject

    public static bool isRightHandSlotFull;
    public static bool isLeftHandSlotFull;
    private Weapon weaponComponent;

    // Start is called before the first frame update
    void Start()
    {
        weaponComponent = GetComponent<Weapon>();

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
            isRightHandSlotFull = isRightHandItem;
            isLeftHandSlotFull = isLeftHandItem;

            if (weaponComponent.primaryWeapon || weaponComponent.secondaryWeapon)
            {
                weaponComponent.weaponSlot.GetComponent<WeaponSlot>().isSlotFull = true;
            }

            // Sets child Component in center of parent
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    protected override void Interact()
    {
        if (isRightHandItem)
        {
            if (parentComponent.childCount == 0 && !isRightHandSlotFull)
            {
                PickUp();
            }
            else if (!weaponComponent.weaponSlot.GetComponent<WeaponSlot>().isSlotFull && parentComponent.childCount == 1)
            {
                PutInSlot();
            }    
            else
            {
                Drop();

            }
        }
        else
        {
            if (parentComponent.childCount == 0 && !isLeftHandSlotFull)
            {
                PickUp();
            }
            else
            {
                Drop();
            }
        }
    }

    public void MakeRightHandEmpty()
    {
        isRightHandSlotFull = false;
    }

    private void PickUp()
    {
        equipped = true;
        isRightHandSlotFull = isRightHandItem;
        isLeftHandSlotFull = isLeftHandItem;

        // Make object a child of the parent Component
        transform.SetParent(parentComponent);
        //transform.localPosition = Vector3.zero;
        ComponentChanges();
        
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void Drop()
    {
        equipped = false;

        if (isRightHandItem)
        {
            isRightHandSlotFull = false;

            
            if(transform.parent == parentComponent.transform)
            {
                if (weaponComponent.primaryWeapon || weaponComponent.secondaryWeapon)
                {
                    weaponComponent.weaponSlot.GetComponent<WeaponSlot>().isSlotFull = false;
                }
            }
        }
        else if (isLeftHandItem)
        {
            isLeftHandSlotFull = false;
        }

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

    // public void PutInSlot()
    private void PutInSlot()
    {
        transform.SetParent(weaponComponent.weaponSlot);

        ComponentChanges();

        if (weaponComponent.primaryWeapon)
        {
            transform.localRotation = Quaternion.Euler(-90, 0, 0); // Rotates gun upwards while on players back
        }
        else
        {
            transform.localRotation = Quaternion.Euler(90, 0, 0); // Rotates gun downwards while on players side hip
        }
    }

    private void ComponentChanges()
    {
        transform.localPosition = Vector3.zero;

        if (isRightHandItem)
        { 
            weaponComponent.weaponSlot.GetComponent<WeaponSlot>().isSlotFull = true;
        }

        // Make Rigidbody kinematic and BoxCollider true
        rigidbody.isKinematic = true;
        collider.isTrigger = true;
    }
}
