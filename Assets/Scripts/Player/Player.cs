using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour,
    IGamePauseListener, IGameResumeListener
{
    [Header("Modules")]
    [SerializeField] private PlayerCamera cam;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private BaseWeapon secondWeapon;
    [SerializeField] private WheaponChange wheaponChange;

    [Header("Stats")]
    [SerializeField] private float maxHealth = 30f;

    [Header("Audio")]
    [SerializeField] private AudioClip dieAudio;
    [SerializeField] private AudioClip damagedAudio;

    private int collectedStarCount;

    private PlayerInput input;

    public  Transform OriginTransform => movement.transform;
    public PlayerMovement PlayerMovement => movement;
    //public Observer<float> FOVMultiplier => movement.FOVMultuplier;

    public HealthSystem Health => healthSystem;

    public PlayerCamera Cam => cam;

    public Vector3 CameraPosition => cam.transform.position;
    public Vector3 LookDirection => cam.transform.forward;

    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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
        secondWeapon.Init(input);

        healthSystem.HealthChanged += (health) => AudioManager.Instance.PlaySound(damagedAudio, Random.Range(0.9f, 1.1f));
        healthSystem.Died += () => StartCoroutine(OnDied());;
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
        Instance.collectedStarCount++;
    }

    public static void RemoveStar()
    {
        if (Instance.collectedStarCount > 0)
            Instance.collectedStarCount--;
    }

    public void OnGamePaused()
    {
        input.Disable();
        wheaponChange.Disable();
    }

    public void OnGameResumed()
    {
        input.Enable();
        wheaponChange.Enable();
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

    private IEnumerator OnDied()
    {
        AudioManager.Instance.PlaySound(dieAudio, Random.Range(0.9f, 1.1f));
        yield return new WaitForEndOfFrame();
        SceneController.ReloadScene();
    }

    private void OnDestroy()
    {
        LevelsManager.Instance.SetStars(collectedStarCount);
    }
}