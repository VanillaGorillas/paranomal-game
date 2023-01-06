using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private PlayerLook playerLook;

    [HideInInspector]
    public float speed;

    [Space]
    [Header("Walk")]
    [Space]

    [SerializeField]
    private float walkSpeed;

    [Header("Sprint")]
    [Space]

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

    private float originalHeight;
    private readonly float crouchChangeSpeed = 5;

    public bool holdInCrouch;

    [Space]
    [Header("Head Bob")]
    [Space]

    [SerializeField]
    private bool enableHeadBob = true;

    [SerializeField]
    private Transform joint; 

    public float bobSpeed;

    public float walkBobAmountX;
    public float walkBobAmountY;
    public float sprintBobAmountX;
    public float sprintBobAmountY;

    [SerializeField]
    private Camera playerCamera;

    private float sprintRemaining;
    private Vector3 jointOriginalPosition;
    private float timer = 0;
    private bool isWalking = false;

    //TODO: Maybe make player walk slower when aiming

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerLook = GetComponent<PlayerLook>();
        originalHeight = controller.height;
        jointOriginalPosition = joint.localPosition;
        sprintRemaining = sprintDuration;
        speed = walkSpeed;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        
        if (isCrouching)
        {
            controller.height = Mathf.Lerp(controller.height, crouchHeight, crouchChangeSpeed * Time.deltaTime);
            Walk();
        }
        else if(!isCrouching)
        {
            controller.height = Mathf.Lerp(controller.height, originalHeight, crouchChangeSpeed * Time.deltaTime);
        }

        SprintFunction();

        if (enableHeadBob)
        {
            HeadBob();
        }
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

        if (moveDirection.x != 0 || moveDirection.z != 0 && isGrounded)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
            isSprinting = false;
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
        speed = GetComponent<AimDownSight>().aimPressed ? walkSpeed / 2 : walkSpeed;
        isSprinting = false;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, playerLook.fov, sprintFOVStepTime * Time.deltaTime);
    }

    private void SprintFunction()
    {
        if (isSprinting && sprintRemaining >= 1f)
        {
            // Makes sure when player is moving and aim is true it calls release aim
            if (GetComponent<AimDownSight>().aimPressed)
            {
                GetComponent<AimDownSight>().ReleaseAim();
            }

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

    //TODO: test later
    //private Vector3 FootStepMotion()
    //{
    //    Vector3 pos = Vector3.zero;
    //    pos.y += Mathf.Sin(Time.time * bobSpeed) * walkBobAmountX;
    //    pos.x += Mathf.Cos(Time.time * bobSpeed / 2) * walkBobAmountX * 2;
    //    return pos;
    //}

    private void HeadBob()
    {
        if (isWalking)
        {
            timer = timer > 30f ? 0 : timer;

            if (isSprinting)
            {
                timer += Time.deltaTime * bobSpeed;

                joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPosition.x + sprintBobAmountX * Mathf.Sin(timer), Time.deltaTime), jointOriginalPosition.y + Mathf.Sin(timer) * sprintBobAmountY, jointOriginalPosition.z);
            }
            else
            {
                joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPosition.x + walkBobAmountX * Mathf.Sin(Time.time * bobSpeed), Time.deltaTime), jointOriginalPosition.y + Mathf.Sin(Time.time * bobSpeed) * walkBobAmountY, jointOriginalPosition.z);
            }
        }
        else
        {
            if (timer != 0)
            {
                timer = 0;
            }

            joint.localPosition = new Vector3(Mathf.Lerp(jointOriginalPosition.x, jointOriginalPosition.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPosition.y, Time.deltaTime * bobSpeed), jointOriginalPosition.z);
        }
    }
}
