using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask layerMask;
    private PlayerUI playerUI;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<FirstPersonController>().playerCamera;
        playerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);

        // Creates ray at center of the camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);

        // Stores collision informatio.
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                playerUI.UpdateText(interactable.promptMessage);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Will get the Interactable function on the Component the Script is attached to.
                    interactable.BaseInteract();
                }
            }
        }
    }
}
