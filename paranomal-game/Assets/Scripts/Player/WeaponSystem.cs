using UnityEngine;
using TMPro;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject rightHand;
    private Weapon rightHandWeapon;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

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
        if (rightHandWeapon.readyToShoot && !rightHandWeapon.reloading && rightHandWeapon.bulletsLeft > 0)
        {
            // Set bullets shot to 0
            rightHandWeapon.bulletsShot = 0;
            rightHandWeapon.ShootPhysics();
        }

    }

    public void Reload()
    {
        rightHandWeapon.Reload();
    }
}
