using UnityEngine;

public class WeaponFiringMode : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    [HideInInspector]
    public bool switchingModes; // Used to make sure shooting can't happen 

    // Start is called before the first frame update
    void Awake()
    {
        CheckFiringMode();
    }

    private void CheckFiringMode()
    {
        weapon.isBurstFire = false;
        weapon.isFullAuto = weapon.allowedAutomaticFire && !weapon.allowedBurstFire && !weapon.allowedSingleShotFire;
        weapon.isSemiAutomatic = !weapon.isFullAuto && !weapon.isBurstFire;
    }

    public void SelectFiringMode()
    {
        // Semi-auto, Burst and Full-auto
        if(weapon.allowedSingleShotFire && weapon.allowedBurstFire && weapon.allowedAutomaticFire)
        {
            SwitchingNotAllowed();

            if (weapon.isFullAuto)
            {
                weapon.isSemiAutomatic = true;
                weapon.isFullAuto = false;
            }
            else if (!weapon.isFullAuto && weapon.isSemiAutomatic)
            {
                weapon.isBurstFire = true;
                weapon.isSemiAutomatic = false;
            }
            else if (weapon.isBurstFire)
            {
                weapon.isFullAuto = true;
                weapon.isBurstFire = false;
            }
        }
        else if (weapon.allowedSingleShotFire && weapon.allowedBurstFire && !weapon.allowedAutomaticFire) // Semi-auto and Burst
        {
            SwitchingNotAllowed();

            weapon.isSemiAutomatic = !weapon.isSemiAutomatic;
            weapon.isBurstFire = !weapon.isBurstFire;
        }
        else if (weapon.allowedSingleShotFire && !weapon.allowedBurstFire && weapon.allowedAutomaticFire) // Semi-auto and Full-auto
        {
            SwitchingNotAllowed();

            weapon.isFullAuto = !weapon.isFullAuto;
            weapon.isSemiAutomatic = !weapon.isSemiAutomatic;
        }

        CheckForBurst();

        Invoke(nameof(SwitchingAllowed), 1f);
    }

    private void CheckForBurst()
    {
        weapon.bulletsPerTap = weapon.isBurstFire ? 3 : 1;
    }

    private void SwitchingNotAllowed()
    {
        switchingModes = true;
    }

    private void SwitchingAllowed()
    {
        switchingModes = false;
    }
}
