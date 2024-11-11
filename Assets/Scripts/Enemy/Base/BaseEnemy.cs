using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public abstract class BaseEnemy : InvertableBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;

    public HealthSystem HealthSystem { get; protected set; }

    protected virtual void Awake()
    {
        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.Init(maxHealth);

        HealthSystem.Died += OnDied;
    }

    protected virtual void OnDied()
    {
        Destroy(gameObject);
    }
}