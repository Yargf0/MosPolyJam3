using UnityEngine;

public class Player : MonoBehaviour,
    IGamePauseListener, IGameResumeListener
{
    [Header("Modules")]
    [SerializeField] private PlayerCamera cam;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private HealthSystem healthSystem;

    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;

    private int collectedStarCount;
    
    private PlayerInput input;

    public static Player Instance { get; private set; }
    public static Transform OriginTransform => Instance.movement.transform;
    public static HealthSystem Health => Instance.healthSystem;
    public static Vector3 CameraPosition => Instance.cam.transform.position;
    public static Vector3 LookDirection => Instance.cam.transform.forward;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();

        GameManager.Instance.RegisterListener(this);

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Init()
    {
        input = new PlayerInput();

        cam.Init(input);
        movement.Init(input, cam);

        healthSystem.Init(maxHealth);
    }

    private void Update()
    {
        input.Update();
    }

    private void FixedUpdate()
    {
        input.FixedUpdate();
    }

    public void AddStar()
    {
        collectedStarCount++;
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