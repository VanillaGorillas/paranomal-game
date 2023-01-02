using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private SwapWeapon swapWeapon;
    private WeaponSystem weaponSystem;
    private AimDownSight aimDownSight;
    private PlayerMovement playerMovement;
    private PlayerLook playerLook;

    [SerializeField]
    private GameObject rightHand;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        swapWeapon = GetComponent<SwapWeapon>();
        weaponSystem = GetComponent<WeaponSystem>();
        aimDownSight = GetComponent<AimDownSight>();
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();

        onFoot.PrimaryWeaponSwap.performed += ctx => swapWeapon.SwapToPrimary();
        onFoot.SecondaryWeaponSwap.performed += ctx => swapWeapon.SwapToSecondary();

        onFoot.Jump.performed += ctx => playerMovement.Jump();

        if (playerMovement.holdInSprint)
        {
            onFoot.Sprint.started += ctx => playerMovement.Sprint();
            onFoot.Sprint.canceled += ctx => playerMovement.Walk();
        }
        else
        {
            onFoot.Sprint.performed += ctx => playerMovement.Sprint();
        }

        if (playerMovement.holdInCrouch)
        {
            onFoot.Crouch.started += ctx => playerMovement.Crouch();
            onFoot.Crouch.canceled += ctx => playerMovement.Stand();
        }
        else
        {
            if (playerMovement.isCrouching)
            {
                onFoot.Crouch.performed += ctx => playerMovement.Stand();
            }
            else
            {
                onFoot.Crouch.performed += ctx => playerMovement.Crouch();
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        {
            FullAutoCheck();
            onFoot.Reload.performed += ctx => weaponSystem.Reload();
            onFoot.SelectFiringMode.performed += ctx => weaponSystem.ChangingFiringMode();         
        }
    }

    private void FixedUpdate()
    {
        playerMovement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());

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

    private void FullAutoCheck()
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
    }

    private void DownSight()
    {
        if (!aimDownSight.holdIn)
        {
            onFoot.AimDownSight.performed += ctx => aimDownSight.ChangeAimState();
        }
        else
        {
            onFoot.AimDownSight.started += ctx => aimDownSight.Aim();
            onFoot.AimDownSight.canceled += ctx => aimDownSight.ReleaseAim();
        }
    }
}
