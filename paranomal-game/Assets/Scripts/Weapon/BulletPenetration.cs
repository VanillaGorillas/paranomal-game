using UnityEngine;

public class BulletPenetration : MonoBehaviour
{
    // Will probably have to add onto the magazine when doing bullet changes
    [SerializeField]
    private float penetrationAmount;

    public Vector3? endPoint;
    public Vector3? penetrationPoint;
    public Vector3? impactPoint;
    
    public Vector3 endPointTest;
    //public Vector3? penetrationPoint;
    //public Vector3? impactPoint;

    [SerializeField]
    private Weapon weaponScript; // Must get the weapon that where the attack point is attached to

    //private float weaponMuzzleVelocity;

    //private void Awake()
    //{
    //    weaponMuzzleVelocity = weaponScript.muzzleVelocity;
    //}

    private void Update()
    {
        //UpdatePenetration();
    }

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
                //endPoint = transform.position + transform.forward * 1000;
                //endPointTest = transform.position + transform.forward * 1000;
                //endPoint = transform.position + transform.forward;
                endPoint = transform.position;
            }
            else
            {
                Debug.Log("else");
                endPoint = impactPoint.Value + ray.direction * penetrationAmount;
                endPointTest = transform.position + transform.forward * 1000;
                penetrationPoint = endPoint;
            }
        }
        else
        {
            //endPoint = transform.position + transform.forward * 1000;
            endPointTest = transform.position + transform.forward * 1000;
            endPoint = null;
            penetrationPoint = null;
            impactPoint = null;
        }

        Debug.Log(impactPoint.Value + " impact");
        Debug.Log(penetrationPoint.Value + " pen");
    }

    //private void OnDrawGizmos() // Will remove
    //{
    //    UpdatePenetration();

    //    if (!penetrationPoint.HasValue || !impactPoint.HasValue)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(this.transform.position, endPointTest);
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(this.transform.position, impactPoint.Value);
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(impactPoint.Value, penetrationPoint.Value);
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(penetrationPoint.Value, endPointTest);
    //    }
    //}
}
