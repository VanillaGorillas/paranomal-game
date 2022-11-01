using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public float swayAmountA;
    public float swayAmountB;
    public float swayScale;
    public float swayLerpSpeed;

    public float swayTime;
    public Vector3 swayPosition;

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 1)
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
        Vector3 targetPosition = LissajousCurve(swayTime, swayAmountA, swayAmountB) / swayScale;

        swayPosition = Vector3.Lerp(swayPosition, targetPosition, Time.smoothDeltaTime * swayLerpSpeed);
        swayTime += Time.deltaTime;

        if (swayTime > 6.3f)
        {
            swayTime = 0;
        }

        transform.localPosition = swayPosition;
    }
}
