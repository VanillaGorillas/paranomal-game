using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private SwapWeapon swapWeapon;
    private WeaponSystem weaponSystem;
    private AimDownSight aimDownSight;

    [SerializeField]
    private GameObject rightHand;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        swapWeapon = GetComponent<SwapWeapon>();
        weaponSystem = GetComponent<WeaponSystem>();
        aimDownSight = GetComponent<AimDownSight>();

        onFoot.PrimaryWeaponSwap.performed += ctx => swapWeapon.SwapToPrimary();
        onFoot.SecondaryWeaponSwap.performed += ctx => swapWeapon.SwapToSecondary();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        {
            if (rightHand.GetComponentInChildren<Weapon>().isFullAuto) // TODO: make this not nested
            {
                onFoot.Shoot.started += ctx => weaponSystem.FullAutoShoot();
                onFoot.Shoot.canceled += ctx => weaponSystem.CancelShooting();
            }
            else
            {
                onFoot.Shoot.performed += ctx => weaponSystem.Shoot();
            }
            onFoot.Reload.performed += ctx => weaponSystem.Reload();
            onFoot.SelectFiringMode.performed += ctx => weaponSystem.ChangingFiringMode();
            
        }
    }

    void LateUpdate()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        {
            DownSight();
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

    private void DownSight()
    {
        if (!aimDownSight.holdIn)
        {
            CheckDownSightClick();
        }
        else
        {
            onFoot.AimDownSight.started += ctx => aimDownSight.ChangeAimState("HoldAim");
            onFoot.AimDownSight.canceled += ctx => aimDownSight.ChangeAimState("");
        }
    }

    private void CheckDownSightClick()
    {
        if (!aimDownSight.aimPressed)
        {
            onFoot.AimDownSight.performed += ctx => aimDownSight.ChangeAimState("ClickAim");
        }
        else
        {
            onFoot.AimDownSight.performed += ctx => aimDownSight.ChangeAimState("ClickHipAim");
        }
    }
}
