using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rightHand;
    private Weapon rightHandWeapon;

    [HideInInspector]
    public bool triggerDown = false; // Used for when holding down mouse and is full auto on weapon

    // Update is called once per frame
    void Update()
    {
        if (rightHand.transform.childCount != 0)
        {
            rightHandWeapon = rightHand.GetComponentInChildren<Weapon>(); // Gets RightHand GameObject

            if (triggerDown && rightHandWeapon.isFullAuto) // Gets called even though shouldn't be allowed to
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
}
