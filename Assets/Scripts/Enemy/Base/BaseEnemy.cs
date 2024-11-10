using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public abstract class BaseEnemy : InvertableBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    protected HealthSystem healthSystem;

    public HealthSystem HealthSystem => healthSystem;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Init(maxHealth);

        healthSystem.Died += OnDied;
    }

    protected virtual void OnDied()
    {
        Destroy(gameObject);
    }
}