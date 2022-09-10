using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask layerMask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    private GameObject leftHand;
    private GameObject rightHand;
    private SwapWeapon swapWeapon;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<FirstPersonController>().playerCamera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        swapWeapon = GetComponent<SwapWeapon>();
        leftHand = GameObject.Find("LeftHand");
        rightHand = GameObject.Find("RightHand");
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Interactable leftHandInteractable = leftHand.GetComponentInChildren<Interactable>(); // Gets LeftHand GameObject
        Interactable rightHandInteractable = rightHand.GetComponentInChildren<Interactable>(); // Gets RightHang GameObject

        // Creates ray at center of the camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);

        // Stores collision informatio.
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                playerUI.UpdateText(interactable.promptMessage);

                // When The E Key or Button West [Gamepad] is Pressed it will pick up the GameObjects
                if (inputManager.onFoot.Interact.triggered)
                {
                    //// If LeftHand GameObject has Child it will Dropped when RightHand Child is Equipped
                    //if(leftHand.transform.childCount == 1 && interactable.GetComponent<PickUpDropItem>().isRightHandItem && rightHand.transform.childCount == 0)
                    //{
                    //    leftHandInteractable.BaseInteract(); // These three lines must be changed so secondary weapon goes to primary
                    //}
                    //if (interactable.GetComponent<PickUpDropItem>().isLeftHandItem && rightHandInteractable.GetComponent<Weapon>().primaryWeapon)
                    //{
                    //    inputManager.onFoot.PrimaryWeaponSwap.performed += ctx => swapWeapon.SwapToPrimary();
                    //}

                    // Will get the Interactable function on the Component the Script is attached to.
                    interactable.BaseInteract();
                }
            }
        }
        
        // When the F Key or Button East [Gamepad] is Pressed it will drop the GameObjects
        if (inputManager.onFoot.DropItem.triggered)
        {
            if (leftHand.transform.childCount == 1)
            {
                leftHandInteractable.BaseInteract(); // Drops GameObject in LeftHand
            }
            else if(leftHand.transform.childCount == 0 && rightHand.transform.childCount == 1)
            {
                rightHandInteractable.BaseInteract(); // Drops GameObject in RightHand [Primary Weapon]
            }
            
        }
    }
}
