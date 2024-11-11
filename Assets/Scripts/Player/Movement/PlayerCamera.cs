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

    public Transform DirectionXZTransform => orientationTransform;

    public override void Init(PlayerInput input)
    {
        base.Init(input);

        input.OnRotate += Rotate;

        Player.Instance.FOVMultiplier.ValueChanged += OnFOVMultiplierValueChanged;
    }

    private void Start()
    {
        cam = GetComponent<Camera>();
        defaultFOV = cam.fieldOfView;
    }

    private void Update()
    {
        Move();
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

    private void OnFOVMultiplierValueChanged(float prevValue, float newValue)
    {
        tween?.Kill();
        tween = cam.DOFieldOfView(defaultFOV * newValue, fovTweenOptions.Duration).
            SetEase(fovTweenOptions.Ease).
            Play();
    }
}