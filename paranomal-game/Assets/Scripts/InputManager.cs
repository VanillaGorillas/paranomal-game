using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public sealed class InputManager : NetworkBehaviour
{
    #region Networking 

    [SyncVar]
    public string username;

    [SyncVar]
    public bool isReady;

    #endregion

    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private SwapWeapon swapWeapon;
    private WeaponSystem weaponSystem;

    [SerializeField]
    private GameObject rightHand;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        swapWeapon = GetComponent<SwapWeapon>();
        weaponSystem = GetComponent<WeaponSystem>();

        onFoot.PrimaryWeaponSwap.performed += ctx => swapWeapon.SwapToPrimary();
        onFoot.SecondaryWeaponSwap.performed += ctx => swapWeapon.SwapToSecondary();

    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            ServerRPCSetIsReady(!isReady);
        }

        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
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
            onFoot.SelectFiringMode.performed += ctx => weaponSystem.ChangingFiringMode();
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

    /**
     * Is a Remote-Procedure-Call which is sent from the client to the server. 
     * It allows you to call code to be executed server side from the client,
     * TODO: MOve this to a definitions file later on.
     */
    [ServerRpc]
    public void ServerRPCSetIsReady(bool value)
    {
        isReady = value;
    }
    

    #region Networking Ovverrides

    public override void OnStartServer()
    {
        base.OnStartServer();

        GameManager.instance.players.Add(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        GameManager.instance.players.Remove(this);
    }

    #endregion
}
