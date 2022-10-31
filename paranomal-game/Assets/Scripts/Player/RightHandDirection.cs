using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));

        //attackPoint.TransformDirection(0.5f, 0.5f, 0f);

        //Vector3 lookAtPosition = fpsCamera.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, fpsCamera.nearClipPlane));

        //transform.LookAt(lookAtPosition);
        //transform.LookAt(fpsCamera.transform.forward);

        //attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
        //attackPoint.LookAt(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
        //attackPoint.TransformPoint(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
        //attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
        //transform.TransformPoint(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
        //attackPoint.TransformPoint(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));



        // Failed
        ////transform.TransformDirection(fpsCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f)));
        ////attackPoint.TransformDirection(fpsCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0f)));
        //transform.TransformDirection(fpsCamera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
        //attackPoint.TransformDirection(fpsCamera.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));


        //Find the exact hit position using a raycast
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Ray through the middle of screen

        //Check if ray hits something
        Vector3 targetPoint;
        //Must create bullet come out of weapon MAYBE
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            targetPoint = hit.point;
            //targetPoint = hit.transform.position - attackPoint.position;
            //Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
            //attackPoint.TransformDirection(targetPoint);
            //transform.TransformDirection(targetPoint);
            //attackPoint.TransformDirection(targetPoint);

            //transform.TransformPoint(directionWithoutSpread);
            //attackPoint.TransformPoint(directionWithoutSpread);
        }
        else
        {
            
            targetPoint = ray.GetPoint(75); // A point far away from the player
            //targetPoint = attackPoint.transform.forward;
            //targetPoint = new Vector3(0.5f, 0.5f, 0f);
            //attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)));
        }

        Vector3 directionWithoutSpread = targetPoint - transform.position;

        //Vector3 newDirection = Vector3.RotateTowards(attackPoint.transform.forward, targetPoint, muzzleVelocity * Time.deltaTime, 0.0f);

        transform.forward = directionWithoutSpread.normalized;

        //attackPoint.transform.forward = directionWithoutSpread.normalized;

        //float yes = Vector3.Distance(targetPoint, attackPoint.position);
        //Debug.Log(yes);
        ////attackPoint.TransformDirection(ray.GetPoint(yes));
        ////attackPoint.TransformDirection(fpsCamera.ViewportToWorldPoint(directionWithoutSpread.normalized));
        //attackPoint.LookAt(ray.GetPoint(yes));

    }
}
