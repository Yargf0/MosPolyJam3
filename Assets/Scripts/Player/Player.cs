using UnityEngine;

public class Player : MonoBehaviour,
    IGamePauseListener, IGameResumeListener
{
    [Header("Modules")]
    [SerializeField] private PlayerCamera cam;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private BaseWeapon weapon;

    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;

    private int collectedStarCount;

    private PlayerInput input;

    public static Transform OriginTransform => instance.movement.transform;
    public static PlayerMovement PlayerMovement => instance.movement;
    public static Observer<float> FOVMultiplier => instance.movement.FOVMultuplier;

    public static HealthSystem Health => instance.healthSystem;

    public static Vector3 CameraPosition => instance.cam.transform.position;
    public static Vector3 LookDirection => instance.cam.transform.forward;

    private static Player instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogError($"[{nameof(Player)}] Multiple instances of type");
            enabled = false;
        }

        Init();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Init()
    {
        GameManager.Instance.RegisterListener(this);

        input = new PlayerInput();

        cam.Init(input);
        movement.Init(input, cam);

        healthSystem.Init(maxHealth);

        weapon.Init(input);
    }

    private void Update()
    {
        input.Update();
    }

    private void FixedUpdate()
    {
        input.FixedUpdate();
    }

    public static void AddStar()
    {
        instance.collectedStarCount++;
    }

    public void OnGamePaused()
    {
        input.Disable();
    }

    public void OnGameResumed()
    {
        input.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
        }
    }
}