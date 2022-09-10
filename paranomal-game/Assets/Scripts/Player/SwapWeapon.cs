using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    [SerializeField] private Transform primarayWeaponSlot, secondaryWeaponSlot, rightHand;
    private Weapon weaponType;

    public void SwapToPrimary()
    {
        if (rightHand.childCount != 0)
        {
            weaponType = rightHand.GetChild(0).GetComponent<Weapon>();

            if (!weaponType.primaryWeapon)
            {
                Transform weaponInHand = rightHand.GetChild(0);
                weaponInHand.SetParent(secondaryWeaponSlot);
                weaponInHand.localPosition = Vector3.zero;
                weaponInHand.localRotation = Quaternion.Euler(90, 0, 0); // Rotates gun downwards while on players side hip

                if (primarayWeaponSlot.GetComponent<WeaponSlot>().isSlotFull)
                {
                    Transform weapon = primarayWeaponSlot.GetChild(0);
                    weapon.SetParent(rightHand);
                    weapon.localPosition = Vector3.zero;
                    weapon.localRotation = Quaternion.Euler(Vector3.zero);
                }
                else
                {
                    weaponType.GetComponent<PickUpDropItem>().MakeRightHandEmpty();
                }
            }             
        }
        else if (primarayWeaponSlot.childCount != 0)
        {
            Transform weapon = primarayWeaponSlot.GetChild(0);
            weapon.SetParent(rightHand);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.Euler(Vector3.zero);
        }

    }

    public void SwapToSecondary()
    {
        if (rightHand.childCount != 0)
        {
            weaponType = rightHand.GetChild(0).GetComponent<Weapon>();

            if (!weaponType.secondaryWeapon)
            {
                Transform weaponInHand = rightHand.GetChild(0);
                weaponInHand.SetParent(primarayWeaponSlot);
                weaponInHand.localPosition = Vector3.zero;
                weaponInHand.localRotation = Quaternion.Euler(-90, 0, 0); // Rotates gun upwards while on players back

                if (secondaryWeaponSlot.GetComponent<WeaponSlot>().isSlotFull)
                {
                    Transform weapon = secondaryWeaponSlot.GetChild(0);
                    weapon.SetParent(rightHand);
                    weapon.localPosition = Vector3.zero;
                    weapon.localRotation = Quaternion.Euler(Vector3.zero);
                }
                else
                {
                    weaponType.GetComponent<PickUpDropItem>().MakeRightHandEmpty();
                }
            }  
        }
        else if (secondaryWeaponSlot.childCount != 0)
        {
            Transform weapon = secondaryWeaponSlot.GetChild(0);
            weapon.SetParent(rightHand);
            weapon.localPosition = Vector3.zero;
            weapon.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
