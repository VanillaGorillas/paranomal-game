using System.Net;
using UnityEngine;

public class BulletPenetration : MonoBehaviour
{
    // Will probably have to add onto the magazine when doing bullet changes
    [SerializeField]
    private float penetrationAmount;

    public Vector3? penetrationPoint;
    public Vector3? impactPoint;

    private void Update()
    {
        UpdatePenetration();
    }

    public void UpdatePenetration()
    {
        Ray ray = new Ray(transform.position, transform.forward);
 
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            impactPoint = hit.point;

            Ray penRay = new(hit.point + ray.direction * penetrationAmount, -ray.direction);

            if (hit.collider.Raycast(penRay, out RaycastHit penHit, penetrationAmount))
            {
                penetrationPoint = penHit.point;
            }
            else
            {
                penetrationPoint = impactPoint;
            }
        }
        else
        {
            penetrationPoint = null;
            impactPoint = null;
        }
    }
}
