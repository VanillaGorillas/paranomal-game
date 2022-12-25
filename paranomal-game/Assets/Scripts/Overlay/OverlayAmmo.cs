using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverlayAmmo : MonoBehaviour
{
    public CanvasGroup mag;
    public CanvasGroup ammoHub; // Might change
    public Image magAmount;
    //public GameObject ammoDisplay;

    //[SerializeField]
    //private Canvas canvas;
    [SerializeField]
    private TextMeshProUGUI magCount;

    [SerializeField]
    private GameObject rightHand;

    // Might not use group for this but will see as don't want my whole image backgroud to be seen otherwise i change in photoshop
    //private short transparent = 0;
    //private float full = 1;
    //// TODO: will be an amount I use to get off player later
    private float magazineAmout = 5;

    private Weapon weaponInRightHand;

    // TODO: work on fade in with weapon not active
    void Update()
    {
        if (rightHand.transform.childCount == 0 && ammoHub.alpha != 0 && rightHand.GetComponentInChildren<Weapon>() == null)
        {
            Fade();
        }
        else if (rightHand.transform.childCount == 1)
        {
            FadeIn();
        }

        if (rightHand.GetComponentInChildren<Weapon>())
        {
            weaponInRightHand = rightHand.GetComponentInChildren<Weapon>();

            DisplayAmmo(weaponInRightHand.bulletsLeft, weaponInRightHand.magazineSize);
        }
        else if (rightHand.GetComponentInChildren<Weapon>() == null)
        {
            NoDisplayAmmo();
        }
    }

    private void Fade()
    {
        ammoHub.alpha = Mathf.MoveTowards(ammoHub.alpha, 0, 0.1f * Time.fixedDeltaTime);
    }

    private void FadeIn()
    {
        ammoHub.alpha = Mathf.MoveTowards(ammoHub.alpha, 1, 0.1f * Time.fixedDeltaTime);
    }
    //TODO: create enums
    private void DisplayAmmo(float bulletsLeft, float magazineSize)
    {
        mag.alpha = 1;
        magCount.alpha = 1;

        MagCapacityCalculation(bulletsLeft);
        
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
        
        magCount.SetText($"x{magazineAmout}");
    }

    private void ReFillMag()
    {
        magAmount.fillAmount = 1;
    }

    private void NoDisplayAmmo()
    {
        mag.alpha = 0;
        magCount.alpha = 0;
        magAmount.fillAmount = 1;
    }

    private void MagCapacityCalculation(float bulletsLeft) 
    {
        if (bulletsLeft < 4)
        {
            magAmount.color = new Color(255f, 0f, 0f, 1f);
        }
        else if (bulletsLeft > 5)
        {
            magAmount.color = new Color(255f, 255f, 255f, 1f);
        }
    } 

    private void AmmoLow() { }
}
