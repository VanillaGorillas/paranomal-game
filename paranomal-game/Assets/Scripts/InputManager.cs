using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private SwapWeapon swapWeapon;
    private WeaponSystem weaponSystem;

    [SerializeField]
    private GameObject rightHand;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        swapWeapon = GetComponent<SwapWeapon>();
        weaponSystem = GetComponent<WeaponSystem>();

        onFoot.PrimaryWeaponSwap.performed += ctx => swapWeapon.SwapToPrimary();
        onFoot.SecondaryWeaponSwap.performed += ctx => swapWeapon.SwapToSecondary();

        // Do check for sprinting
        if (CheckChild())
        {
            onFoot.SelectFiringMode.performed += ctx => weaponSystem.ChangingFiringMode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        if (CheckChild())
        {
            if (rightHand.GetComponentInChildren<Weapon>().isFullAuto)
            {
                onFoot.Shoot.started += ctx => weaponSystem.FullAutoShoot();
                onFoot.Shoot.canceled += ctx => weaponSystem.CancelShooting();
            }
            else
            {
                onFoot.Shoot.performed += ctx => weaponSystem.Shoot();
            }
            onFoot.Reload.performed += ctx => weaponSystem.Reload();
        }
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    private bool CheckChild()
    {
        return rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0;
    }
}
