using UnityEngine;

public class WeaponDirection : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;

    private void Awake()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray through the middle of screen

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
            //Vector3 directionWithoutSpread = targetPoint - transform.position;

            //transform.forward = directionWithoutSpread.normalized;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
            //transform.position = position;
            //transform.rotation = rotation;
        }

        Vector3 directionWithoutSpread = targetPoint - transform.position;

        transform.forward = directionWithoutSpread.normalized;

    }
}
