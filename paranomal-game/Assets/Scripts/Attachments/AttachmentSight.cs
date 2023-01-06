using UnityEngine;

public class AttachmentSight : MonoBehaviour
{
    public float fieldOfViewZoom;

    [Tooltip("For Player Camera to change FOV")]
    public float fieldOfViewScopeLook;
    public bool scope;
    public Vector3 weaponPositionChangeAim;

    [SerializeField]
    private Camera scopeCamera; // Each scope will have there own camera

    private void Awake()
    {
        if (scope)
        {
            scopeCamera.fieldOfView = fieldOfViewZoom;
        }
    }
}