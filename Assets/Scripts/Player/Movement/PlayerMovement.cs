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
    [SerializeField] private float walkSpeed = 30f;
    [SerializeField] private float runSpeed = 60f;
    [Space(10)]
    [SerializeField, Min(0f)] private float inAirDrag;
    [SerializeField, Min(0f)] private float onGroundDrag = 4f;
    [Space(10)]
    [SerializeField, Tooltip("XZ Velocity will be multiplied by value when not grounded")] private float airVelocityMultiplier = 0.001f;
    [Space(10)]
    [SerializeField] private float runFovMultiplier = 1.5f;

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

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius = 0.1f;

    private float currentSpeed;
    private float speedMultiplayer = 1f;

    private bool readyToJump = true;
    private bool isGrounded;

    public Rigidbody rb;
    private Transform cameraTransform;
    private Transform directionTransform;

    private PlayerMovementState state;

    public Observer<float> FOVMultuplier { get; private set; } = new(1f);

    public void Init(PlayerInput input, PlayerCamera cam)
    {
        base.Init(input);
        cameraTransform = cam.transform;
        directionTransform = cam.DirectionXZTransform;

        input.OnMove += HandleMovement;

        input.OnRun += Run;
        input.OnCrouch += Crouch;

        input.OnJump += Jump;
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
            currentSpeed = runSpeed* speedMultiplayer;
            state = PlayerMovementState.Run;

            FOVMultuplier.Value = runFovMultiplier;
        }
        else
        {
            Walk();
        }
    }

    private void HandleMovement(Vector2 moveInput)
    {
        //Vector2 moveInput = input.Move;

        //if (moveInput == Vector2.zero)
        //    return;

        Vector3 force =
            moveInput.y * currentSpeed * directionTransform.forward +
            moveInput.x * currentSpeed * directionTransform.right;

        // aplly air velocity multiplier (because of difference between air and ground drag)
        if (!isGrounded)
        {
            force.x *= airVelocityMultiplier;
            force.z *= airVelocityMultiplier;
        }

        // clamp velocity to current speed
        //if (velocity.magnitude > currentSpeed)
        //    velocity = velocity.normalized * currentSpeed;

        // save y velocity (need for VelocityChange force mode)
        //velocity.y = rb.linearVelocity.y;
        rb.AddForce(force);
    }

    private void Jump()
    {
        if (!readyToJump || !isGrounded)
            return;

        rb.AddForce(jumpForce * transform.up, ForceMode.Impulse);

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

        if (isGroundedCurrentFrame && !isGrounded)
            rb.drag = onGroundDrag;
        else if (!isGroundedCurrentFrame && isGrounded)
            rb.drag = inAirDrag;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);

        if (directionTransform != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(directionTransform.position, directionTransform.position + directionTransform.forward);
        }
    }
}