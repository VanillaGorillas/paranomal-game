using UnityEngine;

public class WeaponDirection : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;

    private float distance = 5f;
    [SerializeField]
    private Camera cam;

    private Vector3 targetPoint;

    private void Awake()
    {
        //position = transform.position;
        //rotation = transform.rotation;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        targetPoint = ray.GetPoint(75);

        Vector3 direction = targetPoint - transform.position;
        transform.forward = direction.normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        //** Maybe doing a lerp check with Time.time and reset could work


        //Quaternion rotation = cam.transform.rotation;

        //position = cam.transform.position + (rotation * Vector3.forward * distance);
        ////transform.LookAt(position);
        //transform.forward = position.normalized;

        //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray through the middle of screen

        //Vector3 targetPoint;

        //if (Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    targetPoint = hit.point;
        //    //Vector3 directionWithoutSpread = targetPoint - transform.position;

        //    //transform.forward = directionWithoutSpread.normalized;
        //}
        //else
        //{
        //    targetPoint = ray.GetPoint(75);
        //    //transform.position = position;
        //    //transform.rotation = rotation;
        //}

        //Vector3 directionWithoutSpread = targetPoint - transform.position;

        //transform.forward = directionWithoutSpread.normalized;

    }
}
