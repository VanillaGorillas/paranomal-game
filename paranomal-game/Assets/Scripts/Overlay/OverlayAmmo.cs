using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enums;

public class OverlayAmmo : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup mag;

    [SerializeField]
    private CanvasGroup ammoHub;

    [SerializeField]
    private Image magAmount;

    [SerializeField]
    private TextMeshProUGUI magCount;

    [SerializeField]
    private GameObject rightHand;

    [Space]
    [Header("AmmoHub Slots")]
    [Space]

    [SerializeField]
    private CanvasGroup rightGroup;

    [SerializeField]
    private CanvasGroup leftGroup;

    [Header("Slots Canvas Group")]
    [Space]

    [SerializeField]
    private CanvasGroup slot1Group;

    [SerializeField]
    private CanvasGroup slot2Group;

    [SerializeField]
    private CanvasGroup slot3Group;

    [Header("Slots Images")]
    [Space]

    [SerializeField]
    private Image slot1Image;

    [SerializeField]
    private Image slot2Image;

    [SerializeField]
    private Image slot3Image;


    // TODO: will be an amount I use to get off player later
    private float magazineAmout = 5;

    private Weapon weaponInRightHand;

    private void Awake()
    {
        HideOnAwake();
    }

    void Update()
    {
        Fade();

        if (rightHand.GetComponentInChildren<Weapon>())
        {
            weaponInRightHand = rightHand.GetComponentInChildren<Weapon>();

            DisplayAmmo(weaponInRightHand.bulletsLeft, weaponInRightHand.magazineSize);
        }
        else if (rightHand.GetComponentInChildren<Weapon>() == null)
        {
            HideDisplayAmmo();
        }
    }

    private void HideOnAwake()
    {
        ammoHub.alpha = (float)EnumOverlay.Empty;
        slot1Group.alpha = (float)EnumOverlay.Empty;
        slot2Group.alpha = (float)EnumOverlay.Empty;
        slot3Group.alpha = (float)EnumOverlay.Empty;
    }

    public void Fade()
    {
        if (ammoHub.alpha != 0)
        {
            ammoHub.alpha = Mathf.MoveTowards(ammoHub.alpha, (float)EnumOverlay.Empty, 0.05f * Time.fixedDeltaTime);
        }

        if (slot1Group.alpha != 0) 
        { 
            slot1Group.alpha = Mathf.MoveTowards(slot1Group.alpha, (float)EnumOverlay.Empty, 0.1f * Time.fixedDeltaTime);
        }

        if (slot2Group.alpha != 0) 
        {
            slot2Group.alpha = Mathf.MoveTowards(slot2Group.alpha, (float)EnumOverlay.Empty, 0.1f * Time.fixedDeltaTime);
        }

        if (slot3Group.alpha != 0) 
        {
            slot3Group.alpha = Mathf.MoveTowards(slot3Group.alpha, (float)EnumOverlay.Empty, 0.1f * Time.fixedDeltaTime);
        }
    }

    public void DisplayAmmoHub()
    {
        ammoHub.alpha = (float)EnumOverlay.Full;
    }

    // TODO: Will need to only display slots if occupied and if switch to the items
    public void DisplaySlot1Group()
    {
        // TODO: will need to do checks later on for slot groups
        // TODO: will need to do function to add images of said weapons
    }

    private void DisplayAmmo(float bulletsLeft, float magazineSize)
    {
        mag.alpha = (float)EnumOverlay.Full;
        magCount.alpha = (float)EnumOverlay.Full;

        MagCapacityColorChange(bulletsLeft);
        
        if (weaponInRightHand.triggerPressed)
        {
            float magSizeCalc = 100 / magazineSize; // For get different sizes of the weapon capcity
            float magCapcity = magSizeCalc * (bulletsLeft / 100); // Does calculation that returns a float number that is below 1
            magAmount.fillAmount = magCapcity;
        }
        else if (weaponInRightHand.reloading)
        {
            Invoke(nameof(ReFillMag), weaponInRightHand.reloadTime);
        }
        
        magCount.SetText($"{magazineAmout}");
    }

    private void ReFillMag()
    {
        magAmount.fillAmount = (float)EnumOverlay.Full;
    }

    private void HideDisplayAmmo()
    {
        mag.alpha = (float)EnumOverlay.Empty;
        magCount.alpha = (float)EnumOverlay.Empty;
        magAmount.fillAmount = (float)EnumOverlay.Full;
    }

    private void MagCapacityColorChange(float bulletsLeft) 
    {
        if (bulletsLeft <= 4)
        {
            magAmount.color = new Color(255f, 0f, 0f, 1f);
        }
        else if (bulletsLeft > 5)
        {
            magAmount.color = new Color(255f, 255f, 255f, 1f);
        }
    } 

    //TODO: Will increase alpha of canvas group that is in hand or give border white
    private void InHandSelected()
    {

    }

    // TODO: Will decrease alpha of canvas group that is not in hand or take away border white
    private void NotInHandSelected()
    {

    }
}
