using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    // Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    // Settings
    private float snappiness = 0f; // Might need change to use another weapon aspect // How quick the gun goes to new location

    // The lower the return speed the more it will move upwards
    private float returnSpeed = 0f; // Will also be affective by other stuff // Will be affected by grip of player

    [SerializeField]
    private GameObject rightHand;

    private Weapon weapon;

    [SerializeField]
    private InputManager playerPrefebInputManger;

    // Update is called once per frame
    void Update()
    {
        if (rightHand.GetComponentInChildren<Weapon>() != null && rightHand.transform.childCount != 0)
        {
            weapon = rightHand.GetComponentInChildren<Weapon>();
            if(weapon.isFullAuto && playerPrefebInputManger.GetComponent<InputManager>().triggerDown)
            {
                snappiness = weapon.isFullAutoRecoilEnergy;
                returnSpeed = weapon.isFullAutoGripStabilizer;
            } 
            else
            {
                snappiness = weapon.recoilEnergy;
                returnSpeed = weapon.gripStabilizer;
            }
        }

        // Still need to add restriction for going to far up
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        // weapon.verticalRecoil will move the weapon upwards on the X axis with the negative sign
        // weapon.horizontalRecoil will move the weapon along the sides of the Y axis
        if(weapon.isFullAuto && playerPrefebInputManger.GetComponent<InputManager>().triggerDown) // Posible don't use
        {
            targetRotation += new Vector3(-weapon.isFullAutoVerticalRecoil, Random.Range(-weapon.isFullAutoHorizontalRecoil, weapon.isFullAutoHorizontalRecoil), 0);
        }
        else
        {
            targetRotation += new Vector3(-weapon.verticalRecoil, Random.Range(-weapon.horizontalRecoil, weapon.horizontalRecoil), 0);
        }
    }
}
