using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Modules")]
    [SerializeField] private PlayerCamera cam;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private HealthSystem healthSystem;

    [Header("Stats")]
    [SerializeField] private float maxHealth = 100f;

    private PlayerInput input;

    private void Start()
    {
        Init();

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
}