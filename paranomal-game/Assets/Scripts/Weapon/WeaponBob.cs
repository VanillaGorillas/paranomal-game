using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public float swayAmountA; // increases distance of sway
    public float swayAmountB; // increases distance of sway
    public float swayLerpSpeed; // This affects how far they go apart more

    private float swayTime;
    private Vector3 swayPosition;

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 1 && GetComponent<PickUpDropItem>().equipped)
        {
            CalculateWeaponSway();
        }
    }

    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }

    private void CalculateWeaponSway()
    {
        Vector3 targetPosition = LissajousCurve(swayTime, swayAmountA, swayAmountB);

        swayPosition = Vector3.Lerp(swayPosition, targetPosition, Time.smoothDeltaTime * swayLerpSpeed);
        swayTime += Time.deltaTime;

        if (swayTime > 6.3f)
        {
            swayTime = 0;
        }

        transform.localRotation = Quaternion.Euler(swayPosition);
    }
}
