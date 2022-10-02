using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private SwapWeapon swapWeapon;
    private WeaponSystem weaponSystem;
    [SerializeField]
    private GameObject rightHand;

    private bool triggerDown = false; // Used for when holding down mouse and is full auto on weapon

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        swapWeapon = GetComponent<SwapWeapon>();
        weaponSystem = GetComponent<WeaponSystem>();

        onFoot.PrimaryWeaponSwap.performed += ctx => swapWeapon.SwapToPrimary();
        onFoot.SecondaryWeaponSwap.performed += ctx => swapWeapon.SwapToSecondary();

        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        {
            if (rightHand.GetComponentInChildren<Weapon>().isFullAuto)
            {
                
                onFoot.Shoot.started += ctx => TriggerDown();
                onFoot.Shoot.canceled += ctx => TriggerRelease();
            }
            else
            {
                onFoot.Shoot.performed += ctx => weaponSystem.Shoot();
            }
            onFoot.Reload.performed += ctx => weaponSystem.Reload();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerDown)
        {
            weaponSystem.Shoot();
        }
    }

    private void TriggerDown()
    {
        triggerDown = true;
    }

    private void TriggerRelease()
    {
        triggerDown = false;
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
