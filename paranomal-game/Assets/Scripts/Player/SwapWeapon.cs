using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    
    [SerializeField]
    private Transform primarayWeaponSlot;
    [SerializeField] 
    private Transform secondaryWeaponSlot;
    [SerializeField]
    private Transform rightHand;
    [SerializeField]
    private Transform leftHand;
    private Weapon weaponType;

    public void SwapToPrimary()
    {
        if (rightHand.childCount != 0 && leftHand.childCount == 0)
        {
            weaponType = rightHand.GetChild(0).GetComponent<Weapon>();

            if (!weaponType.primaryWeapon)
            {
                Transform weaponInHand = rightHand.GetChild(0);
                Swap(weaponInHand, secondaryWeaponSlot);
                weaponInHand.localRotation = Quaternion.Euler(90, 0, 0); // Rotates gun downwards while on players side hip
                weaponInHand.GetComponent<PickUpDropItem>().equipped = false;

                if (primarayWeaponSlot.GetComponent<WeaponSlot>().isSlotFull)
                {
                    Transform weapon = primarayWeaponSlot.GetChild(0);
                    Swap(weapon, rightHand);
                    weapon.localRotation = Quaternion.Euler(Vector3.zero);
                    weapon.GetComponent<PickUpDropItem>().equipped = true;
                }
                else
                {
                    weaponType.GetComponent<PickUpDropItem>().MakeRightHandEmpty();
                }
            }             
        }
        else if (primarayWeaponSlot.childCount != 0 && leftHand.childCount == 0) // Moves ChildComponent in PrimaryWeapon GameObject to RightHand GameObject
        {
            Transform weapon = primarayWeaponSlot.GetChild(0);
            Swap(weapon, rightHand);
            weapon.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.GetComponent<PickUpDropItem>().equipped = true;
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
                Swap(weaponInHand, primarayWeaponSlot);
                weaponInHand.localRotation = Quaternion.Euler(-90, 0, 0); // Rotates gun upwards while on players back
                weaponInHand.GetComponent<PickUpDropItem>().equipped = false;

                if (secondaryWeaponSlot.GetComponent<WeaponSlot>().isSlotFull)
                {
                    Transform weapon = secondaryWeaponSlot.GetChild(0);
                    Swap(weapon, rightHand);
                    weapon.localRotation = Quaternion.Euler(Vector3.zero);
                    weapon.GetComponent<PickUpDropItem>().equipped = true;
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
            Swap(weapon, rightHand);
            weapon.localRotation = Quaternion.Euler(Vector3.zero);
            weapon.GetComponent<PickUpDropItem>().equipped = true;
        }
    }

    // Will take child component and move it to new parent component
    private void Swap(Transform childComponent, Transform parentComponent)
    {
        childComponent.SetParent(parentComponent);
        childComponent.localPosition = Vector3.zero;
    }
}
