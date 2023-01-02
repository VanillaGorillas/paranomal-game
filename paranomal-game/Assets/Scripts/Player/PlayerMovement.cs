using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private PlayerLook playerLook;

    [HideInInspector]
    public float speed; // TODO: might make this private down the line

    [Space]
    [Header("Walk")]
    [Space]

    [SerializeField]
    private float walkSpeed;

    [Header("Sprint")]
    [Space]

    public bool holdInSprint;

    [SerializeField]
    private bool isSprinting; // Might need to be public 

    [SerializeField]
    private float sprintFOV;

    [SerializeField]
    private float sprintFOVStepTime;

    [SerializeField]
    private float sprintSpeed;

    public float sprintDuration;
    public float sprintCooldown;

    [Space]
    [Header("Gravity")]
    [Space]

    [SerializeField]
    private float gravity;

    [Space]
    [Header("Jump")]
    [Space]

    public float jumpHeight;

    [Space]
    [Header("Crouch")]
    [Space]

    public bool isCrouching;

    [SerializeField]
    private float crouchHeight;

    public bool holdInCrouch;

    [SerializeField]
    private float crouchSpeed;

    private Vector3 originalScale;

    [Space]
    [Header("Head Bob")]
    [Space]

    [SerializeField]
    private Transform joint;

    public float bobSpeed;
    public Vector3 bobAmount; // new Vector3(.15f, .05f, 0f);

    [SerializeField]
    private Camera playerCamera;

    private float sprintRemaining;
    private Vector3 jointOriginalPosition;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerLook = GetComponent<PlayerLook>();
        originalScale = transform.localScale;
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        
        if (isCrouching)
        {
            transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
            Walk();
        }
        else if(!isCrouching)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }

        SprintFunction();
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        // This is to make sure the playerVelocity.y dosn't go beyond -2
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; 
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            Stand();
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        isCrouching = true;
    }

    public void Stand()
    {
        isCrouching = false;
    }

    public void Sprint()
    {
        speed = sprintSpeed;
        isSprinting = true;
    }

    public void Walk()
    {
        speed = isCrouching ? crouchSpeed : walkSpeed;
        isSprinting = false;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, playerLook.fov, sprintFOVStepTime * Time.deltaTime);
    }

    private void SprintFunction()
    {
        if (isSprinting)
        {
            Stand();

            // Changes FOV of player wen sprinting
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);

            // Drain sprint remaining while sprinting
            sprintRemaining -= 1 * Time.deltaTime;

            if (sprintRemaining <= 0)
            {
                isSprinting = false;
            }
        }
        else if (sprintRemaining >= 0)
        {
            // Regain sprint while not sprinting
            sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);
            Walk();
        }
    }

    private void HeadBob() { }
}
