using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public float bobFrequency = 1f;
    public float bobSharpness = 1f;
    public float defaultBobAmount = 0.05f;

    private float weaponBobFactor = 0.05f;
    Vector3 weaponBobPosition;
    Vector3 weaponMainPosition;
    Vector3 lastCharacterPosition;
    Vector3 targetRotation;
    Vector3 currentRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        if (GameObject.Find("RightHand").transform.childCount == 1)
        {
            //UpdateWeaponBob();
            //transform.localRotation = Quaternion.Slerp(Quaternion.Euler(weaponBobPosition), transform.localRotation, 1f * Time.fixedDeltaTime);

            targetRotation = Vector3.Lerp(weaponBobPosition, Vector3.zero, 1 * Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, 4 * Time.deltaTime);

            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    private void UpdateWeaponBob()
    {
        //Vector3 playerCharacterVelocity = (inputManager.transform.position - lastCharacterPosition) / Time.deltaTime;

        float movementFactor = 0f;

        movementFactor = Mathf.Clamp01(2f / 2f * 2);

        weaponBobFactor = Mathf.Lerp(weaponBobFactor, movementFactor, bobSharpness * weaponBobFactor);

        float bobAmount = 0.05f;
        float frequency = bobFrequency;
        float hBobValue = Mathf.Sin(Time.time * frequency) * bobAmount * weaponBobFactor;
        float vBobValue = ((Mathf.Sin(Time.time * frequency * 2f) * 0.5f) + 0.5f) * bobAmount * weaponBobFactor;

        weaponBobPosition.x = hBobValue * 10f;
        weaponBobPosition.y = Mathf.Abs(vBobValue) * 15f;

        //lastCharacterPosition = inputManager.transform.position;
    }
}
