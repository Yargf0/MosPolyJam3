using UnityEngine;

[RequireComponent(typeof(EnemyHealthSystem))]
public abstract class BaseEnemy : MonoBehaviour
{
    protected HealthSystem healthSystem;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }
}