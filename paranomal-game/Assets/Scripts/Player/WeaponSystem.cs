using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rightHand;
    private Weapon rightHandWeapon;
    private WeaponFiringMode weaponFiringMode;

    [HideInInspector]
    public bool triggerDown = false; // Used for when holding down mouse and is full auto on weapon

    // Update is called once per frame
    void Update()
    {
        if (rightHand.transform.childCount != 0)
        {
            rightHandWeapon = rightHand.GetComponentInChildren<Weapon>(); // Gets RightHand GameObject
            weaponFiringMode = rightHand.GetComponentInChildren<WeaponFiringMode>();

            if (triggerDown && rightHandWeapon.isFullAuto)
            {
                Shoot();
            }
        }     
    }

    public void Shoot()
    {
         // Shooting
        if (rightHandWeapon.readyToShoot && !rightHandWeapon.reloading && rightHandWeapon.bulletsLeft > 0 && rightHand.GetComponentInChildren<Weapon>() != null)
        {
            // Set bullets shot to 0
            rightHandWeapon.bulletsShot = 0;
            rightHandWeapon.ShootPhysics();
        }
    }

    public void Reload()
    {
        if(rightHand.GetComponentInChildren<Weapon>() != null)
        {
            rightHandWeapon.Reload();
        }
    }

    public void FullAutoShoot()
    {
        triggerDown = true;
    }

    public void CancelShooting()
    {
        triggerDown = false;
    }

    public void ChangingFiringMode()
    {
        // When we have our own personal player controller we would need to take into factor of if the player is sprinting
        if (!rightHandWeapon.reloading && !rightHandWeapon.triggerPressed) 
        {
            weaponFiringMode.SelectFiringMode();
        }
    }
}
