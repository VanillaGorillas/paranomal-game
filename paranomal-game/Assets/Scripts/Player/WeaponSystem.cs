using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rightHand;
    private Weapon rightHandWeapon;

    // Update is called once per frame
    void Update()
    {
        if (rightHand.transform.childCount != 0)
        {
            rightHandWeapon = rightHand.GetComponentInChildren<Weapon>(); // Gets RightHand GameObject
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
}
