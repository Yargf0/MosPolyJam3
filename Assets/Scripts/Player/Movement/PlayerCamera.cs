using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : PlayerModule
{
    [Header("Movement")]
    [SerializeField] private Transform followTransform;

    [Header("Rotation")]
    [SerializeField] private Vector2 sensetivity = new(20f, 20f);
    public float sensetivityMultiplayer = 40f;
    [SerializeField] private float minHorizontalAngle = -90f;
    [SerializeField] private float maxHorizontalAngle = 90f;
    [Space(10)]
    [SerializeField] private Transform orientationTransform;

    [Header("FOV Tween")]
    [SerializeField] private TweenOptions fovTweenOptions = new(0.5f, Ease.InOutCubic);

    private Vector3 rotation;

    private Tween tween;
    private Camera cam;
    private float defaultFOV;
    private float currentFOV;

    public Transform DirectionXZTransform => orientationTransform;


    public Rigidbody rb;              // ������ �� Rigidbody ������

    public float minFOV = 50f;        // ����������� �������� FOV
    public float maxFOV = 135f;       // ������������ �������� FOV
    public float walkSpeed = 5.71f;   // �������� ��� ������
    public float runSpeed = 9f;      // �������� ��� ����
    public float walkFOV = 60f;       // FOV ��� ������
    public float runFOV = 90f;        // FOV ��� ����
    public float transitionSpeed = 5f;

    public override void Init(PlayerInput input)
    {
        base.Init(input);

        rotation = transform.rotation.eulerAngles;
        input.OnRotate += Rotate;
        rotation = transform.rotation.eulerAngles;
        // Player.Instance.FOVMultiplier.ValueChanged += OnFOVMultiplierValueChanged;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
        currentFOV = cam.fieldOfView;
        rotation = transform.rotation.eulerAngles;

        runSpeed = (Player.Instance.PlayerMovement.runSpeed / Player.Instance.PlayerMovement.onGroundDrag)-1;
        walkSpeed = Player.Instance.PlayerMovement.walkSpeed / Player.Instance.PlayerMovement.onGroundDrag;
        rb = Player.Instance.PlayerMovement.Rigidbody;
    }

    private void Update()
    {
        Move();
        float  horizontalSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;

        float targetFOV = CalculateFOV(horizontalSpeed);
        targetFOV = targetFOV*Player.Instance.PlayerMovement.speedMultiplayer;
        currentFOV = Mathf.Lerp(currentFOV, targetFOV, Time.deltaTime * transitionSpeed);
        cam.fieldOfView = Mathf.Clamp(currentFOV, minFOV, maxFOV);
        //OnFOVMultiplierValueChanged(targetFOV);
    }

    private void Move()
    {
        transform.position = followTransform.position;
    }

    private void Rotate(Vector2 inputLook)
    {
        rotation.y += inputLook.x * sensetivity.x*sensetivityMultiplayer * Time.deltaTime;

        rotation.x += inputLook.y * sensetivity.y*sensetivityMultiplayer * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, minHorizontalAngle, maxHorizontalAngle);

        transform.rotation = Quaternion.Euler(rotation);
        orientationTransform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    // ������� ��� ���������� FOV �� ������ ��������
    float CalculateFOV(float speed)
    {
        // �������� ������������ ��� ��������� FOV � ����������� �� ��������
        float fov = Mathf.Lerp(walkFOV, runFOV, (speed - walkSpeed) / (runSpeed - walkSpeed));
        return fov;
    }
    //private void OnFOVMultiplierValueChanged(float newValue)
    //{
    //    tween?.Kill();
    //    tween = cam.DOFieldOfView(defaultFOV * newValue, fovTweenOptions.Duration).
    //        SetEase(fovTweenOptions.Ease).
    //        Play();
    //}
}