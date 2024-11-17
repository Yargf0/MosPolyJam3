using System.Collections;
using UnityEngine;

public enum PlayerMovementState
{
    Walk,
    Run,
    Crouch
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : PlayerModule
{
    [Header("General")]
    public float walkSpeed = 30f;
    [SerializeField] private float maxSlopeAngle = 40f;
    [Space(10)]
    [SerializeField, Min(0f)] private float inAirDrag;
    public float onGroundDrag = 7f;
    [Space(10)]
    [SerializeField, Tooltip("XZ Velocity will be multiplied by value when not grounded")] private float airVelocityMultiplier = 0.001f;
    [SerializeField] private float airVelocityMultiplierOnSpring = 1f;

    [Header("Run Settings")]
    public float runSpeed = 60f;
    //[SerializeField] private float runFovMultiplier = 1.5f;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed = 15f;
    [SerializeField] private float crouchYScale = 0.5f;
    [Space(10)]
    [SerializeField] private Transform colliderTransform;

    [Header("FBX References")]
    [SerializeField] private Transform fbxRootTransform;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 1f;

    [Header("Climb Settings")]
    //[SerializeField] private float minContactOffset = 0.01f;
    [SerializeField] private float frontClimbJumpForceMultiplyer = 1.5f;
    [SerializeField] private float backClimbJumpForceMultiplyer = 1f;
    [Space(5)]
    [SerializeField] private float horizontalClimbOffset = 0.75f;
    [SerializeField] private float verticalClimbOffset = 0.5f;

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Audio")]
    [SerializeField] private AudioClip footstepAudio;
    [SerializeField] private AudioClip jumpAudio;

    private CountdownTimer footstepTimer;

    public float currentSpeed;
    public float speedMultiplayer = 1f;

    private bool readyToJump = true;
    private bool isGrounded;

    private bool isOnSpring;

    private bool isAlmostGrounded;
    private bool isFrontAlmostGrounded;

    public Rigidbody rb;
    private Transform cameraTransform;
    private Transform directionTransform;

    private PlayerMovementState state;

    public Observer<float> FOVMultuplier { get; private set; } = new(1f);
    public Rigidbody Rigidbody => rb;

    public void Init(PlayerInput input, PlayerCamera cam)
    {
        base.Init(input);
        cameraTransform = cam.transform;
        directionTransform = cam.DirectionXZTransform;

        input.OnMove += HandleMovement;

        input.OnRun += Run;
        input.OnCrouch += Crouch;

        input.OnJump += Jump;

        footstepTimer = new CountdownTimer();
        footstepTimer.OnFinished(delegate
        {
            if (isGrounded && rb.linearVelocity != Vector3.zero)
                AudioManager.Instance.PlaySound(footstepAudio, Random.Range(0.9f, 1.1f));
        });
    }

    public override void Init(PlayerInput input)
    {
        Init(input, FindAnyObjectByType<PlayerCamera>());
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = walkSpeed * speedMultiplayer;
        state = PlayerMovementState.Walk;
    }

    private void Update()
    {
        HandleFBXRotation();
    }

    private void FixedUpdate()
    {
        //HandleMovement();
        CalculateGrounded();
    }

    public void JumpOnSpring()
    {
        isOnSpring = true;
    }

    private void Walk()
    {
        currentSpeed = walkSpeed * speedMultiplayer;
        state = PlayerMovementState.Walk;

        FOVMultuplier.Value = 1f;
    }

    private void Crouch(bool isCrouching)
    {
        if (isCrouching && isGrounded)
        {
            currentSpeed = crouchSpeed * speedMultiplayer;
            state = PlayerMovementState.Crouch;

            colliderTransform.localScale = new Vector3(colliderTransform.localScale.x, crouchYScale, colliderTransform.localScale.z);
            fbxRootTransform.localScale = new Vector3(fbxRootTransform.localScale.x, crouchYScale, fbxRootTransform.localScale.z);

            FOVMultuplier.Value = 1f;
        }
        else
        {
            colliderTransform.localScale = new Vector3(colliderTransform.localScale.x, 1f, colliderTransform.localScale.z);
            fbxRootTransform.localScale = new Vector3(fbxRootTransform.localScale.x, 1f, fbxRootTransform.localScale.z);

            Walk();
        }
    }

    private void Run(bool isRunning)
    {
        if (state == PlayerMovementState.Crouch)
            return;

        if (isRunning)
        {
            currentSpeed = runSpeed * speedMultiplayer;
            state = PlayerMovementState.Run;

           // FOVMultuplier.Value = runFovMultiplier;
        }
        else
        {
            Walk();
        }
    }

    private void HandleMovement(Vector2 moveInput)
    {
        Vector3 force =
            moveInput.y * currentSpeed * directionTransform.forward +
            moveInput.x * currentSpeed * directionTransform.right;

        if (isOnSpring)
        {
            force.x *= airVelocityMultiplierOnSpring;
            force.z *= airVelocityMultiplierOnSpring;
        }
        else if (!isGrounded)
        {
            force.x *= airVelocityMultiplier;
            force.z *= airVelocityMultiplier;
        }
        else if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, groundCheckRadius, groundLayerMask))
        {
            Vector3 parallelVector = Vector3.Cross(hitInfo.normal, Vector3.Cross(force, hitInfo.normal)).normalized;
            force = currentSpeed * parallelVector;

            footstepTimer.Play(10f / currentSpeed);
        }

        rb.AddForce(force);
    }

    private void Jump()
    {
        if (!readyToJump || (!isGrounded && !isAlmostGrounded))
            return;

        Vector3 force = jumpForce * transform.up;

        if (isAlmostGrounded)
        {
            if (isFrontAlmostGrounded)
            {
                if (directionTransform.forward.Angle(rb.linearVelocity) < 90f)
                {
                    force *= frontClimbJumpForceMultiplyer;
                    force += directionTransform.forward;
                    Debug.Log("Front Climb");
                }
            }
            else
            {
                force *= backClimbJumpForceMultiplyer;
                Debug.Log("Back Climb");
            }
        }

        rb.AddForce(force, ForceMode.Impulse);
        isAlmostGrounded = false;
        
        AudioManager.Instance.PlaySound(jumpAudio, Random.Range(0.9f, 1.1f));

        readyToJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void HandleFBXRotation()
    {
        fbxRootTransform.rotation = Quaternion.LookRotation(directionTransform.forward);
    }

    private void CalculateGrounded()
    {
        bool isGroundedCurrentFrame = Physics.CheckSphere(transform.position, groundCheckRadius, groundLayerMask);

        //isAlmostGrounded = Physics.Raycast(transform.position, -directionTransform.forward, groundCheckRadius + 0.7f, groundLayerMask) ||
        //    Physics.Raycast(transform.position, directionTransform.forward, groundCheckRadius + 0.3f, groundLayerMask) ? true : isGrounded;

        if (isGrounded == isGroundedCurrentFrame)
            return;

        if (isGroundedCurrentFrame)
        {
            isAlmostGrounded = false;
        }
        else
        {
            Vector3 horizontalOffset = horizontalClimbOffset * directionTransform.forward;
            Vector3 verticalOffset = verticalClimbOffset * Vector3.up;
            Vector3 frontCheckStart = transform.position + verticalOffset + horizontalOffset;
            Vector3 frontCheckEnd = transform.position - verticalOffset + horizontalOffset;
            Vector3 backCheckStart = transform.position + verticalOffset - horizontalOffset;
            Vector3 backCheckEnd = transform.position - verticalOffset - horizontalOffset;

            if (Physics.Linecast(frontCheckStart, frontCheckEnd, groundLayerMask))
            {
                isAlmostGrounded = true;
                isFrontAlmostGrounded = true;
            }
            else if (Physics.Linecast(backCheckStart, backCheckEnd, groundLayerMask))
            {
                isAlmostGrounded = true;
                isFrontAlmostGrounded = false;
            }
        }

        if (isGroundedCurrentFrame && !isGrounded)  
        {
            isOnSpring = false;
            rb.linearDamping = onGroundDrag;           
            rb.useGravity = false;
        }
        else if (!isGroundedCurrentFrame && isGrounded)
        {
            rb.linearDamping = inAirDrag;
            rb.useGravity = true;
        }

        isGrounded = isGroundedCurrentFrame;
    }

    public void ChangeSpeedTemporarily(float changeSpeedMultiplayer, float duration)
    {
        StartCoroutine(ChangeSpeed(changeSpeedMultiplayer, duration));
    }

    private IEnumerator ChangeSpeed(float changeSpeedMultiplayer, float duration)
    {
        speedMultiplayer += changeSpeedMultiplayer;
        yield return new WaitForSeconds(duration);
        speedMultiplayer -= changeSpeedMultiplayer;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        ContactPoint contactPoint = collision.contacts[0];

    //        //if (collisionPoint.y > transform.position.y + minContactOffset)
    //            //isAlmostGrounded = true;
            
    //        isAlmostGrounded = true;
    //        isFrontAlmostGrounded = directionTransform.forward.Angle(contactPoint.normal) > 90f;
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);

        if (directionTransform != null)
        {
            Vector3 horizontalOffset = horizontalClimbOffset * directionTransform.forward;
            Vector3 verticalOffset = verticalClimbOffset * Vector3.up;
            Vector3 frontCheckStart = transform.position + verticalOffset + horizontalOffset;
            Vector3 frontCheckEnd = transform.position - verticalOffset + horizontalOffset;
            Vector3 backCheckStart = transform.position + verticalOffset - horizontalOffset;
            Vector3 backCheckEnd = transform.position - verticalOffset - horizontalOffset;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(frontCheckStart, frontCheckEnd);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(backCheckStart, backCheckEnd);

            //    Gizmos.DrawRay(transform.position, (groundCheckRadius + 0.7f) * directionTransform.forward);
            //    Gizmos.DrawRay(transform.position, (groundCheckRadius + 0.3f)  * -directionTransform.forward);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(directionTransform.position, directionTransform.position + directionTransform.forward);
        }
    }
}