using UnityEngine;

public class BulletPenetration : MonoBehaviour
{
    // Will probably have to add onto the magazine when doing bullet changes
    [SerializeField]
    private float penetrationAmount;

    public Vector3? penetrationPoint;
    public Vector3? impactPoint;
    
    // Must do if penetration point is null it must destory gameobject

    public void UpdatePenetration() // Working out will be done on bullet behaiour side. Just getting values
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
                penetrationPoint = null;
            }
        }
        else
        {
            penetrationPoint = null;
            impactPoint = null;
        }
    }
}
