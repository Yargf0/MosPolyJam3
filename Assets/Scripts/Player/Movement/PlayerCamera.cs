using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : PlayerModule
{
    [Header("Movement")]
    [SerializeField] private Transform followTransform;

    [Header("Rotation")]
    [SerializeField] private Vector2 sensetivity = new(800f, 800f);
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


    public Rigidbody rb;              // Ссылка на Rigidbody игрока

    public float minFOV = 45f;        // Минимальное значение FOV
    public float maxFOV = 120f;       // Максимальное значение FOV
    public float walkSpeed = 5.71f;   // Скорость при ходьбе
    public float runSpeed = 9f;      // Скорость при беге
    public float walkFOV = 60f;       // FOV при ходьбе
    public float runFOV = 80f;        // FOV при беге
    public float transitionSpeed = 5f;

    public override void Init(PlayerInput input)
    {
        base.Init(input);

        input.OnRotate += Rotate;

       // Player.Instance.FOVMultiplier.ValueChanged += OnFOVMultiplierValueChanged;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
        currentFOV = cam.fieldOfView;

        runSpeed = (Player.Instance.PlayerMovement.runSpeed / Player.Instance.PlayerMovement.onGroundDrag)-1;
        walkSpeed = Player.Instance.PlayerMovement.walkSpeed / Player.Instance.PlayerMovement.onGroundDrag;
        rb = Player.Instance.PlayerMovement.Rigidbody;
    }

    private void Update()
    {
        Move();
        float  horizontalSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

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
        rotation.y += inputLook.x * sensetivity.x * Time.deltaTime;

        rotation.x += inputLook.y * sensetivity.y * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, minHorizontalAngle, maxHorizontalAngle);

        transform.rotation = Quaternion.Euler(rotation);
        orientationTransform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    // Функция для вычисления FOV на основе скорости
    float CalculateFOV(float speed)
    {
        // Линейная интерполяция для получения FOV в зависимости от скорости
        float fov = Mathf.Lerp(walkFOV, runFOV, (speed - walkSpeed) / (runSpeed - walkSpeed));
        return fov;
    }
    private void OnFOVMultiplierValueChanged(float newValue)
    {
        tween?.Kill();
        tween = cam.DOFieldOfView(defaultFOV * newValue, fovTweenOptions.Duration).
            SetEase(fovTweenOptions.Ease).
            Play();
    }
}